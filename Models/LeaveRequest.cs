using System.ComponentModel.DataAnnotations;

namespace RHManager.Models
{
    public class LeaveRequest
    {
        public int LeaveRequestId { get; set; }

        [Required(ErrorMessage = "Debe seleccionar un empleado.")]
        [Display(Name = "Empleado")]
        public int EmployeeId { get; set; }
        public Employee? Employee { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de inicio.")]
        [Display(Name = "Fecha de Inicio")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha final.")]
        [Display(Name = "Fecha de Fin")]
        public DateTime EndDate { get; set; }

        [Required(ErrorMessage = "Debe ingresar una razón.")]
        [StringLength(500)]
        [Display(Name = "Razón")]
        public string Reason { get; set; }

        [Required]
        [Display(Name = "Estado")]
        public int Status { get; set; } // 0=Pendiente, 1=Aprobado, 2=Rechazado

        // ✔ Propiedad auxiliar NO mapeada que te da el enum listo para usar
        public LeaveStatus StatusEnum => (LeaveStatus)Status;
    }
}