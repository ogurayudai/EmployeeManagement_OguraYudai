namespace EmployeeManagement.Web.Infrastructures.Entitys;

/// <summary>
/// ログインEntity
/// </summary>
public class LoginEntity
{
    public int Id { get; set; }

    public string EmailAddress { get; set; } = string.Empty;

    public int EmployeeId { get; set; }

    public string LoginPassword { get; set; } = string.Empty;

    public string EmployeeName { get; set; } = string.Empty;
}