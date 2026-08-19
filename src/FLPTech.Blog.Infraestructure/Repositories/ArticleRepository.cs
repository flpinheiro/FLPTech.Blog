using FLPTech.Blog.Domain.Entities;
using FLPTech.Blog.Domain.Services.Repositories;
using FLPTech.Blog.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace FLPTech.Blog.Infraestructure.Repositories;

public class ArticleRepository(AppDbContext dbContext) : IArticleRepository
{
    public void Add(Article article)
    {
        dbContext.Articles.Add(article);
    }

    public void Delete(Article article)
    {
        dbContext.Articles.Remove(article);
    }

    public async Task<IEnumerable<Article>> GetAsync(CancellationToken cancellationToken = default)
    {
        return await dbContext.Articles
            .AsNoTracking()
            .Select(a => new Article
                {
                    Id = a.Id,
                    Title = a.Title,
                    PublishedDate = a.PublishedDate
                })
            .ToListAsync(cancellationToken);
    }

    public async Task<Article?> GetByIdAsync(Guid articleId, CancellationToken cancellationToken = default)
    {
        return await dbContext.Articles.FindAsync(articleId, cancellationToken);
    }

    public void Update(Article article)
    {
        dbContext.Articles.Update(article);
    }
}
