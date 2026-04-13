using Cine.BusinessLogic;
using Cine.BusinessLogic.Abstractions;
using Cine.Repository;
using Cine.Repository.Abstractions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Cine.ServiceFactory;

public static class DependencyInjection
{
    public static IServiceCollection AddCineDependencies(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("CineDb")
            ?? throw new InvalidOperationException("Connection string 'CineDb' was not found.");

        services.AddDbContext<CineDbContext>(options =>
            options.UseSqlServer(connectionString));

        // Keeps existing repositories compatible while using AppDbContext as the main EF context.
        services.AddScoped<AppDbContext>(serviceProvider => serviceProvider.GetRequiredService<CineDbContext>());

        services.AddScoped<IMovieRepository, EfMovieRepository>();
        services.AddScoped<ISessionRepository, EfSessionRepository>();
        services.AddScoped<IMovieService, MovieService>();
        services.AddScoped<ISessionService, SessionService>();

        return services;
    }
}
