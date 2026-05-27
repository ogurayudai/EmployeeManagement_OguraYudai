using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Services;
using EmployeeManagement.Web.Exceptions;
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

        return View(new EmployeeFormViewModel
        {
            Departments = departments
        });
    }

    [HttpPost]
    public async Task<IActionResult> Create(EmployeeFormViewModel viewModel)
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

        string? errorMessage;

        try
        {
            errorMessage =
                await _employeeService.RegisterAsync(employee);
        }
        catch (DomainException e)
        {
            ModelState.AddModelError("", e.Message);

            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        if (errorMessage is not null)
        {
            ModelState.AddModelError("", errorMessage);

            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        return RedirectToAction("Index");
    }

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

        return View(new EmployeeFormViewModel
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
        });
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EmployeeFormViewModel viewModel)
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

        string? errorMessage;

        try
        {
            errorMessage =
                await _employeeService.UpdateAsync(employee);
        }
        catch (DomainException e)
        {
            ModelState.AddModelError("", e.Message);

            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        if (errorMessage is not null)
        {
            ModelState.AddModelError("", errorMessage);

            viewModel.Departments =
                await _departmentService.GetAllAsync();

            return View(viewModel);
        }

        return RedirectToAction("Index");
    }

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

    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
        await _employeeService.DeleteAsync(id);

        return RedirectToAction("Index");
    }
}