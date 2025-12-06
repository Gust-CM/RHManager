using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RHManager.Models
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        // 👇 ESTA ES LA PROPIEDAD QUE FALTABA
        public List<Position> Positions { get; set; } = new();
    }
}
