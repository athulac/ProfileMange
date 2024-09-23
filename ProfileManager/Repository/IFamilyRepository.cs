using ProfileManager.Data.Models;

namespace ProfileManager.Repository
{
    public interface IFamilyRepository
    {
        Task<int> CreateAsync(Family family);
        Task<IQueryable<Family>> GetAllAsync();
        Task<Family> GetAsync(Guid id);
        Task<Family> GetByIdentityIdAsync(Guid id);
        Task<Family> ModifyAsync(Family family);
        Task<IQueryable<Family>> GetAllByIdentityIdAsync(Guid identityId);
    }
}
