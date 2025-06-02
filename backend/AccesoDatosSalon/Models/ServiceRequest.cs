namespace AccesoDatosSalon.Models;

public partial class ServiceRequest
{
    public int Id { get; set; }

    public string ServiceName { get; set; } = null!;

    public int DurationMinutes { get; set; }

    public decimal ServicePrice { get; set; }

    public string? ServiceDescription { get; set; }

    public bool IsAvailable { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
