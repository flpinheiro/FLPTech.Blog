namespace FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;

public class GetArticleByIdQueryDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishedDate { get; set; }
}

