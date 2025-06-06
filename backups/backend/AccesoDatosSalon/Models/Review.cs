namespace AccesoDatosSalon.Models;

public partial class Review
{
    public int Id { get; set; }

    public int AppointmentId { get; set; }

    public int RatingValue { get; set; }

    public string? ReviewComment { get; set; }

    public DateOnly ReviewDate { get; set; }

    public string? Response { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;
}
