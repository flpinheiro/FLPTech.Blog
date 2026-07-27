using Cortex.Mediator.Commands;
using FLPTech.Blog.Domain.Services.Repositories;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.DeleteArticle;

public class DeleteArticleCommand : ICommand<bool>
{
    public Guid Id { get; set; }
}


public class DeleteArticleCommandHandler(IUnitOfWork uow) : ICommandHandler<DeleteArticleCommand, bool>
{
    public async Task<bool> Handle(DeleteArticleCommand command, CancellationToken cancellationToken)
    {
        var article = await uow.Articles.GetByIdAsync(command.Id, cancellationToken);
        if (article == null)
        {
            return false;
        }
        uow.Articles.Delete(article);
        await uow.SaveChangesAsync(cancellationToken);
        return true;
    }
}