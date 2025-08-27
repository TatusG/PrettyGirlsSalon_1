using AccesoDatosSalon.Models;
using AccesoDatosSalon.Opetarions;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOS;
using WebAPI.Services;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private readonly AppointmentService _appointmentService;

        public AppointmentController(AppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        [HttpGet("mis-citas")]
        public async Task<IActionResult> GetStylistAppointments()
        {
            var userName = User.Identity.Name;
            var citas = await _appointmentService.getStylistAppointments(userName);
            return Ok(citas);
        }

        [HttpPost("crear-cita")]
        public async Task<IActionResult> CreateAppointment([FromBody] AppointmentBookingDTO cita)
        {
            try
            {
                var result = await _appointmentService.bookAppointment(cita);
                return Ok(new { success = true, message = "Cita creada exitosamente", data = result });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        [HttpGet("citas-id")]
        public async Task<IActionResult> GetAppointment(int id)
        {
            if (id <= 0)
            {
                return BadRequest(new { message = "ID de cita inválido" });
            }

            try
            {
                var appointment = await _appointmentService.getAppointment(id);
                return appointment != null ? Ok(appointment) : NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        [HttpPut("actualizar-status")]
        public async Task<IActionResult> UpdateAppointmentStatus([FromBody] AppointmentStatusUpdateDTO statusDTO)
        {
            if (statusDTO == null || statusDTO.Id <= 0 || string.IsNullOrEmpty(statusDTO.NewStatus))
            {
                return BadRequest(new { message = "Datos incompletos" });
            }
            try
            {
                bool updated = await _appointmentService.updateAppointmentStatus(statusDTO.Id, statusDTO.NewStatus);
                return updated ? Ok(new { message = "Estado actualizado" }) : NotFound(new { message = "Cita no encontrada" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }

        }

        [HttpPatch("actualizar-cita")]
        public IActionResult UpdateAppointment([FromBody] AppointmentUpdateDTO updateDTO)
        {
            if (updateDTO == null || updateDTO.Id <= 0)
            {
                return BadRequest(new { message = "Datos de cita inválidos" });
            }
            try
            {
                var updatedAppointment = _appointmentService.updateAppointment(updateDTO);
                return Ok(new { message = "Cita actualizada exitosamente", updatedAppointment });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }


        // Obtener horarios disponibles para un estilista en una fecha específica
        [HttpGet("horarios")]
        public IActionResult GetAvailability(
            [FromQuery] string stylistUserName,
            [FromQuery] DateTime date,
            [FromQuery] int? serviceId = null)
        {
            if (string.IsNullOrEmpty(stylistUserName))
            {
                return BadRequest(new { message = "Nombre de estilista requerido" });
            }

            try
            {
                var slots = _appointmentService.getAvailableTimeSlots(new AvailabilityRequestDTO
                {
                    StylistUserName = stylistUserName,
                    Date = date,
                    ServiceId = serviceId
                });
                return Ok(slots);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // Obtener citas de un cliente específico
        [HttpGet("cliente")]
        public IActionResult GetClientAppointments(int clientId)
        {
            if (clientId <= 0)
            {
                return BadRequest(new { message = "ID de cliente inválido" });
            }

            var appointments = _appointmentService.getAppointment(clientId);
            return Ok(appointments);
        }
    }
}
