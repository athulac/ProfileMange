using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.CodeAnalysis;
using Org.BouncyCastle.Bcpg;
using ProfileManager.Common.Enums;
using ProfileManager.Common.Paginate;
using ProfileManager.Data.Models;
using ProfileManager.Repository;
using ProfileManager.ViewModels;
using System.Linq;

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
                UserId = profileViewModel.UserId,
                MemberId = profileViewModel.MemberId,
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                BirthDate = profileViewModel.BirthDateTime,

                City = profileViewModel.City,
                Distirct = profileViewModel.District,
                Country = profileViewModel.Country,

                Gender = profileViewModel.Gender,
                CivilStatus = profileViewModel.CivilStatus,
                Cast = profileViewModel.Cast,
                Race = profileViewModel.Race,
                Religion = profileViewModel.Religion,
                Job = profileViewModel.Job,
                Introduction = profileViewModel.Introduction,
                CreatedOn = DateTime.Now,        
            };
            res = await profileRepository.CreateAsync(profile);

            return res;
        }

        public async Task<Paginate<ProfileViewModel>> GetAllAsync(PageData userParams)
        {
            var res = await profileRepository.GetAllAsync();
            var resMapped = res.Select(x => new ProfileViewModel
            {
                Id = x.Id,
                UserId = x.UserId,
                MemberId = x.MemberId,
                FirstName = x.FirstName,
                LastName = x.LastName,
                BirthDateTime = x.BirthDate,

                City = x.City,
                District = x.Distirct,
                Country = x.Country,

                Gender = x.Gender,
                CivilStatus = x.CivilStatus,
                Cast = x.Cast,
                Race = x.Race,
                Religion = x.Religion,
                Job = x.Job,
                Introduction = x.Introduction,
                CreatedOn = x.CreatedOn,
            });

            var resMappedPaged = await PagedList<ProfileViewModel>.CreateAsync(resMapped, userParams.PageNumber, userParams.PageSize);

            return resMappedPaged;
        }

        public async Task<ProfileViewModel> GetAsync(Guid id)
        {
            var res = await profileRepository.GetAsync(id);
            var resMapped = new ProfileViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                MemberId = res.MemberId,
                FirstName = res.FirstName,
                LastName = res.LastName,
                BirthDateTime = res.BirthDate,

                City = res.City,
                District = res.Distirct,
                Country = res.Country,

                Gender = res.Gender,
                CivilStatus = res.CivilStatus,
                Cast = res.Cast,
                Race = res.Race,
                Religion = res.Religion,
                Job = res.Job,
                Introduction = res.Introduction,
                CreatedOn = res.CreatedOn,
            };

            return resMapped;
        }

        public async Task<ProfileViewModel> GetByIdentityIdAsync(Guid id)
        {
            var res = await profileRepository.GetByIdentityIdAsync(id);
            var resMapped = new ProfileViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                MemberId = res.MemberId,
                FirstName = res.FirstName,
                LastName = res.LastName,
                BirthDateTime = res.BirthDate,

                City = res.City,
                District = res.Distirct,
                Country = res.Country,

                Gender = res.Gender,
                CivilStatus = res.CivilStatus,
                Cast = res.Cast,
                Race = res.Race,
                Religion = res.Religion,
                Job = res.Job,
                Introduction = res.Introduction,
                CreatedOn = res.CreatedOn,
            };

            return resMapped;
        }

        public async Task<ProfileViewModel> ModifyAsync(ProfileViewModel profileViewModel)
        {
            var profileMapped = new Profile
            {
                Id = profileViewModel.Id,
                UserId = profileViewModel.UserId,
                MemberId = profileViewModel.MemberId,
                FirstName = profileViewModel.FirstName,
                LastName = profileViewModel.LastName,
                BirthDate = profileViewModel.BirthDateTime,

                City = profileViewModel.City,
                Distirct = profileViewModel.District,
                Country = profileViewModel.Country,

                Gender = profileViewModel.Gender,
                CivilStatus = profileViewModel.CivilStatus,
                Cast = profileViewModel.Cast,
                Race = profileViewModel.Race,
                Religion = profileViewModel.Religion,
                Job = profileViewModel.Job,
                Introduction = profileViewModel.Introduction,               
            };

            var res = await profileRepository.ModifyAsync(profileMapped);
            var resMapped = new ProfileViewModel
            {
                Id = res.Id,
                UserId = res.UserId,
                MemberId = res.MemberId,
                FirstName = res.FirstName,
                LastName = res.LastName,
                BirthDateTime = res.BirthDate,

                City = res.City,
                District = res.Distirct,
                Country = res.Country,

                Gender = res.Gender,
                CivilStatus = res.CivilStatus,
                Cast = res.Cast,
                Race = res.Race,
                Religion = res.Religion,
                Job = res.Job,
                Introduction = res.Introduction,
                CreatedOn = res.CreatedOn,
            };

            return resMapped;
        }


        public static bool CalYear (DateTime dob)
        {
            try
            {
                var dadif = DateTime.Today.Subtract(dob.Date);
                int datesdf = Convert.ToInt32(DateTime.Today.Subtract(dob.Date).TotalDays);

                int yrDiff = Convert.ToInt32(datesdf / 365);
                if (yrDiff > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {

                throw;
            }
          
        }

        public async Task<Paginate<ProfileViewModel>> FilterAsync(FilterViewModel filter)
        {
            var resPaged = new Paginate<Profile>();
            var data = (await profileRepository.GetAllAsync());
            //resPaged.Data = res.ToList();


            if (filter.Gender != null && filter.Gender != GenderEnum.All)
            {
                data = data.Where(x => x.Gender == filter.Gender);
            }
            if (filter.District.HasValue)
            {
                data = data.Where(x => x.Distirct == filter.District);
            }
            if (filter.AgeFrom > 0 && filter.AgeTo > 0)
            {
                foreach (var item in data)
                {
                    var ff = CalYear(item.BirthDate);
                }

                //var ffd = data.Where(x => CalYear(x.BirthDate));

                //var ffdd = new List<Profile>(ffd);

                
                //if (resC.Any())
                //{
                //    data = data.Where(x => filter.AgeFrom <= CalYear(x.BirthDate) && filter.AgeTo >= CalYear(x.BirthDate));
                //}
            }

            if (!data.Any())
            {
                return new Paginate<ProfileViewModel>() { Data = new List<ProfileViewModel>() };
            }

            var res = await PagedList<Profile>.CreateAsync(data, filter.Page.PageNumber, filter.Page.PageSize);

            var resMapped = new Paginate<ProfileViewModel>()
            {
                CurrentPage = res.CurrentPage,
                PageSize = res.PageSize,
                TotalCount = res.TotalCount,
                TotalPages = res.TotalPages,
                Data = res.Data.Select(x => new ProfileViewModel
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    MemberId = x.MemberId,
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    BirthDateTime = x.BirthDate,

                    City = x.City,
                    District = x.Distirct,
                    Country = x.Country,

                    Gender = x.Gender,
                    CivilStatus = x.CivilStatus,
                    Cast = x.Cast,
                    Race = x.Race,
                    Religion = x.Religion,
                    Job = x.Job,
                    Introduction = x.Introduction,
                    CreatedOn = x.CreatedOn,
                }).ToList(),
            };

            return resMapped;
        }
    }

}

