using BankApi.ActionClass.HelperClass.ValidateData;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using WEB_bankomat_statistic_api.ActionClass.HelperClass.Account;
using WEB_bankomat_statistic_api.Models;

namespace WEB_bankomat_statistic_api.Controllers
{
    [Route("api/")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly BankomatContext? _context;
        public IConfiguration _configuration;

        public TokenController(IConfiguration config, BankomatContext? context)
        {
            _configuration = config;
            _context = context;
        }

        [HttpPost("auth")]
        public async Task<IActionResult> Post(AccountAuth _userData)
        {
            if (_userData.Phone != null && _userData.Password != null)
            {
                var user = await GetUser(_userData.Phone, _userData.Password);


                if (user != null)
                {
                    _context.Roles.ToList();
                    var refreshToken = GenerateTokenRT();
                    var DateExpiryRt = DateTime.UtcNow.AddDays(3);

                    var claims = new[] {
                        new Claim(JwtRegisteredClaimNames.Sub, _configuration.GetSection("Jwt:Subject").Value),
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                        new Claim("Firstname", user.Firstname),
                        new Claim("Lastname", user.Lastname),
                        new Claim("Patronomic", user.Patronomic),
                        new Claim("Phone", user.Phone),
                        new Claim("Roles", user.Role.Name),
                        new Claim("RefreshToken", refreshToken.ToString()),
                        new Claim("ExpiryTimeRT", DateExpiryRt.ToString())
                    };

                    var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt:Secret").Value));
                    var signIn = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                    var token = new JwtSecurityToken(
                        _configuration.GetSection("Jwt:ValidIssuer").Value,
                        _configuration.GetSection("Jwt:ValidAudience").Value,
                        claims,
                        expires: DateTime.UtcNow.AddMinutes(1),
                        signingCredentials: signIn);
                    string tokenUser = new JwtSecurityTokenHandler().WriteToken(token);

                    _context.SaveChanges();

                    return Ok(tokenUser);
                }
                else
                {
                    return BadRequest("Данные неверны. Повторите авторизацию");
                }
            }
            else
            {
                return BadRequest();
            }
        }
        private static string GenerateTokenRT()
        {
            var randomNumber = new byte[32];
            using var rng = RandomNumberGenerator.Create();
            rng.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        /// <summary>
        /// Получение данных для генерации токена
        /// </summary>
        /// <param name="phone">Введите телефон пользователя</param>
        /// <param name="password">Введите пароль пользователя</param>
        /// <returns>Возвращает коллекцию по пользователю</returns>
        private async Task<User> GetUser(object phone, string password) => await _context.Users.FirstOrDefaultAsync(p => p.Phone == phone && p.Password == new ConvertertClassMD5(password).CreateMD5());
        

    }
}
