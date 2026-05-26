using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;

namespace EmployeeManagement.Web.Applications.Services;

/// <summary>
/// ログインサービス
/// </summary>
public class LoginService
{
    private readonly ILoginRepository _loginRepository;

    public LoginService(ILoginRepository loginRepository)
    {
        _loginRepository = loginRepository;
    }

    public async Task<LoginUser?> LoginAsync(
        string emailAddress,
        string password)
    {
        return await _loginRepository.LoginAsync(emailAddress, password);
    }
}