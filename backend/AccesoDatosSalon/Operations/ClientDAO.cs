using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;

namespace AccesoDatosSalon.Opetarions
{
    public class ClientDAO
    {
        public PrettyGirlSalonContext contexto = new PrettyGirlSalonContext(); //crea instancia de contexto de base datos

        public List<Client> getAll() //Selecciona todos los clientes
        {
            var clients = contexto.Clients.ToList<Client>();
            return clients;
        }

        public Client getClient(int id) //selecciona cliente por ID
        {
            var client = contexto.Clients.Where(c => c.Id == id).FirstOrDefault(); //recorre la tabla hasta encontrar el id coincidente
            return client;
        }

        public bool addClient(string dni, string name, string phone, string email)
        {
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(name))
            {
                return false;
            }

            try
            {
                Client client = new Client();

                client.Dni = dni;
                client.FullName = name;
                client.Phone = phone;
                client.Email = email;
                client.RegistrationDate = DateOnly.FromDateTime(DateTime.Now);
                contexto.Clients.Add(client);
                contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateClient(int id, string dni, string name, string phone, string email, DateOnly fechaRegistro)
        {
            if (string.IsNullOrEmpty(dni) || string.IsNullOrEmpty(name))
            {
                return false;
            }

            try
            {
                var cliente = getClient(id);
                if (cliente == null)
                {
                    return false;
                }
                else
                {
                    cliente.Dni = dni;
                    cliente.FullName = name;
                    cliente.Phone = phone?? cliente.Phone; //Mantiene valor actual si phone es null
                    cliente.Email = email?? cliente.Email; //Mantiene valor actual si email es null                       
                    contexto.SaveChanges();
                    return true;                                        
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool deleteCliente(int id)
        {
            try
            {
                var cliente = getClient(id);
                if (cliente == null)
                {
                    return false; //cliente no existe
                }
                else
                {
                    contexto.Clients.Remove(cliente);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public List<Client> SearchClients(string name)
        {
            return contexto.Clients.Where(c=> c.FullName.Contains(name)).ToList();
        }

        public bool ClientExists(int id)
        {
            return contexto.Clients.Any(c => c.Id == id);
        }
    }
}
