using AccesoDatosSalon.Models;

namespace WebAPI.DTOS
{
    public class StylistLoginResponseDTO
    {
        public Stylist Usuario { get; set; }
        public string Token { get; set; }
    }
}