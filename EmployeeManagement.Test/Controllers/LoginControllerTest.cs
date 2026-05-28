using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Applications.Services;
using EmployeeManagement.Web.Presentations.Controllers;
using EmployeeManagement.Web.Presentations.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Test.Controllers;

[TestClass]
public class LoginControllerTest
{
    [TestMethod]
    public async Task Index_ログイン成功時Homeへリダイレクトする()
    {
        var repository =
            new SuccessLoginRepository();

        var service =
            new LoginService(repository);

        var controller =
            new LoginController(service);

        controller.ControllerContext =
            new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    Session = new FakeSession()
                }
            };

        var viewModel =
            new LoginViewModel
            {
                EmailAddress = "test@example.com",
                Password = "password"
            };

        var result =
            await controller.Index(viewModel);

        Assert.IsInstanceOfType(
            result,
            typeof(RedirectToActionResult));

        var redirectResult =
            (RedirectToActionResult)result;

        Assert.AreEqual(
            "Index",
            redirectResult.ActionName);

        Assert.AreEqual(
            "Home",
            redirectResult.ControllerName);
    }

    [TestMethod]
    public async Task Index_ログイン失敗時Viewを返す()
    {
        var repository =
            new FailedLoginRepository();

        var service =
            new LoginService(repository);

        var controller =
            new LoginController(service);

        var viewModel =
            new LoginViewModel
            {
                EmailAddress = "test@example.com",
                Password = "wrongpassword"
            };

        var result =
            await controller.Index(viewModel);

        Assert.IsInstanceOfType(
            result,
            typeof(ViewResult));
    }

    [TestMethod]
    public async Task Index_ModelStateエラー時Viewを返す()
    {
        var repository =
            new FailedLoginRepository();

        var service =
            new LoginService(repository);

        var controller =
            new LoginController(service);

        controller.ModelState.AddModelError(
            "EmailAddress",
            "メールアドレスは必須です。");

        var viewModel =
            new LoginViewModel();

        var result =
            await controller.Index(viewModel);

        Assert.IsInstanceOfType(
            result,
            typeof(ViewResult));
    }
}

public class SuccessLoginRepository : ILoginRepository
{
    public Task<LoginUser?> LoginAsync(
        string emailAddress,
        string password)
    {
        return Task.FromResult<LoginUser?>(
            new LoginUser
            {
                Id = 1,
                EmployeeId = 1,
                EmployeeName = "山田太郎",
                EmailAddress = emailAddress
            });
    }
}

public class FailedLoginRepository : ILoginRepository
{
    public Task<LoginUser?> LoginAsync(
        string emailAddress,
        string password)
    {
        return Task.FromResult<LoginUser?>(null);
    }
}

public class FakeSession : ISession
{
    private readonly Dictionary<string, byte[]> _sessionStorage =
        new Dictionary<string, byte[]>();

    public bool IsAvailable => true;

    public string Id => Guid.NewGuid().ToString();

    public IEnumerable<string> Keys => _sessionStorage.Keys;

    public void Clear()
    {
        _sessionStorage.Clear();
    }

    public Task CommitAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public Task LoadAsync(
        CancellationToken cancellationToken = default)
    {
        return Task.CompletedTask;
    }

    public void Remove(string key)
    {
        _sessionStorage.Remove(key);
    }

    public void Set(string key, byte[] value)
    {
        _sessionStorage[key] = value;
    }

    public bool TryGetValue(
        string key,
        out byte[] value)
    {
        return _sessionStorage.TryGetValue(
            key,
            out value!);
    }
}