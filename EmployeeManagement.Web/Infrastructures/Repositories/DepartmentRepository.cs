using Dapper;
using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Infrastructures.Context;

namespace EmployeeManagement.Web.Infrastructures.Repositories;

/// <summary>
/// 部門Repository
/// </summary>
public class DepartmentRepository : IDepartmentRepository
{
    private readonly DapperContext _context;

    public DepartmentRepository(DapperContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Department>> SelectAllAsync()
    {
        const string sql = @"
            SELECT
                id AS Id,
                dept_name AS DeptName
            FROM department
            ORDER BY id;
        ";

        using var connection = _context.CreateConnection();
        return await connection.QueryAsync<Department>(sql);
    }

    public async Task<Department?> SelectByIdAsync(int id)
    {
        const string sql = @"
            SELECT
                id AS Id,
                dept_name AS DeptName
            FROM department
            WHERE id = @Id;
        ";

        using var connection = _context.CreateConnection();
        return await connection.QueryFirstOrDefaultAsync<Department>(
            sql,
            new { Id = id });
    }

    public async Task InsertAsync(Department department)
    {
        const string sql = @"
            INSERT INTO department
            (
                dept_name
            )
            VALUES
            (
                @DeptName
            );
        ";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, department);
    }

    public async Task UpdateAsync(Department department)
    {
        const string sql = @"
            UPDATE department
            SET
                dept_name = @DeptName
            WHERE id = @Id;
        ";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, department);
    }

    public async Task DeleteAsync(int id)
    {
        const string sql = @"
            DELETE FROM department
            WHERE id = @Id;
        ";

        using var connection = _context.CreateConnection();
        await connection.ExecuteAsync(sql, new { Id = id });
    }


    /// <summary>
    /// 部門名検索
    /// </summary>
    public async Task<Department?> SelectByDeptNameAsync(string deptName)
    {
        const string sql = @"
            SELECT
                id AS Id,
                dept_name AS DeptName
            FROM department
            WHERE dept_name = @DeptName;
        ";

        using var connection = _context.CreateConnection();

        return await connection.QueryFirstOrDefaultAsync<Department>(
            sql,
            new { DeptName = deptName });
    }

    /// <summary>
    /// 部門に所属する社員数取得
    /// </summary>
    public async Task<int> CountEmployeesAsync(int departmentId)
    {
        const string sql = @"
            SELECT
                COUNT(*)
            FROM employee
            WHERE department_id = @DepartmentId;
        ";

        using var connection = _context.CreateConnection();

        return await connection.ExecuteScalarAsync<int>(
            sql,
            new { DepartmentId = departmentId });
    }

}