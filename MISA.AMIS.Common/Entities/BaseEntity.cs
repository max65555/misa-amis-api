using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common
{
    public class BaseEntity
    {
        /// <summary>
        /// who created this row
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// when created this row
        /// </summary>
        public string? CreatedDate { get; set; }

        /// <summary>
        /// who update this row
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// when update this row
        /// </summary>
        public string? ModifiedDate { get; set; }

    }
}
