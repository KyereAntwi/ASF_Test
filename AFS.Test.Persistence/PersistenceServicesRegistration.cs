using AFS.Test.Application.Contracts.Persistence;
using AFS.Test.Persistence.Data;
using AFS.Test.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AFS.Test.Persistence;

public static class PersistenceServicesRegistration
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services,
        IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection") ??
                               throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
        
        services.AddDbContext<TranslationDbContext>(options =>
            options.UseSqlite(connectionString, b => b.MigrationsAssembly("AFS.Test.Presentation.WebApp")));
        
        services.AddScoped<ITranslationCallRepository, TranslationCallRepository>();

        return services;
    }
}