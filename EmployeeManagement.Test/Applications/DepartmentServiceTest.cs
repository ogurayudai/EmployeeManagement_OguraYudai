using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Applications.Services;

namespace EmployeeManagement.Test.Applications;

[TestClass]
public class DepartmentServiceTest
{
    [TestMethod]
    public async Task RegisterAsync_部門名が重複している場合falseを返す()
    {
        var repository = new FakeDepartmentRepository
        {
            ExistsDeptName = true
        };

        var service = new DepartmentService(repository);

        var result = await service.RegisterAsync(new Department
        {
            DeptName = "営業"
        });

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task RegisterAsync_部門名が重複していない場合trueを返す()
    {
        var repository = new FakeDepartmentRepository();

        var service = new DepartmentService(repository);

        var result = await service.RegisterAsync(new Department
        {
            DeptName = "開発"
        });

        Assert.IsTrue(result);
    }

    [TestMethod]
    public async Task DeleteAsync_所属社員がいる場合falseを返す()
    {
        var repository = new FakeDepartmentRepository
        {
            EmployeeCount = 1
        };

        var service = new DepartmentService(repository);

        var result = await service.DeleteAsync(1);

        Assert.IsFalse(result);
    }

    [TestMethod]
    public async Task DeleteAsync_所属社員がいない場合trueを返す()
    {
        var repository = new FakeDepartmentRepository
        {
            EmployeeCount = 0
        };

        var service = new DepartmentService(repository);

        var result = await service.DeleteAsync(1);

        Assert.IsTrue(result);
    }
}

public class FakeDepartmentRepository : IDepartmentRepository
{
    public bool ExistsDeptName { get; set; }

    public int EmployeeCount { get; set; }

    public Task<Department?> SelectByDeptNameAsync(string deptName)
    {
        if (ExistsDeptName)
        {
            return Task.FromResult<Department?>(new Department
            {
                Id = 1,
                DeptName = deptName
            });
        }

        return Task.FromResult<Department?>(null);
    }

    public Task<int> CountEmployeesAsync(int departmentId)
    {
        return Task.FromResult(EmployeeCount);
    }

    public Task InsertAsync(Department department)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Department>> SelectAllAsync()
    {
        return Task.FromResult<IEnumerable<Department>>(new List<Department>());
    }

    public Task<Department?> SelectByIdAsync(int id)
    {
        return Task.FromResult<Department?>(null);
    }

    public Task UpdateAsync(Department department)
    {
        return Task.CompletedTask;
    }
}