using Cortex.Mediator;
using FLPTech.Blog.ApiService.Mappers;
using FLPTech.Blog.ApiService.Requests.Articles;
using FLPTech.Blog.Domain.Application.UseCases.Articles.DeleteArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;
using Microsoft.AspNetCore.Mvc;

namespace FLPTech.Blog.ApiService.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ArticleController(IMediator mediator) : ControllerBase
{
    private readonly ArticlesRequestMappers mappers = new ();

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateArticleRequest request, CancellationToken ct)
    {
        var command = mappers.CreateRequestToCreateCommand(request);
        if (command == null)
            return BadRequest("Invalid request data.");
        var articleId = await mediator.SendAsync(command, ct);
        return CreatedAtAction(nameof(Create), new { id = articleId }, null);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Get(Guid id, CancellationToken ct)
    {
        var query = new GetArticleByIdQuery { Id = id };
        var article = await mediator.QueryAsync(query, ct);
        if (article == null)
            return NotFound();
        var articleResponse = mappers.GetByIdQueryDtoToResponse(article);
        return Ok(articleResponse);
    }

    [HttpGet]
    public async Task<IActionResult> Get(CancellationToken ct)
    {
        var query = new GetArticleQuery();
        var articles = await mediator.QueryAsync(query, ct);
        var articlesResponse = mappers.GetQueryDtoToResponse(articles) ?? [];
        return Ok(articlesResponse);
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateArticleRequest request, CancellationToken ct)
    {
        var command = mappers.UpdateRequestToUpdateCommand(request, id);
        if (command == null)
            return BadRequest("Invalid request data.");
        if (await mediator.SendAsync(command, ct))
        {
            return BadRequest("Failed to update article.");
        }
        return Ok();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
    {
        var command = new DeleteArticleCommand { Id = id };
        if (command == null)
            return BadRequest("Invalid request data.");
        if (await mediator.SendAsync(command, ct))
        {
            return BadRequest("Failed to delete article.");
        }
        return Ok();
    }
}
