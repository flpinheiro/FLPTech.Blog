using FLPTech.Blog.Domain.Application.UseCases.Articles.CreateArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;
using FLPTech.Blog.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace FLPTech.Blog.Domain.Application.Mappers;

[Mapper]
public partial class ArticlesMappers
{
    [MapperIgnoreTarget(nameof(Article.Id))]
    [MapperIgnoreTarget(nameof(Article.PublishedDate))]
    public partial Article CreateArticleCommandToArticle(CreateArticleCommand command);

    public partial GetArticleByIdQueryDto ArticleToGetArticleByIdQueryDto(Article article);
    
    [MapperIgnoreTarget(nameof(Article.Content))]
    [MapperIgnoreSource(nameof(Article.Content))]
    public partial IEnumerable<GetArticleQueryDto> ArticlesToGetArticleQueryDtos(IEnumerable<Article> articles);
}
