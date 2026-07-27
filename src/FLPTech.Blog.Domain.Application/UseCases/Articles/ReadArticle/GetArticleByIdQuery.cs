using Cortex.Mediator.Queries;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;

public class GetArticleByIdQuery : IQuery<GetArticleByIdQueryDto>
{
    public Guid Id { get; set; }
}

