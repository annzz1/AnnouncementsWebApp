using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Announcement> Announcements { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Announcement>()
              .HasKey(g => g.Id);

        modelBuilder.Entity<Announcement>()
            .HasIndex(g => g.Id)
            .IsUnique();
    }
}
