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

    public IActionResult Index(Event newEvent)
    {   
        List<Event> events = new List<Event>();
        Event event1 = new Event();
        event1.name = "test event 1";
        event1.desc = "test event description";
        event1.date = "2024-10-11";

        Event event2 = new Event();
        event2.name = "test event 2";
        event2.desc = "test event description";
        event2.date = "2024-11-02";

        Event event3 = new Event();
        event3.name = "test event 3";
        event3.desc = "test event description";
        event3.date = "2024-10-23";

        events.Add(event1);
        events.Add(event2);
        events.Add(event3);
        
        if (newEvent.name != null){
            events.Add(newEvent);
        }



        return View(events);


        //connection string is refrenced from appsettings.json
        // Database.Instance.SetConnectionString(_configuration.GetValue<string>("ConnectionString"));
        // return View(Database.Instance.GetEvents());
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
