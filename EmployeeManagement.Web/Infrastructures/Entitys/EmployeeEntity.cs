namespace EmployeeManagement.Web.Infrastructures.Entitys;

/// <summary>
/// 社員Entity
/// </summary>
public class EmployeeEntity
{
    public int Id { get; set; }

    public int DepartmentId { get; set; }

    public string EmployeeNo { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string NameKana { get; set; } = string.Empty;

    public string EmailAddress { get; set; } = string.Empty;

    public DateTime Birthday { get; set; }

    public int Gender { get; set; }

    public string DepartmentName { get; set; } = string.Empty;
}