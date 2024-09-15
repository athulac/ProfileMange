using ProfileManager.Data.Models;
using ProfileManager.Repository;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Diagnostics;
using System.Xml.Linq;

namespace ProfileManager.Services
{
    public class ProfileServcie : IProfileServcie
    {
        private readonly IProfileRepository profileRepository;

        public ProfileServcie(IProfileRepository studentRepository)
        {
            this.profileRepository = studentRepository;
        }

   
        public async Task<int> CreateAsync(ProfileViewModel profileViewModel)
        {
            int res = 0;

            Profile profile = new Profile
            {
                Id = Guid.NewGuid(),
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                City = profileViewModel.City,
                Distirct = profileViewModel.Distirct,
                BirthDate = profileViewModel.BirthDateTime,
                Gender = profileViewModel.Gender,
                UserId = profileViewModel.UserId,               
            };
            res = await profileRepository.CreateAsync(profile);

            return res;
        }


        public async Task<List<ProfileViewModel>> GetAllAsync()
        {
            var res = await profileRepository.GetAllAsync();
            var resMapped = res.Select(x => new ProfileViewModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Distirct = x.Distirct,
                City = x.City,
                Gender = x.Gender,
                UserId = x.UserId,
                BirthDateTime = x.BirthDate,
            }).ToList();

            return resMapped;
        }

        public async Task<ProfileViewModel> GetAsync(Guid id)
        {
            var res = await profileRepository.GetAsync(id);
            var resMapped = new ProfileViewModel
            {
                Id = res.Id,
                FirstName = res.FirstName,
                LastName = res.LastName,
                Distirct = res.Distirct,
                City = res.City,
                Gender = res.Gender,
                UserId = res.UserId,
                BirthDateTime = res.BirthDate,
            };

            return resMapped;
        }

        public async Task<ProfileViewModel> GetByIdentityIdAsync(Guid id)
        {
            var res = await profileRepository.GetByIdentityIdAsync(id);
            var resMapped = new ProfileViewModel
            {
                Id = res.Id,
                FirstName = res.FirstName,
                LastName = res.LastName,
                Distirct = res.Distirct,
                City = res.City,
                Gender = res.Gender,
                UserId = res.UserId,
                BirthDateTime = res.BirthDate,
            };

            return resMapped;
        }

        public async Task<ProfileViewModel> ModifyAsync(ProfileViewModel profile)
        {
            var profileMapped = new Profile
            {
                Id = profile.Id,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Distirct = profile.Distirct,
                City = profile.City,
                Gender = profile.Gender,
                UserId = profile.UserId,
                BirthDate = profile.BirthDateTime,
            };

            var res = await profileRepository.ModifyAsync(profileMapped);
            var resMapped = new ProfileViewModel
            {
                Id = res.Id,
                FirstName = res.FirstName,
                LastName = res.LastName,
                Distirct = res.Distirct,
                City = res.City,
                Gender = res.Gender,
                UserId = res.UserId,
                BirthDateTime = res.BirthDate,
            };

            return resMapped;
        }
    }

}

