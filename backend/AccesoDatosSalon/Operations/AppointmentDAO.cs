using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;


namespace AccesoDatosSalon.Opetarions
{
    public class AppointmentDAO
    {
        private readonly PrettyGirlSalonContext context = new PrettyGirlSalonContext();

        // Crear una nueva cita
        public async Task<bool> createAppointment(Appointment appointment)
        {
            try
            {
                // Verificar disponibilidad primero
                if (!await isTimeSlotAvailable(appointment.StylistUser, appointment.AppointmentDateTime))
                {
                    return false;
                }

                context.Appointments.Add(appointment);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al crear cita: {ex.Message}");
                return false;
            }
        }

        // Verificar si un horario está disponible para un estilista
        public async Task<bool> isTimeSlotAvailable(string stylistUser, DateTime date)
        {
            // Validar horario laboral(9am - 7pm)
            if (date.TimeOfDay < TimeSpan.FromHours(9) || date.TimeOfDay >= TimeSpan.FromHours(19))
            {
                return false;
            }

            // Verificar si ya existe una cita en ese horario
            return !context.Appointments.Any(a =>
                a.StylistUser == stylistUser &&
                a.AppointmentDateTime == date &&
                a.AppointmentStatus != "Cancelled");
        }


        // Obtener una cita por ID
        public async Task<Appointment> getAppointment(int id)
        {
            return context.Appointments.FirstOrDefault(a => a.Id == id);
        }

        // Obtener citas pendientes de un estilista
        public async Task<bool> updateAppointment(AppointmentUpdateData updateData)
        {
            // Validar que la cita exista
            var appointment = await getAppointment(updateData.Id);

            // Si la cita no existe, retornar false
            if (appointment == null)
            {
                return false; // Cita no encontrada
            }

            // Actualizar los campos de la cita según los datos proporcionados
            if (!string.IsNullOrEmpty(updateData.NewStatus))
            {
                appointment.AppointmentStatus = updateData.NewStatus;
            }

            // Actualizar la fecha y hora de la cita si se proporciona
            if (updateData.NewDateTime.HasValue)
            {
                appointment.AppointmentDateTime = updateData.NewDateTime.Value;
            }

            // Actualizar el estilista si se proporciona
            if (!string.IsNullOrEmpty(updateData.NewNotes))
            {
                appointment.Notes = updateData.NewNotes;
            }
            context.SaveChanges();
            return true; // Cita actualizada exitosamente
        }

        // Obtener citas pendientes de un estilista
        public async Task<List<Appointment>> getAppointmentsByStylist(string stylistUser, DateTime? date = null)
        {
            // Filtrar citas por estilista y estado
            var query = context.Appointments.Where(a => a.StylistUser == stylistUser && a.AppointmentStatus != "Cancelled");

            // Si se proporciona una fecha, filtrar por esa fecha
            if (date.HasValue)
            {
                // Asegurarse de comparar solo la fecha, ignorando la hora
                query = query.Where(a => a.AppointmentDateTime.Date == date.Value.Date);
            }
            // Ordenar las citas por fecha y hora
            return query.OrderBy(a => a.AppointmentDateTime).ToList();
        }

        // Obtener citas por cliente
        public async Task<List<Appointment>> getAppointmentsByClient(int clientId, bool onlyUpcomming = false)
        {
            var query = context.Appointments.Where(a => a.ClientId == clientId);

            if (onlyUpcomming)
            {
                query = query.Where(a => a.AppointmentDateTime >= DateTime.Now && a.AppointmentStatus != "Cancelled");
            }
            return query.OrderByDescending(a => a.AppointmentDateTime).ToList();
        }

        //Cancelar una cita
        public async Task<bool> cancelAppointment(int appointmentId, string cancellationNotes = " ")
        {
            try
            {
                var appointment = context.Appointments.FirstOrDefault(a => a.Id == appointmentId);
                if (appointment == null || appointment.AppointmentStatus == "Cancelled")
                {
                    return false;
                }

                appointment.AppointmentStatus = "Cancelled";
                appointment.Notes = string.IsNullOrEmpty(cancellationNotes)
                    ? $"Cita cancelada el {DateTime.Now}"
                    : $"Cita cancelada: {cancellationNotes}";

                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cancelar cita: {ex.Message}");
                return false;
            }
        }

        public async Task<List<DateTime>> getAvailableTimeSlots(string stylistUser, DateTime date)
        {
            // Implementación básica - genera slots cada 30 minutos en el horario laboral
            var slots = new List<DateTime>();
            var startTime = date.Date.AddHours(9); // 9:00 AM
            var endTime = date.Date.AddHours(19);  // 7:00 PM

            for (var time = startTime; time < endTime; time = time.AddMinutes(30))
            {
                slots.Add(time);
            }

            return slots;
        }


        // Obtener citas por fecha
        public async Task<List<Appointment>> getAppointmentByDate(DateTime date, bool includeCancelled = false)
        {
            var query = context.Appointments.Where(a => a.AppointmentDateTime.Date == date.Date);

            if (!includeCancelled)
            {
                query = query.Where(a => a.AppointmentStatus != "Cancelled");
            }
            return query.OrderBy(a => a.AppointmentDateTime).ToList();
        }

        //Verificar disponibilidad con duración de servicio
        public async Task<bool> IsTimeSlotAvailableWithDuration(string stylistUser, DateTime startTime, int durationMinutes)
        {
            // Verificar horario laboral
            if (startTime.TimeOfDay < TimeSpan.FromHours(9) || startTime.AddMinutes(durationMinutes).TimeOfDay > TimeSpan.FromHours(19))
            {
                return false;
            }

            // Verificar si ya existe una cita en ese horario para el estilista
            var endTime = startTime.AddMinutes(durationMinutes);

            return !context.Appointments.Any(a => a.StylistUser == stylistUser &&
                                                   a.AppointmentStatus != "Cancelled" &&
                                                   ((a.AppointmentDateTime < endTime &&
                                                   a.AppointmentDateTime.AddMinutes(a.Service.DurationMinutes) > startTime)));
        }
    }
}
