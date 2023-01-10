using MISA.AMIS.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL { 
    public interface IEmployeeBL : IBaseBL<Employee>
    {
        /// <summary>
        /// get recently employee's Code has been added 
        /// Author:toanlk
        /// </summary>
        /// <returns>newest Employee Code</returns>
        public string GetNewestEmployeeCode();
    }
}
