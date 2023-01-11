using Dapper;
using Microsoft.AspNetCore.Mvc;
using MySqlConnector;
using System.Data;
using Microsoft.AspNetCore.Http;
using MISA.AMIS.Common;
using System.Security.Cryptography;
using System.Reflection;
using System.Linq;
using System.Security.AccessControl;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;

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
        public object? InsertRecord(T record)
        {
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                var ObjectName = typeof(T).Name;
                //chuẩn bị tên store procedure
                string storedProcedure = String.Format(StoreProcedureName.PROCEDURE_NAME_INSERT, ObjectName);

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
                int numberOfChanges;
                using (var mysqlConnection = new MySqlConnection(ConnectionString.MYSQL_CONNECTION_STRING))
                { 
                    numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                }
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
        public Object? UpdateRecord(Guid recordID, T record)
        {
            try
            {
                PropertyInfo[] properties = typeof(T).GetProperties();
                var ObjectName = typeof(T).Name;
                //chuẩn bị tên store procedure
                string storedProcedure = String.Format(StoreProcedureName.PROCEDURE_NAME_UPDATE, ObjectName);

                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();
                List<dynamic> errorMessages = new List<dynamic>();
                foreach (PropertyInfo property in properties)
                {
                    // required field validate
                    RequiredAttribute? requireAttribute = 
                        (RequiredAttribute?)property.GetCustomAttribute(typeof(RequiredAttribute), false);
                    if (requireAttribute != null && property.GetValue(record,null) == null)
                    {
                        errorMessages.Add(new
                        {
                            ErrorMessage = requireAttribute.ErrorMessage,
                            ErrorField = property.Name
                        }
                        );
                    }
                    if (requireAttribute != null  && string.IsNullOrEmpty(property.GetValue(record)?.ToString()))
                    {
                        errorMessages.Add(new
                        {
                            ErrorMessage = requireAttribute.ErrorMessage,
                            ErrorField = property.Name
                        }
                        );
                    }
                    //add value into procedure
                    if (property.Name == $"{ObjectName}ID")
                    {
                        parammeters.Add($"@{ObjectName}ID", recordID);
                    }
                    else
                    {
                        parammeters.Add($"@{property.Name}", property.GetValue(record, null));
                    }
                }
                if(errorMessages.Count > 0)
                {
                    return errorMessages;
                }
                int numberOfChanges;
                using (var mysqlConnection = new MySqlConnection(ConnectionString.MYSQL_CONNECTION_STRING))
                {
                    numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                }
                //// gọi vào DB để chạy stored ở trên
                
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
                string storedProcedure = String.Format(StoreProcedureName.PROCEDURE_NAME_DELETE,currentGenericTypeName);
                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();
                parammeters.Add($"@{currentGenericTypeName}ID", recordId);
                // khởi tạo kết nối tới DB 
                int numberOfChanges;
                using (var mysqlConnection = new MySqlConnection(ConnectionString.MYSQL_CONNECTION_STRING))
                {
                    numberOfChanges = mysqlConnection.Execute(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                }
                // gọi vào DB để chạy stored ở trên
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
                string storedProcedure = String.Format(StoreProcedureName.PROCEDURE_NAME_READ, typeof(T).Name);
                //chẩn bị tham số đầu vào
                // khởi tạo kết nối tới DB mysql
                List<T> records;
                using (var mysqlConnection = new MySqlConnection(ConnectionString.MYSQL_CONNECTION_STRING))
                {
                    records = mysqlConnection.Query<T>(storedProcedure, commandType: CommandType.StoredProcedure).ToList();
                }
                // gọi vào DB để chạy stored ở trên
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
                string storedProcedure = String.Format(StoreProcedureName.PROCEDURE_NAME_FILTER,typeof(T).Name);
                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();
                parammeters.Add("@keyword", keyword);
                parammeters.Add("@sort", sort);
                parammeters.Add("@limit", limit);
                parammeters.Add("@offset", offset);

                // khởi tạo kết nối tới DB mysql
                SqlMapper.GridReader? MultipleRecordResult;
                using (var mysqlConnection = new MySqlConnection(ConnectionString.MYSQL_CONNECTION_STRING))
                {
                    MultipleRecordResult = mysqlConnection.QueryMultiple(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                    // gọi vào DB để chạy stored ở trên
                    var recordsResult = MultipleRecordResult.Read<T>().ToList();
                    var totalRecord = MultipleRecordResult.Read<int>().Single().ToString();
                    return Tuple.Create(totalRecord, recordsResult);
                }
                return null;
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
        /// <summary>
        /// get record by its id
        /// Author: toanlk (9/1/2022)
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns>instance of T object</returns>
        public T? ReadByID(Guid recordID)
        {
            try
            {
                //chuẩn bị tên store procedure
                string storedProcedure = String.Format(StoreProcedureName.PROCEDURE_NAME_READ_BY_ID, typeof(T).Name);
                //chẩn bị tham số đầu vào
                var parammeters = new DynamicParameters();
                parammeters.Add($"@{typeof(T).Name}ID", recordID);
                // khởi tạo kết nối tới DB mysql
                T result;
                using (var mysqlConnection = new MySqlConnection(ConnectionString.MYSQL_CONNECTION_STRING))
                {
                    result = mysqlConnection.QuerySingle<T>(storedProcedure, parammeters, commandType: CommandType.StoredProcedure);
                    // gọi vào DB để chạy stored ở trên
                }
                return result;
                //return ;
                //xử lý kết quả trả về
                //return ;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return default;
            }
        }

        
    }
}
