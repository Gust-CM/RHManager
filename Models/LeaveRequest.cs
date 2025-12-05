using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHManager.Models
{
    public class LeaveRequest
    {
        [Key]
        public int LeaveRequestId { get; set; }

        // FK hacia empleado
        [Required]
        public int EmployeeId { get; set; }

        [ForeignKey("EmployeeId")]
        public Employee Employee { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        [StringLength(500)]
        public string Reason { get; set; }

        [Required]
        public LeaveStatus Status { get; set; } = LeaveStatus.Pending;
    }

    public enum LeaveStatus
    {
        Pending,
        Approved,
        Rejected
    }
}