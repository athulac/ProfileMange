using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProfileManager.Common.Enums;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Security.Claims;

namespace ProfileManager.Pages
{
    public class SiblingModel : PageModel
    {

        private readonly IFamilyService _familyServcie;
        private readonly IProfileServcie _profileService;

        public SiblingModel(IFamilyService familyServcie, IProfileServcie profileServcie)
        {
            _familyServcie = familyServcie;
            _profileService = profileServcie;
        }

        [BindProperty]
        public ProfileViewModel Profile { get; set; }


        public async Task OnGet()
        {
            ClaimsPrincipal currentUser = this.User;
            Guid currUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            ProfileViewModel profile = await _profileService.GetByIdentityIdAsync(currUserId);
            this.Profile = profile;

            List<FamilyViewModel> families = await _familyServcie.GetAllByIdentityIdAsync(currUserId);

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
        }


        public async Task<IActionResult> OnPost()
        {
            ClaimsPrincipal currentUser = this.User;
            Guid currUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            var profile = this.Profile;

            profile.SiblingOne.FamilyType = (FamilyTypeEnum)profile.SiblingOne.SiblingType;         
            profile.SiblingTwo.FamilyType = (FamilyTypeEnum)profile.SiblingTwo.SiblingType;
            profile.SiblingThree.FamilyType = (FamilyTypeEnum)profile.SiblingThree.SiblingType;
            profile.SiblingOne.UserId = currUserId;
            profile.SiblingTwo.UserId = currUserId;
            profile.SiblingThree.UserId = currUserId;


            var ressa = await _familyServcie.CreateAsync(profile.SiblingOne);
            var ressb = await _familyServcie.CreateAsync(profile.SiblingTwo);
            var ressc = await _familyServcie.CreateAsync(profile.SiblingThree);


            List<FamilyViewModel> families = await _familyServcie.GetAllByIdentityIdAsync(currUserId);
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

            return Redirect("~/Home/Index");

        }
    }
}
