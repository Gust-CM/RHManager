using System.ComponentModel.DataAnnotations;

namespace RHManager.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // Relación 1:N → Un departamento tiene muchos puestos
        public ICollection<Position> Positions { get; set; }
    }
}