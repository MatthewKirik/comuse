using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthConfig
{
    public interface IAuthOptions
    {
        string Audience { get; }
        IConfiguration Configuration { get; }
        string Issuer { get; }
        string Key { get; }
        int Lifetime { get; }

        SymmetricSecurityKey GetSymmetricSecurityKey();
    }
}