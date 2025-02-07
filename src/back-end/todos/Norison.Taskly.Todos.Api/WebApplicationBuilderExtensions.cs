using Norison.Taskly.Todos.Application;
using Norison.Taskly.Todos.Persistence;

using OpenTelemetry.Logs;
using OpenTelemetry.Metrics;
using OpenTelemetry.Trace;

namespace Norison.Taskly.Todos.Api;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddApi(this WebApplicationBuilder builder)
    {
        builder.Services.AddOpenApi();
        builder.Services.AddPersistence(builder.Configuration);
        builder.Services.AddApplication();
        builder.AddOpenTelemetry();
        return builder;
    }

    private static void AddOpenTelemetry(this WebApplicationBuilder builder)
    {
        builder.Logging.AddOpenTelemetry(options =>
        {
            options.IncludeScopes = true;
            options.IncludeFormattedMessage = true;
            options.AddOtlpExporter();
        });

        builder.Services
            .AddOpenTelemetry()
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
                options.AddOtlpExporter();
            })
            .WithLogging();
    }
}