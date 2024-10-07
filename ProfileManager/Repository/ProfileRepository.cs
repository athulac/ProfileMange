using ProfileManager.Common.Enums;
using ProfileManager.Common.Paginate;
using ProfileManager.Data;
using ProfileManager.Data.Models;
using ProfileManager.ViewModels;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using System.Runtime.ExceptionServices;

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


        public async Task<Profile> GetByIdentityIdAsync(Guid id)
        {
            
            var res = Find(x => x.UserId == id);
            if (res.Any())
            {
                return res.First();
            }

            return null;
        }

        public async Task<Profile> ModifyAsync(Profile profile)
        {
            Update(profile);
            Save();
            return profile;
        }

        public async Task<Paginate<Profile>> FilterAsync(Expression<Func<Profile, bool>> expression, [AllowNull]PageData? page)
        {
            var res = Find(expression);

            if (page != null)
            {
                var resPaged = await PagedList<Profile>.CreateAsync(res, page.PageNumber, page.PageSize);
                return resPaged;
            }

            return new Paginate<Profile> { Data = res.ToList() };
        }

        public async Task<Paginate<Profile>> GetAllAsync(PageData page)
        {
            IQueryable<Profile> res;
            res = GetAll();

            var resPaged = await PagedList<Profile>.CreateAsync(res, page.PageNumber, page.PageSize);

            return resPaged;
        }

        //public async Task<IQueryable<Profile>> GetByGrade(Profile grade)
        //{
        //    var res = Find(x => x.Id == grade.Id);
        //    return res.AsQueryable();
        //}
    }
}
