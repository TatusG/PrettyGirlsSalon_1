using AccesoDatosSalon.Models;
using AccesoDatosSalon.Opetarions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientContraller : ControllerBase
    {
        private ClientDAO clientDAO = new ClientDAO();
               
        [HttpGet("Clients")]
        public List<Client> GetAllClients() => clientDAO.getAll();

        [HttpPost("Client")]
        public bool addCliente([FromBody] Client client, string dni, string name, string phone, string email)
        {
            return clientDAO.addClient(client.Dni, client.FullName, client.Phone, client.Email);
        }

        [HttpPut("Client")]
        public bool updateClient([FromBody] Client client)
        {
            return clientDAO.updateClient(client.Id, client.Dni, client.FullName, client.Phone, client.Email, client.RegistrationDate);
        }

    }
    
}
