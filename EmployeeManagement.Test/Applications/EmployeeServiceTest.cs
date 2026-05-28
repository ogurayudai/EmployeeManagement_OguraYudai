using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Applications.Services;

namespace EmployeeManagement.Test.Applications;

[TestClass]
public class EmployeeServiceTest
{
    [TestMethod]
    public async Task RegisterAsync_社員番号が重複している場合エラーメッセージを返す()
    {
        var repository = new FakeEmployeeRepository
        {
            ExistsEmployeeNo = true
        };

        var service = new EmployeeService(repository);

        var employee = new Employee
        {
            EmployeeNo = "200801",
            NameKana = "やまだたろう",
            EmailAddress = "new@example.com",
            Birthday = new DateTime(2000, 1, 1)
        };

        var result = await service.RegisterAsync(employee);

        Assert.AreEqual(
            "入力された社員番号は既に使用されています。",
            result);
    }

    [TestMethod]
    public async Task RegisterAsync_メールアドレスが重複している場合エラーメッセージを返す()
    {
        var repository = new FakeEmployeeRepository
        {
            ExistsEmailAddress = true
        };

        var service = new EmployeeService(repository);

        var employee = new Employee
        {
            EmployeeNo = "200802",
            NameKana = "やまだたろう",
            EmailAddress = "test@example.com",
            Birthday = new DateTime(2000, 1, 1)
        };

        var result = await service.RegisterAsync(employee);

        Assert.AreEqual(
            "入力されたメールアドレスは既に使用されています。",
            result);
    }

    [TestMethod]
    public async Task RegisterAsync_重複がない場合nullを返す()
    {
        var repository = new FakeEmployeeRepository();

        var service = new EmployeeService(repository);

        var employee = new Employee
        {
            EmployeeNo = "200803",
            NameKana = "やまだたろう",
            EmailAddress = "ok@example.com",
            Birthday = new DateTime(2000, 1, 1)
        };

        var result = await service.RegisterAsync(employee);

        Assert.IsNull(result);
    }
}

public class FakeEmployeeRepository : IEmployeeRepository
{
    public bool ExistsEmployeeNo { get; set; }

    public bool ExistsEmailAddress { get; set; }

    public Task<Employee?> SelectByEmployeeNoAsync(
        string employeeNo)
    {
        if (ExistsEmployeeNo)
        {
            return Task.FromResult<Employee?>(
                new Employee
                {
                    Id = 1,
                    EmployeeNo = employeeNo
                });
        }

        return Task.FromResult<Employee?>(null);
    }

    public Task<Employee?> SelectByEmailAddressAsync(
        string emailAddress)
    {
        if (ExistsEmailAddress)
        {
            return Task.FromResult<Employee?>(
                new Employee
                {
                    Id = 1,
                    EmailAddress = emailAddress
                });
        }

        return Task.FromResult<Employee?>(null);
    }

    public Task InsertAsync(Employee employee)
    {
        return Task.CompletedTask;
    }

    public Task<IEnumerable<Employee>> SelectAllAsync()
    {
        return Task.FromResult<IEnumerable<Employee>>(
            new List<Employee>());
    }

    public Task<IEnumerable<Employee>> SearchAsync(
        string keyword)
    {
        return Task.FromResult<IEnumerable<Employee>>(
            new List<Employee>());
    }

    public Task<Employee?> SelectByIdAsync(int id)
    {
        return Task.FromResult<Employee?>(null);
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