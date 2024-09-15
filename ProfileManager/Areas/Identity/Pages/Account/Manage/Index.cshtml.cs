using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ProfileManager.Areas.Identity.Data;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Security.Claims;

namespace ProfileManager.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly IProfileServcie _profileServcie;
        private readonly UserManager<ProfileManagerUser> _userManager;

        public IndexModel(IProfileServcie profileServcie, UserManager<ProfileManagerUser> userManager)
        {
            _profileServcie = profileServcie;
            _userManager = userManager;     

        }

        [BindProperty]
        public ProfileViewModel Profile { get; set; }

        public async void OnGet()
        {
            ClaimsPrincipal currentUser = this.User;
            Guid currUserId = Guid.Parse(currentUser.FindFirst(ClaimTypes.NameIdentifier).Value);

            ProfileViewModel profile = await _profileServcie.GetByIdentityIdAsync(currUserId);
            this.Profile = profile;

        }

        public async void OnPost()
        {
            var profile = this.Profile;
            var res = await _profileServcie.ModifyAsync(profile);
            this.Profile = res;
        }

    }
}
