namespace EmployeeManagement.Web.Applications.Domains;

/// <summary>
/// ログインユーザー
/// </summary>
public class LoginUser
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// メールアドレス
    /// </summary>
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// 社員ID
    /// </summary>
    public int EmployeeId { get; set; }

    /// <summary>
    /// パスワード
    /// </summary>
    public string LoginPassword { get; set; } = string.Empty;

    /// <summary>
    /// 社員名
    /// </summary>
    public string EmployeeName { get; set; } = string.Empty;
}