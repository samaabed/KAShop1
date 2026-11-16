using KASHOP.DAL.DTO.Requests;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL
{
    public interface IAuthService
    {

        Task<(bool Success, List<string> Errors)> RegisterAsync(RegisterRequest request);
        Task<(bool Success, List<string> Errors, LoginResponse Response)> LoginAsync(LoginRequest request);
        Task<(bool Success, string? Error)> ConfirmEmailAsync(string userId, string token);

    }
}
