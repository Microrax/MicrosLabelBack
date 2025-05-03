namespace MicrosLabel.Application.Commands.Configuration.Quuerys;

public interface IQueryBus
{

    Task<TResponse> SendAsync<TQuery, TResponse>(TQuery query)
        where TQuery : IQuery<TResponse>;
}
