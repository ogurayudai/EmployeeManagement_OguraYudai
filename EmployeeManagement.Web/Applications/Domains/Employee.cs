namespace EmployeeManagement.Web.Applications.Domains;

/// <summary>
/// 社員ドメイン
/// </summary>
public class Employee
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 部門ID
    /// </summary>
    public int DepartmentId { get; set; }

    /// <summary>
    /// 社員番号
    /// </summary>
    public string EmployeeNo { get; set; } = string.Empty;

    /// <summary>
    /// 氏名
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 氏名かな
    /// </summary>
    public string NameKana { get; set; } = string.Empty;

    /// <summary>
    /// メールアドレス
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// 生年月日
    /// </summary>
    public DateTime Birthday { get; set; }

    /// <summary>
    /// 性別
    /// </summary>
    public int Gender { get; set; }

    /// <summary>
    /// 部門名
    /// </summary>
    public string DepartmentName { get; set; } = string.Empty;
}