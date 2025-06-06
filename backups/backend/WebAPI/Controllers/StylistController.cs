using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;
using AccesoDatosSalon.Opetarions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class StylistController : ControllerBase
    {
        private StylistDAO stylistDAO = new StylistDAO();

        [HttpPost("Authentication")]

        public string login([FromBody] Stylist estil)
        {
            var stylis = stylistDAO.login(estil.UserName, estil.UserPassword);

            if (stylis != null)
            {
                return estil.UserName;
            }
            else 
            {
                return null;
            }
        }        

        [HttpGet("Stylist")]

        public Stylist selectStylist(string username)
        {
            return stylistDAO.getStylist(username);
        }

    }
}
