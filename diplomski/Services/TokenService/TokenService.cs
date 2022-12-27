using diplomski.Data.Context;
using diplomski.Data.Dtos;
using diplomski.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace diplomski.Services.TokenService
{
    public class TokenService : ITokenService
    {
        private IConfiguration _config;
        private readonly ApplicationDbContext _dbContext;
        public TokenService(IConfiguration config, ApplicationDbContext dbContext)
        {
            _config = config;
            _dbContext = dbContext;
        }

        public async Task<bool> AuthenticateUser(AdminInputDataDto data)
        {
            AdminData? adminData = await _dbContext.AdminDatas.FirstOrDefaultAsync(x => x.Email == data.Email && x.Password == data.Password);
            if (adminData != null)
                return true;

            return false;
        }

        public string GenerateJwt(AdminInputDataDto data)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[] {
                 new Claim(JwtRegisteredClaimNames.Sub, data.Email),
            };

            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
              _config["Jwt:Issuer"],
              claims,
              expires: DateTime.Now.AddMinutes(120),
              signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
