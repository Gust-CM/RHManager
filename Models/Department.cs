using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RHManager.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Nombre")]
        public string Name { get; set; }
        public List<Position> Positions { get; set; } = new();
    }
}
