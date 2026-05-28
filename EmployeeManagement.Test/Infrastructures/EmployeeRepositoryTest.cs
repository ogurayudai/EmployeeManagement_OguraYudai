using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Infrastructures.Adapters;
using EmployeeManagement.Web.Infrastructures.Context;
using EmployeeManagement.Web.Infrastructures.Repositories;
using Microsoft.Extensions.Configuration;

namespace EmployeeManagement.Test.Infrastructures;

[TestClass]
public class EmployeeRepositoryTest
{
    private readonly EmployeeRepository _repository;

    public EmployeeRepositoryTest()
    {
        var configuration =
            new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

        var context =
            new DapperContext(configuration);

        var adapter =
            new EmployeeEntityAdapter();

        _repository =
            new EmployeeRepository(
                context,
                adapter);
    }

    [TestMethod]
    public async Task SelectAllAsync_社員一覧を取得できる()
    {
        var employees =
            await _repository.SelectAllAsync();

        Assert.IsNotNull(employees);

        Assert.IsTrue(employees.Any());
    }

    [TestMethod]
    public async Task SelectByIdAsync_社員を取得できる()
    {
        var employee =
            await _repository.SelectByIdAsync(1);

        Assert.IsNotNull(employee);

        Assert.AreEqual(1, employee.Id);
    }

    [TestMethod]
    public async Task InsertAsync_社員を登録できる()
    {
        var unique =
            DateTime.Now.ToString("yyyyMMddHHmmssfff");

        var employeeNo =
            unique[^10..];

        var emailAddress =
            $"repository-insert-{unique}@test.com";

        await DeleteIfExistsAsync(employeeNo);

        var employee =
            new Employee
            {
                DepartmentId = 1,
                EmployeeNo = employeeNo,
                Name = "テスト太郎",
                NameKana = "てすとたろう",
                EmailAddress = emailAddress,
                Birthday = new DateTime(2000, 1, 1),
                Gender = 1
            };

        await _repository.InsertAsync(employee);

        var insertedEmployee =
            await _repository.SelectByEmployeeNoAsync(
                employeeNo);

        Assert.IsNotNull(insertedEmployee);

        Assert.AreEqual(
            employeeNo,
            insertedEmployee.EmployeeNo);

        await DeleteIfExistsAsync(employeeNo);
    }

    [TestMethod]
    public async Task DeleteAsync_社員を削除できる()
    {
        var unique =
            DateTime.Now.ToString("yyyyMMddHHmmssfff");

        var employeeNo =
            unique[^10..];

        var emailAddress =
            $"repository-delete-{unique}@test.com";

        await DeleteIfExistsAsync(employeeNo);

        var employee =
            new Employee
            {
                DepartmentId = 1,
                EmployeeNo = employeeNo,
                Name = "削除太郎",
                NameKana = "さくじょたろう",
                EmailAddress = emailAddress,
                Birthday = new DateTime(2000, 1, 1),
                Gender = 1
            };

        await _repository.InsertAsync(employee);

        var insertedEmployee =
            await _repository.SelectByEmployeeNoAsync(
                employeeNo);

        Assert.IsNotNull(insertedEmployee);

        await _repository.DeleteAsync(
            insertedEmployee.Id);

        var deletedEmployee =
            await _repository.SelectByEmployeeNoAsync(
                employeeNo);

        Assert.IsNull(deletedEmployee);
    }

    private async Task DeleteIfExistsAsync(
        string employeeNo)
    {
        var employee =
            await _repository.SelectByEmployeeNoAsync(
                employeeNo);

        if (employee is not null)
        {
            await _repository.DeleteAsync(employee.Id);
        }
    }
}