using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOS
{
    public class StylistRegisteredDTO
    {
        [Required(ErrorMessage = ("El user es obligatorio"))]
        public string UserName { get; set; } 
        [Required(ErrorMessage = ("La contraseña es obligatorio"))]
        public string UserPassword { get; set; } 
        [Required(ErrorMessage = ("El nombre es obligatorio"))]
        public string FullName { get; set; } 
        [Required(ErrorMessage = ("La especialidad es obligatorio"))]
        public string Specialty { get; set; }
        [Required(ErrorMessage = ("El email es obligatorio"))]
        public string Email { get; set; } 
        public bool IsActive { get; set; } = true;
    }
}
