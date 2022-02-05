using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shaper.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shaper.Web.Areas.User.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly AppSettings _appSettings;

        public AuthenticationService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;

        }

        public void Authentication(ApplicationUser user)
        {
            var tokenHandeler = new JwtSecurityTokenHandler();

            var tokenDescriptor = CreateTokenDescriptor(user);
            var token = tokenHandeler.CreateToken(tokenDescriptor);

        }

        public SecurityTokenDescriptor CreateTokenDescriptor(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.ShaperKey);
            return new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id)
                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }


    }
}
