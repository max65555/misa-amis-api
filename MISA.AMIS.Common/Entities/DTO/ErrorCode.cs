namespace MISA.AMIS.Common.Entities
{
    public enum ErrorCode
    {
        /// <summary>
        /// got expection in try catch 
        /// </summary>
        Exception = 0,

        /// <summary>
        /// insert into datatbase has been failed
        /// </summary>x
        InsertFailed = 1,

        /// <summary>
        /// updating an record failed
        /// </summary>
        UpdateFailed = 2,

        /// <summary>
        /// deleting an record failed
        /// </summary>
        DeleteFailed = 3,

        /// <summary>
        /// could get data from database
        /// </summary>
        GetDataFailed = 4,

        /// <summary>
        /// input value is invalid
        /// </summary>
        InvalidInput = 5,
        /// <summary>
        /// not found 
        /// </summary>
        NotFound = 6

    }
}
