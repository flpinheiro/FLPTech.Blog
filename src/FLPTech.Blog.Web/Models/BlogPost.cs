namespace FLPTech.Blog.Web.Models;

public class BlogPost
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string Content { get; set; } = string.Empty;
    public DateTime PublishedDate { get; set; }

    internal static class Routes
    {
        public const string ViewPost = "/post/{id}";
        public const string EditPost = "/post/edit/{id}";
        public const string NewPost = "/post/new";
        public const string Home = "/";
    }
}