using EmployeeManagement.Web.Applications.Domains;

namespace EmployeeManagement.Web.Applications.Repositories;

/// <summary>
/// 部門Repositoryインターフェイス
/// </summary>
public interface IDepartmentRepository
{
    /// <summary>
    /// 部門一覧取得
    /// </summary>
    Task<IEnumerable<Department>> SelectAllAsync();

    /// <summary>
    /// 部門ID検索
    /// </summary>
    Task<Department?> SelectByIdAsync(int id);

    /// <summary>
    /// 部門登録
    /// </summary>
    Task InsertAsync(Department department);

    /// <summary>
    /// 部門更新
    /// </summary>
    Task UpdateAsync(Department department);

    /// <summary>
    /// 部門削除
    /// </summary>
    Task DeleteAsync(int id);

    /// <summary>
    /// 部門名検索
    /// </summary>
    Task<Department?> SelectByDeptNameAsync(string deptName);

    /// <summary>
    /// 部門に所属する社員数取得
    /// </summary>
    Task<int> CountEmployeesAsync(int departmentId);  
}