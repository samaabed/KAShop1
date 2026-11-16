using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL
{
    public class TokenService : ITokenService
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IConfiguration _configuration;

        public TokenService(UserManager<ApplicationUser> userManger, IConfiguration configuration)
        {
            _userManger = userManger;
            _configuration = configuration;
        }

        public async Task<string> GenerateJwt(ApplicationUser user)
        {
            var roles = await _userManger.GetRolesAsync(user);
            var userClaims = new List<Claim>()
            {
                new Claim("Id", user.Id),
                new Claim("UserName", user.UserName),
                new Claim("Email", user.Email),
                new Claim("Roles", string.Join(",", roles))
            };

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: userClaims,
                expires: DateTime.Now.AddDays(5),
                signingCredentials: credentials
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
