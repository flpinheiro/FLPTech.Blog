using FLPTech.Blog.Domain.Entities;
using FLPTech.Blog.Domain.Services.Repositories;
using FLPTech.Blog.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FLPTech.Blog.Infraestructure.Repositories;

internal class ArticleRepository(AppDbContext dbContext) : IArticleRepository
{
    public void AddArticle(Article article)
    {
        dbContext.Articles.Add(article);
    }

    public void DeleteArticle(Article article)
    {
        dbContext.Articles.Remove(article);
    }

    public async Task<IEnumerable<Article>> GetAllArticlesAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Articles.AsNoTracking().Select(a => new Article
        {
            Id = a.Id,
            Title = a.Title,
            PublishedDate = a.PublishedDate
        }).ToListAsync(cancellationToken);
    }

    public async Task<Article?> GetArticleByIdAsync(Guid articleId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Articles.FindAsync(articleId, cancellationToken);
    }

    public void UpdateArticle(Article article)
    {
        dbContext.Articles.Update(article);
    }
}
