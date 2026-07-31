using FLPTech.Blog.Web.Models;

namespace FLPTech.Blog.Web.Clients;

public class BlogApiClient(HttpClient httpClient, ILogger<BlogApiClient> logger)
{
    public async Task<BlogPost[]> GetBlogPostsAsync(int maxItems = 10, CancellationToken cancellationToken = default)
    {
        try
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
        catch (Exception)
        {
            logger.LogError("Failed to fetch blog posts from the API.");
        }
        return []; // Return an empty array if the API call fails
    }

    public async Task<BlogPost> GetBlogPostByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var post = await httpClient.GetFromJsonAsync<BlogPost>($"/api/article/{id}", cancellationToken);
            if (post is null)
            {
                throw new InvalidOperationException($"Blog post with ID {id} not found.");
            }
            return post;
        }
        catch (Exception)
        {
            logger.LogError($"Failed to fetch blog post with ID {id} from the API.");
        }
        return new();
    }

    public async Task CreateBlogPostAsync(BlogPost post, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PostAsJsonAsync("/api/article", post, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            logger.LogError("Failed to create a new blog post via the API.");
        }
    }

    public async Task UpdateBlogPostAsync(Guid id, BlogPost post, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.PutAsJsonAsync($"/api/article/{id}", post, cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            logger.LogError("failed to update the blog post with ID {id} via the API.", id);
        }
    }

    public async Task DeleteBlogPostAsync(Guid id, CancellationToken cancellationToken = default)
    {
        try
        {
            var response = await httpClient.DeleteAsync($"/api/article/{id}", cancellationToken);
            response.EnsureSuccessStatusCode();
        }
        catch (Exception)
        {
            logger.LogError("Failed to delete the blog post with ID {id} via the API.", id);
        }
    }
}
