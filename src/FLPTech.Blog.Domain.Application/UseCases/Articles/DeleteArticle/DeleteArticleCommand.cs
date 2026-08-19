using Cortex.Mediator.Commands;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.DeleteArticle;

public class DeleteArticleCommand : ICommand<bool>
{
    public Guid Id { get; set; }
}
