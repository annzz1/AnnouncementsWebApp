using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace Infrastructure.Repository;

public class AnnouncementRepository(AppDbContext context) : IAnnouncementRepository
{
    public async Task AddAsync(Announcement announcement)
    {
        await context.Announcements.AddAsync(announcement);
        await context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Guid id)
    {
        var announcement = await context.Announcements.FindAsync(id);
        if (announcement != null)
        {
            context.Announcements.Remove(announcement);
            await context.SaveChangesAsync();
        }
    }

    public async Task<IEnumerable<Announcement>> GetAllAsync(string? searchTitle)
    {
        IQueryable<Announcement> query = context.Announcements;
        if (searchTitle != null)
        {
            query = query.Where(a => a.Title.Contains(searchTitle));
        }

        var announcements = await query.ToListAsync();
        return announcements ?? throw new ArgumentNullException("Error.The value was null.");
    }


    public async Task<Announcement> GetByIdAsync(Guid id)
    {
       var announcement = await context.Announcements.FirstOrDefaultAsync(x => x.Id == id);
        return announcement ?? throw new ArgumentNullException($"The Announcement with ID: {id} does not exist.");
    }

    public async Task UpdateAsync(Guid id, Announcement announcement)
    {
        ArgumentNullException.ThrowIfNull(announcement);
        ArgumentNullException.ThrowIfNull(id);
        var announcementToUpdate = await context.Announcements.FirstOrDefaultAsync(x => x.Id == id);
        ArgumentNullException.ThrowIfNull(announcementToUpdate);
        announcementToUpdate.Title = announcement.Title;
        announcementToUpdate.Description = announcement.Description;
        announcementToUpdate.Image = announcement.Image;
        announcementToUpdate.Phone = announcement.Phone;

        await context.SaveChangesAsync();

    }
}
