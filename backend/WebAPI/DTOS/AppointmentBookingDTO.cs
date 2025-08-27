using Microsoft.OpenApi.MicrosoftExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebAPI.DTOS
{
    public class AppointmentBookingDTO // DTO para reservar una cita
    {        
        public int ClientId { get; set; }
        public int ServiceId{ get; set; }        
        public string StylistUserName { get; set; }
        public DateTime? AppointmentDateTime { get; set; }
        public string Notes { get; set; }
    }
}
