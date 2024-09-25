using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

/// <summary>
/// არის application database context, რომლის საშუალებით ბაზასთან და მის ცხრილებთან ინტერაქციას და დაკავშირებას ვახერხებთ.
/// </summary>
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
