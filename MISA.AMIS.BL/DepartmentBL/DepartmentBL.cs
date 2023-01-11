using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class DepartmentBL : BaseBL<Department>, IDepartmentBL
    {
        #region Fields

        public IDepartmentDL _departmentDL { get; set; }

        #endregion

        #region Constructor

        public DepartmentBL(IDepartmentDL deparmentDL) : base(deparmentDL)
        {
            this._departmentDL = deparmentDL;
        } 
        #endregion
    }
}
