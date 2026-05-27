namespace EmployeeManagement.Web.Exceptions;

/// <summary>
/// ドメインルール違反例外
/// </summary>
public class DomainException : Exception
{
    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DomainException(string message)
        : base(message)
    {
    }

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public DomainException(
        string message,
        Exception innerException)
        : base(message, innerException)
    {
    }
}