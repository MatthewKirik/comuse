using DataTransfer.Models;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IUserAuthRepository
    {
        Task<UserDTO> AddUser(UserDTO user, UserCredentialsDTO userCredentials);
        Task<bool> CheckEmailUniqueness(string email);
        Task<UserCredentialsDTO> GetUserCredentials(string email);
    }
}
