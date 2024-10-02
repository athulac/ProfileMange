using ProfileManager.Common.Enums;
using ProfileManager.Common.Paginate;
using ProfileManager.Data.Models;
using ProfileManager.ViewModels;
using System.Diagnostics;

namespace ProfileManager.Repository
{
    public interface IProfileRepository
    {
        //Task<List<Student>> GetByGrade(Grade grade);

        Task<int> CreateAsync(Profile profile);
        Task<IQueryable<Profile>> GetAllAsync();
        Task<Profile> GetAsync(Guid id);
        Task<Profile> GetByIdentityIdAsync(Guid id);
        Task<Profile> ModifyAsync(Profile profile);

        Task<Paginate<Profile>> FilterAsync(PageData page, Func<Profile, bool>? exp);
    }
}
