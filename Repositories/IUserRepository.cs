using DataTransfer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> GetUser(int id);
        Task<UserDTO> GetUser(string email);
    }
}
