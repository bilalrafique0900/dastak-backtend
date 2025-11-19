using DastakWebApi.ConfigModels;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DastakWebApi.Extentions
{
    public static class JwtAuthExtentions
    {
        public static IServiceCollection AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            //Bind Jwt configuration with strongly typed
            services.Configure<JwtConfigurations>(configuration.GetSection(JwtConfigurations.Section));

            //Get configuration jwt object values
            var jwtConfig = services.BuildServiceProvider()
                                  .GetRequiredService<IOptions<JwtConfigurations>>()
                                  .Value;

            //Add Authentications
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                bearerOptions.SaveToken = true;
                bearerOptions.TokenValidationParameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SecurityKey))
                };
                bearerOptions.Events = new JwtBearerEvents
                {
                    OnMessageReceived = (context) =>
                    {
                        string accessToken = context.Request.Query["access_token"];
                        var url = context.HttpContext.Request.Path;
                        if (url.StartsWithSegments("/hub") && !string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };
            });
            return services;
        }
    }
}
