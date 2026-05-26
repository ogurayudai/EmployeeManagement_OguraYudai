using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Services;
using EmployeeManagement.Web.Presentations.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Presentations.Controllers;

/// <summary>
/// 社員Controller
/// </summary>
public class EmployeeController : Controller
{
    private readonly EmployeeService _employeeService;
    private readonly DepartmentService _departmentService;

    public EmployeeController(
        EmployeeService employeeService,
        DepartmentService departmentService)
    {
        _employeeService = employeeService;
        _departmentService = departmentService;
    }

    /// <summary>
    /// 社員一覧画面
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index(string? keyword)
    {
        var employeeName =
            HttpContext.Session.GetString("LoginEmployeeName");

        if (string.IsNullOrEmpty(employeeName))
        {
            return RedirectToAction("Index", "Login");
        }

        var employees =
            await _employeeService.SearchAsync(keyword ?? string.Empty);

        ViewBag.Keyword = keyword;

        return View(employees);
    }

    /// <summary>
    /// 社員追加画面
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Create()
    {
        var employeeName =
            HttpContext.Session.GetString("LoginEmployeeName");

        if (string.IsNullOrEmpty(employeeName))
        {
            return RedirectToAction("Index", "Login");
        }

        var departments =
            await _departmentService.GetAllAsync();

        var viewModel = new EmployeeFormViewModel
        {
            Departments = departments
        };

        return View(viewModel);
    }

    /// <summary>
    /// 社員追加処理
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(
        EmployeeFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        var employee = new Employee
        {
            DepartmentId = viewModel.DepartmentId,
            EmployeeNo = viewModel.EmployeeNo,
            Name = viewModel.Name,
            NameKana = viewModel.NameKana,
            EmailAddress = viewModel.EmailAddress,
            Birthday = viewModel.Birthday,
            Gender = viewModel.Gender
        };

        var errorMessage =
            await _employeeService.RegisterAsync(employee);

        if (errorMessage is not null)
        {
            ModelState.AddModelError("", errorMessage);

            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// 社員更新画面
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var employee =
            await _employeeService.GetByIdAsync(id);

        if (employee is null)
        {
            return RedirectToAction("Index");
        }

        var departments =
            await _departmentService.GetAllAsync();

        var viewModel = new EmployeeFormViewModel
        {
            Id = employee.Id,
            DepartmentId = employee.DepartmentId,
            EmployeeNo = employee.EmployeeNo,
            Name = employee.Name,
            NameKana = employee.NameKana,
            EmailAddress = employee.EmailAddress,
            Birthday = employee.Birthday,
            Gender = employee.Gender,
            Departments = departments
        };

        return View(viewModel);
    }

    /// <summary>
    /// 社員更新処理
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(
        EmployeeFormViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        var employee = new Employee
        {
            Id = viewModel.Id,
            DepartmentId = viewModel.DepartmentId,
            EmployeeNo = viewModel.EmployeeNo,
            Name = viewModel.Name,
            NameKana = viewModel.NameKana,
            EmailAddress = viewModel.EmailAddress,
            Birthday = viewModel.Birthday,
            Gender = viewModel.Gender
        };

        var errorMessage =
            await _employeeService.UpdateAsync(employee);

        if (errorMessage is not null)
        {
            ModelState.AddModelError("", errorMessage);

            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// 社員削除確認画面
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var employee =
            await _employeeService.GetByIdAsync(id);

        if (employee is null)
        {
            return RedirectToAction("Index");
        }

        return View(employee);
    }

    /// <summary>
    /// 社員削除処理
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        await _employeeService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}