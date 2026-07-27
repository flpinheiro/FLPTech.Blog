using Cortex.Mediator.Queries;
using FLPTech.Blog.Domain.Application.Mappers;
using FLPTech.Blog.Domain.Services.Repositories;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;

public class GetArticleByIdQueryHandler(IUnitOfWork uow) : IQueryHandler<GetArticleByIdQuery, GetArticleByIdQueryDto?>
{
    public async Task<GetArticleByIdQueryDto?> Handle(GetArticleByIdQuery query, CancellationToken cancellationToken)
    {
        var mapper = new ArticlesMappers();

        var article = await uow.Articles.GetArticleByIdAsync(query.Id);

        if (article is null) return null;

        var dto = mapper.ArticleToGetArticleByIdQueryDto(article);

        return dto;
    }
}

