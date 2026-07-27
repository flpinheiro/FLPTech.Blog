using FLPTech.Blog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLPTech.Blog.Domain.Services.Repositories;

public interface IArticleRepository
{
    Task<Article?> GetByIdAsync(Guid articleId, CancellationToken cancellationToken = default);
    Task<IEnumerable<Article>> GetAsync(CancellationToken cancellationToken = default);

    void Update(Article article);
    void Add(Article article);
    void Delete(Article article);
}
