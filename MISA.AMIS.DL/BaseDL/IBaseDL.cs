using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// insert a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be inserted into database</param>
        /// <returns>recordID's of added record</returns>
        public Guid? InsertRecord(T record);

        /// <summary>
        /// update a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be updated into database</param>
        /// <returns>recordID's of changed record</returns>
        public Guid? UpdateRecord(Guid recordID,T record);

        /// <summary>
        /// delete a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="recordId">record'id is going to be deleted</param>
        /// <returns>is success</returns>
        public bool DeleteRecordById(Guid recordId);

        /// <summary>
        /// read all Records from database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <returns>List of record</returns>
        public List<T>? ReadRecords();
        /// <summary>
        /// read with filter
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <returns>List of filtered record</returns>
        public Tuple<string,List<T>>? ReadFilteredRecords(string? keyword,string? sort,string? limit, string? offset);

    }
}
