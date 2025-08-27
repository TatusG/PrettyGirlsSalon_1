namespace WebAPI.DTOS
{
    public class AppointmentUpdateDTO // DTO para actualizar una cita
    {
        public int Id { get; set; } // ID de la cita a actualizar
        public DateTime? AppointmentDateTime { get; set; } // Fecha y hora de la cita
        public int? ServiceId { get; set; } // ID del servicio asociado a la cita
        public string? Notes { get; set; } // Notas adicionales sobre la cita
        public string? StylistUserName { get; set; } // Nombre de usuario del estilista asignado a la cita
    }
}
