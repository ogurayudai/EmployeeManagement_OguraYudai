using Dapper;
using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Exceptions;
using EmployeeManagement.Web.Infrastructures.Adapters;
using EmployeeManagement.Web.Infrastructures.Context;
using EmployeeManagement.Web.Infrastructures.Entitys;

namespace EmployeeManagement.Web.Infrastructures.Repositories;

/// <summary>
/// 社員Repository
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    private readonly DapperContext _context;
    private readonly EmployeeEntityAdapter _employeeEntityAdapter;

    public EmployeeRepository(
        DapperContext context,
        EmployeeEntityAdapter employeeEntityAdapter)
    {
        _context = context;
        _employeeEntityAdapter = employeeEntityAdapter;
    }

    public async Task<IEnumerable<Employee>> SelectAllAsync()
    {
        try
        {
            const string sql = @"
                SELECT
                    e.id AS Id,
                    e.department_id AS DepartmentId,
                    e.employee_no AS EmployeeNo,
                    e.name AS Name,
                    e.name_kana AS NameKana,
                    e.email_address AS EmailAddress,
                    e.birthday::timestamp AS Birthday,
                    e.gender AS Gender,
                    d.dept_name AS DepartmentName
                FROM employee e
                INNER JOIN department d
                    ON e.department_id = d.id
                ORDER BY e.employee_no;
            ";

            using var connection = _context.CreateConnection();

            var entities =
                await connection.QueryAsync<EmployeeEntity>(sql);

            return entities.Select(entity =>
                _employeeEntityAdapter.Restore(entity));
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員一覧を取得できませんでした。",
                e);
        }
    }

    public async Task<Employee?> SelectByIdAsync(int id)
    {
        try
        {
            const string sql = @"
                SELECT
                    e.id AS Id,
                    e.department_id AS DepartmentId,
                    e.employee_no AS EmployeeNo,
                    e.name AS Name,
                    e.name_kana AS NameKana,
                    e.email_address AS EmailAddress,
                    e.birthday::timestamp AS Birthday,
                    e.gender AS Gender,
                    d.dept_name AS DepartmentName
                FROM employee e
                INNER JOIN department d
                    ON e.department_id = d.id
                WHERE e.id = @Id;
            ";

            using var connection = _context.CreateConnection();

            var entity =
                await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(
                    sql,
                    new { Id = id });

            if (entity is null)
            {
                return null;
            }

            return _employeeEntityAdapter.Restore(entity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員情報を取得できませんでした。",
                e);
        }
    }

    public async Task<Employee?> SelectByEmployeeNoAsync(string employeeNo)
    {
        try
        {
            const string sql = @"
                SELECT
                    e.id AS Id,
                    e.department_id AS DepartmentId,
                    e.employee_no AS EmployeeNo,
                    e.name AS Name,
                    e.name_kana AS NameKana,
                    e.email_address AS EmailAddress,
                    e.birthday::timestamp AS Birthday,
                    e.gender AS Gender,
                    d.dept_name AS DepartmentName
                FROM employee e
                INNER JOIN department d
                    ON e.department_id = d.id
                WHERE e.employee_no = @EmployeeNo;
            ";

            using var connection = _context.CreateConnection();

            var entity =
                await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(
                    sql,
                    new { EmployeeNo = employeeNo });

            if (entity is null)
            {
                return null;
            }

            return _employeeEntityAdapter.Restore(entity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員番号に一致する社員情報を取得できませんでした。",
                e);
        }
    }

    public async Task<Employee?> SelectByEmailAddressAsync(string emailAddress)
    {
        try
        {
            const string sql = @"
                SELECT
                    e.id AS Id,
                    e.department_id AS DepartmentId,
                    e.employee_no AS EmployeeNo,
                    e.name AS Name,
                    e.name_kana AS NameKana,
                    e.email_address AS EmailAddress,
                    e.birthday::timestamp AS Birthday,
                    e.gender AS Gender,
                    d.dept_name AS DepartmentName
                FROM employee e
                INNER JOIN department d
                    ON e.department_id = d.id
                WHERE e.email_address = @EmailAddress;
            ";

            using var connection = _context.CreateConnection();

            var entity =
                await connection.QueryFirstOrDefaultAsync<EmployeeEntity>(
                    sql,
                    new { EmailAddress = emailAddress });

            if (entity is null)
            {
                return null;
            }

            return _employeeEntityAdapter.Restore(entity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "メールアドレスに一致する社員情報を取得できませんでした。",
                e);
        }
    }

    public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
    {
        try
        {
            const string sql = @"
                SELECT
                    e.id AS Id,
                    e.department_id AS DepartmentId,
                    e.employee_no AS EmployeeNo,
                    e.name AS Name,
                    e.name_kana AS NameKana,
                    e.email_address AS EmailAddress,
                    e.birthday::timestamp AS Birthday,
                    e.gender AS Gender,
                    d.dept_name AS DepartmentName
                FROM employee e
                INNER JOIN department d
                    ON e.department_id = d.id
                WHERE e.employee_no LIKE @Keyword
                   OR e.name LIKE @Keyword
                   OR e.name_kana LIKE @Keyword
                   OR e.email_address LIKE @Keyword
                   OR d.dept_name LIKE @Keyword
                ORDER BY e.employee_no;
            ";

            using var connection = _context.CreateConnection();

            var entities =
                await connection.QueryAsync<EmployeeEntity>(
                    sql,
                    new { Keyword = $"%{keyword}%" });

            return entities.Select(entity =>
                _employeeEntityAdapter.Restore(entity));
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員情報を検索できませんでした。",
                e);
        }
    }

    public async Task InsertAsync(Employee employee)
    {
        try
        {
            const string sql = @"
                INSERT INTO employee
                (
                    department_id,
                    employee_no,
                    name,
                    name_kana,
                    email_address,
                    birthday,
                    gender
                )
                VALUES
                (
                    @DepartmentId,
                    @EmployeeNo,
                    @Name,
                    @NameKana,
                    @EmailAddress,
                    @Birthday,
                    @Gender
                );
            ";

            using var connection = _context.CreateConnection();

            var entity =
                _employeeEntityAdapter.Convert(employee);

            await connection.ExecuteAsync(sql, entity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員情報を登録できませんでした。",
                e);
        }
    }

    public async Task UpdateAsync(Employee employee)
    {
        try
        {
            const string sql = @"
                UPDATE employee
                SET
                    department_id = @DepartmentId,
                    employee_no = @EmployeeNo,
                    name = @Name,
                    name_kana = @NameKana,
                    email_address = @EmailAddress,
                    birthday = @Birthday,
                    gender = @Gender
                WHERE id = @Id;
            ";

            using var connection = _context.CreateConnection();

            var entity =
                _employeeEntityAdapter.Convert(employee);

            await connection.ExecuteAsync(sql, entity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員情報を更新できませんでした。",
                e);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            const string sql = @"
                DELETE FROM employee
                WHERE id = @Id;
            ";

            using var connection = _context.CreateConnection();

            await connection.ExecuteAsync(
                sql,
                new { Id = id });
        }
        catch (Exception e)
        {
            throw new InternalException(
                "社員情報を削除できませんでした。",
                e);
        }
    }
}