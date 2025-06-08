using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;
using AccesoDatosSalon.Opetarions;
using Azure.Messaging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class AppointmentController : ControllerBase
    {
        private AppointmentDAO appointmentDAO = new AppointmentDAO();

        [HttpGet("appointments/pending")]
        public List<PendingAppointmentDTO> getPendingAppointments(string stylistUser)
        {
            return appointmentDAO.getPendingAppointments(stylistUser);
        }

        [HttpGet("appointment")]
        public Appointment getAppointmen(int id)
        {
            return appointmentDAO.getAppointment(id);
        }

        [HttpPost("appointments")]
        public bool CreateAppointment([FromBody] CreateAppointmentDTO dto)
        {
            // Validación de fecha de cita
            if (dto == null || dto.FechaDeCita < DateTime.Now)
                return false;

            // Verificar disponibilidad
            if (appointmentDAO.IsTimeSlotAvailable(dto.EstilistaUser, dto.FechaDeCita))
                return false;

            // Crear la cita
            return appointmentDAO.createAppointment(
                dto.ClienteId,
                dto.ServicioId,
                dto.EstilistaUser,
                dto.FechaDeCita,
                dto.notas
            );
        }

        [HttpPut("appointment")]
        public bool updateAppointment([FromBody] Appointment appointment)
        {
            return appointmentDAO.updateAppointmentStatus
                (
                    appointment.Id,                   
                    appointment.AppointmentStatus
                );
        }

        [HttpGet("appointments/client")]
        public List<Appointment> GetAppointmentByClient(int clientId)
        {
            return appointmentDAO.GetAppointmentsByClient(clientId);
        }

        [HttpGet("appointment/date")]
        public List<Appointment> GetAppointmentsByDate(DateTime date)
        {
            return appointmentDAO.GetAppointmentByDate(date.Date);
        }


        [HttpDelete("appointment")]
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
