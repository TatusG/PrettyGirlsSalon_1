using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOS
{
    public class LoginDTO
    {
        [Required(ErrorMessage =("El user es obligatorio"))]
        public string UserName { get; set; } 
        [Required(ErrorMessage = ("El password es obligatorio"))]
        public string Password { get; set; } 
    }
}
