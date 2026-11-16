using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Mapster;
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
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManger;
        private readonly IConfiguration _configuration;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManger, IConfiguration configuration, ITokenService tokenService)
        {
            _userManger = userManger;
            _configuration = configuration;
            _tokenService = tokenService;
        }

        public async Task<(bool Success, List<string> Errors)> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();

            var result = await _userManger.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToList());

            await _userManger.AddToRoleAsync(user, "User");

            return (true, []);
        }

        public async Task<(bool Success, List<string> Errors, LoginResponse Reponse)> LoginAsync(LoginRequest request)
        {
            var user = await _userManger.FindByEmailAsync(request.Email);

            if (user is null)
                return (false, ["Invalid Email or Password"], null);

            var isValidPassowrd = await _userManger.CheckPasswordAsync(user, request.Password);

            if (!isValidPassowrd)
                return (false, ["Invalid Email or Password"], null);

            var token = await _tokenService.GenerateJwt(user);
            var response = new LoginResponse()
            {
                Token = token
            };

            return (true, [], response);

        }
    }
}
