using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using Microsoft.AspNetCore.Http;
using MISA.AMIS.Common.Entities;
using System.Security.Cryptography;
using System.Reflection;
using System.Linq;

namespace MISA.AMIS.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
       
        /// <summary>
        /// insert a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be inserted into database</param>
        /// <returns>recordID's of added record</returns>
        public Guid? InsertRecord(T record)
        {
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                var ObjectName = typeof(T).Name;
                //chuẩn bị tên store procedure
                string storedProcedure = $"Proc_{ObjectName}_Insert";

                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();

                var recordID = Guid.NewGuid();
                foreach (PropertyInfo property in properties)
                {
                    if(property.Name == $"{ObjectName}ID")
                    {
                        parammeters.Add($"@{ObjectName}ID", recordID);
                    }
                    else
                    {
                        parammeters.Add($"@{property.Name}", property.GetValue(record, null));
                    }
                }
                //// khởi tạo kết nối tới DB mysql
                string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
                var mysqlConnection = new MySqlConnection(connectionString);
                //// gọi vào DB để chạy stored ở trên
                var numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                if(numberOfChanges > 0)
                {
                    return recordID;
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }
        /// <summary>
        /// update a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be updated into database</param>
        /// <returns>recordID's of changed record</returns>
        public Guid? UpdateRecord(Guid recordID,T record)
        {
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                var ObjectName = typeof(T).Name;
                //chuẩn bị tên store procedure
                string storedProcedure = $"Proc_{ObjectName}_Update";

                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();
                foreach (PropertyInfo property in properties)
                {
                    if (property.Name == $"{ObjectName}ID")
                    {
                        parammeters.Add($"@{ObjectName}ID", recordID);
                    }
                    else
                    {
                        parammeters.Add($"@{property.Name}", property.GetValue(record, null));
                    }
                }
                //// khởi tạo kết nối tới DB mysql
                string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
                var mysqlConnection = new MySqlConnection(connectionString);
                //// gọi vào DB để chạy stored ở trên
                var numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                if (numberOfChanges > 0)
                {
                    return recordID;
                }
                else
                {
                    return null;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return null;
            }
        }

        /// <summary>
        /// delete a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="recordId">record'id is going to be deleted</param>
        /// <returns>is success</returns>
        public bool DeleteRecordById(Guid recordId)
        {
            try
            {
                var currentGenericTypeName = typeof(T).Name;
                string storedProcedure = $"Proc_{currentGenericTypeName}_Delete";
                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();
                parammeters.Add($"@{currentGenericTypeName}ID", recordId);
                // khởi tạo kết nối tới DB mysql
                string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
                var mysqlConnection = new MySqlConnection(connectionString);
                // gọi vào DB để chạy stored ở trên
                var numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                //xử lý kết quả trả về
                if (numberOfChanges > 0)
                {
                    //return SttusCod
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                return false;
            }
            
        }


        /// <summary>
        /// read all Records from database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <returns>List of record</returns>
        public List<T>? ReadRecords()
        {
            try
            {
                //chuẩn bị tên store procedure
                string storedProcedure = $"Proc_{typeof(T).Name}_read";
                //chẩn bị tham số đầu vào
                // khởi tạo kết nối tới DB mysql
                string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
                var mysqlConnection = new MySqlConnection(connectionString);
                // gọi vào DB để chạy stored ở trên
                List<T> records = mysqlConnection.Query<T>(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
                //xử lý kết quả trả về
                return records;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
        /// <summary>
        /// read with filter
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <returns>List of filtered record</returns>
        public Tuple<string,List<T>>? ReadFilteredRecords(string? keyword, string? sort,string? limit, string? offset)
        {
            try
            {
                //chuẩn bị tên store procedure
                string storedProcedure = $"Proc_{typeof(T).Name}_filter";
                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();
                parammeters.Add("@keyword", keyword);
                parammeters.Add("@sort", sort);
                parammeters.Add("@limit", limit);
                parammeters.Add("@offset", offset);

                // khởi tạo kết nối tới DB mysql
                string connectionString = "Server=localhost;Port=3306;Database=misa.web11.toanlk;Uid=root;Pwd=Lekhanhtoan183461;";
                var mysqlConnection = new MySqlConnection(connectionString);
                // gọi vào DB để chạy stored ở trên
               var MultipleRecordResult = mysqlConnection.QueryMultiple(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                var recordsResult = MultipleRecordResult.Read<T>().ToList();
                var totalRecord = MultipleRecordResult.Read<int>().Single().ToString();
                Console.WriteLine(totalRecord);
                return Tuple.Create(totalRecord, recordsResult);    
                //return ;
                //xử lý kết quả trả về
                //return ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }
    }
}
