using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;
using System.ComponentModel.Design;

namespace AccesoDatosSalon.Opetarions
{
    public class ServiceDAO
    {
        public PrettyGirlSalonContext contexto = new PrettyGirlSalonContext();

        public List<ServiceRequest> allServices()
        {
            var services = contexto.ServiceRequests.Where(s => s.IsAvailable).ToList();
            return services;
        }

        public ServiceRequest getService (int id)
        {
            var service = contexto.ServiceRequests.Where(s => s.Id == id).FirstOrDefault();
            return service;
        }

        public bool AddService(string serviceName, int duration, decimal price, string description, bool available)
        {
            try
            {
                ServiceRequest service = new ServiceRequest();

                service.ServiceName = serviceName;
                service.DurationMinutes = duration;
                service.ServicePrice = price;
                service.ServiceDescription = description;
                service.IsAvailable = available;

                contexto.ServiceRequests.Add(service);
                contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateService(int id, string serviceName, int duration, decimal price, string description, bool available)
        {
            try
            {
                var service = getService(id);

                if (service == null)
                {
                    return false;
                }
                else
                {
                    service.ServiceName = serviceName;
                    service.DurationMinutes = duration;
                    service.ServicePrice = price;
                    service.ServiceDescription = description;
                    service.IsAvailable = available;
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool deleteService(int id)
        {

            try
            {
                var service = getService(id);
                if (service == null)
                {
                    return false;
                }
                else
                {
                    bool hasFutureAppointments = contexto.Appointments.Any(a => a.ServiceId == id && a.AppointmentDateTime > DateTime.Now && a.AppointmentStatus != "cancelled");

                    if (hasFutureAppointments)
                    {
                        service.IsAvailable = false;
                        contexto.SaveChanges();
                        return true;
                    }
                    else
                    {
                        contexto.ServiceRequests.Remove(service);
                        contexto.SaveChanges();
                        return true;
                    }
                }               
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar servicio : {ex.Message}");
                return false;
            }
        }
       

    }
}
