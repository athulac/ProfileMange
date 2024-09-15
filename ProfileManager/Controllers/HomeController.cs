using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ProfileManager.Models;
using ProfileManager.Services;
using ProfileManager.ViewModels;
using System.Diagnostics;

namespace ProfileManager.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IProfileServcie _profileServcie;

        public HomeController(ILogger<HomeController> logger, IProfileServcie profileServcie)
        {
            _logger = logger;
            _profileServcie = profileServcie;
        }

        public async Task<IActionResult> Index()        
        {
            var profiles = await _profileServcie.GetAllAsync();

            return View(profiles);
        }

        public async Task<IActionResult> Details(Guid id)
        {  

            var profile = await _profileServcie.GetAsync(id);

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