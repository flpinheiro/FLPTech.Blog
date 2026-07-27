using Cortex.Mediator.Commands;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLPTech.Blog.Domain.Application.UseCases.Articles.CreateArticle;

public class CreateArticleCommand : ICommand<Guid>
{
    public string Title { get; set; } = null!;
    public string Content { get; set; } = null!;
}
