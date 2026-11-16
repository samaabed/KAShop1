using KASHOP.DAL.DTO.Requests;
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
    }
}
