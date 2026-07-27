namespace FLPTech.Blog.Domain.Entities;

public class Article
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
    public DateTime PublishedDate { get; set; } = DateTime.UtcNow;
}
