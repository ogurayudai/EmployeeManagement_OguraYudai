using EmployeeManagement.Web.Presentations.Controllers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Test.Controllers;

[TestClass]
public class HomeControllerTest
{
    [TestMethod]
    public void Index_ログイン済みの場合Viewを返す()
    {
        var controller =
            new HomeController();

        var session =
            new FakeSession();

        session.SetString(
            "LoginEmployeeName",
            "山田太郎");

        controller.ControllerContext =
            new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    Session = session
                }
            };

        var result =
            controller.Index();

        Assert.IsInstanceOfType(
            result,
            typeof(ViewResult));
    }

    [TestMethod]
    public void Index_未ログインの場合Loginへリダイレクトする()
    {
        var controller =
            new HomeController();

        controller.ControllerContext =
            new ControllerContext
            {
                HttpContext = new DefaultHttpContext
                {
                    Session = new FakeSession()
                }
            };

        var result =
            controller.Index();

        Assert.IsInstanceOfType(
            result,
            typeof(RedirectToActionResult));

        var redirectResult =
            (RedirectToActionResult)result;

        Assert.AreEqual(
            "Index",
            redirectResult.ActionName);

        Assert.AreEqual(
            "Login",
            redirectResult.ControllerName);
    }
}