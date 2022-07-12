using BuyBerDinner.Application.Services.Authentication.Commands;
using BuyBerDinner.Application.Services.Authentication.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace BuyBerDinner.Application.Common;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddScoped<IAuthenticationCommandService, AuthenticationCommandService>();
        services.AddScoped<IAuthenticationQueryService, AuthenticationQueryService>();

        return services;
    }
}