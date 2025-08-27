using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOS;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class StylistController : ControllerBase
    {
        private readonly StylistService _stylistService;

        public StylistController(StylistService stylistService)
        {
            _stylistService = stylistService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] LoginDTO loginDTO)
        {
            var response = await _stylistService.login(loginDTO);
            if (response != null)
            {
                return Ok(new
                {
                    success = true,
                    message = "Estilista logueado correctamente",
                    token = response.Token,
                    usuario = new
                    {
                        usuario = response.Usuario.UserName,
                        nombre = response.Usuario.FullName,
                        especialidad = response.Usuario.Specialty,
                        email = response.Usuario.Email,

                    }
                });
            }
            return Unauthorized(new { success = false, message = "Usuario o contraseña incorrectos" });
        }

        [HttpPost("registrar")]        
        public async Task<IActionResult> register([FromBody] StylistRegisteredDTO stylistRegistered)
        {
            bool registered = await _stylistService.AddStylist(stylistRegistered);
            if (registered)
            {
                return Ok(new { success = true, message = "Estilista registrado exitosamente" });
            }
            else
            {
                return BadRequest(new { success = false, message = "Error al registrar el estilista" });
            }                              
        }

        [HttpPatch("actualizar-password")]        
        public async Task <IActionResult> updatePassword([FromBody] UpdatePasswordDTO updatePasswordDTO)
        {
            bool updated = await _stylistService.updateStylistPassword(updatePasswordDTO);
            if (!updated)
            {
                return BadRequest(new { success = false, message = "Error al actualizar la contraseña" });
            }
            return Ok(new { success = true, message = "Contraseña actualizada exitosamente" });                
        }

        [HttpGet("buscar-estilista")]
        public async Task<IActionResult> GetStylist(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest("Nombre de usuario no proporcionado");

            var stylist = await _stylistService.getStylist(userName);
            return stylist != null ?
                Ok(stylist) :
                NotFound(new { message = "Estilista no encontrado" });
        }

        [HttpPatch("actualizar")]        
        public async Task<IActionResult> UpdateStylist([FromBody] StylistUpdateDTO updatedStylist)
        {
            bool updated = await _stylistService.updateStylist(updatedStylist);
            return updated ?
                Ok(new { success = true, message = "Estilista actualizado exitosamente" }) :
                NotFound(new { success = false, message = "Estilista no encontrado" });
        }        

        [HttpDelete("borrar")]
        [Authorize(Policy ="soloAdministrador")]
        public async Task<IActionResult> DeleteStylist(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest(new { message = "Nombre de usuario no proporcionado" });

            bool isDeleted = await _stylistService.deleteStylist(userName);
            return isDeleted ?
                Ok(new { message = "Estilista eliminado exitosamente" }) :
                NotFound(new { message = $"Estilista {userName} no encontrado" });
        }

        [HttpDelete("desactivar")]
        [Authorize(Policy ="soloAdministrador")]
        public async Task<IActionResult> DeactivateStylist(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
                return BadRequest(new { Message = "Nombre de usuario requerido" });

            bool isDeactivated = await _stylistService.ToggleStylistStatus(userName, false);
            return isDeactivated ?
                Ok(new { Success = true, Message = $"Estilista {userName} desactivado correctamente" }) :
                NotFound(new { Success = false, Message = $"No se encontró el estilista {userName}" });
        }
    }
}