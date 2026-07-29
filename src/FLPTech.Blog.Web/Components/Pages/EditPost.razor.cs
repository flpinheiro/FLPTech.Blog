using FLPTech.Blog.Web.Clients;
using FLPTech.Blog.Web.Models;
using Microsoft.AspNetCore.Components;

namespace FLPTech.Blog.Web.Components.Pages;

public partial class EditPost(BlogApiClient apiClient, NavigationManager navigationManager)
{
    [Parameter]
    public string? Id { get; set; }

    private BlogPost BlogPost = new();
    private bool IsNewPost => string.IsNullOrEmpty(Id);

    protected override async Task OnInitializedAsync()
    {
        if (Id is not null && Guid.TryParse(Id, out var postId))
        {
            BlogPost = await apiClient.GetBlogPostByIdAsync(postId);
        }
    }

    private async Task SavePostAsync()
    {
        if(BlogPost is null)
        {
            throw new InvalidOperationException("BlogPost cannot be null when saving.");
        }
        if (IsNewPost)
        {
            await apiClient.CreateBlogPostAsync(BlogPost);
        }
        else
        {
            await apiClient.UpdateBlogPostAsync(Guid.Parse(Id), BlogPost);
        }
        navigationManager.NavigateTo(BlogPost.Routes.Home);
    }
    private void CancelEdit()
    {
        navigationManager.NavigateTo(BlogPost.Routes.Home);
    }
}
