using System.ComponentModel.DataAnnotations;
using EmployeeManagement.Web.Applications.Domains;

namespace EmployeeManagement.Web.Presentations.ViewModels;

/// <summary>
/// 社員フォームViewModel
/// </summary>
public class EmployeeFormViewModel
{
    /// <summary>
    /// ID
    /// </summary>
    public int Id { get; set; }

    /// <summary>
    /// 部門ID
    /// </summary>
    [Required(ErrorMessage = "部門を選択してください。")]
    public int DepartmentId { get; set; }

    /// <summary>
    /// 社員番号
    /// </summary>
    [Required(ErrorMessage = "社員番号を入力してください。")]
    [StringLength(
        10,
        ErrorMessage = "社員番号は10文字以内で入力してください。")]
    public string EmployeeNo { get; set; } = string.Empty;

    /// <summary>
    /// 氏名
    /// </summary>
    [Required(ErrorMessage = "氏名を入力してください。")]
    [StringLength(
        50,
        ErrorMessage = "氏名は50文字以内で入力してください。")]
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// 氏名かな
    /// </summary>
    [Required(ErrorMessage = "氏名かなを入力してください。")]
    [StringLength(
        50,
        ErrorMessage = "氏名かなは50文字以内で入力してください。")]
    public string NameKana { get; set; } = string.Empty;

    /// <summary>
    /// メールアドレス
    /// </summary>
    [Required(ErrorMessage = "メールアドレスを入力してください。")]
    [EmailAddress(ErrorMessage = "メールアドレス形式で入力してください。")]
    [StringLength(
        100,
        ErrorMessage = "メールアドレスは100文字以内で入力してください。")]
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// 生年月日
    /// </summary>
    [Required(ErrorMessage = "生年月日を入力してください。")]
    public DateTime Birthday { get; set; }

    /// <summary>
    /// 性別
    /// </summary>
    [Required(ErrorMessage = "性別を選択してください。")]
    public int Gender { get; set; }

    /// <summary>
    /// 部門一覧
    /// </summary>
    public IEnumerable<Department> Departments
    {
        get;
        set;
    } = new List<Department>();
}