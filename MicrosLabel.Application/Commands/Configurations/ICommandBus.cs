namespace MicrosLabel.Application.Commands.Configuration;

public interface ICommandBus
{
    Task SendAsync<TCommand>(TCommand command) where TCommand : ICommand;

}
