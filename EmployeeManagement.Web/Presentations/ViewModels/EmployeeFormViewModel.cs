using EmployeeManagement.Web.Applications.Domains;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Presentations.ViewModels;

/// <summary>
/// 社員登録・更新画面ViewModel
/// </summary>
public class EmployeeFormViewModel
{
    public int Id { get; set; }

    [Required(ErrorMessage = "部門を選択してください。")]
    public int DepartmentId { get; set; }

    [Required(ErrorMessage = "社員番号を入力してください。")]
    public string EmployeeNo { get; set; } = string.Empty;

    [Required(ErrorMessage = "氏名を入力してください。")]
    public string Name { get; set; } = string.Empty;

    [Required(ErrorMessage = "氏名かなを入力してください。")]
    public string NameKana { get; set; } = string.Empty;

    [Required(ErrorMessage = "メールアドレスを入力してください。")]
    [EmailAddress(ErrorMessage = "メールアドレス形式で入力してください。")]
    public string EmailAddress { get; set; } = string.Empty;

    [Required(ErrorMessage = "生年月日を入力してください。")]
    public DateTime Birthday { get; set; } = DateTime.Today;

    [Required(ErrorMessage = "性別を選択してください。")]
    public int Gender { get; set; }

    public IEnumerable<Department> Departments { get; set; } =
        new List<Department>();
}