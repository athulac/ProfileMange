using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using ProfileManager.Areas.Identity.Data;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Text;
using System.Text.Encodings.Web;

namespace ProfileManager.Areas.Identity.Pages.Account
{
    public class RegisterFinishedModel : PageModel
    {

        private readonly SignInManager<ProfileManagerUser> _signInManager;
        private readonly UserManager<ProfileManagerUser> _userManager;
        private readonly IUserStore<ProfileManagerUser> _userStore;
        private readonly IUserEmailStore<ProfileManagerUser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;
        private readonly IEmailService _emailService;
        private readonly IProfileServcie _profileServcie;

        private IWebHostEnvironment Environment { get; set; }

        public RegisterFinishedModel(
         UserManager<ProfileManagerUser> userManager,
         IUserStore<ProfileManagerUser> userStore,
         SignInManager<ProfileManagerUser> signInManager,
         ILogger<RegisterModel> logger,
         IEmailService emailService,
         IProfileServcie profileServcie,
         IWebHostEnvironment environment
         )
        {
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailService = emailService;
            _profileServcie = profileServcie;
            this.Environment = environment;
        }


        [BindProperty]
        public InputModel Input { get; set; }
        public string ReturnUrl { get; set; }
        public IList<AuthenticationScheme> ExternalLogins { get; set; }


        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }

            public ProfileViewModel Profile { get; set; }
        }


        public async Task<IActionResult> OnGetAsync(string user = null)
        {
            ReturnUrl = user;
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            Input = new InputModel
            {
                Profile = new ProfileViewModel { UserId = Guid.Parse(user) }
            };

            await OnPostAsync();

            return Page();

        }


        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //returnUrl ??= Url.Content("~/");
            returnUrl = Url.Content("~/");
            //ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (true)
            {
                var curusrid = Input.Profile.UserId.ToString();

                var currUser = await _userManager.FindByIdAsync(curusrid);

                Input.Profile = await _profileServcie.GetByIdentityIdAsync(Input.Profile.UserId);

                Input.Email = currUser.NormalizedUserName;

                var user = currUser;
                //var user = CreateUser();

                await _userStore.SetUserNameAsync(user, Input.Email, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);

                //var result = await _userManager.CreateAsync(user, Input.Password);

                if (true)
                {

                    //returnUrl = "/Account/RegisterConfirmation";

                    _logger.LogInformation("User created a new account with password.");

                    //var userId = await _userManager.GetUserIdAsync(user);
                    var userId = Input.Profile.UserId;

                    //create profile
                    var prof = Input.Profile;
                    prof.UserId = userId;
                    //await _profileServcie.ModifyBaseUserIdAsync(prof);
                    //if (resProf == 0)
                    //{

                    //}

                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);
                    callbackUrl.Replace("&amp;", "&");


                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");
                    //await _emailService.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{callbackUrl}'>clicking here</a>.");
                    await _emailService.SendEmailAsync(Input.Email, "Confirm email", PopulateBody(Input.Profile.FirstName, "Confirm your email",
                       callbackUrl, "Please confirm your account"));



                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        //return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                        return Page();
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }                    
                    
                }
                //foreach (var error in result.Errors)
                //{
                //    ModelState.AddModelError(string.Empty, error.Description);
                //}
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }



        private ProfileManagerUser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<ProfileManagerUser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(ProfileManagerUser)}'. " +
                    $"Ensure that '{nameof(ProfileManagerUser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<ProfileManagerUser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<ProfileManagerUser>)_userStore;
        }


        private string PopulateBody(string name, string title, string url, string description)
        {
            string body = string.Empty;
            string path = Path.Combine(this.Environment.WebRootPath, "Template\\AccountConfirm.html");
            //string path = Path.Combine(this.Environment.WebRootPath, "Template\\EmailTemplate.htm");
            using (StreamReader reader = new StreamReader(path))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{UserName}", name);
            body = body.Replace("{Title}", title);
            body = body.Replace("{Url}", url);
            body = body.Replace("{Description}", description);
            return body;
        }

    }
}
