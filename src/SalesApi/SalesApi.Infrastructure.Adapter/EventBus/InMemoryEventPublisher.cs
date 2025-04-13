using System.Text.Json;
using Microsoft.Extensions.Logging;
using SalesApi.Application.Events.Contracts;

namespace SalesApi.Infrastructure.Adapter.EventBus;

public class InMemoryEventPublisher : IEventPublisher
{
    private readonly ILogger<InMemoryEventPublisher> _logger;
    private readonly JsonSerializerOptions _jsonSerializerOptions;

    public InMemoryEventPublisher(ILogger<InMemoryEventPublisher> logger)
    {
        _logger = logger;
        _jsonSerializerOptions = new()
        {
            WriteIndented = true,
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase
        };
    }

    public Task PublishAsync<T>(T @event) where T : class
    {
        var eventName = @event.GetType().Name;
        var eventJson = JsonSerializer.Serialize(@event, _jsonSerializerOptions);

        _logger.LogInformation(
            "Event published - Type: {EventType}\nContent: {EventContent}",
            eventName,
            Environment.NewLine + eventJson);

        return Task.CompletedTask;
    }
}
