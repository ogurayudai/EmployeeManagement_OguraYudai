using Dapper;
using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Exceptions;
using EmployeeManagement.Web.Infrastructures.Adapters;
using EmployeeManagement.Web.Infrastructures.Context;
using EmployeeManagement.Web.Infrastructures.Entitys;

namespace EmployeeManagement.Web.Infrastructures.Repositories;

/// <summary>
/// ログインRepository
/// </summary>
public class LoginRepository : ILoginRepository
{
    private readonly DapperContext _context;
    private readonly LoginEntityAdapter _loginEntityAdapter;

    public LoginRepository(
        DapperContext context,
        LoginEntityAdapter loginEntityAdapter)
    {
        _context = context;
        _loginEntityAdapter = loginEntityAdapter;
    }

    public async Task<LoginUser?> LoginAsync(
        string emailAddress,
        string password)
    {
        try
        {
            const string sql = @"
                SELECT
                    l.id AS Id,
                    l.email_address AS EmailAddress,
                    l.employee_id AS EmployeeId,
                    l.login_password AS LoginPassword,
                    e.name AS EmployeeName
                FROM login l
                INNER JOIN employee e
                    ON l.employee_id = e.id
                WHERE l.email_address = @EmailAddress
                  AND l.login_password = @Password;
            ";

            using var connection = _context.CreateConnection();

            var loginEntity =
                await connection.QueryFirstOrDefaultAsync<LoginEntity>(
                    sql,
                    new
                    {
                        EmailAddress = emailAddress,
                        Password = password
                    });

            if (loginEntity is null)
            {
                return null;
            }

            return _loginEntityAdapter.Restore(loginEntity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "ログイン情報を取得できませんでした。",
                e);
        }
    }
}