using EmployeeManagement.Web.Applications.Domains;

namespace EmployeeManagement.Web.Applications.Repositories;

/// <summary>
/// 社員Repositoryインターフェイス
/// </summary>
public interface IEmployeeRepository
{
    /// <summary>
    /// 社員一覧取得
    /// </summary>
    Task<IEnumerable<Employee>> SelectAllAsync();

    /// <summary>
    /// 社員ID検索
    /// </summary>
    Task<Employee?> SelectByIdAsync(int id);

    /// <summary>
    /// 社員番号検索
    /// </summary>
    Task<Employee?> SelectByEmployeeNoAsync(string employeeNo);

    /// <summary>
    /// 社員登録
    /// </summary>
    Task InsertAsync(Employee employee);

    /// <summary>
    /// 社員更新
    /// </summary>
    Task UpdateAsync(Employee employee);

    /// <summary>
    /// 社員削除
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// 社員検索
    /// </summary>
    Task<IEnumerable<Employee>> SearchAsync(string keyword);

    /// <summary>
    /// メールアドレス検索
    /// </summary>
    Task<Employee?> SelectByEmailAddressAsync(string emailAddress);
}