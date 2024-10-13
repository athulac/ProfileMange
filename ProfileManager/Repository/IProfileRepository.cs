using ProfileManager.Common.Enums;
using ProfileManager.Common.Paginate;
using ProfileManager.Data.Models;
using ProfileManager.ViewModels;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;

namespace ProfileManager.Repository
{
    public interface IProfileRepository : IGenericRepository<Profile>
    {
        //Task<List<Student>> GetByGrade(Grade grade);

        Task<int> CreateAsync(Profile profile);
        Task<IQueryable<Profile>> GetAllAsync();
        Task<Profile> GetAsync(Guid id);
        Task<Profile> GetByIdentityIdAsync(Guid id);
        Task<Profile> ModifyAsync(Profile profile);

        Task<Paginate<Profile>> FilterAsync(Expression<Func<Profile, bool>> expression, [AllowNull]PageData? page);
        Task<Paginate<Profile>> GetAllAsync(PageData page);

    }
}
