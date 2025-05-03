namespace MicrosLabel.Application.Commands.Configuration;

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}


