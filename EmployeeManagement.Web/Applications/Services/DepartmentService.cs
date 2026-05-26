using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;

namespace EmployeeManagement.Web.Applications.Services;

/// <summary>
/// 部門サービス
/// </summary>
public class DepartmentService
{
    private readonly IDepartmentRepository _departmentRepository;

    public DepartmentService(IDepartmentRepository departmentRepository)
    {
        _departmentRepository = departmentRepository;
    }

    public async Task<IEnumerable<Department>> GetAllAsync()
    {
        return await _departmentRepository.SelectAllAsync();
    }

    public async Task<Department?> GetByIdAsync(int id)
    {
        return await _departmentRepository.SelectByIdAsync(id);
    }

    public async Task<bool> RegisterAsync(Department department)
    {
        var existsDepartment =
            await _departmentRepository.SelectByDeptNameAsync(department.DeptName);

        if (existsDepartment is not null)
        {
            return false;
        }

        await _departmentRepository.InsertAsync(department);

        return true;
    }

    public async Task<bool> UpdateAsync(Department department)
    {
        var existsDepartment =
            await _departmentRepository.SelectByDeptNameAsync(department.DeptName);

        if (existsDepartment is not null &&
            existsDepartment.Id != department.Id)
        {
            return false;
        }

        await _departmentRepository.UpdateAsync(department);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var employeeCount =
            await _departmentRepository.CountEmployeesAsync(id);

        if (employeeCount > 0)
        {
            return false;
        }

        await _departmentRepository.DeleteAsync(id);

        return true;
    }
}