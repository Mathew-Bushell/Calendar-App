using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Calendar_App.Models;
using System.ComponentModel;

namespace Calendar_App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IConfiguration _configuration;

    public HomeController(ILogger<HomeController> logger, IConfiguration configuration)
    {
        _logger = logger;
        _configuration = configuration;
    }

    public IActionResult Index()
    {   
        //connection string is refrenced from appsettings.json
        Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        return View(Database.Instance.GetEvents());
    }

    public IActionResult NewEventResult(Event calEvent){
        Database.Instance.AddEvent(calEvent);
        return View(calEvent);
    }
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
