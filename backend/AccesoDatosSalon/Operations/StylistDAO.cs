using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;

namespace AccesoDatosSalon.Opetarions
{
    public class StylistDAO
    {
        public PrettyGirlSalonContext contexto = new PrettyGirlSalonContext();

        public LoginDTO login(string user, string password)
        {
            var stylist = contexto.Stylists.Where(s => s.UserName.Equals(user) 
            && s.UserPassword.Equals(password)).Select(s=>new LoginDTO
            {
                UserName = s.UserName,
                Password = s.UserPassword,
            }).FirstOrDefault();
            return stylist;
        }

        public List<Stylist> selectStylist()
        {
            var estylists = contexto.Stylists.ToList<Stylist>();
            return estylists;
        }

        public Stylist getStylist (string userName)
        {
            var stylist = contexto.Stylists.Where(s => s.UserName == userName).FirstOrDefault();
            return stylist;

        }

        public bool addStylist(Stylist newStylist)
        {
            try
            {
                contexto.Stylists.Add(newStylist);
                contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateStylist(Stylist updatedStylist)
        {
            try
            {
                var existing = getStylist(updatedStylist.UserName);
                if (existing == null)
                {
                    Console.WriteLine("Estilista no encontrado");
                    return false;
                }

                // Actualiza solo los campos modificables (no el UserName que es el ID)
                existing.UserPassword = updatedStylist.UserPassword;
                existing.FullName = updatedStylist.FullName;
                existing.Specialty = updatedStylist.Specialty;
                existing.Email = updatedStylist.Email;
                existing.IsActive = updatedStylist.IsActive;

                contexto.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}"); // Para debugging
                return false;
            }
        }

        public bool deleteStylist(string userName)
        {
            try
            {
                var stylist = getStylist(userName);

                if (stylist == null)
                {
                    return false;
                }
                else
                {
                    contexto.Stylists.Remove(stylist);
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }
        }          
    }
}
