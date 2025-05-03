using MicrosLabel.Api;
using MicrosLabel.Application;
using MicrosLabel.Infrastructure;
using MicrosLabel.Infrastructure.Configurations;
using Serilog;

Console.Title = "MicrosLabel Api";

var builder = WebApplication.CreateBuilder(args);

var startUpLogger = new LoggerConfiguration()
    .WriteTo.File("ApplicationStartup.log")
    .WriteTo.Console()
    .CreateLogger();

try
{
    // Add services to the container.
    builder.Services.AddConfigurationControllers();

    builder.Services
        .Configure<MicrosLabelConfiguration>(builder.Configuration)
        .Configure<AzureBlobContainerConfiguration>(builder.Configuration.GetSection("AzureBlobContainer"))
        .Configure<AzureServiceBusConfiguration>(builder.Configuration.GetSection("AzureServiceBus"))
        .AddHttpContextAccessor()
        .AddEndpointsApiExplorer()
        .AddSwaggerGen()
        .AddCosmosDB()
        .AddQueryHandler()
        .AddQueryRepositories()
        .AddCommands()
        .AddRepositories()
        .AddReadConnectionStrings()
        .AddInfraestructureServices()
        .AddPrintingService();

        builder.Services.AddCors(options =>
        options.AddDefaultPolicy(policy =>
            policy.WithOrigins("http://localhost:4200/client", "http://localhost:4200", "http://localhost:4200/", "'Access-Control-Allow-Origin'")
            .AllowCredentials()
            .AllowAnyHeader()
            .AllowAnyMethod()

        ));

    var app = builder.Build();

    app
        .UseRequestLocalization()
        .UseRouting()
        .UseHttpExceptionHandler()
        .UseCors();

    app.UseStaticFiles();
    app.UseAuthorization();

    if (!app.Environment.IsDevelopment())
    {
        app.UseHttpsRedirection();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseAuthorization();

    app.MapControllers();

    startUpLogger.Information($"***** App startup log finished at {DateTime.Now} *****");

    app.Run();
}
catch (Exception ex)
{
    startUpLogger.Fatal(ex, "Application startup failed");
    throw;
}