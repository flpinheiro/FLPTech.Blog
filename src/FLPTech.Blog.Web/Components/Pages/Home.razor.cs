using FLPTech.Blog.Web.Clients;
using FLPTech.Blog.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FLPTech.Blog.Web.Components.Pages;

public partial class Home(BlogApiClient apiClient, NavigationManager NavigationManager)
{
    private BlogPost[]? posts;

    protected override async Task OnInitializedAsync()
    {

        posts = await apiClient.GetBlogPostsAsync();
    }

    private void NewPost()
    {
        NavigationManager.NavigateTo(BlogPost.Routes.NewPost);
    }
    private void EditPost(Guid postId)
    {
        NavigationManager.NavigateTo(BlogPost.Routes.EditPost.Replace("{id}", postId.ToString()));
    }
    private async Task DeletePost(Guid postId)
    {
        await DeleteAndRefreshAsync(postId);
    }
    private void ViewPost(Guid postId)
    {
        NavigationManager.NavigateTo(BlogPost.Routes.ViewPost.Replace("{id}", postId.ToString()));
    }

    private async Task DeleteAndRefreshAsync(Guid postId)
    {
        try
        {
            await apiClient.DeleteBlogPostAsync(postId);
            posts = await apiClient.GetBlogPostsAsync();
            StateHasChanged();
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine($"Error deleting post {postId}: {ex.Message}");
        }
    }
}
