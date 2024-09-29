using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProfileManager.Common.Enums;
using ProfileManager.Models;
using ProfileManager.Paginate;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Diagnostics;

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

        public async Task<IActionResult> Index(UserParams userParams)        
        {
            var profiles = await _profileServcie.GetAllAsync(userParams);

            return View(profiles);
        }

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

    }
}