using Cortex.Mediator;
using FLPTech.Blog.ApiService.Mappers;
using FLPTech.Blog.ApiService.Requests.Articles;
using FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;
using Microsoft.AspNetCore.Mvc;

namespace FLPTech.Blog.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(IMediator mediator) : ControllerBase
{
    private readonly ArticlesRequestMappers mappers = new ArticlesRequestMappers();

    [HttpPost]
    public async Task<IActionResult> CreateArticle([FromBody] CreateArticleRequest request, CancellationToken ct)
    {
        var command = mappers.CreateArticleRequestToCreateArticleCommand(request);
        if(command == null)
            return BadRequest("Invalid request data.");
        var articleId = await mediator.SendAsync(command, ct);
        return CreatedAtAction(nameof(CreateArticle), new { id = articleId }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetArticle(Guid id, CancellationToken ct)
    {
        var query = new GetArticleByIdQuery { Id = id };
        var article = await mediator.QueryAsync(query, ct);
        if (article == null)
            return NotFound();
        var articleResponse = mappers.GetArticleByIdQueryDtoToArticleResponse(article);
        return Ok(articleResponse);
    }

    [HttpGet]
    public async Task<IActionResult> GetArticles(CancellationToken ct)
    {
        var query = new GetArticleQuery();
        var articles = await mediator.QueryAsync(query, ct);
        var articlesResponse = mappers.GetArticleQueryDtoToArticlesResponse(articles) ?? [];
        return Ok(articlesResponse);
    }
    //[HttpPut("{id:guid}")]
    //public async Task<IActionResult> UpdateArticle([FromRoute] Guid id, [FromBody] UpdateArticleRequest request, CancellationToken ct)
    //{
    //    var command = mappers.UpdateArticleRequestToUpdateArticleCommand(request);
    //    if (command == null)
    //        return BadRequest("Invalid request data.");
    //    await mediator.SendAsync(command, ct);
    //    return NoContent();
    //}

    //[HttpDelete("{id:guid}")]
    //public async Task<IActionResult> DeleteArticle([FromRoute] Guid id, CancellationToken ct)
    //{
    //    var command = mappers.DeleteArticleRequestToDeleteArticleCommand(request);
    //    if (command == null)
    //        return BadRequest("Invalid request data.");
    //    await mediator.SendAsync(command, ct);
    //    return NoContent();
    //}
}
