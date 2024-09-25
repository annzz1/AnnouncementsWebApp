using Domain.IRepository;
using Domain.Models;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System;

namespace Infrastructure.Repository;

/// <summary>
/// განაცხადის რეპოზიტორია.
/// </summary>
public class AnnouncementRepository(AppDbContext context) : IAnnouncementRepository
{
    /// <summary>
    /// დამატება ახალი განცხადების.
    /// </summary>
    /// <param name="announcement">პარამენტრად განცხადება რომელსაც უნდა დაემატოს.</param>
    public async Task AddAsync(Announcement announcement)
    {
        await context.Announcements.AddAsync(announcement);
        await context.SaveChangesAsync();
    }

    /// <summary>
    /// წაშლა განცხადების უნიკალური იდენტიფიკატორით
    /// </summary>
    /// <param name="id">Guid ტიპის განცხადების უნიკალური იდენტიფიკატორი.</param>
    public async Task DeleteAsync(Guid id)
    {
        var announcement = await context.Announcements.FindAsync(id);
        if (announcement != null)
        {
            context.Announcements.Remove(announcement);
            await context.SaveChangesAsync();
        }
    }

    /// <summary>
    /// დაბრუნება ყველა განცხადების, შესაძლოა სათაურის მიხედვით.
    /// </summary>
    /// <param name="searchTitle">სათაური, რომლითაც ხდება განცხადებების ძიება.</param>
    /// <returns>განცხადებების სია.</returns>
    public async Task<IEnumerable<Announcement>> GetAllAsync(string? searchTitle)
    {
        IQueryable<Announcement> query = context.Announcements;
        if (!searchTitle.IsNullOrEmpty())
        {
            query = query.Where(a => a.Title.Contains(searchTitle));
        }

        var announcements = await query.ToListAsync();
        return announcements ?? throw new ArgumentNullException("Error.The value was null.");
    }

    /// <summary>
    /// დაბრუნება განცხადების მისი უნიკალური იდენტიფიკატორით.
    /// </summary>
    /// <param name="id">Guid ტიპის განცხადების უნიკალური იდენტიფიკატორი.</param>
    /// <returns>განცხადებას მისი არსებობის შემთხვევაში.</returns>
    public async Task<Announcement> GetByIdAsync(Guid id)
    {
        var announcement = await context.Announcements.FirstOrDefaultAsync(x => x.Id == id);
        return announcement ?? throw new ArgumentNullException($"The Announcement with ID: {id} does not exist.");
    }

    /// <summary>
    /// განახლება არსებული განცხადების.
    /// </summary>
    /// <param name="id">>Guid ტიპის განცხადების უნიკალური იდენტიფიკატორი.</param>
    /// <param name="announcement">განახლებული განცხადების დეტალები.</param>
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
