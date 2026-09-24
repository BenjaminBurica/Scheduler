using Microsoft.AspNetCore.Mvc;
using BusinessLogic.UseCases;
using Entities;
using Microsoft.AspNetCore.Authorization;

namespace SchedulerWeb.Controllers;

[Authorize(Roles = "Admin,ServiceProvider")]
public class CustomerProfilesController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}