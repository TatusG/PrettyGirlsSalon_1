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
        
        // Insertar nueva cita
        public bool createAppointment(int clientId, int serviceId, string stylistUser, DateTime date) 
        { 
            try
            {
                var appointment = new Appointment
                {
                    ClientId = clientId,
                    ServiceId = serviceId,
                    StylistUser = stylistUser,
                    AppointmentDateTime = date,
                    AppointmentStatus = "Pending"
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

        // Actualizar cita existente
        public bool updateAppointment(int id, int clientId, int serviceId, string stylistUser, DateTime date, string status, string commen = null)
        {
            try
            {
                var appointment = getAppointment(id);
                if (date == null) return false;

                appointment.ClientId = clientId;
                appointment.ServiceId = serviceId;
                appointment.StylistUser = stylistUser;
                appointment.AppointmentDateTime = date;
                appointment.AppointmentStatus = status;
                appointment.Notes = commen;

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
    }
}
