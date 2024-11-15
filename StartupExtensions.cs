using Microsoft.OpenApi.Models;

namespace Net8StarterAuthApi;

public static class StartupExtensions
{
    public static void AddSwaggerDocExtensions(this IServiceCollection services, IConfiguration configuration)
    {
        var appName = configuration.GetSection("App:AppName").Value;

        services.AddSwaggerGen(c =>
        {
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Description = "JWT Authorization Scheme Enter 'Bearer [space]' ",
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "0auth2",
                        Name = "Bearer",
                        In = ParameterLocation.Header
                    },
                    new List<string>()
                }
            });

            c.SwaggerDoc("v1", new OpenApiInfo { Title = appName, Version = "v1" });
        });
    }

    public static void AddCorsCustom(this IServiceCollection services, IConfiguration configuration)
    {
        var allowedHosts = configuration["AllowedHosts"] ?? throw new InvalidOperationException();
        services.AddCors(p =>
            p.AddPolicy("corsPolicy", b => { b.WithOrigins(allowedHosts).AllowAnyMethod().AllowAnyHeader(); }));
    }
}