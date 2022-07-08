using BuyBerDinner.Application.Common.Interfaces.Authentication;
using BuyBerDinner.Application.Common.Interfaces.Services;
using BuyBerDinner.Application.Common.Persistence;
using BuyBerDinner.Infrastructure.Authentication;
using BuyBerDinner.Infrastructure.Persistence;
using BuyBerDinner.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BuyBerDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        ConfigurationManager builderConfiguration)
    {
        services.Configure<JwtSettings>(builderConfiguration.GetSection(JwtSettings.SectionName));
        
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();

        services.AddScoped<IUserRepository, UserRepository>();
        
        return services;
    }
}