using EmployeeManagement.Web.Applications.Services;
using EmployeeManagement.Web.Presentations.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Presentations.Controllers;

/// <summary>
/// ログインController
/// </summary>
public class LoginController : Controller
{
    private readonly LoginService _loginService;

    public LoginController(LoginService loginService)
    {
        _loginService = loginService;
    }

    /// <summary>
    /// ログイン画面表示
    /// </summary>
    [HttpGet]
    public IActionResult Index()
    {
        return View(new LoginViewModel());
    }

    /// <summary>
    /// ログイン処理
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Index(LoginViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return View(viewModel);
        }

        var loginUser = await _loginService.LoginAsync(
            viewModel.EmailAddress,
            viewModel.Password);

        if (loginUser is null)
        {
            viewModel.ErrorMessage =
                "ログインID、もしくはパスワードが間違っています";

            return View(viewModel);
        }

        HttpContext.Session.SetInt32("LoginEmployeeId", loginUser.EmployeeId);
        HttpContext.Session.SetString("LoginEmployeeName", loginUser.EmployeeName);

        return RedirectToAction("Index", "Home");
    }

    /// <summary>
    /// ログアウト
    /// </summary>
    [HttpPost]
    public IActionResult Logout()
    {
        HttpContext.Session.Clear();

        return RedirectToAction("Index", "Login");
    }
}