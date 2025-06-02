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
        public List<PendingAppointmentDTO> getPendingAppointment(string stylistUser)
        {
            return appointmentDAO.getPendingAppointments(stylistUser);
        }

        [HttpGet("Appointment")]
        public Appointment getAppointmen(int id) 
        {
            return appointmentDAO.getAppointment(id);
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

    }
}
