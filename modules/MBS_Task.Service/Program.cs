using MBS_Task.Application.Interfaces;
using MBS_Task.Infrastructure.DataLoaders;
using MBS_Task.Infrastructure.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace MBS_Task
{
    internal static class Program
    {
        internal static void Main(string[] args)
        {
            int result = Run(args, Console.In, Console.Out);
            Environment.ExitCode = 0;
        }

        private static int Run(string[] args, TextReader reader, TextWriter writer)
        {
            var services = new ServiceCollection();
            services.AddInfrastructureServices();
            var serviceProvider = services.BuildServiceProvider();

            string gridPath = null;

            if (args.Length == 2 && args[0] == "--input")
            {
                gridPath = args[1];
                if (!File.Exists(gridPath))
                {
                    writer.WriteLine($"Файл не найден: {gridPath}");
                    return -1;
                }
                reader = new StreamReader(gridPath);
            }
            else
            {
                writer.WriteLine("Введите Данные:");
            }

            try
            {
                var loader = serviceProvider.GetRequiredService<TextGridLoader>();
                var grid = loader.Load(reader);

                var pathFinder = serviceProvider.GetRequiredService<IPathFinder>();
                int shortestPath = pathFinder.FindShortestPath(grid);

                writer.WriteLine(shortestPath);
                return shortestPath;
            }
            catch
            {
                writer.WriteLine("-1");
                return -1;
            }
        }
    }
}