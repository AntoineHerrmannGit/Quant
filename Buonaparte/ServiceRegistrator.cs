using Buonaparte.Core;
using Buonaparte.Interface;
using Microsoft.Extensions.DependencyInjection;

namespace Buonaparte;

public static class ServiceRegistrator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); // Ajouter les controllers au pipeline

        // Injection du service dans le conteneur de dépendance
        services.AddSingleton<IBuonaparteService, BuonaparteService>();
    }
}
