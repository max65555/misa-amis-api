
namespace MISA.AMIS.Common.Entities
{
    public class ErrorResponse
    {
        /// <summary>
        /// Error code with specific meaning
        /// </summary>
        public ErrorCode ErrorCode { get; set; }

        /// <summary>
        /// message for developer
        /// </summary>
        public string DevMsg { get; set; }

        /// <summary>
        /// message for user
        /// </summary>
        public string UserMsg { get; set; }

        /// <summary>
        /// description about getting this error
        /// </summary>
        public string MoreInfo { get; set; }

        /// <summary>
        /// Trace ID
        /// </summary>
        public string TracedID { get; set; }

    }
}
