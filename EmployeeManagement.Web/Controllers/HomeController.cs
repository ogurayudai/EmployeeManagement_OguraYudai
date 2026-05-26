using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Presentations.Controllers;

/// <summary>
/// ホームController
/// </summary>
public class HomeController : Controller
{
    /// <summary>
    /// ホーム画面
    /// </summary>
    [HttpGet]
    public IActionResult Index()
    {
        var employeeName =
            HttpContext.Session.GetString("LoginEmployeeName");

        if (string.IsNullOrEmpty(employeeName))
        {
            return RedirectToAction("Index", "Login");
        }

        return View("Index", employeeName);
    }
}