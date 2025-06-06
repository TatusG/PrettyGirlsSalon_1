namespace AccesoDatosSalon.Models;

public partial class Stylist
{
    public string? UserName { get; set; } = null!;

    public string? UserPassword { get; set; } = null!;

    public string? FullName { get; set; } = null!;

    public string? Specialty { get; set; } = null!;

    public string? Email { get; set; } = null!;

    public bool? IsActive { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
}
