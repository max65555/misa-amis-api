using Dapper;
using MISA.AMIS.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public class EmployeeDL : BaseDL<Employee>,IEmployeeDL
    {
        /// <summary>
        /// get recently employee's Code has been added 
        /// Author:toanlk
        /// </summary>
        /// <returns>newest Employee Code</returns>
        public string? GetNewestEmployeeCode()
        {
            try
            {
                //chuẩn bị tên store procedure
                string storedProcedure = "Proc_employee_newestCode";
                // khởi tạo kết nối tới DB mysql
                string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
                var mysqlConnection = new MySqlConnection(connectionString);
                // gọi vào DB để chạy stored ở trên
                var newestEmployeeCode = mysqlConnection.QuerySingle<string>(storedProcedure, commandType: CommandType.StoredProcedure);
                mysqlConnection.Close();
                //tăng nhân viên lên 
                int employeeCodeWithOutNV = Int32.Parse(newestEmployeeCode.Replace("nv-", ""));
                Console.WriteLine("employeeCode without nv" + employeeCodeWithOutNV);
                double inscremeCode = Decimal.ToDouble(employeeCodeWithOutNV + 1);
                string stringCount = (inscremeCode / 1000) + "";

                int zeroCount = Regex.Matches(stringCount, "0").Count;
                string returnString = "nv-";
                for (int i = 0; i < zeroCount; i++)
                {
                    returnString += "0";
                }
                returnString += inscremeCode;
                //xử lý kết quả trả về
                return returnString;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

    }
}
