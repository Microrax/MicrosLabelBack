using MicrosLabel.Application.Commands.Configuration;

namespace MicrosLabel.Application.Commands.Configuration.Quuerys;

public sealed class QueryBus : IQueryBus
{
    private readonly IServiceProvider _serviceProvider;

    public QueryBus(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<TResponse> SendAsync<TQuery, TResponse>(TQuery query) where TQuery : IQuery<TResponse>
    {
        if (query is null)
        {
            throw new ArgumentNullException(nameof(query));
        }

        var handler = _serviceProvider.GetService(typeof(IQueryHandler<TQuery, TResponse>)) as IQueryHandler<TQuery, TResponse>;

        if (handler is null)
        {
            throw new ArgumentNullException(nameof(handler));
        }

        return await handler.HandleAsync(query);
    }
}
