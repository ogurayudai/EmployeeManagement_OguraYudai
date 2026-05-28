using EmployeeManagement.Web.Applications.Domains;
using EmployeeManagement.Web.Exceptions;

namespace EmployeeManagement.Test.Applications;

[TestClass]
public class EmployeeTest
{
    [TestMethod]
    public void Validate_正常な社員情報なら例外が発生しない()
    {
        var employee = new Employee
        {
            EmployeeNo = "200801",
            NameKana = "やまだたろう",
            Birthday = new DateTime(2000, 1, 1)
        };

        employee.Validate();
    }

    [TestMethod]
    public void Validate_社員番号に英字が含まれる場合DomainExceptionが発生する()
    {
        var employee = new Employee
        {
            EmployeeNo = "A001",
            NameKana = "やまだたろう",
            Birthday = new DateTime(2000, 1, 1)
        };

        try
        {
            employee.Validate();
            Assert.Fail();
        }
        catch (DomainException)
        {
        }
    }

    [TestMethod]
    public void Validate_氏名かなに漢字が含まれる場合DomainExceptionが発生する()
    {
        var employee = new Employee
        {
            EmployeeNo = "200801",
            NameKana = "山田たろう",
            Birthday = new DateTime(2000, 1, 1)
        };

        try
        {
            employee.Validate();
            Assert.Fail();
        }
        catch (DomainException)
        {
        }
    }

    [TestMethod]
    public void Validate_生年月日が未来日の場合DomainExceptionが発生する()
    {
        var employee = new Employee
        {
            EmployeeNo = "200801",
            NameKana = "やまだたろう",
            Birthday = DateTime.Now.AddDays(1)
        };

        try
        {
            employee.Validate();
            Assert.Fail();
        }
        catch (DomainException)
        {
        }
    }
}