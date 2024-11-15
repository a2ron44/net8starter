using Amazon;
using Amazon.CognitoIdentityProvider;
using Amazon.Runtime;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Net8StarterAuthApi.Auth.Services;

namespace Net8StarterAuthApi.Auth;

public static class AuthStartupExtensions
{
    public static void ConfigureAuthWithAws(this IServiceCollection services, IConfiguration configuration)
    {
        var credentials = new BasicAWSCredentials(configuration["AWS:AWS_ACCESS_KEY_ID"],
            configuration["AWS:AWS_SECRET_ACCESS_KEY"]);
        services.AddSingleton<IAmazonCognitoIdentityProvider>(
            new AmazonCognitoIdentityProviderClient(credentials,
                RegionEndpoint.GetBySystemName(configuration["AWS:REGION"])));

        services.AddSingleton<CognitoAuthService>();

        var clientId = configuration["AWS:Cognito:ClientId"];
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["AWS:Cognito:Issuer"],
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = false,

                    ValidAudience = configuration["AWS:Cognito:Authority"],
                    // AudienceValidator = (audiences, securityToken, validationParameters) =>
                    // {
                    //     //This is necessary because Cognito tokens doesn't have "aud" claim. Instead the audience is set in "client_id"
                    //     var castedToken = securityToken as JwtSecurityToken;
                    //     var hasAud = castedToken!.Claims.Any(a => a.Type == "client_id" && a.Value == clientId);
                    //     return hasAud;
                    // },
                    ValidateLifetime = true
                };
                options.Authority = configuration["AWS:Cognito:Authority"];
                options.Audience = clientId;
            });
    }
}