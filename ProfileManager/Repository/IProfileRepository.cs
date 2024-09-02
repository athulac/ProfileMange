using ProfileManager.Data.Models;
using System.Diagnostics;

namespace ProfileManager.Repository
{
    public interface IProfileRepository
    {
        //Task<List<Student>> GetByGrade(Grade grade);

        Task<int> CreateAsync(Profile profile);
        Task<IQueryable<Profile>> GetAllAsync();
    }
}
