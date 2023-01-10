using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion
        

        #region Methods   

        /// <summary>
        /// insert a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be inserted into database</param>
        /// <returns>recordID's of added record</returns>
        [HttpPost()]
        public IActionResult InsertRecord([FromBody] T record)
        {
            try
            {
                Guid? recordID = _baseBL.InsertRecord(record);
                if (recordID != null)
                {
                    return StatusCode(StatusCodes.Status200OK, recordID);
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ErrorResponse
                    {

                        ErrorCode = ErrorCode.InsertFailed,
                        DevMsg = "Đầu vào không hợp lệ",
                        UserMsg = "Xin vui lòng nhập lại",
                        MoreInfo = "//",
                        TracedID = HttpContext.TraceIdentifier
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse
                    {

                        ErrorCode = ErrorCode.Exception,
                        DevMsg = "Đã xảy ra lỗi ở phía server",
                        UserMsg = "Thêm mới thất bại! Xin vui lòng thử lại",
                        MoreInfo = "//",
                        TracedID = HttpContext.TraceIdentifier
                    });
            }
        }

        /// <summary>
        /// update a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be updated into database</param>
        /// <returns>recordID's of changed record</returns>
        [HttpPut("{recordID}")]
        public IActionResult UpdateRecord([FromRoute] Guid recordID,[FromBody] T record)
        {
            try
            {
                Guid? UpdateRecordID = _baseBL.UpdateRecord(recordID,record);
                if (UpdateRecordID == null)
                {
                    return StatusCode(StatusCodes.Status200OK, recordID);
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ErrorResponse
                    {

                        ErrorCode = ErrorCode.UpdateFailed,
                        DevMsg = "Đầu vào không hợp lệ",
                        UserMsg = "Xin vui lòng nhập lại",
                        MoreInfo = "//",
                        TracedID = HttpContext.TraceIdentifier
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse
                    { 
                        ErrorCode = ErrorCode.Exception,
                        DevMsg = "Đã xảy ra lỗi ở phía server",
                        UserMsg = "Thê mới thất bại! Xin vui lòng thử lại",
                        MoreInfo = "//",
                        TracedID = HttpContext.TraceIdentifier
                    });
            }
        }

        /// <summary>
        /// delete a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="recordId">record'id is going to be deleted</param>
        /// <returns>is success</returns>
        [HttpDelete("{recordId}")]
        public IActionResult DeleteRecordById([FromRoute] Guid recordId)
        {
            try { 
                var hasDeleteYet = _baseBL.DeleteRecordById(recordId);
                if (hasDeleteYet)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }
                return StatusCode(StatusCodes.Status400BadRequest,
                    new ErrorResponse
                    {

                        ErrorCode = ErrorCode.DeleteFailed,
                        DevMsg = "Xảy ra lỗi với dữ liệu xóa",
                        UserMsg = $"Không tìm thấy {typeof(T).Name}ID để xóa!",
                        MoreInfo = "//",
                        TracedID = HttpContext.TraceIdentifier
                    });
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new ErrorResponse
                    {

                        ErrorCode = ErrorCode.Exception,
                        DevMsg = "Đã xảy ra lỗi ở phía server",
                        UserMsg = "Hệ thống có trục trặc! Xin vui lòng thử lại sau",
                        MoreInfo = "//",
                        TracedID = HttpContext.TraceIdentifier
                    });
            }
        }

        /// <summary>
        /// read with filter
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <returns>List of filtered record</returns>
        [HttpGet("filter")]
        public IActionResult ReadFilteredRecords([FromQuery] string? keyword, [FromQuery] string? sort,[FromQuery] string? limit, [FromQuery] string? offset)
        {
            var multipleResult = _baseBL.ReadFilteredRecords(keyword, sort, limit, offset);
            //return StatusCode(StatusCodes.Status200OK)
            return StatusCode(StatusCodes.Status200OK, new { data = multipleResult.Item1 , total = multipleResult.Item2});
        }
        #endregion

    }
}
