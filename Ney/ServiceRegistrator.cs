using Microsoft.Extensions.DependencyInjection;
using Ney.Service;
using Ney.Interface;

namespace Ney;

public static class ServiceRegistrator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); // Ajouter les controllers au pipeline

        // Injection du service dans le conteneur de dépendance
        services.AddSingleton<INeyService, NeyService>();
    }
}
