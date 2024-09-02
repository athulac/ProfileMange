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
                //BirthDate = profileViewModel.BirthDate,
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
                UserId = x.UserId
            }).ToList();

            return resMapped;
        }


    }

}

