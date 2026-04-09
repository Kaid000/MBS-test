using MBS_Task.Application.DependencyInjection;
using MBS_Task.Infrastructure.DataLoaders;
using Microsoft.Extensions.DependencyInjection;

namespace MBS_Task.Infrastructure.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, string defaultGridPath = null)
        {
            services.AddApplicationServices();
            services.AddSingleton(new TextGridLoader(defaultGridPath));

            return services;
        }
    }
}
