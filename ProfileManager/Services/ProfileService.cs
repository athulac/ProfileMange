using ProfileManager.Data.Models;
using ProfileManager.Repository;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Diagnostics;

namespace ProfileManager.Services
{
    public class ProfileServcie : IProfileServcie
    {
        private readonly IProfileRepository studentRepository;

        public ProfileServcie(IProfileRepository studentRepository)
        {
            this.studentRepository = studentRepository;
        }

        //public async Task<List<Student>> GetByGrade(Grade grade)
        //{
        //    var res = await studentRepository.GetByGrade(grade);
        //    if (!res.Any())
        //    {
        //        return new List<Student> { };
        //    }

        //    return res.ToList();
        //}


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
            res = await studentRepository.CreateAsync(profile);

            return res;
        }
    }

}

