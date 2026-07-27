using Cortex.Mediator.Commands;
using FLPTech.Blog.Domain.Services.Repositories;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.UpdateArticle;

public class UpdateArticleCommandHandler(IUnitOfWork uow) : ICommandHandler<UpdateArticleCommand, bool>
{
    public async Task<bool> Handle(UpdateArticleCommand command, CancellationToken cancellationToken)
    {
        var article = await uow.Articles.GetByIdAsync(command.Id, cancellationToken);
        if (article == null)
            return false;

        article.Title = command.Title;
        article.Content = command.Content;

        uow.Articles.Update(article);

        await uow.SaveChangesAsync(cancellationToken);

        return true;
    }
}
