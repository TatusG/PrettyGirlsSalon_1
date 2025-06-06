using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatosSalon.Models.DTOS
{
    public class PendingAppointmentDTO
    {
        public int Id { get; set; }
        public string ClientName { get; set; }
        public string Service { get; set; }
        public DateTime DateTime { get; set; }        
    }
}
