using Cortex.Mediator.Commands;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.CreateArticle;

public class CreateArticleCommand : ICommand<Guid>
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}
