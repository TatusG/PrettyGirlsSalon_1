using AccesoDatosSalon.Context;
using AccesoDatosSalon.Models;
using AccesoDatosSalon.Plugins;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccesoDatosSalon.Operations
{
    public class StylistDAO
    {
        private readonly PrettyGirlSalonContext context = new PrettyGirlSalonContext();
                
        public async Task<Stylist> login(string user, string password)
        {
            string passwordHash = HashUtil.ObtenerMD5(password);
            var styl = context.Stylists.Where(s => s.UserName == user && s.UserPassword == passwordHash).FirstOrDefault();
            return styl;
        }

        public async Task <bool> addStylist(Stylist stylist)
        {
            bool exists = context.Stylists.Any(s => s.UserName == stylist.UserName);

            if (exists)
            {                
                return false;
            }
            
            stylist.UserPassword = HashUtil.ObtenerMD5(stylist.UserPassword);
            context.Stylists.Add(stylist);
            context.SaveChanges();
            return true;
        }

        public async Task<bool> updatePassword(string user, string newPassword)
        {
            var stylist = context.Stylists.FirstOrDefault(s => s.UserName == user);

            if (stylist == null) return false;

            stylist.UserPassword = HashUtil.ObtenerMD5(newPassword);
            context.SaveChanges();
            return true;
        }

        public async Task <bool> updateStylist(Stylist updateStylist)
        {
            try
            {
                var stylist = context.Stylists.FirstOrDefault( s => s.UserName == updateStylist.UserName);
                if (stylist == null)
                {
                    Console.WriteLine("Estilista no encontrado");
                    return false;
                }

                // Actualiza solo los campos modificables (no el UserName que es el ID)                
                stylist.FullName = updateStylist.FullName;
                stylist.Specialty = updateStylist.Specialty;
                stylist.Email = updateStylist.Email;
                stylist.IsActive = updateStylist.IsActive;

                if (!string.IsNullOrEmpty(updateStylist.UserPassword))
                {
                    stylist.UserPassword = HashUtil.ObtenerMD5(updateStylist.UserPassword);
                }
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}"); // Para debugging
                return false;
            }
        }

        public async Task<List<Stylist>> selectStylist(bool active)
        {
            return context.Stylists.Where(s => s.IsActive == active).ToList();
        }

        public async Task<Stylist> getStylist(string userName)
        {
            return context.Stylists.FirstOrDefault(s => s.UserName == userName);
        }

        public async Task<bool> deleteStylist(string userName)
        {
            try
            {
                var stylist = context.Stylists.FirstOrDefault(s => s.UserName == userName);
                if (stylist == null)
                {
                    return false;
                }

                context.Stylists.Remove(stylist);
                context.SaveChanges();
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al eliminar: {ex.Message}");
                return false;
            }
        }
    }
}