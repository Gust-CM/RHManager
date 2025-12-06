using System.ComponentModel.DataAnnotations;

namespace RHManager.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un empleado.")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de inicio.")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha final.")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Debe ingresar una razón.")]
        [StringLength(500)]
        public string Reason { get; set; }

        [Required]
        public int Status { get; set; } // 0=Pendiente, 1=Aprobado, 2=Rechazado

        // ✔ Propiedad auxiliar NO mapeada que te da el enum listo para usar
        public LeaveStatus StatusEnum => (LeaveStatus)Status;
    }
}