using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Applications.Services;
using EmployeeManagement.Web.Presentations.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Test.Controllers;

[TestClass]
public class DepartmentControllerTest
{
    [TestMethod]
    public async Task Create_正常登録時Indexへリダイレクトする()
    {
        var repository =
            new FakeDepartmentRepositoryForController();

        var service =
            new DepartmentService(repository);

        var controller =
            new DepartmentController(service);

        var department = new Department
        {
            DeptName = "開発"
        };

        var result =
            await controller.Create(department);

        Assert.IsInstanceOfType(
            result,
            typeof(RedirectToActionResult));

        var redirectResult =
            (RedirectToActionResult)result;

        Assert.AreEqual(
            "Index",
            redirectResult.ActionName);
    }

    [TestMethod]
    public async Task Create_部門名重複時Viewを返す()
    {
        var repository =
            new DuplicateDepartmentRepositoryForController();

        var service =
            new DepartmentService(repository);

        var controller =
            new DepartmentController(service);

        var department = new Department
        {
            DeptName = "営業"
        };

        var result =
            await controller.Create(department);

        Assert.IsInstanceOfType(
            result,
            typeof(ViewResult));
    }

    [TestMethod]
    public async Task Create_ModelStateエラー時Viewを返す()
    {
        var repository =
            new FakeDepartmentRepositoryForController();

        var service =
            new DepartmentService(repository);

        var controller =
            new DepartmentController(service);

        controller.ModelState.AddModelError(
            "DeptName",
            "部門名は必須です。");

        var department =
            new Department();

        var result =
            await controller.Create(department);

        Assert.IsInstanceOfType(
            result,
            typeof(ViewResult));
    }
}

public class FakeDepartmentRepositoryForController
    : IDepartmentRepository
{
    public Task<IEnumerable<Department>> SelectAllAsync()
    {
        return Task.FromResult<IEnumerable<Department>>(
            new List<Department>());
    }

    public Task<Department?> SelectByIdAsync(int id)
    {
        return Task.FromResult<Department?>(null);
    }

    public virtual Task<Department?> SelectByDeptNameAsync(
        string deptName)
    {
        return Task.FromResult<Department?>(null);
    }

    public Task InsertAsync(Department department)
    {
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Department department)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        return Task.CompletedTask;
    }

    public Task<int> CountEmployeesAsync(int departmentId)
    {
        return Task.FromResult(0);
    }
}

public class DuplicateDepartmentRepositoryForController
    : FakeDepartmentRepositoryForController
{
    public override Task<Department?> SelectByDeptNameAsync(
        string deptName)
    {
        return Task.FromResult<Department?>(new Department
        {
            Id = 1,
            DeptName = deptName
        });
    }
}