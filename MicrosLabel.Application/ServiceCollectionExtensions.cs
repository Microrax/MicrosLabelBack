using MicrosLabel.Application.Commands.Configuration;
using MicrosLabel.Application.Commands.Configuration.Quuerys;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MicrosLabel.Application
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCommands(this IServiceCollection services)
        {
            services.AddScoped<ICommandBus, CommandBus>();
            services.AddScoped<IQueryBus, QueryBus>();

            var handlers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    x.IsClass &&
                    !x.IsAbstract &&
                    x.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        (i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) 
                            || i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>))));

            foreach (var handler in handlers)
            {
                var generic = handler
                    .GetInterfaces()
                    .Single(i => i.GetGenericTypeDefinition() == typeof(ICommandHandler<>) ||
                        i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

                services.AddScoped(generic, handler);
            }

            return services;
        }

        public static IServiceCollection AddQueryHandler(this IServiceCollection services)
        {
            services.AddScoped<IQueryBus, QueryBus>();

            var handlers = Assembly.GetExecutingAssembly().GetTypes()
                .Where(x =>
                    x.IsClass &&
                    !x.IsAbstract &&
                    x.GetInterfaces().Any(i =>
                        i.IsGenericType &&
                        i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>)));

            foreach (var handler in handlers)
            {
                var generic = handler
                    .GetInterfaces()
                    .Single(i => i.GetGenericTypeDefinition() == typeof(IQueryHandler<,>));

                services.AddScoped(generic, handler);
            }

            return services;
        }
    }
}
