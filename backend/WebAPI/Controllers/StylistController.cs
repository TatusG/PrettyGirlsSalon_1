using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;
using AccesoDatosSalon.Opetarions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Update;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class StylistController : ControllerBase
    {
        private StylistDAO stylistDAO = new StylistDAO();

        [HttpPost("login")]

        public string login([FromBody] LoginDTO estil)
        {
            var stylist = stylistDAO.login(estil.UserName, estil.Password);
            return stylist?.UserName;            
        }        

        [HttpGet("stylist")]

        public Stylist selectStylist(string username)
        {
            return stylistDAO.getStylist(username);
        }

        [HttpPost("stylist")]

        public bool addStylist([FromBody] Stylist newStylist)
        {
            return stylistDAO.addStylist(newStylist);
        }

        [HttpPut("stylist")]
        public bool updateStylist([FromBody] Stylist updateStylist)
        {
            if (updateStylist == null || string.IsNullOrEmpty(updateStylist.UserName))
            {
                return false;
            }

            return stylistDAO.updateStylist(updateStylist);
        }

        [HttpDelete("stylist")]
        public bool deleteStylist(string username) 
        { 
            return stylistDAO.deleteStylist(username);
        }
    }
}
