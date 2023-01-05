using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace MISA.AMIS.API.Entities.DTO
{
    public class StatusCodeResponse : IActionResult
    {
        public StatusCodeResult statusCodes { get; set; }
        public ErrorCode errorCode { get; set; }
        public StatusCodeResponse(StatusCodeResult statusCodeResult, ErrorCode errorCode)
        {
            this.statusCodes = statusCodeResult;
            this.errorCode = errorCode;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            throw new NotImplementedException();
        }
    }
}
