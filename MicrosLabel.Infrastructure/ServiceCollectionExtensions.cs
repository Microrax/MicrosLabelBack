using MicrosLabel.Domain.Aggregates.ClientConfigurations;
using MicrosLabel.Domain.Aggregates.Clients;
using MicrosLabel.Domain.Aggregates.Labels;
using MicrosLabel.Domain.Events;
using MicrosLabel.Domain.Services;
using MicrosLabel.Infrastructure.Configurations;
using MicrosLabel.Infrastructure.CosmosDb;
using MicrosLabel.Infrastructure.Events;
using MicrosLabel.Infrastructure.Repositories;
using MicrosLabel.Infrastructure.Repositories.QueryRepositories;
using MicrosLabel.Infrastructure.Services;
using MicrosLabel.Infrastructure.Sql;
using MicrosLabel.Reads.Queries;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace MicrosLabel.Infrastructure
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCosmosDB(this IServiceCollection services)
        {
            var appConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<MicrosLabelConfiguration>>().Value;

            // Microsoft recommends a singleton client instance to be used throughout the application
            // https://docs.microsoft.com/en-us/dotnet/api/microsoft.azure.cosmos.cosmosclient?view=azure-dotnet#definition
            // "CosmosClient is thread-safe. Its recommended to maintain a single instance of CosmosClient per lifetime of the application which enables efficient connection management and performance"
            services.AddSingleton(c =>
            {
                var client = new CosmosClient(appConfiguration.AzureCosmosDB.ConnectionString,
                    new CosmosClientOptions
                    {
                        ConnectionMode = ConnectionMode.Direct,
                        ApplicationRegion = appConfiguration.ApplicationRegion,
                        SerializerOptions = new CosmosSerializationOptions() { IgnoreNullValues = true }
                    });

                return client.GetDatabase(appConfiguration.AzureCosmosDB.DatabaseName);
            });

            return services;
        }

        public static IServiceCollection AddReadConnectionStrings(this IServiceCollection services)
        {
            var appConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<MicrosLabelConfiguration>>().Value;
            return services.AddTransient(sp => new DbConnectionFactory(appConfiguration.LogisCoreConnectionString));
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddTransient<IClientsModelRepository, ClientRepository>();
            services.AddTransient<ILabelsModelRepository, LabelRepository>();
            services.AddTransient<ILabelConfigurationRepository, LabelConfigurationRepository>();
            services.AddTransient<IPrintService, PrintService>();
            services.AddTransient(typeof(CosmosDbRepository<,>));

            return services;
        }

        public static IServiceCollection AddQueryRepositories(this IServiceCollection services)
        {
            services.AddTransient<IClientQueryRepository, ClientQueryRepository>();
            services.AddTransient<ILabelQueryRepository, LabelQueryRepository>();
            services.AddTransient<ILabelConfigurationQueryRepository, LabelConfigurationQueryRepository>();
            services.AddTransient<ILabelPrintQueryRepository, LabelPrintQueryRepository>();

            return services;
        }

        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services)
        {

            services.AddScoped<IEventService, EventService>();

            return services;
        }
        
        public static IServiceCollection AddPrintingService(this IServiceCollection services)
        {
            var azureBlobContainerConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureBlobContainerConfiguration>>().Value;
            var azureServiceBusConfiguration = services.BuildServiceProvider().GetRequiredService<IOptions<AzureServiceBusConfiguration>>().Value;

            services.AddSingleton(new AzureBlobContainerManager(azureBlobContainerConfiguration));
            services.AddSingleton(new AzureServiceBusManager(azureServiceBusConfiguration));
            services.AddScoped<IPrintService, PrintService>();

            return services;
        }
    }
}
