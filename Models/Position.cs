using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RHManager.Models
{
    public class Position
    {
        [Key]
        public int PositionId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Clave foránea
        [Required]
        public int DepartmentId { get; set; }

        // Relación con Department
        [ForeignKey("DepartmentId")]
        public Department Department { get; set; }

        // Relación 1:N → un puesto puede tener varios empleados
        public ICollection<Employee> Employees { get; set; }
    }
}