using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ProTasker.Application.Interfaces.Services;
using ProTasker.Application.Models;
using ProTasker.Domain.Entities;
using ProTasker.Domain.Enums;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ProTasker.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        // Constructor
        public AuthService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        // Kullanıcı girişi ve JWT token üretme
        public async Task<string?> LoginAsync(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return null; // Kullanıcı bulunamadı
            }

            var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, false);
            if (!signInResult.Succeeded)
            {
                return null; // Hatalı şifre
            }

            // JWT token'ı oluştur
            var token = await GenerateJwtTokenAsync(user);
            return token;
        }

        // Kullanıcı kaydı ve giriş
        public async Task<bool> RegisterAsync(string email, string password)
        {
            var user = new ApplicationUser
            {
                UserName = email,
                Email = email,
                Role = UserRole.Member
            };

            var result = await _userManager.CreateAsync(user, password);
            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, "Member"); // Rol ata
                return true;
            }

            return false;
        }

        // JWT token üretme
        private async Task<string> GenerateJwtTokenAsync(ApplicationUser user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()), // Kullanıcı ID
                new Claim(JwtRegisteredClaimNames.Email, user.Email), // Kullanıcı email
                new Claim(ClaimTypes.Name, user.UserName), // Kullanıcı adı
            };

            // Kullanıcının rollerini al ve claim olarak ekle
            var roles = await _userManager.GetRolesAsync(user);  // Asenkron metod kullan
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role)); // Rol bilgisi
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var expireDays = int.Parse(_configuration["Jwt:ExpireDays"] ?? "1");
            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddDays(expireDays), // 1 gün geçerlilik süresi
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}