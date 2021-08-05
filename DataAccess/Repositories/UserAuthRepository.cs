using AutoMapper;
using DataAccess.Entities;
using DataTransfer.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class UserAuthRepository : Repository, IUserAuthRepository
    {
        public UserAuthRepository(ComuseContext comuseContext, IMapper mapper)
            : base(comuseContext, mapper)
        { }

        public async Task<UserDTO> AddUser(UserDTO user, UserCredentialsDTO userCredentials)
        {
            var userEntity = mapper.Map<UserEntity>(user);
            userEntity.Password = userCredentials.Password;
            userEntity.Salt = userCredentials.Salt;
            var added = db.Users.Add(userEntity);
            await db.SaveChangesAsync();
            return mapper.Map<UserDTO>(added.Entity);
        }

        public async Task<UserCredentialsDTO> GetUserCredentials(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user == null)
                return null;
            var credentials = new UserCredentialsDTO
            {
                Email = user.Email,
                Password = user.Password,
                Salt = user.Salt
            };
            return credentials;
        }

        public async Task<bool> CheckEmailUniqueness(string email)
        {
            bool exists = await db.Users.AnyAsync(u => u.Email == email);
            return !exists;
        }
    }
}
