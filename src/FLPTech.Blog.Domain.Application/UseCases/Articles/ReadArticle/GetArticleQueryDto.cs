namespace FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;

public class GetArticleQueryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public DateTime PublishedDate { get; set; }
}

