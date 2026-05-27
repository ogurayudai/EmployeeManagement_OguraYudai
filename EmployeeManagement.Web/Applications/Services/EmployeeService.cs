using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Applications.Repositories;

namespace EmployeeManagement.Web.Applications.Services;

/// <summary>
/// 社員サービス
/// </summary>
public class EmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;

    public EmployeeService(IEmployeeRepository employeeRepository)
    {
        _employeeRepository = employeeRepository;
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _employeeRepository.SelectAllAsync();
    }

    public async Task<IEnumerable<Employee>> SearchAsync(string keyword)
    {
        if (string.IsNullOrWhiteSpace(keyword))
        {
            return await _employeeRepository.SelectAllAsync();
        }

        return await _employeeRepository.SearchAsync(keyword);
    }

    public async Task<Employee?> GetByIdAsync(int id)
    {
        return await _employeeRepository.SelectByIdAsync(id);
    }

    public async Task<Employee?> GetByEmployeeNoAsync(string employeeNo)
    {
        return await _employeeRepository.SelectByEmployeeNoAsync(employeeNo);
    }

    public async Task<string?> RegisterAsync(Employee employee)
    {
        var existsEmployeeNo =
            await _employeeRepository.SelectByEmployeeNoAsync(employee.EmployeeNo);

        if (existsEmployeeNo is not null)
        {
            return "入力された社員番号は既に使用されています。";
        }

        var existsEmail =
            await _employeeRepository.SelectByEmailAddressAsync(employee.EmailAddress);

        if (existsEmail is not null)
        {
            return "入力されたメールアドレスは既に使用されています。";
        }

        employee.Validate();

        await _employeeRepository.InsertAsync(employee);

        return null;
    }

    public async Task<string?> UpdateAsync(Employee employee)
    {
        var existsEmployeeNo =
            await _employeeRepository.SelectByEmployeeNoAsync(employee.EmployeeNo);

        if (existsEmployeeNo is not null &&
            existsEmployeeNo.Id != employee.Id)
        {
            return "入力された社員番号は既に使用されています。";
        }

        var existsEmail =
            await _employeeRepository.SelectByEmailAddressAsync(employee.EmailAddress);

        if (existsEmail is not null &&
            existsEmail.Id != employee.Id)
        {
            return "入力されたメールアドレスは既に使用されています。";
        }

        employee.Validate();

        await _employeeRepository.UpdateAsync(employee);

        return null;
    }

    public async Task DeleteAsync(int id)
    {
        await _employeeRepository.DeleteAsync(id);
    }
}