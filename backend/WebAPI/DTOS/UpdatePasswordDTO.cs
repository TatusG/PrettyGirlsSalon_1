using System.ComponentModel.DataAnnotations;

namespace WebAPI.DTOS
{
    public class UpdatePasswordDTO
    {
        [Required(ErrorMessage = ("El user es obligatorio"))]
        public string UserName { get; set; }
        [Required(ErrorMessage = ("La contraseña actual es obligatorio"))]
        public string OldPassword { get; set; } 
        [Required(ErrorMessage = ("La nueva contraseña es obligatorio"))]
        public string NewPassword { get; set; } 
    }
}
