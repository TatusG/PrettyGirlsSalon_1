using AccesoDatosSalon.Models;
using AccesoDatosSalon.Opetarions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ServiceController : ControllerBase
    {
        private ServiceDAO serviceDAO = new ServiceDAO();

        [HttpGet("services")]
        public List<ServiceRequest> GetAllServices()
        {
            return serviceDAO.allServices();
        }

        [HttpGet("service")]
        public ServiceRequest GetService(int id)
        {
            return serviceDAO.getService(id);
        }

        [HttpPost("service")]
        public bool createService([FromBody] ServiceRequest service)
        {
            if (service == null)
            {
                return false;
            }
            else
            {
                return serviceDAO.AddService(
                    service.ServiceName,
                    service.DurationMinutes,
                    service.ServicePrice,
                    service.ServiceDescription,
                    service.IsAvailable
                );
            }
        }

        [HttpPut("service")]
        public bool updateService([FromBody] ServiceRequest service)
        {
            if (service == null || service.Id <= 0)
            {
                return false;
            }
            else
            {
                return serviceDAO.updateService(
                    service.Id,
                    service.ServiceName,
                    service.DurationMinutes,
                    service.ServicePrice,
                    service.ServiceDescription,
                    service.IsAvailable
                );
            }
        }

        [HttpDelete("service")]
        public bool deleteService(int id)
        {
            return serviceDAO.deleteService(id);
        }

        [HttpGet("services/available")]
        public List<ServiceRequest> GetAvailableServices()
        {
            return serviceDAO.allServices().Where(s => s.IsAvailable).ToList();
        }

        [HttpGet("services/byDuration")]
        public List<ServiceRequest> getServiceByDuration(int minMinutes, int maxMinutes)
        {
            return serviceDAO.allServices()
                .Where(s => s.DurationMinutes >= minMinutes && s.DurationMinutes <= maxMinutes)
                .ToList();
        }
    }
}
