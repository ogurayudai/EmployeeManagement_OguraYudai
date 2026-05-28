using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Applications.Services;

namespace EmployeeManagement.Test.Applications;

[TestClass]
public class LoginServiceTest
{
    [TestMethod]
    public async Task LoginAsync_ログイン成功時LoginUserを返す()
    {
        var repository = new FakeLoginRepository
        {
            LoginSuccess = true
        };

        var service = new LoginService(repository);

        var result = await service.LoginAsync(
            "test@example.com",
            "password");

        Assert.IsNotNull(result);

        Assert.AreEqual(
            "山田太郎",
            result.EmployeeName);
    }

    [TestMethod]
    public async Task LoginAsync_ログイン失敗時nullを返す()
    {
        var repository = new FakeLoginRepository
        {
            LoginSuccess = false
        };

        var service = new LoginService(repository);

        var result = await service.LoginAsync(
            "test@example.com",
            "wrongpassword");

        Assert.IsNull(result);
    }
}

public class FakeLoginRepository : ILoginRepository
{
    public bool LoginSuccess { get; set; }

    public Task<LoginUser?> LoginAsync(
        string emailAddress,
        string password)
    {
        if (LoginSuccess)
        {
            return Task.FromResult<LoginUser?>(new LoginUser
            {
                Id = 1,
                EmployeeId = 1,
                EmailAddress = emailAddress,
                EmployeeName = "山田太郎"
            });
        }

        return Task.FromResult<LoginUser?>(null);
    }
}