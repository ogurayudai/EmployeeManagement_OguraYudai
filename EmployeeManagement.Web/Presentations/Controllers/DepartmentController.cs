using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Web.Presentations.Controllers;

/// <summary>
/// 部門Controller
/// </summary>
public class DepartmentController : Controller
{
    private readonly DepartmentService _departmentService;

    public DepartmentController(DepartmentService departmentService)
    {
        _departmentService = departmentService;
    }

    /// <summary>
    /// 部門一覧画面
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var employeeName =
            HttpContext.Session.GetString("LoginEmployeeName");

        if (string.IsNullOrEmpty(employeeName))
        {
            return RedirectToAction("Index", "Login");
        }

        var departments =
            await _departmentService.GetAllAsync();

        return View(departments);
    }

    /// <summary>
    /// 部門追加画面表示
    /// </summary>
    [HttpGet]
    public IActionResult Create()
    {
        var employeeName =
            HttpContext.Session.GetString("LoginEmployeeName");

        if (string.IsNullOrEmpty(employeeName))
        {
            return RedirectToAction("Index", "Login");
        }

        return View(new Department());
    }

    /// <summary>
    /// 部門追加処理
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Create(Department department)
    {
        if (string.IsNullOrWhiteSpace(department.DeptName))
        {
            ModelState.AddModelError(
                "DeptName",
                "部門名を入力してください。");

            return View(department);
        }

        var result =
            await _departmentService.RegisterAsync(department);

        if (!result)
        {
            ModelState.AddModelError(
                "DeptName",
                "入力された部門名は既に使用されています。");

            return View(department);
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// 部門更新画面表示
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var department =
            await _departmentService.GetByIdAsync(id);

        if (department is null)
        {
            return RedirectToAction("Index");
        }

        return View(department);
    }

    /// <summary>
    /// 部門更新処理
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> Edit(Department department)
    {
        if (string.IsNullOrWhiteSpace(department.DeptName))
        {
            ModelState.AddModelError(
                "DeptName",
                "部門名を入力してください。");

            return View(department);
        }

        var result =
            await _departmentService.UpdateAsync(department);

        if (!result)
        {
            ModelState.AddModelError(
                "DeptName",
                "入力された部門名は既に使用されています。");

            return View(department);
        }

        return RedirectToAction("Index");
    }

    /// <summary>
    /// 部門削除確認画面
    /// </summary>
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var department =
            await _departmentService.GetByIdAsync(id);

        if (department is null)
        {
            return RedirectToAction("Index");
        }

        return View(department);
    }

    /// <summary>
    /// 部門削除処理
    /// </summary>
    [HttpPost]
    public async Task<IActionResult> DeleteConfirm(int id)
    {
            var result =
            await _departmentService.DeleteAsync(id);

        if (!result)
        {
            var department =
                await _departmentService.GetByIdAsync(id);

            if (department is null)
            {
                return RedirectToAction("Index");
            }

            ModelState.AddModelError(
                "",
                "この部門に所属している社員がいるため削除できません。");

            return View("Delete", department);
        }

        return RedirectToAction("Index");
    }
}