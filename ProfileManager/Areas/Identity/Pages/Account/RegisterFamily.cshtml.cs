using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProfileManager.Common.Enums;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Security.Claims;

namespace ProfileManager.Areas.Identity.Pages.Account
{
    public class RegisterFamilyModel : PageModel
    {
        private readonly IFamilyService _familyServcie;
        private readonly IProfileServcie _profileService;

        public RegisterFamilyModel(IFamilyService familyServcie, IProfileServcie profileServcie)
        {
            _familyServcie = familyServcie;
            _profileService = profileServcie;
        }



        [BindProperty]
        public ProfileViewModel Profile { get; set; }

        public async Task<IActionResult> OnGetAsync(string user = null)
        {
            //ClaimsPrincipal currentUser = this.User;
            //Guid currUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            //ProfileViewModel profile = await _profileService.GetByIdentityIdAsync(currUserId);
            //this.Profile = profile;

            //List<FamilyViewModel> families = await _familyServcie.GetAllByIdentityIdAsync(currUserId);
            //if (families.Any(x => x.FamilyType == FamilyTypeEnum.Father))
            //{
            //    profile.Father = families.FirstOrDefault(x => x.FamilyType == FamilyTypeEnum.Father);
            //}
            //if (families.Any(x => x.FamilyType == FamilyTypeEnum.Mother))
            //{
            //    profile.Mother = families.FirstOrDefault(x => x.FamilyType == FamilyTypeEnum.Mother);
            //}


            Profile = new ProfileViewModel
            {
                UserId = Guid.Parse(user)
            };

            return Page();

        }


        public async Task<IActionResult> OnPost()
        {
            var userId = Profile.UserId;

            //ClaimsPrincipal currentUser = this.User;
            Guid currUserId = userId;

            var profile = this.Profile;

            profile.Father.FamilyType = FamilyTypeEnum.Father;
            profile.Mother.FamilyType = FamilyTypeEnum.Mother;
            profile.Father.UserId = currUserId;
            profile.Mother.UserId = currUserId;

            var resf = await _familyServcie.CreateAsync(profile.Father);
            var resm = await _familyServcie.CreateAsync(profile.Mother);


            List<FamilyViewModel> families = await _familyServcie.GetAllByIdentityIdAsync(currUserId);
            profile.Father = families.FirstOrDefault(x => x.FamilyType == FamilyTypeEnum.Father);
            profile.Mother = families.FirstOrDefault(x => x.FamilyType == FamilyTypeEnum.Mother);



            //return Redirect("~/RegisterSiblings");
            return RedirectToPage("RegisterSiblings", new { user = userId });

        }

    }
}
