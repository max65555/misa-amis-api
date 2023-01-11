using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.BL;
using MISA.AMIS.Common.Entities;

namespace MISA.AMIS.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        #region Fields

        public IDepartmentBL _departmentBL { get; set; }

        #endregion

        #region Constructor
        public DepartmentsController(IDepartmentBL departmentBL) : base(departmentBL)
        {
            this._departmentBL = departmentBL;
        } 
        #endregion
    }
}
