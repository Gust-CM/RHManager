using System.ComponentModel.DataAnnotations;

namespace RHManager.Models
{
    public class Employee
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio.")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "El apellido es obligatorio.")]
        [StringLength(100)]
        public string LastName { get; set; }

        [Required(ErrorMessage = "El correo es obligatorio.")]
        [EmailAddress(ErrorMessage = "Debe ingresar un correo válido.")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Debe ingresar la fecha de contratación.")]
        public DateTime HireDate { get; set; }

        public bool IsActive { get; set; } = true;

        [Required(ErrorMessage = "Debe seleccionar un puesto.")]
        public int PositionId { get; set; }

        public Position? Position { get; set; }

    }
}