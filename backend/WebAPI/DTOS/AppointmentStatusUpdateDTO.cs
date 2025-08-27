namespace WebAPI.DTOS
{
    public class AppointmentStatusUpdateDTO
    {
        public int Id { get; set; } // ID de la cita a actualizar
        public string NewStatus { get; set; } // Nuevo estado de la cita, e.g., "Confirmed", "Completed", "Cancelled"
    }
}
