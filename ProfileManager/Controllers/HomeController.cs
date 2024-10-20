﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProfileManager.Common.Enums;
using ProfileManager.Common.Helpers;
using ProfileManager.Common.Paginate;
using ProfileManager.Models;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Diagnostics;
using System.Security.AccessControl;
using System.Web;

namespace ProfileManager.Controllers
{

    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProfileServcie _profileServcie;
        private readonly IFamilyService _familyService;

        public HomeController(ILogger<HomeController> logger, IProfileServcie profileServcie,
            IFamilyService familyService)
        {
            _logger = logger;
            _familyService = familyService;
            _profileServcie = profileServcie;           
        }

        //public async Task<IActionResult> Index(UserParams userParams)        
        //{
        //    var profiles = await _profileServcie.GetAllAsync(userParams);

        //    return View(profiles);
        //}


        //public async Task<IActionResult> Index(PageData userParams)
        //{
        //    var profiles = await _profileServcie.GetAllAsync(userParams);

        //    return View(profiles);
        //}


        public async Task<IActionResult> Details(Guid id)
        {  

            var profile = await _profileServcie.GetAsync(id);

    
            List<FamilyViewModel> families = await _familyService.GetAllByIdentityIdAsync(profile.UserId);
            if (families.Any(x => x.FamilyType == FamilyTypeEnum.Father))
            {
                profile.Father = families.FirstOrDefault(x => x.FamilyType == FamilyTypeEnum.Father);
            }
            if (families.Any(x => x.FamilyType == FamilyTypeEnum.Mother))
            {
                profile.Mother = families.FirstOrDefault(x => x.FamilyType == FamilyTypeEnum.Mother);
            }


            int len = families.Where(x => x.FamilyType == FamilyTypeEnum.YoungerSister || x.FamilyType == FamilyTypeEnum.ElderSister ||
                                          x.FamilyType == FamilyTypeEnum.YoungerBrother || x.FamilyType == FamilyTypeEnum.ElderBrother).Count();
            if (len > 0)
            {
                profile.SiblingOne = families.Where(x => x.FamilyType == FamilyTypeEnum.YoungerSister || x.FamilyType == FamilyTypeEnum.ElderSister ||
                                                         x.FamilyType == FamilyTypeEnum.YoungerBrother || x.FamilyType == FamilyTypeEnum.ElderBrother).ToList()?[0];
            }
            if (len > 1)
            {
                profile.SiblingTwo = families.Where(x => x.FamilyType == FamilyTypeEnum.YoungerSister || x.FamilyType == FamilyTypeEnum.ElderSister ||
                                                         x.FamilyType == FamilyTypeEnum.YoungerBrother || x.FamilyType == FamilyTypeEnum.ElderBrother).ToList()?[1];
            }
            if (len > 2)
            {
                profile.SiblingThree = families.Where(x => x.FamilyType == FamilyTypeEnum.YoungerSister || x.FamilyType == FamilyTypeEnum.ElderSister ||
                                                           x.FamilyType == FamilyTypeEnum.YoungerBrother || x.FamilyType == FamilyTypeEnum.ElderBrother).ToList()?[2];
            }



            return View(profile);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult> Edit(Guid id)
        {

            var profile = await _profileServcie.GetAsync(id);

            return View(profile);
        }


        //[HttpGet]       
        //public JsonResult filterby(string filters)
        //{
        //   var fff= HttpUtility.UrlDecode(filters);
        //    return Json("");
        //}

        [HttpGet]
        //public async Task<IActionResult> Index(string filters)
        public async Task<IActionResult> Index(string filters)
        {
            if (filters != null)
            {
                var parsedString = HttpUtility.UrlDecode(filters);

                var pageNo = HttpUtility.ParseQueryString(parsedString)["page[pagenumber]"];
                var pageSize = HttpUtility.ParseQueryString(parsedString)["page[pagesize]"];

                var gender = HttpUtility.ParseQueryString(parsedString)["gender"];
                var district = HttpUtility.ParseQueryString(parsedString)["district"];
                var ageFrom = HttpUtility.ParseQueryString(parsedString)["ageFrom"];
                var ageTo = HttpUtility.ParseQueryString(parsedString)["ageTo"];
                var civilStatus = HttpUtility.ParseQueryString(parsedString)["civilStatus"];

                var job = HttpUtility.ParseQueryString(parsedString)["job"];

                var cast = HttpUtility.ParseQueryString(parsedString)["cast"];
                var race = HttpUtility.ParseQueryString(parsedString)["race"];
                var religion = HttpUtility.ParseQueryString(parsedString)["religion"];

                var memberId = HttpUtility.ParseQueryString(parsedString)["memberId"];


                PageData userParams = new PageData()
                {
                    PageNumber = Convert.ToInt32(pageNo),
                    PageSize = Convert.ToInt32(pageSize),
                };

                FilterViewModel fil = new FilterViewModel();
                fil.Page = userParams;
                fil.Gender = (GenderEnum)Convert.ToInt32(gender);
                fil.District = (DistrictEnum)Convert.ToInt32(district);
                fil.AgeFrom = Convert.ToInt32(ageFrom);
                fil.AgeTo = Convert.ToInt32(ageTo);
                fil.CivilStatus = (CivilStatusEnum)Convert.ToInt32(civilStatus);

                fil.Job = (JobEnum)Convert.ToInt32(job);

                fil.Cast = (CastEnum)Convert.ToInt32(cast);
                fil.Race = (RaceEnum)Convert.ToInt32(race);
                fil.Religion = (ReligionEnum)Convert.ToInt32(religion);

                fil.MemberId = memberId;


                return View(fil);
            }

            PageData pageData = new PageData() { PageNumber = 1, PageSize = 3 };
            FilterViewModel filterModel = new FilterViewModel()
            {
                Page = pageData,
                
            };           
            

            return View(filterModel);
        }



        [HttpPost]
        public async Task<IActionResult> Filter([FromBody] FilterViewModel filter)
        {
            FilterViewModel fm = new FilterViewModel
            {
                Page = new PageData
                {
                    PageNumber = filter.Page.PageNumber,
                    PageSize = filter.Page.PageSize,
                },

                Gender = filter.Gender
            };

            if (filter.District.HasValue)
            {
                fm.District = filter.District;
            }
            if (filter.AgeFrom > 0 && filter.AgeTo > 0)//can add more logic for age filter
            {
                fm.AgeFrom = filter.AgeFrom;
                fm.AgeTo = filter.AgeTo;
            }
            if (filter.CivilStatus.HasValue)
            {
                fm.CivilStatus = filter.CivilStatus;
            }

            if (filter.Job.HasValue)
            {
                fm.Job = filter.Job;
            }

            if (filter.Cast.HasValue)
            {
                fm.Cast = filter.Cast;
            }
            if (filter.Race.HasValue)
            {
                fm.Race = filter.Race;
            }
            if (filter.Religion.HasValue)
            {
                fm.Religion = filter.Religion;
            }

            if (filter.MemberId != null)
            {
                fm.MemberId = filter.MemberId;
            }

            var res = await _profileServcie.FilterAsync(fm);

            res.Data.ForEach(x => x.EnumNames = new ProfileEnumNames
            {
                Gender = EnumExtensions.GetDisplayName(x.Gender),
                District = EnumExtensions.GetDisplayName(x.District),
                CivilStatus = EnumExtensions.GetDisplayName(x.CivilStatus),
                City = EnumExtensions.GetDisplayName(x.City),
                Country = EnumExtensions.GetDisplayName(x.Country),
                Job = EnumExtensions.GetDisplayName(x.Job),
                Race = EnumExtensions.GetDisplayName(x.Race),
                Religion = EnumExtensions.GetDisplayName(x.Religion),
                Cast = EnumExtensions.GetDisplayName(x.Cast),
            });
            res.Data.ForEach(x => x.CreatedTimeAgo = TimeCal.AsTimeAgo(x.CreatedOn));


            return new JsonResult(res);
            //do your stuff...
        }

        //[HttpPost]
        //public async Task<IActionResult> FilterData([FromBody]string filters)
        //{
        //    var fff = HttpUtility.UrlDecode(filters);

        //    PageData userParams = new PageData()
        //    {
        //        PageNumber = 1,
        //        PageSize = 3,
        //    };

        //    var profiles = await _profileServcie.GetAllAsync(userParams);

        //    return View(profiles);
        //}

    }


}