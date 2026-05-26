using Dapper;
using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Infrastructures.Context;

namespace EmployeeManagement.Web.Infrastructures.Repositories;

/// <summary>
/// 社員Repository
/// </summary>
public class EmployeeRepository : IEmployeeRepository
{
    private readonly DapperContext _context;

    /// <summary>
    /// コンストラクタ
    /// </summary>
    public EmployeeRepository(DapperContext context)
    {
        _context = context;
    }

    /// <summary>
    /// 社員一覧取得
    /// </summary>
    public async Task<IEnumerable<Employee>> SelectAllAsync()
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

        return await connection.QueryAsync<Employee>(sql);
    }

    /// <summary>
    /// 社員ID検索
    /// </summary>
    public async Task<Employee?> SelectByIdAsync(int id)
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

        return await connection.QueryFirstOrDefaultAsync<Employee>(
            sql,
            new { Id = id });
    }

    /// <summary>
    /// 社員番号検索
    /// </summary>
    public async Task<Employee?> SelectByEmployeeNoAsync(string employeeNo)
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

        return await connection.QueryFirstOrDefaultAsync<Employee>(
            sql,
            new { EmployeeNo = employeeNo });
    }

    /// <summary>
    /// 社員登録
    /// </summary>
    public async Task InsertAsync(Employee employee)
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

        await connection.ExecuteAsync(sql, employee);
    }

    /// <summary>
    /// 社員更新
    /// </summary>
    public async Task UpdateAsync(Employee employee)
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

        await connection.ExecuteAsync(sql, employee);
    }

    /// <summary>
    /// 社員削除
    /// </summary>
    public async Task DeleteAsync(int id)
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

    /// <summary>
    /// 社員検索
    /// </summary>
    public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
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

        return await connection.QueryAsync<Employee>(
            sql,
            new { Keyword = $"%{keyword}%" });
    }

    /// <summary>
    /// メールアドレス検索
    /// </summary>
    public async Task<Employee?> SelectByEmailAddressAsync(string emailAddress)
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

        return await connection.QueryFirstOrDefaultAsync<Employee>(
            sql,
            new { EmailAddress = emailAddress });
    }
}