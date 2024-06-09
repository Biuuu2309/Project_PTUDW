using Microsoft.AspNetCore.Mvc;
using Project_UDW.Models;
using System.Diagnostics;

namespace Project_UDW.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ProfileUser()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            return View();
        }
        public IActionResult AdminChampions()
        {
            return View();
        }
        public IActionResult AdminChampionsSkill()
        {
            return View();
        }
        public IActionResult AdminChampionsSkin()
        {
            return View();
        }
        public IActionResult AdminDetailsChamp()
        {
            return View();
        }
        public IActionResult AdminUpdate()
        {
            return View();
        }
        public IActionResult AdminItem()
        {
            return View();
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
