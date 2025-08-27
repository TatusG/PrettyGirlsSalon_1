using AccesoDatosSalon.Models;
using AccesoDatosSalon.Operations;
using AccesoDatosSalon.Opetarions;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using WebAPI.DTOS;

namespace WebAPI.Services
{
    public class StylistService
    {
        private readonly StylistDAO _stylistDAO;
        private string claveSecreta;

        public StylistService(StylistDAO stylistDAO, IConfiguration configuration)
        {
            _stylistDAO = stylistDAO;
            claveSecreta = configuration.GetValue<string>("ApiSettings:Secreta");
        }

        public async Task<bool> AddStylist(StylistRegisteredDTO stylistRegistered)
        {
            var stylist = new Stylist
            {
                UserName = stylistRegistered.UserName,
                UserPassword = stylistRegistered.UserPassword,
                FullName = stylistRegistered.FullName,
                Specialty = stylistRegistered.Specialty,
                Email = stylistRegistered.Email                
            };
            return await _stylistDAO.addStylist(stylist);
        }

        public async Task<StylistLoginResponseDTO?> login(LoginDTO loginDTO)
        {
            var stylist = await _stylistDAO.login(loginDTO.UserName, loginDTO.Password);
            if (stylist == null) return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(claveSecreta);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, stylist.UserName),  
                    new Claim("FullName", stylist.FullName),
                    new Claim("Specialty", stylist.Specialty),
                    new Claim("Email", stylist.Email)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            return new StylistLoginResponseDTO
            {
                Usuario = stylist,
                Token = tokenString
            };
        }

        public async Task <bool> updateStylistPassword(UpdatePasswordDTO updatePasswordDTO)
        {
            var stylist = await _stylistDAO.login(updatePasswordDTO.UserName, updatePasswordDTO.OldPassword);
            if (stylist == null) return false;

            return await _stylistDAO.updatePassword(updatePasswordDTO.UserName, updatePasswordDTO.NewPassword);
        }

        public async Task <StylistResponseDTO?> getStylist(string userName)
        {
            var stylist = await _stylistDAO.getStylist(userName);
            if (stylist == null) return null;   

            return new StylistResponseDTO
            {
                UserName = stylist.UserName,
                FullName = stylist.FullName,
                Specialty = stylist.Specialty,
                Email = stylist.Email,
                IsActive = (bool)stylist.IsActive
            };
        }

        public async Task<List<StylistResponseDTO>> getStylists(bool activeOnly = true)
        {
            var stylists = await _stylistDAO.selectStylist(activeOnly);
            return stylists.Select(s => new StylistResponseDTO
            {
                UserName = s.UserName,
                FullName = s.FullName,
                Specialty = s.Specialty,
                Email = s.Email,
                IsActive = (bool)s.IsActive
            }).ToList();
        }

        public async Task<bool> updateStylist(StylistUpdateDTO updatedStylist)
        {
            var existingStylist = await _stylistDAO.getStylist(updatedStylist.UserName);
            if (existingStylist == null) return false;

            var stylist = new Stylist
            {
                UserName = updatedStylist.UserName,
                FullName = updatedStylist.FullName ?? existingStylist.FullName,
                Specialty = updatedStylist.Specialty ?? existingStylist.Specialty,
                Email = updatedStylist.Email ?? existingStylist.Email,
                IsActive = updatedStylist.IsActive ?? existingStylist.IsActive,
                UserPassword = string.IsNullOrEmpty(updatedStylist.NewPassword)? existingStylist.UserPassword : updatedStylist.NewPassword
            };

            return await _stylistDAO.updateStylist(stylist);
        }

        public async Task<bool> deleteStylist(string userName)
        {
            return await _stylistDAO.deleteStylist(userName);
        }

        public async Task<bool> ToggleStylistStatus(string userName, bool? newStatus = null)
        {
            var stylist = await _stylistDAO.getStylist(userName);
            if (stylist == null) return false;

            var updateDto = new StylistUpdateDTO
            {
                UserName = userName,
                IsActive = newStatus ?? !stylist.IsActive
            };

            return await updateStylist(updateDto);
        }        
    }
}