namespace Application.UseCases.UseCases;

public interface ICommandHandler<TCommand, TResult> where TCommand : ICommand<TResult>
{
    Task<TResult> Handle( TCommand command );
}