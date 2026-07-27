using System.Text.Json.Serialization;

namespace FLPTech.Blog.ApiService.Requests.Articles;

public class CreateArticleRequest
{
    [JsonPropertyName("title")]
    public string Title { get; set; } = null!;
    [JsonPropertyName("content")]
    public string Content { get; set; } = null!;
}
