using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOS
{
    public class AppointmentResponseDTO // DTO para la respuesta de una cita
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string ServiceName { get; set; }
        public string StylistName { get; set; }
        public DateTime? AppointmentDateTime { get; set; }        
        public string Status {  get; set; }
        public string Notes { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
