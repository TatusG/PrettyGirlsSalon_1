using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOS
{
    public class StylistUpdateDTO
    {
        [Required(ErrorMessage = ("El user es obligatorio"))]
        public string UserName { get; set; } 
        [Required(ErrorMessage = ("La contraseña es obligatorio"))]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = ("El nombre es obligatorio"))]
        public string FullName { get; set; } 
        [Required(ErrorMessage = ("La especialidad es obligatorio"))]
        public string Specialty { get; set; }
        [Required(ErrorMessage = ("El email es obligatorio"))]
        public string Email { get; set; } 
        public bool? IsActive { get; set; }      
    }
}
