using System.Text;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace invoice_system.Utils.Configs;

public static class JwtConfig{
    public static AuthenticationBuilder AddJwtAuthentication(this IServiceCollection services , IConfiguration config){
        return services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>{
            options.TokenValidationParameters = new TokenValidationParameters {
                ValidateIssuer = true,
                ValidateAudience =true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = config["Jwt:Issuer"],
                ValidAudience = config["Jwt:Audience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"])),
                ClockSkew = TimeSpan.Zero
            };
            options.Events = new JwtBearerEvents {
                OnAuthenticationFailed = context => {
                    if(context.Exception is SecurityTokenExpiredException){
                        context.Response.Headers.Add("Token-Expired" , "true");
                    }
                    return Task.CompletedTask;
                }
            };
        });
    }
}