using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOS
{
    public class AvailabilityRequest //DTO para solicitar disponibilidad de citas
    {
        public string StylistUser { get; set; }    
        public DateTime Date { get; set; } //Solo fecha, no hora
        public int ServiceId { get; set; } //para calcular la duracion de la cita
    }
}
