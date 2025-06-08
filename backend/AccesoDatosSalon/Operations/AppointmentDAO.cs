using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;

namespace AccesoDatosSalon.Opetarions
{
    public class AppointmentDAO
    {
        public PrettyGirlSalonContext contexto = new PrettyGirlSalonContext();

        // Obtener todas las citas
        public List<Appointment> getAllAppointments()
        {
            return contexto.Appointments.ToList();
        }

        // Seleccionar cita por ID
        public Appointment getAppointment(int id)
        {
            return contexto.Appointments.FirstOrDefault(a => a.Id == id);
        }

        public bool IsTimeSlotAvailable(string stylistUser, DateTime date)
        {
            //Verifica si el rango está dentro del horario laboral (9am-7pm)
            if (date.TimeOfDay < TimeSpan.FromHours(9) || date.TimeOfDay >= TimeSpan.FromHours(19))
            {
                return false;
            }
            else
            {
                return contexto.Appointments.Any(
                    a=> a.StylistUser == stylistUser && 
                    a.AppointmentDateTime == date &&
                    a.AppointmentStatus != "Cancelled");
            }
        }
        
        // Insertar nueva cita
        public bool createAppointment(int clientId, int serviceId, string stylistUser, DateTime date, string notes) 
        { 
            try
            {
                var appointment = new Appointment
                {
                    ClientId = clientId,
                    ServiceId = serviceId,
                    StylistUser = stylistUser,
                    AppointmentDateTime = date,                    
                    AppointmentStatus = "Pending",
                    Notes = notes
                };

                contexto.Appointments.Add(appointment);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {                
                return false;
            }
        }
        
        public bool updateAppointmentStatus(int id, string status)
        {
            try
            {
                var appointment = getAppointment(id);
                if (appointment == null) return false;

                var validStatuses = new[] { "Pendind", "Comfirmed", "Completed", "Cancelled" };
                if (validStatuses.Contains(status))
                {
                    return false ;
                }
                appointment.AppointmentStatus = status;
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Eliminar cita
        public bool deleteAppointment(int id)
        {
            try
            {
                var appointment = getAppointment(id);
                if (appointment == null) return false;

                contexto.Appointments.Remove(appointment);
                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        // Método para ver citas pendientes
        public List<PendingAppointmentDTO> getPendingAppointments(string stylistUser)
        {
            var query = from a in contexto.Appointments
                        join c in contexto.Clients on a.ClientId equals c.Id
                        join s in contexto.ServiceRequests on a.ServiceId equals s.Id
                        join st in contexto.Stylists on a.StylistUser equals st.UserName
                        where (a.StylistUser == stylistUser && a.AppointmentStatus == "pending")
                        select new PendingAppointmentDTO
                        {
                            Id = a.Id,
                            ClientName = c.FullName,
                            Service = s.ServiceName,
                            DateTime = a.AppointmentDateTime
                        };
            return query.ToList();
        }

        public List<DateTime> GetAvailableTimeSlots(string stylistUser, DateTime date)
        {
            var citaExistente = contexto.Appointments.Where(a=> a.StylistUser == stylistUser && a.AppointmentDateTime.Date == date)
                        .Select(a => a.AppointmentDateTime).ToList();
            var horariosDisponibles = new List<DateTime>();
            var horaInicio = date.Date.AddHours(9);
            var horaFin = date.Date.AddHours(19);

            for (var hora = horaInicio; hora < horaFin; hora = hora.AddMinutes(30))
            {
                if (!citaExistente.Any(c => c == hora))
                    horariosDisponibles.Add(hora);
            }
            return horariosDisponibles;
        }

        public List<Appointment> GetAppointmentsByClient(int clientId)
        {
            return contexto.Appointments.Where(a => a.ClientId == clientId).OrderByDescending(a => a.AppointmentDateTime).ToList();
        }

        public List<Appointment> GetAppointmentByDate(DateTime date)
        {
            return contexto.Appointments.Where(a => a.AppointmentDateTime.Date == date.Date).OrderBy(a => a.AppointmentDateTime).ToList();
        }
    }
}
