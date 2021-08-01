using DataTransfer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories
{
    public interface IUserRepository
    {
        Task<UserDTO> AddUser(UserDTO user, string password);
        Task<UserDTO> GetUser(int id);
    }
}
