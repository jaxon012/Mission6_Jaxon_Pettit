using Microsoft.AspNetCore.Mvc;

namespace Mission6_Jaxon_Pettit.Controllers;

public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    public IActionResult GetToKnowJoel()
    {
        return View();
    }
}