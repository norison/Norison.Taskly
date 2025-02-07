using Norison.Taskly.Todos.Api.Options;
using Norison.Taskly.Todos.Application;
using Norison.Taskly.Todos.Persistence;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;

namespace Norison.Taskly.Todos.Api;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
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

        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
            options.AddOtlpExporter();
            options.SetResourceBuilder(resourceBuilder);
        });

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