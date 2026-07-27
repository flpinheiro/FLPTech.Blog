using System.Text.Json.Serialization;

namespace FLPTech.Blog.ApiService.Responses.Articles;

public class ArticleResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("content")]
    public string? Content { get; set; }
    [JsonPropertyName("publishedDate")]
    public DateTime PublishedDate { get; set; }
}
