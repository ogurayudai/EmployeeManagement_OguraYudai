using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Infrastructures.Entitys;

namespace EmployeeManagement.Web.Infrastructures.Adapters;

/// <summary>
/// 社員Entity変換Adapter
/// </summary>
public class EmployeeEntityAdapter
{
    /// <summary>
    /// EntityからDomainを復元する
    /// </summary>
    public Employee Restore(EmployeeEntity entity)
    {
        return new Employee
        {
            Id = entity.Id,
            DepartmentId = entity.DepartmentId,
            EmployeeNo = entity.EmployeeNo,
            Name = entity.Name,
            NameKana = entity.NameKana,
            EmailAddress = entity.EmailAddress,
            Birthday = entity.Birthday,
            Gender = entity.Gender,
            DepartmentName = entity.DepartmentName
        };
    }

    /// <summary>
    /// DomainをEntityへ変換する
    /// </summary>
    public EmployeeEntity Convert(Employee domain)
    {
        return new EmployeeEntity
        {
            Id = domain.Id,
            DepartmentId = domain.DepartmentId,
            EmployeeNo = domain.EmployeeNo,
            Name = domain.Name,
            NameKana = domain.NameKana,
            EmailAddress = domain.EmailAddress,
            Birthday = domain.Birthday,
            Gender = domain.Gender,
            DepartmentName = domain.DepartmentName
        };
    }
}