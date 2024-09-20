using ProfileManager.Data.Models;
using ProfileManager.Repository;
using ProfileManager.ViewModels;

namespace ProfileManager.Services
{
    public class FamilyService : IFamilyService
    {
        private readonly IFamilyRepository familyRepository;

        public FamilyService(IFamilyRepository familyRepository)
        {
            this.familyRepository = familyRepository;
        }

        public async Task<int> CreateAsync(FamilyViewModel familyViewModel)
        {
            int res = 0;

            Family family = new Family
            {
                Id = Guid.NewGuid(),
                UserId = familyViewModel.UserId,              
                FamilyType = familyViewModel.FamilyType,
                Job = familyViewModel.Job,
                OtherDetails = familyViewModel.OtherDetails,

                Religion = familyViewModel.Religion,
                Cast = familyViewModel.Cast,
                Race = familyViewModel.Race,
            };
            res = await familyRepository.CreateAsync(family);

            return res;
        }

        public async Task<List<FamilyViewModel>> GetAllAsync()
        {
            var res = await familyRepository.GetAllAsync();
            var resMapped = res.Select(x => new FamilyViewModel
            {
                Id = x.Id,
                UserId = x.UserId,
                FamilyType = x.FamilyType,
                Job = x.Job,
                OtherDetails = x.OtherDetails,

                Religion = x.Religion,
                Cast = x.Cast,
                Race = x.Race,
                
            }).ToList();

            return resMapped;
        }

        public async Task<FamilyViewModel> GetAsync(Guid id)
        {
            var res = await familyRepository.GetAsync(id);
            var resMapped = new FamilyViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                FamilyType = res.FamilyType,
                Job = res.Job,
                OtherDetails = res.OtherDetails,

                Religion = res.Religion,
                Cast = res.Cast,
                Race = res.Race,
          
            };

            return resMapped;
        }

        public async Task<FamilyViewModel> GetByIdentityIdAsync(Guid id)
        {
            var res = await familyRepository.GetByIdentityIdAsync(id);
            var resMapped = new FamilyViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                FamilyType = res.FamilyType,
                Job = res.Job,
                OtherDetails = res.OtherDetails,

                Religion = res.Religion,
                Cast = res.Cast,
                Race = res.Race,
            };

            return resMapped;
        }

        public async Task<FamilyViewModel> ModifyAsync(FamilyViewModel familyViewModel)
        {
            var profileMapped = new Family
            {
                Id = familyViewModel.Id,
                UserId = familyViewModel.UserId,
                FamilyType = familyViewModel.FamilyType,
                Job = familyViewModel.Job,
                OtherDetails = familyViewModel.OtherDetails,

                Religion = familyViewModel.Religion,
                Cast = familyViewModel.Cast,
                Race = familyViewModel.Race,
            };

            var res = await familyRepository.ModifyAsync(profileMapped);
            var resMapped = new FamilyViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                FamilyType = res.FamilyType,
                Job = res.Job,
                OtherDetails = res.OtherDetails,

                Religion = res.Religion,
                Cast = res.Cast,
                Race = res.Race,
            };

            return resMapped;
        }
    }
}
