using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using DnsForItLearningLabs;

var host = new HostBuilder()
    .ConfigureFunctionsWebApplication(builder => {
        // Set default JSON Serialization options
        builder.Services.Configure<JsonSerializerOptions>(jsonSerializerOptions => {
            jsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
            jsonSerializerOptions.Converters.Add(JsonDateTimeConverter.Instance);
        });
    })
    .ConfigureServices(services => {
        services.AddApplicationInsightsTelemetryWorkerService();
        services.ConfigureFunctionsApplicationInsights();
    })
    .Build();

host.Run();
