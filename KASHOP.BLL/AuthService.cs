using KASHOP.DAL.DTO.Requests;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL
{
    public class AuthService : IAuthService
    {
        public Task<(bool Success, List<string> Errors)> RegisterAsync(RegisterRequest request)
        {
            
        }
    }
}
