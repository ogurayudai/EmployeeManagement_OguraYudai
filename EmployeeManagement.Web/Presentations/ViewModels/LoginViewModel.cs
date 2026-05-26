using System.ComponentModel.DataAnnotations;

namespace EmployeeManagement.Web.Presentations.ViewModels;

/// <summary>
/// ログイン画面ViewModel
/// </summary>
public class LoginViewModel
{
    /// <summary>
    /// メールアドレス
    /// </summary>
    [Required(ErrorMessage = "ログインIDを入力してください。")]
    [EmailAddress(ErrorMessage = "メールアドレス形式で入力してください。")]
    public string EmailAddress { get; set; } = string.Empty;

    /// <summary>
    /// パスワード
    /// </summary>
    [Required(ErrorMessage = "パスワードを入力してください。")]
    public string Password { get; set; } = string.Empty;

    /// <summary>
    /// エラーメッセージ
    /// </summary>
    public string ErrorMessage { get; set; } = string.Empty;
}