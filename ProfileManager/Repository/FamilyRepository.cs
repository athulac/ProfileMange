using ProfileManager.Data;
using ProfileManager.Data.Models;

namespace ProfileManager.Repository
{
    public class FamilyRepository : GenericRepository<Family>, IFamilyRepository
    {
        public FamilyRepository(ProfileManagerDataDbContext context) : base(context)
        {
        }

        public async Task<int> CreateAsync(Family family)
        {
            int res = 0;
            res = await AddAsync(family);
            return res;
        }

        public async Task<IQueryable<Family>> GetAllAsync()
        {
            IQueryable<Family> res;
            res = GetAll();
            return res;
        }

        public async Task<Family> GetAsync(Guid id)
        {
            Family res;
            res = GetById(id);
            return res;
        }


        public async Task<Family> GetByIdentityIdAsync(Guid id)
        {

            var res = Find(x => x.UserId == id);
            if (res.Any())
            {
                return res.First();
            }

            return null;
        }

        public async Task<Family> ModifyAsync(Family family)
        {
            await UpdateAsync(family);
            await SaveAsync();
            return family;
        }


        public async Task<IQueryable<Family>> GetAllByIdentityIdAsync(Guid identityId)
        {
            var res = Find(x => x.UserId == identityId);

            return res;
        }
    }
}
