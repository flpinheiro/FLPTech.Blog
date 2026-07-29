using FLPTech.Blog.Web.Clients;
using FLPTech.Blog.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FLPTech.Blog.Web.Components.Pages;

public partial class ViewPost(BlogApiClient apiClient, NavigationManager navigationManager)
{
    [Parameter]
    public string? Id { get; set; }

    private BlogPost? BlogPost;

    protected override async Task OnInitializedAsync()
    {
        if(Guid.TryParse(Id, out var postId))
        {
            BlogPost = await apiClient.GetBlogPostByIdAsync(postId);
        }       
    }
    private void BackToList()
    {
        navigationManager.NavigateTo(BlogPost.Routes.Home);
    }
}
