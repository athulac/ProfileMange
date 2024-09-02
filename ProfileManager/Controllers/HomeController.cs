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
            //List<ProfileViewModel> partners = new List<ProfileViewModel>()
            //{
            //    new ProfileViewModel { Id= new Guid(), Age = 19, City = "Balangoda", Distirct = "Ratnapura", FirstName = "Athula", Gender = "Male", LastName = "Chandrawansah" },
            //    new ProfileViewModel { Id= new Guid(), Age = 27, City = "Ratnapura", Distirct = "Ratnapura", FirstName = "Anil", Gender = "Male", LastName = "Jayantha" },
            //    new ProfileViewModel { Id= new Guid(), Age = 30, City = "Kandy", Distirct = "Kandy", FirstName = "Anura", Gender = "Male", LastName = "Sampath" },
            //    new ProfileViewModel { Id= new Guid(), Age = 45, City = "Hambanthota", Distirct = "Hambanthota", FirstName = "Kusumi", Gender = "Feale", LastName = "Gmalath" },
            //    new ProfileViewModel { Id= new Guid(), Age = 50, City = "Balangoda", Distirct = "Ratnapura", FirstName = "Amila", Gender = "Feale", LastName = "Nadeesha" },
            //};

            var profiles = await _profileServcie.GetAllAsync();

            return View(profiles);
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


    }
}