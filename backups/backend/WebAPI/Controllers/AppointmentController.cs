using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;
using AccesoDatosSalon.Opetarions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private AppointmentDAO appointmentDAO = new AppointmentDAO();

        [HttpGet("PendingAppointment")]
        public List<PendingAppointmentDTO> getPendingAppointments(string stylistUser)
        {
            return appointmentDAO.getPendingAppointments(stylistUser);
        }

        [HttpGet("Appointment")]
        public Appointment getAppointmen(int id)
        {
            return appointmentDAO.getAppointment(id);
        }

        [HttpPost("Appointment")]
        public bool CreateAppointment([FromBody] CreateAppointmentDTO dto)
        {
            return appointmentDAO.createAppointment(
                dto.ClienteId,
                dto.ServicioId,
                dto.EstilistaUser,
                dto.FechaDeCita
            );
        }

        [HttpPut("Appointment")]
        public bool updateAppointment([FromBody] Appointment appointment)
        {
            return appointmentDAO.updateAppointment
                (
                    appointment.Id,
                    appointment.ClientId,
                    appointment.ServiceId,
                    appointment.StylistUser,
                    appointment.AppointmentDateTime,
                    appointment.AppointmentStatus
                );
        }

        [HttpDelete("Appointment")]
        public bool deleteAppointment(int id)
        {
            return appointmentDAO.deleteAppointment(id);
        }

        [HttpGet("AvailableAppointments")]
        public List<DateTime> GetAvailableAppointments(string stylistUser, DateTime date)
        {
            return appointmentDAO.GetAvailableTimeSlots(stylistUser, date);
        }

    }
}
