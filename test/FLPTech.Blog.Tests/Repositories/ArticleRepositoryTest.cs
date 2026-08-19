using FLPTech.Blog.Infraestructure.Repositories;
using FLPTech.Blog.Tests.Configs.Fixtures;

namespace FLPTech.Blog.Tests.Repositories;

public class ArticleRepositoryTest: IAsyncDisposable
{
    private readonly AppDbContextFixture fixture = new AppDbContextFixture();
    private ArticleRepository? articleRepository;
    private static readonly TimeSpan DefaultTimeout = TimeSpan.FromSeconds(60);

    public async ValueTask DisposeAsync()
    {
        await fixture.DisposeAsync();
        GC.SuppressFinalize(this);
    }

    [Fact]
    public async Task ShouldCheckArticleCount()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var count = 10;
        await fixture.InitializeAsync(cancellationToken);
        fixture.SeedArticles(count);
        var total =  fixture.Object?.Articles.Count();

        Assert.Equal(count, total) ;
    }

    [Fact]
    public async Task ShouldGetAllArticles()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var count = 10;
        await fixture.InitializeAsync(cancellationToken);
        fixture.SeedArticles(count);
        articleRepository = new ArticleRepository(fixture.Object!);

        var articles = await articleRepository.GetAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);

        Assert.Equal(count, articles.Count());

    }
    [Fact]
    public async Task ShouldGetArticleById()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var count = 10;
        await fixture.InitializeAsync(cancellationToken);
        var articles = fixture.SeedArticles(count);
        articleRepository = new ArticleRepository(fixture.Object!);

        var articleExpected = articles.First();

        var article = await articleRepository.GetByIdAsync(articleExpected.Id, cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);

        Assert.NotNull(article);
        Assert.Equal(articleExpected.Id, article?.Id);
        Assert.Equal(articleExpected.Title, article?.Title);
        Assert.Equal(articleExpected.Content, article?.Content);
    }


    [Fact]
    public async Task ShouldAddArticle()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        await fixture.InitializeAsync(cancellationToken);
        articleRepository = new ArticleRepository(fixture.Object!);
        var article = new ArticleFixture().Generate();

        articleRepository.Add(article);
        await fixture.Object?.SaveChangesAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken)!;

        var articleFromDb = await articleRepository.GetByIdAsync(article.Id, cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);

        Assert.NotNull(articleFromDb);
        Assert.Equal(article.Id, articleFromDb?.Id);
        Assert.Equal(article.Title, articleFromDb?.Title);
        Assert.Equal(article.Content, articleFromDb?.Content);
    }
    [Fact]
    public async Task ShouldRemoveArticle()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var count = 10;
        await fixture.InitializeAsync(cancellationToken);
        var articles = fixture.SeedArticles(count);
        articleRepository = new ArticleRepository(fixture.Object!);
        var articleToRemove = articles.First();

        articleRepository.Delete(articleToRemove);

        await fixture.Object?.SaveChangesAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken)!;

        var articleFromDb = await articleRepository.GetByIdAsync(articleToRemove.Id, cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);

        Assert.Null(articleFromDb);
    }

    [Fact]
    public async Task ShouldUpdateArticle()
    {
        var cancellationToken = TestContext.Current.CancellationToken;
        var count = 10;
        await fixture.InitializeAsync(cancellationToken);
        var articles = fixture.SeedArticles(count);
        articleRepository = new ArticleRepository(fixture.Object!);

        var articleToUpdate = articles.First();
        
        var newTitle = "Updated Title";
        var newContent = "Updated Content";
        
        articleToUpdate.Title = newTitle;
        articleToUpdate.Content = newContent;
        
        articleRepository.Update(articleToUpdate);
        await fixture.Object?.SaveChangesAsync(cancellationToken).WaitAsync(DefaultTimeout, cancellationToken)!;

        var articleFromDb = await articleRepository.GetByIdAsync(articleToUpdate.Id, cancellationToken).WaitAsync(DefaultTimeout, cancellationToken);
        
        Assert.NotNull(articleFromDb);
        Assert.Equal(newTitle, articleFromDb?.Title);
        Assert.Equal(newContent, articleFromDb?.Content);
    }
}
