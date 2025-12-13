using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RHManager.Models
{
    public class Position
    {
        public int PositionId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }

        [Required]
        public int DepartmentId { get; set; }
        public Department? Department { get; set; }
        public List<Employee> Employees { get; set; } = new();
    }
}
