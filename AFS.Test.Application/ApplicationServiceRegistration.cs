using AFS.Test.Application.Profiles;
using Microsoft.Extensions.DependencyInjection;

namespace AFS.Test.Application;

public static class ApplicationServiceRegistration
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
        services.AddMediatR(cfg => 
            cfg.RegisterServicesFromAssemblyContaining<MappingProfiles>());

        return services;
    }
}