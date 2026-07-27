using FLPTech.Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLPTech.Blog.Domain.Services.Repositories;

public interface IArticleRepository
{
    Task<Article?> GetArticleByIdAsync(Guid articleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Article>> GetAllArticlesAsync(CancellationToken cancellationToken = default);

    void UpdateArticle(Article article);
    void AddArticle(Article article);
    void DeleteArticle(Article article);
}
