using MBS_Task.Application.Interfaces;
using MBS_Task.Application.Services.NeighborProviders;
using MBS_Task.Application.Services.PathFinders;
using MBS_Task.Application.Services.Strategies;
using Microsoft.Extensions.DependencyInjection;

namespace MBS_Task.Application.DependencyInjection
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<ICostStrategy, DefaultCostStrategy>();
            services.AddSingleton<INeighborProvider, GridNeighborProvider>();
            services.AddSingleton<IPathFinder, DijkstraPathFinder>();

            return services;
        }
    }
}
