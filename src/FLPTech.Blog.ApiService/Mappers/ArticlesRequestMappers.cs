using FLPTech.Blog.ApiService.Requests.Articles;
using FLPTech.Blog.ApiService.Responses.Articles;
using FLPTech.Blog.Domain.Application.UseCases.Articles.CreateArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.UpdateArticle;
using Riok.Mapperly.Abstractions;

namespace FLPTech.Blog.ApiService.Mappers;

[Mapper]
public partial class ArticlesRequestMappers
{
    public partial CreateArticleCommand CreateRequestToCreateCommand(CreateArticleRequest request);
    public partial ArticleResponse GetByIdQueryDtoToResponse(GetArticleByIdQueryDto dto);

    public partial IEnumerable<ArticlesResponse> GetQueryDtoToResponse(IEnumerable<GetArticleQueryDto> dtos);
    public partial UpdateArticleCommand UpdateRequestToUpdateCommand(UpdateArticleRequest request, Guid id);
}
