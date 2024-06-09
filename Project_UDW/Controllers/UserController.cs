using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Project_UDW.Controllers
{
    public class UserController : Controller
    {
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Champion()
        {
            return View();
        }
    }
}
