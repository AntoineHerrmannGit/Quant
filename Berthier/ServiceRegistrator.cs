using Berthier.Interface;
using Berthier.Service;
using Microsoft.Extensions.DependencyInjection;

namespace Berthier;

public static class ServiceRegistrator
{
    public static void ConfigureServices(IServiceCollection services)
    {
        services.AddControllers(); // Ajouter les controllers au pipeline

        // Injection du service dans le conteneur de dépendance
        services.AddSingleton<IBerthierService, BerthierService>();
    }
}
