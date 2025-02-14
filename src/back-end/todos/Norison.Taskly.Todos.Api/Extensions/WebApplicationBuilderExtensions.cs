using Norison.Taskly.Todos.Api.Options;
using Norison.Taskly.Todos.Application;
using Norison.Taskly.Todos.Persistence;

using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

using Serilog;

namespace Norison.Taskly.Todos.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.OpenTelemetry(options =>
            {
                options.ResourceAttributes = new Dictionary<string, object> { ["service.name"] = "Taskly-Todos" };
            })
            .CreateLogger();

        builder.Logging.ClearProviders();
        builder.Services.AddSerilog();
        builder.Services.AddOpenApi();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddApplication();
        builder.AddOpenTelemetry();

        builder.Services.AddProblemDetails();
        builder.Services.AddRequestLocalization(options =>
        {
            options.SetDefaultCulture("en");
            options.AddSupportedCultures("en", "uk");
            options.AddSupportedUICultures("en", "uk");
        });
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
        builder.Services
            .AddOpenTelemetry()
            .ConfigureResource(resource => resource.AddService("Taskly-Todos"))
            .WithMetrics(options =>
            {
                options.AddHttpClientInstrumentation();
                options.AddAspNetCoreInstrumentation();
                options.AddOtlpExporter();
            })
            .WithTracing(options =>
            {
                options.AddHttpClientInstrumentation();
                options.AddAspNetCoreInstrumentation();
                options.AddEntityFrameworkCoreInstrumentation("todos-db", efOptions =>
                {
                    efOptions.SetDbStatementForText = true;
                    efOptions.SetDbStatementForStoredProcedure = true;
                });
                options.AddOtlpExporter();
            });
    }
}