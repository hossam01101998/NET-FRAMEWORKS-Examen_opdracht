using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Models;
using NET_FRAMEWORKS_EXAMEN_OPDRACHT.Areas.Identity.Data;

namespace NET_FRAMEWORKS_EXAMEN_OPDRACHT.Controllers
{
    
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        //cookies
        private readonly SignInManager<Garage2User> _signInManager;


        public HomeController(ILogger<HomeController> logger, SignInManager<Garage2User> signInManager)
        {
            _logger = logger;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {

            // verificar si el usuario está autenticado
            if (_signInManager.IsSignedIn(User))
            {
                // verificar si la cookie está presente
                bool cookiePresente = (bool)HttpContext.Items["CookiePresente"];

            }
            else
            {
                // El usuario no está autenticado, realizar la lógica correspondiente

                return Redirect("/Identity/Account/Login");

                //http://localhost:5162/Identity%2FAccount/Login
                //http://localhost:5162/Identity/Account/Login
            }

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