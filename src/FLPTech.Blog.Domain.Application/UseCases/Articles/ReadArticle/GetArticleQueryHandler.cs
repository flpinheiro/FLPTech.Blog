using Cortex.Mediator.Queries;
using FLPTech.Blog.Domain.Application.Mappers;
using FLPTech.Blog.Domain.Services.Repositories;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;

public class GetArticleQueryHandler(IUnitOfWork uow) : IQueryHandler<GetArticleQuery, IEnumerable<GetArticleQueryDto>>
{
    public async Task<IEnumerable<GetArticleQueryDto>> Handle(GetArticleQuery query, CancellationToken cancellationToken)
    {
        var articles =  await uow.Articles.GetAsync(cancellationToken);

        var mapper = new ArticlesMappers();
        var dtos = mapper.ArticlesToGetArticleQueryDtos(articles);

        return dtos;
    }
}

