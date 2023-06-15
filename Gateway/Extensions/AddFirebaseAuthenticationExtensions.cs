using Gateway.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;

namespace Gateway.Extensions
{
    public static class AddFirebaseAuthenticationExtensions
    {
        private static readonly ILoggerFactory loggerFactory = LoggerFactory.Create(builder =>
            builder.AddEventLog()
        );
        private static readonly ILogger _logger = loggerFactory.CreateLogger("AddFirebaseAuthenticationExtensions");
        public static IServiceCollection AddFirebaseAuthentication(this IServiceCollection services)
        {
            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddScheme<AuthenticationSchemeOptions, FirebaseAuthenticationHandler>(JwtBearerDefaults.AuthenticationScheme, (o) => {
                    o.Events = new JwtBearerEvents()
                    {
                        OnAuthenticationFailed = c =>
                        {
                            _logger.LogInformation($"Authentication failed for token with user email: '{c.HttpContext.GetFirebaseUser().Email}'");
                            return Task.CompletedTask;
                        },
                        OnChallenge = c =>
                        {
                            _logger.LogInformation($"Jwt Bearer challenged for token with user email: '{c.HttpContext.GetFirebaseUser().Email}'");
                            return Task.CompletedTask;
                        }

                    };
                });

            services.AddScoped<FirebaseAuthenticationFunctionHandler>();

            return services;
        }
    }
}
