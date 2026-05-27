using Dapper;
using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;
using EmployeeManagement.Web.Exceptions;
using EmployeeManagement.Web.Infrastructures.Adapters;
using EmployeeManagement.Web.Infrastructures.Context;
using EmployeeManagement.Web.Infrastructures.Entitys;

namespace EmployeeManagement.Web.Infrastructures.Repositories;

/// <summary>
/// 部門Repository
/// </summary>
public class DepartmentRepository : IDepartmentRepository
{
    private readonly DapperContext _context;
    private readonly DepartmentEntityAdapter _departmentEntityAdapter;

    public DepartmentRepository(
        DapperContext context,
        DepartmentEntityAdapter departmentEntityAdapter)
    {
        _context = context;
        _departmentEntityAdapter = departmentEntityAdapter;
    }

    public async Task<IEnumerable<Department>> SelectAllAsync()
    {
        try
        {
            const string sql = @"
                SELECT
                    id AS Id,
                    dept_name AS DeptName
                FROM department
                ORDER BY id;
            ";

            using var connection = _context.CreateConnection();

            var departmentEntities =
                await connection.QueryAsync<DepartmentEntity>(sql);

            return departmentEntities
                .Select(entity =>
                    _departmentEntityAdapter.Restore(entity));
        }
        catch (Exception e)
        {
            throw new InternalException(
                "部門一覧を取得できませんでした。",
                e);
        }
    }

    public async Task<Department?> SelectByIdAsync(int id)
    {
        try
        {
            const string sql = @"
                SELECT
                    id AS Id,
                    dept_name AS DeptName
                FROM department
                WHERE id = @Id;
            ";

            using var connection = _context.CreateConnection();

            var departmentEntity =
                await connection.QueryFirstOrDefaultAsync<DepartmentEntity>(
                    sql,
                    new { Id = id });

            if (departmentEntity is null)
            {
                return null;
            }

            return _departmentEntityAdapter.Restore(departmentEntity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "部門情報を取得できませんでした。",
                e);
        }
    }

    public async Task<Department?> SelectByDeptNameAsync(string deptName)
    {
        try
        {
            const string sql = @"
                SELECT
                    id AS Id,
                    dept_name AS DeptName
                FROM department
                WHERE dept_name = @DeptName;
            ";

            using var connection = _context.CreateConnection();

            var departmentEntity =
                await connection.QueryFirstOrDefaultAsync<DepartmentEntity>(
                    sql,
                    new { DeptName = deptName });

            if (departmentEntity is null)
            {
                return null;
            }

            return _departmentEntityAdapter.Restore(departmentEntity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "部門名に一致する部門情報を取得できませんでした。",
                e);
        }
    }

    public async Task InsertAsync(Department department)
    {
        try
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

            var departmentEntity =
                _departmentEntityAdapter.Convert(department);

            await connection.ExecuteAsync(sql, departmentEntity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "部門情報を登録できませんでした。",
                e);
        }
    }

    public async Task UpdateAsync(Department department)
    {
        try
        {
            const string sql = @"
                UPDATE department
                SET
                    dept_name = @DeptName
                WHERE id = @Id;
            ";

            using var connection = _context.CreateConnection();

            var departmentEntity =
                _departmentEntityAdapter.Convert(department);

            await connection.ExecuteAsync(sql, departmentEntity);
        }
        catch (Exception e)
        {
            throw new InternalException(
                "部門情報を更新できませんでした。",
                e);
        }
    }

    public async Task DeleteAsync(int id)
    {
        try
        {
            const string sql = @"
                DELETE FROM department
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
                "部門情報を削除できませんでした。",
                e);
        }
    }

    public async Task<int> CountEmployeesAsync(int departmentId)
    {
        try
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
        catch (Exception e)
        {
            throw new InternalException(
                "部門に所属する社員数を取得できませんでした。",
                e);
        }
    }
}