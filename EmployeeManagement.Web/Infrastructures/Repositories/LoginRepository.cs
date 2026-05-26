using Dapper;
using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Infrastructures.Context;

namespace EmployeeManagement.Web.Infrastructures.Repositories;

/// <summary>
/// ログインRepository
/// </summary>
public class LoginRepository : ILoginRepository
{
    private readonly DapperContext _context;

    public LoginRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<LoginUser?> LoginAsync(
        string emailAddress,
        string password)
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
        return await connection.QueryFirstOrDefaultAsync<LoginUser>(
            sql,
            new
            {
                EmailAddress = emailAddress,
                Password = password
            });
    }
}