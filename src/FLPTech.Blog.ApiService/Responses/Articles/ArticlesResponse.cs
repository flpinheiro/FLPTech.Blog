using System.Text.Json.Serialization;

namespace FLPTech.Blog.ApiService.Responses.Articles;

public class ArticlesResponse
{
    [JsonPropertyName("id")]
    public Guid? Id { get; set; }
    [JsonPropertyName("title")]
    public string? Title { get; set; }
    [JsonPropertyName("publishedDate")]
    public DateTime PublishedDate { get; set; }
}
