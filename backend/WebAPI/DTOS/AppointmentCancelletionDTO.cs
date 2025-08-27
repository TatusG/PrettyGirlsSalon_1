namespace WebAPI.DTOS
{
    public class AppointmentCancelletionDTO
    {
        public int AppointmentId { get; set; } // ID de la cita a cancelar
        public string CancellationReason { get; set; } // Motivo de la cancelación        
    }
}
