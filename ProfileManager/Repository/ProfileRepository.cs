using ProfileManager.Data;
using ProfileManager.Data.Models;
using System.Diagnostics;

namespace ProfileManager.Repository
{


    public class ProfileRepository : GenericRepository<Profile>, IProfileRepository
    {
        public ProfileRepository(ProfileManagerDataDbContext context) : base(context)
        {
        }


        public async Task<int> CreateAsync(Profile profile)
        {
            int res = 0;
            res = await AddAsync(profile);
            return res;
        }

        public async Task<IQueryable<Profile>> GetAllAsync()
        {
            IQueryable<Profile> res;
            res = GetAll();
            return res;
        }

        public async Task<Profile> GetAsync(Guid id)
        {
            Profile res;
            res = GetById(id);
            return res;
        }

        //public async Task<IQueryable<Profile>> GetByGrade(Profile grade)
        //{
        //    var res = Find(x => x.Id == grade.Id);
        //    return res.AsQueryable();
        //}
    }
}
