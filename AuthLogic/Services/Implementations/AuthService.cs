using AuthConfig;
using DataTransfer.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Repositories;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AuthLogic.Services.Implementations
{
    public class AuthService : IAuthService
    {
        IAuthOptions AuthOptions { get; }
        IUserAuthRepository UserAuthRepository { get; }
        IUserRepository UserRepository { get; }

        public AuthService(
            IAuthOptions authOptions,
            IUserAuthRepository userAuthRepository,
            IUserRepository userRepository)
        {
            AuthOptions = authOptions;
            UserAuthRepository = userAuthRepository;
            UserRepository = userRepository;
        }

        public async Task<string> GetJWT(string email, string password)
        {
            var credentials = await UserAuthRepository.GetUserCredentials(email);
            var verified = await PasswordHashing.VerifyPassword(password, credentials.Password, credentials.Salt);
            if (!verified)
                return null;
            var identity = await GetIdentity(email);
            var token = await GenerateToken(identity);
            return token;
        }

        public async Task<UserDTO> RegisterUser(string email, string password)
        {
            bool unique = await UserAuthRepository.CheckEmailUniqueness(email);
            if (!unique)
                return null;

            var hashAndSalt = await PasswordHashing.HashPassword(password);
            var userCredentirals = new UserCredentialsDTO
            {
                Email = email,
                Password = hashAndSalt.Item1,
                Salt = hashAndSalt.Item2
            };

            string defaultUsername = new string(email.TakeWhile(c => c != '@').ToArray());
            var userDTO = new UserDTO
            {
                Email = email,
                Name = defaultUsername,
                Image = null
            };

            var addedUser = await UserAuthRepository.AddUser(userDTO, userCredentirals);
            return addedUser;
        }

        private async Task<string> GenerateToken(ClaimsIdentity identity)
        {
            string encodedJwt = null;
            await Task.Run(() =>
            {
                DateTime now = DateTime.UtcNow;
                DateTime expires = now.Add(TimeSpan.FromMinutes(AuthOptions.Lifetime));
                var signingCredentials = new SigningCredentials(
                    AuthOptions.GetSymmetricSecurityKey(),
                    SecurityAlgorithms.HmacSha256);
                var jwt = new JwtSecurityToken(
                        issuer: AuthOptions.Issuer,
                        audience: AuthOptions.Audience,
                        notBefore: DateTime.Now,
                        claims: identity.Claims,
                        expires: expires,
                        signingCredentials: signingCredentials);
                var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);
            });
            return encodedJwt;
        }

        private async Task<ClaimsIdentity> GetIdentity(string email)
        {
            var user = await UserRepository.GetUser(email);
            if (user == null)
                return null;
            var claims = new List<Claim>
                {
                    new Claim("email", user.Email),
                    new Claim("name", user.Name)
                };
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Token");
            return claimsIdentity;
        }

    }
}
