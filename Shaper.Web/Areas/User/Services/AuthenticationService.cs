using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Shaper.DataAccess.IdentityContext;
using Shaper.Models.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Shaper.Web.Areas.User.Services
{
    public class AuthenticationService : IAuthenticationService
    {

        private readonly AppSettings _appSettings;

        private readonly IdentityAppDbContext _db;

        public AuthenticationService(IOptions<AppSettings> appSettings, IdentityAppDbContext db)
        {
            _appSettings = appSettings.Value;
            _db = db;
        }

        public async Task HandingOverToken(ApplicationUser user)
        {
            await VerifyingUserRole(user);
            var tokenHandeler = new JwtSecurityTokenHandler();
            var tokenDescriptor = CreateTokenDescriptor(user);
            var token = tokenHandeler.CreateToken(tokenDescriptor);
            user.Token = tokenHandeler.WriteToken(token);
        }

        private SecurityTokenDescriptor CreateTokenDescriptor(ApplicationUser user)
        {
            var key = Encoding.ASCII.GetBytes(_appSettings.ShaperKey);

            return new()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.Id),
                    new Claim(ClaimTypes.Role, user.Role)

                }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
        }

        private async Task VerifyingUserRole(ApplicationUser user)
        {
            var roleId = await _db.UserRoles.FirstOrDefaultAsync(x => x.UserId == user.Id);
            var role = await _db.Roles.FirstOrDefaultAsync(x => x.Name == roleId.RoleId);
            user.Role = role.Name;
        }


    }
}
