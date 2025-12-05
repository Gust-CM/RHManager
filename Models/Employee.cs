using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHManager.Models
{
    public class Employee
    {
        [Key]
        public int EmployeeId { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; } = true;

        // FK hacia Position
        [Required]
        public int PositionId { get; set; }

        [ForeignKey("PositionId")]
        public Position Position { get; set; }

        // Relación 1:N → empleado tiene muchas solicitudes
        public ICollection<LeaveRequest> LeaveRequests { get; set; }
    }
}