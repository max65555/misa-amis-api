using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.Common;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System.Data;
using System.Text.RegularExpressions;
using MISA.AMIS.Common.Entities;
using MISA.AMIS.BL;

namespace MISA.AMIS.API.Controllers
{
    //[Route("api/v1/[controller]")]
    //[ApiController]
    public class EmployeesController : BasesController<Employee>
    {


        #region Fields

        private IEmployeeBL _employeeBL;

        #endregion
        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion
        [HttpGet("newest-employee-code")]
        
        public IActionResult getNewestEmployeeCode()
        {
            try
            {
                string? newestCode = _employeeBL.GetNewestEmployeeCode();
                if (newestCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, newestCode);
                }
                else
                {
                    return StatusCode(StatusCodes.Status404NotFound,new ErrorResponse()
                    {
                        ErrorCode = ErrorCode.NotFound,
                        DevMsg = "không tìm thấy mã nhân viên mới nhất",
                        UserMsg = "Không tìm thấy mã mới nhất! Vui lòng nhập mã nhân viên!",
                        MoreInfo = "//",
                        TracedID = HttpContext.TraceIdentifier
                    });
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse()
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = "Đã xảy ra lỗi ở phía server",
                    UserMsg = "Thêm mới thất bại! Xin vui lòng thử lại",
                    MoreInfo = "//",
                    TracedID = HttpContext.TraceIdentifier
                });
            }
        }
        ///// <summary>
        ///// author: toanlk
        ///// description: insert an new employee 
        ///// modifiedBy: 
        ///// createdBy: toanlk 26/12/2022
        ///// </summary>
        ///// <param name="employee">the employee</param>
        ///// <returns>notify of action </returns>
        //[HttpPost]
        //public IActionResult InsertEmployee([FromBody] Employee employee)
        //{
        //    try
        //    {

        //        if (employee.EmployeeId.ToString() != ""
        //            && employee.EmployeeCode != ""
        //            && employee.DepartmentID.ToString() != "")
        //        {

        //            //chuẩn bị tên store procedure
        //            string storedProcedure = "Proc_employee_Insert";

        //            //chẩn bị tham số đầu vào
        //            var parammeters = new DynamicParameters();
        //            var employeeID = Guid.NewGuid();
        //            parammeters.Add("@employeeID", employeeID);
        //            parammeters.Add("@employeeName", employee.EmployeeName);
        //            parammeters.Add("@employeeCode", employee.EmployeeCode);
        //            parammeters.Add("@departmentID", employee.DepartmentID);
        //            parammeters.Add("@jobPositionID", employee.JobPositionID);
        //            parammeters.Add("@dateOfBirth", employee.DateOfBirth);
        //            parammeters.Add("@gender", employee.Gender);
        //            parammeters.Add("@identityNumber", employee.IdentityNumber);
        //            parammeters.Add("@identityIssueDate", employee.IdentityIssueDate);
        //            parammeters.Add("@identityIssuePlace", employee.IdentityIssuePlace);
        //            parammeters.Add("@address", employee.Address);
        //            parammeters.Add("@phoneNumber", employee.PhoneNumber);
        //            parammeters.Add("@email", employee.Email);
        //            parammeters.Add("@landlinePhoneNumber", employee.LandLineNumber);
        //            parammeters.Add("@bankAccountNumber", employee.BankAccountNumber);
        //            parammeters.Add("@bankName", employee.BankName);
        //            parammeters.Add("@bankBranch", employee.BankBranch);

        //            // khởi tạo kết nối tới DB mysql
        //            string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
        //            var mysqlConnection = new MySqlConnection(connectionString);

        //            // gọi vào DB để chạy stored ở trên
        //            var numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
        //            mysqlConnection.Close();
        //            //xử lý kết quả trả về
        //            if (numberOfChanges > 0)
        //            {
        //                return StatusCode(201, employeeID);
        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
        //                {
        //                    ErrorCode = ErrorCode.InsertFailed,
        //                    DevMsg = "Gọi Procedure insert lỗi từ database",
        //                    UserMsg = "Thêm mới thất bại, vui lòng thử lại",
        //                    MoreInfo = "//",
        //                    TracedID = HttpContext.TraceIdentifier
        //                });
        //            }
        //            //thành công    
        //            //thất bại
        //            //try catch
        //            //return 

        //            return StatusCode(200);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest
        //            //    , new ErrorResponse
        //            //{
        //            //    ErrorCode = ErrorCode.InsertFailed,
        //            //    DevMsg = "dữ liệu đầu vào không hợp lệ",
        //            //    UserMsg = "giá trị vừa nhập không hợp lệ, vui lòng nhập lại",
        //            //    MoreInfo = "//",
        //            //    TracedID = HttpContext.TraceIdentifier
        //            //}
        //                );
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return StatusCode(StatusCodes.Status400BadRequest
        //        //     , new ErrorResponse
        //        //{
        //        //     ErrorCode = ErrorCode.Exception,
        //        //     DevMsg = "có ngoại lệ xả ra",
        //        //     UserMsg = "có lỗi xảy ra, xin hãy thử lại sau",
        //        //     MoreInfo = "//",
        //        //     TracedID = HttpContext.TraceIdentifier
        //        // }
        //        );
        //    }
        //}

        ///// <summary>
        /////  update infomations of an employee by their id
        /////  author: toanlk (2/1/2022)
        /////  modifiedBy:
        ///// </summary>
        ///// <param name="employee">new information of employee after change</param>
        ///// <param name="employeeID">the employeeID is going to be changed</param>
        ///// <returns>the employeeID of the employee has to be changed</returns>
        //[HttpPut("{employeeID}")]
        //public IActionResult UpdateAnEmployee([FromBody] Employee employee, [FromRoute] string employeeID)
        //{
        //    try
        //    {
        //        if (employeeID.ToString() != ""
        //             && employee.EmployeeCode != ""
        //             && employee.DepartmentID.ToString() != "")
        //        {
        //            //chuẩn bị tên store procedure
        //            string storedProcedure = "Proc_employee_Update";
        //            //chẩn bị tham số đầu vào
        //            var parammeters = new DynamicParameters();
        //            parammeters.Add("@employeeID", employeeID);
        //            parammeters.Add("@employeeName", employee.EmployeeName);
        //            parammeters.Add("@employeeCode", employee.EmployeeCode);
        //            parammeters.Add("@departmentID", employee.DepartmentID);
        //            parammeters.Add("@jobPositionID", employee.JobPositionID);
        //            parammeters.Add("@dateOfBirth", employee.DateOfBirth);
        //            parammeters.Add("@gender", employee.Gender);
        //            parammeters.Add("@identityNumber", employee.IdentityNumber);
        //            parammeters.Add("@identityIssueDate", employee.IdentityIssueDate);
        //            parammeters.Add("@identityIssuePlace", employee.IdentityIssuePlace);
        //            parammeters.Add("@address", employee.Address);
        //            parammeters.Add("@phoneNumber", employee.PhoneNumber);
        //            parammeters.Add("@email", employee.Email);
        //            parammeters.Add("@landlinePhoneNumber", employee.LandLineNumber);
        //            parammeters.Add("@bankAccountNumber", employee.BankAccountNumber);
        //            parammeters.Add("@bankName", employee.BankName);
        //            parammeters.Add("@bankBranch", employee.BankBranch);
        //            // khởi tạo kết nối tới DB mysql
        //            string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
        //            var mysqlConnection = new MySqlConnection(connectionString);
        //            // gọi vào DB để chạy stored ở trên
        //            var numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
        //            //xử lý kết quả trả về
        //            if (numberOfChanges > 0)
        //            {
        //                //return StatusCode(numberOfChanges, employeeID);
        //                return StatusCode(500);
        //            }
        //            else
        //            {
        //                return StatusCode(StatusCodes.Status500InternalServerError);
        //                //    , new ErrorResponse
        //                //{
        //                //    ErrorCode = ErrorCode.UpdateFailed,
        //                //    DevMsg = "Gọi Procedure update lỗi từ database",
        //                //    UserMsg = "chỉnh sửa thông tin thất bại, vui lòng thử lại",
        //                //    MoreInfo = "//",
        //                //    TracedID = HttpContext.TraceIdentifier
        //                //});
        //            }
        //            //thành công    
        //            //thất bại
        //            //try catch
        //            //return 
        //            mysqlConnection.Close();
        //            return StatusCode(400);
        //        }
        //        else
        //        {
        //            return StatusCode(StatusCodes.Status400BadRequest);
        //            //    , new ErrorResponse
        //            //{
        //            //    ErrorCode = ErrorCode.UpdateFailed,
        //            //    DevMsg = "dữ liệu đầu vào không hợp lệ",
        //            //    UserMsg = "giá trị vừa nhập không hợp lệ, vui lòng nhập lại",
        //            //    MoreInfo = "//",
        //            //    TracedID = HttpContext.TraceIdentifier
        //            //});
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return StatusCode(StatusCodes.Status400BadRequest);
        //        //    , new ErrorResponse
        //        //{
        //        //    ErrorCode = ErrorCode.Exception,
        //        //    DevMsg = "có ngoại lệ xả ra",
        //        //    UserMsg = "có lỗi xảy ra, xin hãy thử lại sau",
        //        //    MoreInfo = "//",
        //        //    TracedID = HttpContext.TraceIdentifier
        //        //});
        //    }
        //    return StatusCode(StatusCodes.Status400BadRequest);

        //}

        ///// <summary>
        /////  delete employee by their id
        /////  author: toanlk (2/1/2022)
        /////  modifiedBy:
        ///// </summary>
        ///// <param name="employeeID">the employeeID is going to be delete</param>
        ///// <returns>the employeeID of the employee has to be deleted</returns>
        //[HttpDelete("{employeeID}")]
        //public IActionResult DeleteAnEmployee([FromRoute] string employeeID)
        //{
        //    try
        //    {
        //        //chuẩn bị tên store procedure
        //        string storedProcedure = "Proc_employee_Delete";
        //        //chẩn bị tham số đầu vào
        //        var parammeters = new DynamicParameters();
        //        parammeters.Add("@employeeID", employeeID);
        //        // khởi tạo kết nối tới DB mysql
        //        string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
        //        var mysqlConnection = new MySqlConnection(connectionString);
        //        // gọi vào DB để chạy stored ở trên
        //        var numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
        //        //xử lý kết quả trả về
        //        if (numberOfChanges > 0)
        //        {
        //            return Ok(200);
        //        }
        //        else
        //        {
        //            //return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
        //            //{
        //            //    ErrorCode = ErrorCode.UpdateFailed,
        //            //    DevMsg = "Gọi Procedure delete lỗi từ database",
        //            //    UserMsg = "xóa nhân viên thất bại thất bại, vui lòng thử lại",
        //            //    MoreInfo = "//",
        //            //    TracedID = HttpContext.TraceIdentifier
        //            //});
        //            return StatusCode(StatusCodes.Status500InternalServerError);
        //        }
        //        //thành công    
        //        //thất bại
        //        //try catch
        //        //return 
        //        mysqlConnection.Close();
        //    }
        //    catch (Exception e)
        //    {
        //        //Console.WriteLine(e);
        //        //return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
        //        //{
        //        //    ErrorCode = ErrorCode.Exception,
        //        //    DevMsg = "có ngoại lệ xả ra",
        //        //    UserMsg = "có lỗi xảy ra, xin hãy thử lại sau",
        //        //    MoreInfo = "//",
        //        //    TracedID = HttpContext.TraceIdentifier
        //        //});
        //        return StatusCode(StatusCodes.Status500InternalServerError);

        //    }
        //}

        ///// <summary>
        /////  get all employees
        /////  author: toanlk (2/1/2022)
        /////  modifiedBy:
        ///// </summary>
        ///// <returns>json of all employee</returns>
        //[HttpGet]
        //public IActionResult GetAllEmployees()
        //{
        //    try
        //    {
        //        //chuẩn bị tên store procedure
        //        string storedProcedure = "Proc_employee_read";
        //        //chẩn bị tham số đầu vào
        //        // khởi tạo kết nối tới DB mysql
        //        string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
        //        var mysqlConnection = new MySqlConnection(connectionString);
        //        // gọi vào DB để chạy stored ở trên
        //        var employees = mysqlConnection.Query(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
        //        //xử lý kết quả trả về
        //        return Ok(employees);
        //        mysqlConnection.Close();
        //        return StatusCode(500);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
        //        {
        //            ErrorCode = ErrorCode.Exception,
        //            DevMsg = "có ngoại lệ xả ra",
        //            UserMsg = "có lỗi xảy ra, xin hãy thử lại sau",
        //            MoreInfo = "//",
        //            TracedID = HttpContext.TraceIdentifier
        //        });
        //    }
        //}

        ///// <summary>
        /////  get employee with filter
        /////  author: toanlk (2/1/2022)
        /////  modifiedBy:
        ///// </summary>
        ///// <returns>json of all employee</returns>
        //[HttpGet("filter")]
        //public IActionResult GetEmployeeWithFilterEmployee([FromQuery] string keyword, [FromQuery] string departmentID, [FromQuery] string sort, [FromQuery] string limit, [FromQuery] string offset)
        //{
        //    try
        //    {
        //        //chuẩn bị tên store procedure
        //        string storedProcedure = "Proc_employee_filter";

        //        //chẩn bị tham số đầu vào
        //        var parammeters = new DynamicParameters();
        //        parammeters.Add("@keyword", keyword);
        //        parammeters.Add("@departmentID", departmentID);
        //        parammeters.Add("@sort", sort);
        //        parammeters.Add("@limit", limit);
        //        parammeters.Add("@offset", offset);

        //        // khởi tạo kết nối tới DB mysql
        //        string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
        //        var mysqlConnection = new MySqlConnection(connectionString);
        //        var parammeters2 = new DynamicParameters();
        //        parammeters2.Add("@keyword", keyword);
        //        parammeters2.Add("@departmentID", departmentID);
        //        parammeters2.Add("@sort", sort);
        //        parammeters2.Add("@limit", " ");
        //        parammeters2.Add("@offset", " ");
        //        // gọi vào DB để chạy stored ở trên
        //        var employees = mysqlConnection.QueryMultiple(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
        //        var employeeCount = mysqlConnection.Query(storedProcedure, parammeters2, commandType: CommandType.StoredProcedure).ToList().Count;
        //        mysqlConnection.Close();
        //        //xử lý kết quả trả về
        //        //return Ok( new { employees, employeeCount })
        //        return StatusCode(200);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        //return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
        //        //{
        //        //    ErrorCode = ErrorCode.Exception,
        //        //    DevMsg = "Có ngoại lệ xảy ra",
        //        //    UserMsg = "Có lỗi xảy ra, xin hãy thử lại sau!",
        //        //    MoreInfo = "//",
        //        //    TracedID = HttpContext.TraceIdentifier
        //        //});
        //        return StatusCode(StatusCodes.Status500InternalServerError);
        //    }
        //}
        ///// <summary>
        /////  get the newest employeeCode into database
        /////  author: toanlk (2/1/2022)
        /////  modifiedBy:
        ///// </summary>
        ///// <returns>newest employeeCode</returns>
        //[HttpGet("newest-employee-code")]
        //public IActionResult GetNewestEmployeeCode()
        //{
        //    try
        //    {
        //        //chuẩn bị tên store procedure
        //        string storedProcedure = "Proc_employee_newestCode";
        //        // khởi tạo kết nối tới DB mysql
        //        string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
        //        var mysqlConnection = new MySqlConnection(connectionString);

        //        // gọi vào DB để chạy stored ở trên
        //        var newestEmployeeCode = mysqlConnection.QuerySingle<string>(storedProcedure, commandType: CommandType.StoredProcedure);
        //        mysqlConnection.Close();
        //        //tăng nhân viên lên 
        //        int employeeCodeWithOutNV = Int32.Parse(newestEmployeeCode.Replace("nv-", ""));
        //        Console.WriteLine("employeeCode without nv" + employeeCodeWithOutNV);
        //        double inscremeCode = Decimal.ToDouble(employeeCodeWithOutNV + 1);
        //        string stringCount = (inscremeCode / 1000) + "";

        //        int zeroCount = Regex.Matches(stringCount, "0").Count;
        //        string returnString = "nv-";
        //        for (int i = 0; i < zeroCount; i++)
        //        {
        //            returnString += "0";
        //        }
        //        returnString += inscremeCode;
        //        //xử lý kết quả trả về
        //        //TODO: changed this
        //        return Ok(returnString);
        //    }
        //    catch (Exception e)
        //    {
        //        Console.WriteLine(e);
        //        return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResponse
        //        {
        //            ErrorCode = ErrorCode.Exception,
        //            DevMsg = "Có ngoại lệ xảy ra",
        //            UserMsg = "Có lỗi xảy ra, xin hãy thử lại sau!",
        //            MoreInfo = "//",
        //            TracedID = HttpContext.TraceIdentifier
        //        });
        //    }
        //}

    }

}
