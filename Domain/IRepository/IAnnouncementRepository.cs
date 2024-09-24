using Domain.Models;

namespace Domain.IRepository;

public interface IAnnouncementRepository
{
    Task AddAsync(Announcement announcement);

    Task<IEnumerable<Announcement>> GetAllAsync(string? searchTitle);

    Task<Announcement> GetByIdAsync(Guid id);

    Task UpdateAsync(Guid id, Announcement announcement);

    Task DeleteAsync(Guid id);
}
