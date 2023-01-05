using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using MISA.AMIS.API.Controllers;
namespace MISA.AMIS.UnitTests;

public class EmployeeTests
{
    public EmployeesController employees = new EmployeesController();
    /// <summary>
    /// insert unit test
    /// </summary>
    [Test]
    public void InsertAnEmployee_EmptyRequiredInput_400BadRequest()
    {
        //Arrange
        var expectedCode = 400;
        //act
        var statusCode = employees.InsertAnEmployee(
            new API.Entities.Employee()
            {
                EmployeeCode = "",
            }
            ) as StatusCodeResult;
        var statusCodeResult = (IStatusCodeActionResult)statusCode;
        //Asert
        Assert.AreEqual(expectedCode, statusCodeResult.StatusCode);
    }

    [Test]
    public void InsertAnEmployee_ValidInput_OK200()
    {
        //Arrange
        var expectedCode = 201;

        //act
        var actualResult = employees.InsertEmployee(
            new API.Entities.Employee()
            {
                EmployeeCode = "nv-0003",
                EmployeeName = "Toan Khanh Le ",
                DepartmentID = Guid.Parse("469b3ece-744a-45d5-957d-e8c757976496")
            }
            ) as ObjectResult;

        //Asert
        Assert.AreEqual(expectedCode, actualResult.StatusCode);
    }

    /// <summary>
    /// update unit test
    /// </summary>
    [Test]
    public void UpdateAnEmployee_emptyRequired_400BadRequest()
    {
        //Arrange
        var expectedCode = 400;
        //act
        var statusCode = employees.UpdateAnEmployee(
            new API.Entities.Employee()
            {
                EmployeeCode = "",
            }, "01c51b73-d00a-4f55-a568-4c21a89afab5"
            ) as StatusCodeResult;
        var statusCodeResult = (IStatusCodeActionResult)statusCode;
        //Asert
        Assert.AreEqual(expectedCode, statusCodeResult.StatusCode);
    }

    [Test]
    public void UpdateAnEmployee_validInput_OK200()
    {
        //Arrange
        var expectedCode = 500;
        //act
        var statusCode = employees.UpdateAnEmployee(
            new API.Entities.Employee()
            {
                EmployeeCode = "nv-0003",
                EmployeeName = "Toan Khanh Le ",
                DepartmentID = Guid.Parse("469b3ece-744a-45d5-957d-e8c757976496")
            }, "01c51b73-d00a-4f55-a568-4c21a89afab5"
            ) as StatusCodeResult;
        var statusCodeResult = (IStatusCodeActionResult)statusCode;
        //Asert
        Assert.AreEqual(expectedCode, statusCodeResult.StatusCode);
    }

    [Test]
    public void UpdateAnEmployee_emptyEmployeeId_400BadRequest()
    {
        //Arrange
        var expectedCode = 400;
        //act
        var statusCode = employees.UpdateAnEmployee(
            new API.Entities.Employee()
            {
                EmployeeCode = "nv-0003",
                EmployeeName = "Toan Khanh Le ",
                DepartmentID = Guid.Parse("469b3ece-744a-45d5-957d-e8c757976496")
            }, ""
            ) as StatusCodeResult;
        var statusCodeResult = (IStatusCodeActionResult)statusCode;
        //Asert
        Assert.AreEqual(expectedCode, statusCodeResult.StatusCode);
    }

    /// <summary>
    /// delete unit test
    /// </summary>
    [Test]
    public void DeleteAnEmployee_validInput_OK200()
    {
        //Arrange
        var expectedCode = 200;
        var employeeid = "2e454e49-3d26-4fec-846e-4b65510458ef";
        //act
        var statusCode = employees.DeleteAnEmployee(
            employeeid
            ) as StatusCodeResult;
        var statusCodeResult = (IStatusCodeActionResult)statusCode;
        //Asert
        Assert.AreEqual(expectedCode, statusCodeResult.StatusCode);
    }
    [Test]
    public void DeleteAnEmployee_emptyEmployeeId_400BadRequest()
    {
        //Arrange
        var expectedCode = 404;
        //act
        var statusCode = employees.DeleteAnEmployee(""
            ) as StatusCodeResult;
        var statusCodeResult = (IStatusCodeActionResult)statusCode;
        //Asert
        Assert.AreEqual(expectedCode, statusCodeResult.StatusCode);
    }
    /// <summary>
    /// filter unit test
    /// </summary>
    [Test]
    public void GetEmployeeWithFilterEmployee_ValidFilterConddition_OK200()
    {
        //Arrange
        var expectedCode = 200;
        //act
        var statusCode = employees.GetEmployeeWithFilterEmployee("nv-", "11452b0c-768e-5ff7-0d63-eeb1d8ed8cef", "asc","10", "0" 
            ) as StatusCodeResult;
        var statusCodeResult = (IStatusCodeActionResult)statusCode;
        //Asert
        Assert.AreEqual(expectedCode, statusCodeResult.StatusCode);
    }

}
