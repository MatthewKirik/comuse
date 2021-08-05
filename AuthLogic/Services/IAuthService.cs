using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IdentityModel.Tokens.Jwt;
using DataTransfer.Models;

namespace AuthLogic.Services
{
    public interface IAuthService
    {
        Task<string> GetJWT(string email, string password);
        Task<UserDTO> RegisterUser(string email, string password);
    }
}
