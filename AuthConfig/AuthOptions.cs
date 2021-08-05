using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text;

namespace AuthConfig
{
    public class AuthOptions : IAuthOptions
    {
        public string Issuer { get; private set; }
        public string Audience { get; private set; }
        public string Key { get; private set; }
        public int Lifetime { get; private set; }
        public IConfiguration Configuration { get; }

        public AuthOptions(IConfiguration configuration)
        {
            Configuration = configuration;
            InitializeOptions();
        }

        private void InitializeOptions()
        {
            var authConfig = Configuration.GetSection("Auth");
            Issuer = authConfig["JWT_issuer"];
            Audience = authConfig["JWT_audience"];
            Key = authConfig["JWT_KEY"];
            Lifetime = Convert.ToInt32(authConfig["JWT_lifetime"]);
        }

        public SymmetricSecurityKey GetSymmetricSecurityKey()
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Key));
        }
    }
}
