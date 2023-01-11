using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.Common.Entities
{
    public class Department : BaseEntity
    {
        /// <summary>
        /// ID phòng ban
        /// </summary>
        [Required(ErrorMessage ="Có vấn đề phía server khi sinh mã mới")]
        public Guid? DepartmentID { get; set; }

        /// <summary>
        /// mã phòng ban
        /// </summary>
        /// 
        [Required(ErrorMessage ="Mã phòng ban không được để trống")]
        public string DepartmentCode { get; set; }

        /// <summary>
        /// mã phòng ban
        /// </summary>
        [Required(ErrorMessage ="Tên phòng ban không được để trống")]
        public string DepartmentName { get; set; }

        /// <summary>
        /// cấp tổ chức
        /// </summary>
        public string LevelOrganization{ get; set; }

        /// <summary>
        /// Trạng thái phòng ban
        /// </summary>
        public string DepartmentStatus { get; set; }

    }
}
