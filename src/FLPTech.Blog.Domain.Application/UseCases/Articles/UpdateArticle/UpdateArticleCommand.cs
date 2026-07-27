using Cortex.Mediator.Commands;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.UpdateArticle;

public class UpdateArticleCommand : ICommand<bool>
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}
