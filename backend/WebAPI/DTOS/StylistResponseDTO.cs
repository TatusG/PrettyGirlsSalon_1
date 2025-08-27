namespace WebAPI.DTOS
{
    public class StylistResponseDTO
    {
        public string UserName { get; set; }
        public string FullName { get; set; }
        public string Specialty { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }

        //Para mostrar información de manera más amigable. En listados
        public string DisplayInfo => $"{FullName} - {Specialty}";

    }
}
