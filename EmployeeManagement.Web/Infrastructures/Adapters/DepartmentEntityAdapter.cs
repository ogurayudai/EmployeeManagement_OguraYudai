using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Infrastructures.Entitys;

namespace EmployeeManagement.Web.Infrastructures.Adapters;

/// <summary>
/// 部門Entity変換Adapter
/// </summary>
public class DepartmentEntityAdapter
{
    /// <summary>
    /// EntityからDomainを復元する
    /// </summary>
    public Department Restore(DepartmentEntity entity)
    {
        return new Department
        {
            Id = entity.Id,
            DeptName = entity.DeptName
        };
    }

    /// <summary>
    /// DomainをEntityへ変換する
    /// </summary>
    public DepartmentEntity Convert(Department domain)
    {
        return new DepartmentEntity
        {
            Id = domain.Id,
            DeptName = domain.DeptName
        };
    }
}