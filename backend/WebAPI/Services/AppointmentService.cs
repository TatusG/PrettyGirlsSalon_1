using AccesoDatosSalon.Models;
using AccesoDatosSalon.Operations;
using AccesoDatosSalon.Opetarions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOS;

namespace WebAPI.Services
{
    public class AppointmentService
    {
        private readonly AppointmentDAO _appointmentDAO;
        private readonly StylistDAO _stylistDAO;
        private readonly ServiceDAO _serviceDAO;
        private readonly ClientDAO _clientDAO;

        public AppointmentService(
            AppointmentDAO appointmentDAO,
            StylistDAO stylistDAO,
            ServiceDAO serviceDAO,
            ClientDAO clientDAO)
        {
            _appointmentDAO = appointmentDAO;
            _stylistDAO = stylistDAO;
            _serviceDAO = serviceDAO;
            _clientDAO = clientDAO;
        }

        // Reservar una nueva cita
        public async Task<AppointmentResponseDTO> bookAppointment(AppointmentBookingDTO bookingDTO)
        {
            if (bookingDTO == null || !bookingDTO.AppointmentDateTime.HasValue)
            {
                throw new ArgumentException("Datos de cita inválidos");
            }

            // Validar fecha futura
            if (bookingDTO.AppointmentDateTime < DateTime.Now)
            {
                throw new ArgumentException("La fecha de la cita no puede ser en el pasado");
            }

            // Validar cliente
            var client = _clientDAO.getClient(bookingDTO.ClientId);
            if (client == null)
            {
                throw new KeyNotFoundException("Cliente no encontrado");
            }

            // Validar estilista
            var stylist = await _stylistDAO.getStylist(bookingDTO.StylistUserName);
            if (stylist == null || !stylist.IsActive.GetValueOrDefault())
            {
                throw new InvalidOperationException("Estilista no encontrado o inactivo");
            }

            // Validar servicio
            var service = await _serviceDAO.getService(bookingDTO.ServiceId);
            if (service == null || !service.IsAvailable)
            {
                throw new InvalidOperationException("Servicio no disponible");
            }

            // Crear la cita
            var appointment = new Appointment
            {
                ClientId = bookingDTO.ClientId,
                ServiceId = bookingDTO.ServiceId,
                StylistUser = bookingDTO.StylistUserName,
                AppointmentDateTime = bookingDTO.AppointmentDateTime.Value,
                AppointmentStatus = "Pending",
                Notes = bookingDTO.Notes
            };

            // Guardar la cita
            if (!await _appointmentDAO.createAppointment(appointment))
            {
                throw new InvalidOperationException("Horario no disponible");
            }

            return await ConvertToDTO(appointment);
        }

        // Obtener cita por ID
        public async Task<AppointmentResponseDTO> getAppointment(int id)
        {
            var appointment = await _appointmentDAO.getAppointment(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Cita no encontrada");
            }
            return await ConvertToDTO(appointment);
        }

        // Obtener citas de un estilista
        public async Task<IEnumerable<AppointmentResponseDTO>> getStylistAppointments(string stylistUserName, DateTime? date = null)
        {
            var appointments = await _appointmentDAO.getAppointmentsByStylist(stylistUserName, date);
            var dtos = new List<AppointmentResponseDTO>();

            foreach (var appointment in appointments)
            {
                dtos.Add(await ConvertToDTO(appointment));
            }

            return dtos;
        }

        // Actualizar estado de una cita
        public async Task<bool> updateAppointmentStatus(int id, string newStatus)
        {
            var appointment = await _appointmentDAO.getAppointment(id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Cita no encontrada");
            }

            // Validar el nuevo estado
            var validStatuses = new[] { "Pending", "Confirmed", "Cancelled", "Completed" };
            if (!validStatuses.Contains(newStatus))
            {
                throw new ArgumentException("Estado de cita inválido");
            }

            // Validar transición de estado
            if (!isValidStatusTransition(appointment.AppointmentStatus, newStatus))
            {
                throw new InvalidOperationException($"No se puede cambiar de {appointment.AppointmentStatus} a {newStatus}");
            }

            return await _appointmentDAO.updateAppointment(new AppointmentUpdateData
            {
                Id = id,
                NewStatus = newStatus
            });
        }

        // Actualizar cita completa
        public async Task<AppointmentResponseDTO> updateAppointment(AppointmentUpdateDTO updateDTO)
        {
            if (updateDTO == null || updateDTO.Id <= 0)
            {
                throw new ArgumentException("Datos inválidos");
            }

            // Validar que la cita exista
            var appointment = await _appointmentDAO.getAppointment(updateDTO.Id);
            if (appointment == null)
            {
                throw new KeyNotFoundException("Cita no encontrada");
            }

            // Validar fecha futura si se proporciona
            if (updateDTO.AppointmentDateTime.HasValue &&
                updateDTO.AppointmentDateTime < DateTime.Now)
            {
                throw new ArgumentException("La fecha de la cita no puede ser en el pasado");
            }

            // Validar servicio si se proporciona
            if (updateDTO.ServiceId.HasValue)
            {
                var service = await _serviceDAO.getService(updateDTO.ServiceId.Value);
                if (service == null || !service.IsAvailable)
                {
                    throw new InvalidOperationException("Servicio no disponible");
                }
                appointment.ServiceId = updateDTO.ServiceId.Value;
            }

            // Validar estilista si se proporciona
            if (!string.IsNullOrEmpty(updateDTO.StylistUserName))
            {
                var stylist = await _stylistDAO.getStylist(updateDTO.StylistUserName);
                if (stylist == null || !stylist.IsActive.GetValueOrDefault())
                {
                    throw new InvalidOperationException("Estilista no encontrado o inactivo");
                }
                appointment.StylistUser = updateDTO.StylistUserName;
            }

            // Actualizar campos
            if (updateDTO.AppointmentDateTime.HasValue)
            {
                appointment.AppointmentDateTime = updateDTO.AppointmentDateTime.Value;
            }

            if (!string.IsNullOrEmpty(updateDTO.Notes))
            {
                appointment.Notes = updateDTO.Notes;
            }

            // Guardar cambios
            bool success = await _appointmentDAO.updateAppointment(new AppointmentUpdateData
            {
                Id = updateDTO.Id,
                NewDateTime = updateDTO.AppointmentDateTime,
                NewServiceId = updateDTO.ServiceId,
                NewNotes = updateDTO.Notes,
                NewStylistUserName = updateDTO.StylistUserName
            });

            if (!success)
            {
                throw new InvalidOperationException("No se pudo actualizar la cita");
            }

            return await ConvertToDTO(appointment);
        }

        // Obtener horarios disponibles
        public async Task<IEnumerable<DateTime>> getAvailableTimeSlots(AvailabilityRequestDTO request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            // Validar estilista
            var stylist = await _stylistDAO.getStylist(request.StylistUserName);
            if (stylist == null || !(stylist.IsActive ?? false))
            {
                throw new KeyNotFoundException("Estilista no encontrado o inactivo");
            }

            // Obtener duración del servicio (30 minutos por defecto)
            int duration = 30;
            if (request.ServiceId.HasValue && request.ServiceId.Value > 0)
            {
                var service = await _serviceDAO.getService(request.ServiceId.Value);
                if (service != null && service.IsAvailable)
                {
                    duration = service.DurationMinutes;
                }
            }

            // Obtener slots base
            var baseSlots = await _appointmentDAO.getAvailableTimeSlots(request.StylistUserName, request.Date);
            var availableSlots = new List<DateTime>();

            // Verificar disponibilidad considerando la duración
            foreach (var slot in baseSlots)
            {
                if (await _appointmentDAO.IsTimeSlotAvailableWithDuration(
                    request.StylistUserName,
                    slot,
                    duration))
                {
                    availableSlots.Add(slot);
                }
            }

            return availableSlots;
        }

        // Cancelar cita
        public async Task<bool> cancelAppointment(int appointmentId, string cancellationNotes = "")
        {
            return await _appointmentDAO.cancelAppointment(appointmentId, cancellationNotes);
        }

        // Obtener citas por cliente
        public async Task<IEnumerable<AppointmentResponseDTO>> getClientAppointments(int clientId, bool onlyUpcoming = false)
        {
            var appointments = await _appointmentDAO.getAppointmentsByClient(clientId, onlyUpcoming);
            var dtos = new List<AppointmentResponseDTO>();

            foreach (var appointment in appointments)
            {
                dtos.Add(await ConvertToDTO(appointment));
            }

            return dtos;
        }

        // Obtener citas por fecha
        public async Task<IEnumerable<AppointmentResponseDTO>> getAppointmentsByDate(DateTime date, bool includeCancelled = false)
        {
            var appointments = await _appointmentDAO.getAppointmentByDate(date, includeCancelled);
            var dtos = new List<AppointmentResponseDTO>();

            foreach (var appointment in appointments)
            {
                dtos.Add(await ConvertToDTO(appointment));
            }

            return dtos;
        }

        // Validar transición de estado
        private bool isValidStatusTransition(string currentStatus, string newStatus)
        {
            var validTransitions = new Dictionary<string, List<string>>
            {
                { "Pending", new List<string> { "Confirmed", "Cancelled" } },
                { "Confirmed", new List<string> { "Completed", "Cancelled" } },
                { "Completed", new List<string>() },
                { "Cancelled", new List<string>() }
            };

            if (!validTransitions.ContainsKey(currentStatus))
                return false;

            return validTransitions[currentStatus].Contains(newStatus);
        }

        // Convertir Appointment a DTO (versión asíncrona)
        private async Task<AppointmentResponseDTO> ConvertToDTO(Appointment appointment)
        {
            var clientTask = _clientDAO.getClient(appointment.ClientId);
            var serviceTask = _serviceDAO.getService(appointment.ServiceId);
            var stylistTask = _stylistDAO.getStylist(appointment.StylistUser);

            await Task.WhenAll(clientTask, serviceTask, stylistTask);

            return new AppointmentResponseDTO
            {
                Id = appointment.Id,
                ClientName = clientTask.Result?.FullName,
                ServiceName = serviceTask.Result?.ServiceName,
                StylistName = stylistTask.Result?.FullName,
                AppointmentDateTime = appointment.AppointmentDateTime,
                Status = appointment.AppointmentStatus,
                Notes = appointment.Notes,
                Duration = TimeSpan.FromMinutes(serviceTask.Result?.DurationMinutes ?? 30)
            };
        }
    }
}