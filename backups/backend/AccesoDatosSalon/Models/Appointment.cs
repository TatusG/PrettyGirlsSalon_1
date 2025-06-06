namespace AccesoDatosSalon.Models;

public partial class Appointment
{
    public int Id { get; set; }

    public int ClientId { get; set; }

    public int ServiceId { get; set; }

    public string StylistUser { get; set; } = null!;

    public DateTime AppointmentDateTime { get; set; }

    public string AppointmentStatus { get; set; } = null!;

    public string? Notes { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual ICollection<Review> Reviews { get; set; } = new List<Review>();

    public virtual ServiceRequest Service { get; set; } = null!;

    public virtual Stylist StylistUserNavigation { get; set; } = null!;
}
