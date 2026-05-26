namespace EmployeeManagement.Web.Applications.Domains;

/// <summary>
/// 部門ドメイン
/// </summary>
public class Department
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 部門名
    /// </summary>
    public string DeptName { get; set; } = string.Empty;
}