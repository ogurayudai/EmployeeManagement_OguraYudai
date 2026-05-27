using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Infrastructures.Entitys;

namespace EmployeeManagement.Web.Infrastructures.Adapters;

/// <summary>
/// ログインEntity変換Adapter
/// </summary>
public class LoginEntityAdapter
{
    /// <summary>
    /// EntityからDomainを復元する
    /// </summary>
    public LoginUser Restore(LoginEntity entity)
    {
        return new LoginUser
        {
            Id = entity.Id,
            EmailAddress = entity.EmailAddress,
            EmployeeId = entity.EmployeeId,
            LoginPassword = entity.LoginPassword,
            EmployeeName = entity.EmployeeName
        };
    }
}