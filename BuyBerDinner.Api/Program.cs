using BuyBerDinner.Api.Common.Errors;
using BuyBerDinner.Application.Common;
using BuyBerDinner.Infrastructure;
using Microsoft.AspNetCore.Mvc.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
    
    //builder.Services.AddControllers(options => options.Filters.Add<ErrorHandlingFilterAttribute>());
    builder.Services.AddControllers();
    
    builder.Services.AddSingleton<ProblemDetailsFactory, DinnerProblemDetailsFactory>();
}

var app = builder.Build();
{
    // app.UseMiddleware<ErrorHandlingMiddleware>();
    app.UseExceptionHandler("/error");
    
    // for minimal API use only this code below
    // app.Map("/error", (HttpContext httpContext) =>
    // {
    //     Exception? exception = httpContext.Features.Get<IExceptionHandlerFeature>()?.Error;
    //
    //     return Results.Problem();
    // });

    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}