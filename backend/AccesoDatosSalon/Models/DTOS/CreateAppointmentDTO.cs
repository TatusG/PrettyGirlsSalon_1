using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatosSalon.Models.DTOS
{
    public class CreateAppointmentDTO
    {
        public int ClienteId { get; set; } 
        public int ServicioId { get; set; }
        public string EstilistaUser { get; set; }
        public DateTime FechaDeCita { get; set; }
    }
}
