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
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailService _emailService;

        public AuthService(UserManager<ApplicationUser> userManger, IConfiguration configuration, ITokenService tokenService, SignInManager<ApplicationUser> signInManager, IEmailService emailService)
        {
            _userManger = userManger;
            _configuration = configuration;
            _tokenService = tokenService;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        public async Task<(bool Success, List<string> Errors)> RegisterAsync(RegisterRequest request)
        {
            var user = request.Adapt<ApplicationUser>();

            var result = await _userManger.CreateAsync(user, request.Password);

            if (!result.Succeeded)
                return (false, result.Errors.Select(e => e.Description).ToList());

            var token = await _userManger.GenerateEmailConfirmationTokenAsync(user);
            var encodedToken = Uri.EscapeDataString(token);
            var link = $"https://localhost:7089/api/Auth/confirm-email?userId={user.Id}&token={encodedToken}";

            await _emailService.SendEmailAsync(user.Email, "Confirm Your Email", "" +
                "<div>"+
                $"<h2>Welcome {user.UserName}</h2>"+
                "<p>Please confirm your email by clicking on the link below</p>"+
                $"<a href='{link}'>Confirm Email</a>"+
                "</div>" 
                );

            await _userManger.AddToRoleAsync(user, "User");

            return (true, []);
        }

        public async Task<(bool Success, List<string> Errors, LoginResponse Response)> LoginAsync(LoginRequest request)
        {
            var user = await _userManger.FindByEmailAsync(request.Email);

            if (user is null)
                return (false, ["Invalid Email or Password"], null);

            var result = await _signInManager.CheckPasswordSignInAsync(user, request.Password, true);

            if (result.IsNotAllowed)
                return (false, ["Confirm your email"], null);

            if (result.IsLockedOut)
                return (false, ["Account is locked, try again later"], null);

            if (!result.Succeeded)
                return (false, ["Invalid Email or Password"], null);

            var token = await _tokenService.GenerateJwt(user);
            var response = new LoginResponse()
            {
                Token = token
            };

            return (true, [], response);

        }

        public async Task<(bool Success, string? Error)> ConfirmEmailAsync(string userId, string token)
        {
            var user = await _userManger.FindByIdAsync(userId);

            if (user is null)
                return (false, "Invalied confirmation link");

            if (user.EmailConfirmed)
                return (true, null);

            var result = await _userManger.ConfirmEmailAsync(user, token);

            return result.Succeeded ? (true, null) : (false, "Invalied or expired confirmation link");
        }
    }
}
