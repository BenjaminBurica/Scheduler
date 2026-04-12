using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[AllowAnonymous]
public class LoginController : Controller
{   
    public IActionResult Index()
    {
        return View();
    }
}