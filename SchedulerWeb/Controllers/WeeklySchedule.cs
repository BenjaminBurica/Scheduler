using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SchedulerWeb.Models;

namespace SchedulerWeb.Controllers;

[Authorize]
public class WeeklySchedule : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
