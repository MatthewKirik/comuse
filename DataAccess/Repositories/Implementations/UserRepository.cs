using AutoMapper;
using DataAccess.Entities;
using DataTransfer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repositories.Implementations
{
    public class UserRepository : Repository, IUserRepository
    {
        public UserRepository(ComuseContext comuseContext, IMapper mapper)
            : base(comuseContext, mapper)
        { }

        public async Task<UserDTO> AddUser(UserDTO user, string password)
        {
            var userEntity = mapper.Map<UserEntity>(user);
            userEntity.Password = password;
            var added = db.Users.Add(userEntity);
            await db.SaveChangesAsync();
            return mapper.Map<UserDTO>(added);
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            return mapper.Map<UserDTO>(user);
        }
    }
}
