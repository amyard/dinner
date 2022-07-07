using BuyBerDinner.Application.Common.Interfaces.Authentication;
using BuyBerDinner.Application.Common.Interfaces.Services;
using BuyBerDinner.Infrastructure.Authentication;
using BuyBerDinner.Infrastructure.Authentication.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BuyBerDinner.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        return services;
    }
}