using System.ComponentModel.DataAnnotations;

namespace MISA.AMIS.Common.Entities
{
    public class Employee
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>

        [Required]
        public Guid EmployeeID { get; set; }
        /// <summary>
        /// Mã nhân viên
        /// </summary>

        [Required]
        public string EmployeeCode { get; set; }
        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string? EmployeeName { get; set; }
        /// <summary>
        /// mã phòng ban
        /// </summary>
        [Required]
        public Guid DepartmentID { get; set; }
        /// <summary>
        /// mã công việc
        /// </summary>
        public Guid? JobPositionID { get; set; }
        /// <summary>
        /// Ngày sinh
        /// </summary>
        public string? DateOfBirth { get; set; }
        /// <summary>
        /// Giới tính
        /// </summary>
        public int? Gender { get; set; }
        /// <summary>
        /// chứng minh thư nhân dân
        /// </summary>
        public string? IdentityNumber { get; set; }
        /// <summary>
        /// ngày cấp
        /// </summary>
        public string? IdentityIssueDate { get; set; }
        /// <summary>
        /// nơi cấp
        /// </summary>
        public string? IdentityIssuePlace { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? Address { get; set; }
        /// <summary>
        /// Số điện thoại
        /// </summary>
        public string? PhoneNumber { get; set; }
        /// <summary>
        /// Email
        /// </summary>

        public string? Email { get; set; }
        /// <summary>
        /// số điện thoại bàn
        /// </summary>
        public string? LandLinePhoneNumber { get; set; }
        /// <summary>
        /// số tài khoản
        /// </summary>
        public string? BankAccountNumber { get; set; }
        /// <summary>
        /// tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }
        /// <summary>
        /// chi nhánh ngân hàng
        /// </summary>
        public string? BankBranch{ get; set; }
        /// <summary>
        /// Ngày tạo
        /// </summary>
        public string? CreatedDate { get; set; }
        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }
        /// <summary>
        /// Ngày sửa
        /// </summary>
        public string? UpdatedDate { get; set; }
        /// <summary>
        /// Người sửa
        /// </summary>
        public string? UpdatedBy { get; set; }
    }
}
