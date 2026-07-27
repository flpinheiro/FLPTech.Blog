using Cortex.Mediator.Commands;
using FLPTech.Blog.Domain.Application.Mappers;
using FLPTech.Blog.Domain.Services.Repositories;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.CreateArticle;

public class CreateArticleCommandHandler(IUnitOfWork unitOfWork) : ICommandHandler<CreateArticleCommand, Guid>
{
    public async Task<Guid> Handle(CreateArticleCommand command, CancellationToken cancellationToken)
    {
        var mapper = new ArticlesMappers();
        var article = mapper.CreateArticleCommandToArticle(command);

        unitOfWork.Articles.Add(article);
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return article.Id;
    }
}
