using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Entities;
using System.Security.Claims;

[Authorize(Roles = "Admin,ServiceProvider")]
public class ScheduleController : Controller
{   
    public IActionResult Index()
    {
        var claims = User.Claims
            .Select(c => $"{c.Type} = {c.Value}")
            .ToList();

        foreach (var claim in claims)
        {
            Console.WriteLine(claim);
        }

        return View();
    }
}