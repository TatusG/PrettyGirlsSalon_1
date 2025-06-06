using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatosSalon.Models.DTOS
{
    public class AppointmentFilterDTO
    {
        public string? ClienteNombre { get; set; }
        public string? Estilista { get; set; }
        public DateTime? FechaDesde { get; set; }
        public DateTime? FechaHasta { get; set; }
        public string Estado {  get; set; }
    }
}
