using AutoMapper;
using DataAccess.Entities;
using DataTransfer.Models;
using Microsoft.EntityFrameworkCore;
using Repositories;
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

        public async Task<UserDTO> GetUser(int id)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Id == id);
            return mapper.Map<UserDTO>(user);
        }
        public async Task<UserDTO> GetUser(string email)
        {
            var user = await db.Users.FirstOrDefaultAsync(u => u.Email == email);
            return mapper.Map<UserDTO>(user);
        }
    }
}
