using Application.Interfaces;
using Application.MapperProfile;
using Application.Services;
using Domain.IRepository;
using Infrastructure.Data;
using Infrastructure.Repository;
using Microsoft.EntityFrameworkCore;

// ვარეგისტრირებთ შესაბამის სერვისებს და რეპოზიტორიებს program.cs ფაილში. 
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MapperProfile));
builder.Services.AddScoped<IFileService, FileService>();
builder.Services.AddScoped<IAnnouncementRepository,AnnouncementRepository>();
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ვამყარებთ კავშირს SQL სერვერთან connection string -ის მეშვეობით.
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AnnouncementsCnn")));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ვამატებთ სტატიკური ფაილების გამოყენებას UI - სთან ინტერაქციისთვის.
// "https://localhost:7160/index.html" || "http://localhost:5174/index.html"
app.UseStaticFiles();

app.UseRouting();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
