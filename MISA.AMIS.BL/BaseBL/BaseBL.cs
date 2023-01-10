using MISA.AMIS.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.BL
{
    
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL) 
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Methods

        /// <summary>
        /// insert a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be inserted into database</param>
        /// <returns>recordID's of added record</returns>
        public Guid? InsertRecord(T record)
        {
            return _baseDL.InsertRecord(record);
        }

        /// <summary>
        /// update a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="record">record is going to be updated into database</param>
        /// <returns>recordID's of changed record</returns>
        public Guid? UpdateRecord(Guid recordID, T record)
        {
           return _baseDL.UpdateRecord(recordID, record);  
        }

        /// <summary>
        /// delete a record into database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <param name="recordId">record'id is going to be deleted</param>
        /// <returns>is success</returns>
        public bool DeleteRecordById(Guid recordId)
        {
            return _baseDL.DeleteRecordById(recordId);
        }

        /// <summary>
        /// read all Records from database
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <returns>List of record</returns>
        public List<T>? ReadRecords()
        {
            return _baseDL.ReadRecords();
        }

        /// <summary>
        /// read with filter
        /// Author: toanlk (9/1/2023)
        /// </summary>
        /// <returns>List of filtered record</returns>
        public Tuple<string, List<T>>? ReadFilteredRecords(string? keyword,string? sort, string? limit, string? offset)
        {
            return _baseDL.ReadFilteredRecords(keyword, sort, limit, offset);
        }

        #endregion
    }
}

