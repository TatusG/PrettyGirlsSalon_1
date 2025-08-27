using AccesoDatosSalon.Models;
using AccesoDatosSalon.Opetarions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebAPI.DTOS;

namespace WebAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private ClientDAO clientDAO = new ClientDAO();
               
        [HttpGet("clients")]
        public List<Client> GetAllClients() => clientDAO.getAll();

        [HttpPost("client")]
        public bool addCliente([FromBody] Client client)
        {
            if (client == null)
            {
                return false;
            }
            else
            {
                return clientDAO.addClient(client.Dni, client.FullName, client.Phone, client.Email);
            }                
        }

        [HttpPut("client")]
        public async Task <bool> updateClient(int id, [FromBody] Client client)
        {
            if (client == null || id != client.Id)
            {
                return false;
            }
            else
            {
                return await clientDAO.updateClient(
                    client.Id, 
                    client.Dni, 
                    client.FullName, 
                    client.Phone, 
                    client.Email, 
                    client.RegistrationDate
                    );
            }            
        }

        [HttpDelete("client")]
        public async Task <bool> deleteClient(int id)
        {
            return await clientDAO.deleteCliente(id);
        }

        [HttpGet("search")]
        public List<Client> seachClients(string name)
        {
            return clientDAO.SearchClients(name);
        }

    }
    
}
