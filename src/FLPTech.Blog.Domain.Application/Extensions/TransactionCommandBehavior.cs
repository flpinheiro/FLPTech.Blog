using Cortex.Mediator.Commands;
using FLPTech.Blog.Domain.Services.Repositories;

namespace FLPTech.Blog.Domain.Application.Extensions;

internal class TransactionCommandBehavior<TCommand>(IUnitOfWork uow)
    : ICommandPipelineBehavior<TCommand>, IAsyncDisposable where TCommand : ICommand
{
    public async ValueTask DisposeAsync()
    {
        await DisposeAsync(true);
        GC.SuppressFinalize(this);
    }

    private async Task DisposeAsync(bool disposing)
    {
        if (disposing)
        {
            if (uow is IAsyncDisposable asyncDisposable)
            {
                await asyncDisposable.DisposeAsync();
            }
        }
    }

    public async Task Handle(
        TCommand command,
        CommandHandlerDelegate next,
        CancellationToken ct)
    {
        await uow.BeginTransactionAsync();
        try
        {
            await next(); // Execute command
            await uow.CommitTransactionAsync();
        }
        catch
        {
            await uow.RollbackTransactionAsync();
            throw;
        }
    }
}