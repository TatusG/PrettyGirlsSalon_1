using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatosSalon.Models.DTOS
{
    public class AppointmentDetailsDTO
    {
        public int  Id { get; set; }
        public string ClientName { get; set; }
        public string ServiceName { get; set; }
        public string StylistName { get; set; }
        public DateTime? DateTime { get; set; }
        public string Status { get; set; }
    }
}
