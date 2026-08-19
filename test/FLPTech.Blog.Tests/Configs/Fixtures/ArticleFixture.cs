using Bogus;
using FLPTech.Blog.Domain.Entities;

namespace FLPTech.Blog.Tests.Configs.Fixtures;

public class ArticleFixture
{
    private readonly Faker<Article> Faker = new Faker<Article>()
        .StrictMode(true)
        .RuleFor(x => x.Id, fake => fake.Random.Uuid())
        .RuleFor(x => x.Title, fake => fake.Lorem.Sentence())
        .RuleFor(x => x.Content, fake => fake.Lorem.Text())
        .RuleFor(x => x.PublishedDate, fake => fake.Date.Recent());

    public Article Generate() => Faker.Generate();
    public IEnumerable<Article> Generate(int count) => Faker.Generate(count);
}
