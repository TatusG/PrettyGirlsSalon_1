namespace AccesoDatosSalon.Models;

public partial class Client
{
    public int Id { get; set; }

    public string Dni { get; set; } = null!;

    public string FullName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string? Email { get; set; }

    public DateOnly RegistrationDate { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
