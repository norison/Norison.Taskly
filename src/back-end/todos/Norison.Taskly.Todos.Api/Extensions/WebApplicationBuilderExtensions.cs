using Norison.Taskly.Todos.Api.Options;
using Norison.Taskly.Todos.Application;
using Norison.Taskly.Todos.Persistence;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Serilog;
using Serilog.Events;

namespace Norison.Taskly.Todos.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Norison", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            .WriteTo.Console()
            .WriteTo.OpenTelemetry(options =>
            {
                options.ResourceAttributes = new Dictionary<string, object>
                {
                    ["service.name"] = "todos-api"
                };
            })
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Services.AddSerilog();
        builder.Services.AddOpenApi();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddApplication();
        builder.AddOptions();
        builder.AddOpenTelemetry();
        return builder;
    }

    private static void AddOptions(this WebApplicationBuilder builder)
    {
        builder.Services
            .AddOptions<ApplicationOptions>()
            .BindConfiguration("Application")
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }

    private static void AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        var serviceName = builder.Configuration["Application:Name"]!;
        var resourceBuilder = ResourceBuilder.CreateDefault().AddService(serviceName);

        builder.Services
            .AddOpenTelemetry()
            .WithMetrics(options =>
            {
                options.AddHttpClientInstrumentation();
                options.AddAspNetCoreInstrumentation();
                options.AddOtlpExporter();
                options.SetResourceBuilder(resourceBuilder);
            })
            .WithTracing(options =>
            {
                options.AddHttpClientInstrumentation();
                options.AddAspNetCoreInstrumentation();
                options.AddEntityFrameworkCoreInstrumentation();
                options.AddOtlpExporter();
                options.SetResourceBuilder(resourceBuilder);
            });
    }
}