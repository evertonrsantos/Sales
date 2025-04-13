namespace SalesApi.Application.Events.Contracts;

public interface IEventPublisher
{
    Task PublishAsync<T>(T @event) where T : class;
}
