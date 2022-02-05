using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Shaper.Web.AuthenticationOptions
{
    public static class ConfigureAuthentication
    {
        public static void AddShaperAuthentication(this WebApplicationBuilder builder)
        {
            var section = builder.Configuration.GetSection("AppSettings");
            builder.Services.Configure<AppSettings>(section);
            var settings = section.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(settings.ShaperKey);

            builder.AddingShaperAuthentication().AddingShaperJwtBearer(key);
        }

        private static AuthenticationBuilder AddingShaperAuthentication(this WebApplicationBuilder builder)
        {
            return builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            });
        }

        private static AuthenticationBuilder AddingShaperJwtBearer(this AuthenticationBuilder builder, byte[] key)
        {
            return builder.AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }
    }
}
