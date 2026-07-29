using FLPTech.Blog.Web.Models;

namespace FLPTech.Blog.Web.Clients;

public class BlogApiClient(HttpClient httpClient)
{
    public async Task<BlogPost[]> GetBlogPostsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        List<BlogPost>? posts = null;
        await foreach (var post in httpClient.GetFromJsonAsAsyncEnumerable<BlogPost>("/api/article", cancellationToken))
        {
            if (posts?.Count >= maxItems)
            {
                break;
            }
            if (post is not null)
            {
                posts ??= [];
                posts.Add(post);
            }
        }
        return posts?.ToArray() ?? [];
    }

    public async Task<BlogPost> GetBlogPostByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var post = await httpClient.GetFromJsonAsync<BlogPost>($"/api/article/{id}", cancellationToken);
        if (post is null)
        {
            throw new InvalidOperationException($"Blog post with ID {id} not found.");
        }
        return post;
    }

    public async Task CreateBlogPostAsync(BlogPost post, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PostAsJsonAsync("/api/article", post, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task UpdateBlogPostAsync(Guid id, BlogPost post, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.PutAsJsonAsync($"/api/article/{id}", post, cancellationToken);
        response.EnsureSuccessStatusCode();
    }

    public async Task DeleteBlogPostAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await httpClient.DeleteAsync($"/api/article/{id}", cancellationToken);
        response.EnsureSuccessStatusCode();
    }
}
