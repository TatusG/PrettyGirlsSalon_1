using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatosSalon.Models
{
    public class AppointmentUpdateData
    {
        public int Id { get; set; } // Identificador de la cita a actualizar
        public string NewStatus { get; set; } // Nuevo estado de la cita (ej. "Pending", "Confirmed", "Cancelled", "Completed")
        public DateTime? NewDateTime { get; set; } // Nueva fecha y hora de la cita (opcional)
        public int? NewServiceId { get; set; } // Nuevo ID del servicio asociado a la cita (opcional)
        public string? NewNotes { get; set; } // Nuevas notas adicionales sobre la cita (opcional)
        public string? NewStylistUserName { get; set; } 
    }
}
