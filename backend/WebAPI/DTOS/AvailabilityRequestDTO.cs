namespace WebAPI.DTOS
{
    public class AvailabilityRequestDTO
    {
        public string StylistUserName { get; set; } // Nombre de usuario del estilista
        public DateTime Date { get; set; } // Fecha para la cual se solicitan los horarios disponibles
        public int? ServiceId { get; set; } // Duración de la cita en minutos        
    }
}
