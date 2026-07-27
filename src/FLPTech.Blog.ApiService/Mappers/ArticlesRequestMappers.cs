using FLPTech.Blog.ApiService.Requests.Articles;
using FLPTech.Blog.ApiService.Responses.Articles;
using FLPTech.Blog.Domain.Application.UseCases.Articles.CreateArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;
using Riok.Mapperly.Abstractions;

namespace FLPTech.Blog.ApiService.Mappers;

[Mapper]
public partial class ArticlesRequestMappers
{
    public partial CreateArticleCommand CreateArticleRequestToCreateArticleCommand(CreateArticleRequest request);
    public partial ArticleResponse GetArticleByIdQueryDtoToArticleResponse(GetArticleByIdQueryDto dto);

    public partial IEnumerable<ArticlesResponse> GetArticleQueryDtoToArticlesResponse(IEnumerable<GetArticleQueryDto> dtos);
}
