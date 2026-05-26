using EmployeeManagement.Web.Applications.Domains;

namespace EmployeeManagement.Web.Applications.Repositories;

/// <summary>
/// ログインRepositoryインターフェイス
/// </summary>
public interface ILoginRepository
{
    /// <summary>
    /// ログインユーザー取得
    /// </summary>
    Task<LoginUser?> LoginAsync(
        string emailAddress,
        string password);
}