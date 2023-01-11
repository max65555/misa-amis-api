using MISA.AMIS.Common.Entities;
using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {

        #region Fields

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        } 

        #endregion


        #region Methods
        /// <summary>
        /// get recently employee's Code has been added 
        /// Author:toanlk
        /// </summary>
        /// <returns>newest Employee Code</returns>
        public string GetNewestEmployeeCode()
        {

            return _employeeDL.GetNewestEmployeeCode();
        } 

        #endregion

    }
}
