using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Applications.Services;
using EmployeeManagement.Web.Presentations.Controllers;
using EmployeeManagement.Web.Presentations.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Test.Controllers;

[TestClass]
public class EmployeeControllerTest
{
    [TestMethod]
    public async Task Create_正常登録時Indexへリダイレクトする()
    {
        var employeeRepository = new FakeEmployeeRepository();
        var departmentRepository = new FakeDepartmentRepository();

        var employeeService = new EmployeeService(employeeRepository);
        var departmentService = new DepartmentService(departmentRepository);

        var controller = new EmployeeController(
            employeeService,
            departmentService);

        var viewModel = new EmployeeFormViewModel
        {
            DepartmentId = 1,
            EmployeeNo = "200801",
            Name = "山田太郎",
            NameKana = "やまだたろう",
            EmailAddress = "test@example.com",
            Birthday = new DateTime(2000, 1, 1),
            Gender = 1
        };

        var result = await controller.Create(viewModel);

        Assert.IsInstanceOfType(result, typeof(RedirectToActionResult));

        var redirectResult = (RedirectToActionResult)result;

        Assert.AreEqual("Index", redirectResult.ActionName);
    }

    [TestMethod]
    public async Task Create_社員番号重複時Viewを返す()
    {
        var employeeRepository = new DuplicateEmployeeRepository();
        var departmentRepository = new FakeDepartmentRepository();

        var employeeService = new EmployeeService(employeeRepository);
        var departmentService = new DepartmentService(departmentRepository);

        var controller = new EmployeeController(
            employeeService,
            departmentService);

        var viewModel = new EmployeeFormViewModel
        {
            DepartmentId = 1,
            EmployeeNo = "200801",
            Name = "山田太郎",
            NameKana = "やまだたろう",
            EmailAddress = "test@example.com",
            Birthday = new DateTime(2000, 1, 1),
            Gender = 1
        };

        var result = await controller.Create(viewModel);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }

    [TestMethod]
    public async Task Create_ModelStateエラー時Viewを返す()
    {
        var employeeRepository = new FakeEmployeeRepository();
        var departmentRepository = new FakeDepartmentRepository();

        var employeeService = new EmployeeService(employeeRepository);
        var departmentService = new DepartmentService(departmentRepository);

        var controller = new EmployeeController(
            employeeService,
            departmentService);

        controller.ModelState.AddModelError(
            "Name",
            "氏名は必須です。");

        var viewModel = new EmployeeFormViewModel();

        var result = await controller.Create(viewModel);

        Assert.IsInstanceOfType(result, typeof(ViewResult));
    }
}

public class FakeEmployeeRepository : IEmployeeRepository
{
    public Task<IEnumerable<Employee>> SelectAllAsync()
    {
        return Task.FromResult<IEnumerable<Employee>>(
            new List<Employee>());
    }

    public Task<IEnumerable<Employee>> SearchAsync(string keyword)
    {
        return Task.FromResult<IEnumerable<Employee>>(
            new List<Employee>());
    }

    public Task<Employee?> SelectByIdAsync(int id)
    {
        return Task.FromResult<Employee?>(null);
    }

    public virtual Task<Employee?> SelectByEmployeeNoAsync(
        string employeeNo)
    {
        return Task.FromResult<Employee?>(null);
    }

    public Task<Employee?> SelectByEmailAddressAsync(
        string emailAddress)
    {
        return Task.FromResult<Employee?>(null);
    }

    public Task InsertAsync(Employee employee)
    {
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Employee employee)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        return Task.CompletedTask;
    }
}

public class DuplicateEmployeeRepository : FakeEmployeeRepository
{
    public override Task<Employee?> SelectByEmployeeNoAsync(
        string employeeNo)
    {
        return Task.FromResult<Employee?>(new Employee
        {
            Id = 1,
            EmployeeNo = employeeNo
        });
    }
}

public class FakeDepartmentRepository : IDepartmentRepository
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

    public Task<Department?> SelectByDeptNameAsync(
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