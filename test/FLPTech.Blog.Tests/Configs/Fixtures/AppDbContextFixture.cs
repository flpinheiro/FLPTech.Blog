using FLPTech.Blog.Domain.Entities;
using FLPTech.Blog.Infraestructure.Contexts;
using Microsoft.EntityFrameworkCore;
using Testcontainers.MsSql;

namespace FLPTech.Blog.Tests.Configs.Fixtures;

internal class AppDbContextFixture : IAsyncDisposable
{
    public AppDbContext? Object { get; private set; }

    private MsSqlContainer msSqlContainer;
    private bool isDisposed = false;
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

    public AppDbContextFixture()
    {
        msSqlContainer = new MsSqlBuilder("mcr.microsoft.com/mssql/server:2022-CU14-ubuntu-22.04").Build();
        
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        await msSqlContainer.StartAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseSqlServer(msSqlContainer.GetConnectionString())
            .Options;

        Object = new AppDbContext(options);

        await Object.Database.MigrateAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
    }
    public IEnumerable<Article> SeedArticles(int count)
    {
        if(Object is null)  throw new InvalidOperationException("AppDbContext is not initialized. Call InitializeAsync() before seeding data.");
        var articles = new ArticleFixture().Generate(count);
        Object.Articles.AddRange(articles);
        Object.SaveChanges();
        return articles;
    }

    public async ValueTask DisposeAsync()
    {
        await Disposing(!isDisposed);
        GC.SuppressFinalize(this);
    }
    private async ValueTask Disposing(bool disposing)
    {
        if (disposing)
        {
            Object.Dispose();
            await msSqlContainer.StopAsync();
            
            isDisposed = true;
        }
    }
}
