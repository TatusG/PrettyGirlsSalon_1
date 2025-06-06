using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using AccesoDatosSalon.Models.DTOS;

namespace AccesoDatosSalon.Opetarions
{
    public class StylistDAO
    {
        public PrettyGirlSalonContext contexto = new PrettyGirlSalonContext();

        public Stylist login(string user, string password)
        {
            var stylist = contexto.Stylists.Where(e => e.UserName == user && e.UserPassword == password).FirstOrDefault();            
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

        public bool addStylist(string userName, string password, string name, string speciality, string email, bool active)
        {
            try
            {
                Stylist stylist = new Stylist();
                stylist.UserName = userName;
                stylist.UserPassword = password;
                stylist.FullName = name;
                stylist.Specialty = speciality;
                stylist.Email = email;
                stylist.IsActive = active;

                contexto.Stylists.Add(stylist);
                contexto.SaveChanges();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public bool updateStylist(string userName, string password, string name, string speciality, string email, bool active)
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
                    stylist.UserName = userName;
                    stylist.UserPassword = password;
                    stylist.FullName = name;
                    stylist.Specialty = speciality;
                    stylist.Email = email;
                    stylist.IsActive = active;
                    contexto.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
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
