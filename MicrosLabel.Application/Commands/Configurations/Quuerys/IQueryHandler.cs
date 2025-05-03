namespace MicrosLabel.Application.Commands.Configuration.Quuerys;

public interface IQueryHandler<in TQuery, TRespose>
    where TQuery : IQuery<TRespose>
{
    Task<TRespose> HandleAsync(TQuery query);
}
