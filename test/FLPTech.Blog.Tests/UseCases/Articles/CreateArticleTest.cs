using FLPTech.Blog.Domain.Application.UseCases.Articles.CreateArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.DeleteArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.ReadArticle;
using FLPTech.Blog.Domain.Application.UseCases.Articles.UpdateArticle;
using FLPTech.Blog.Domain.Entities;
using FLPTech.Blog.Domain.Services.Repositories;
using FLPTech.Blog.Tests.Configs.Fixtures;
using FLPTech.Blog.Tests.Configs.Mocks;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLPTech.Blog.Tests.UseCases.Articles;

public class CreateArticleTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();

    private readonly CreateArticleCommandHandler handler;
    public CreateArticleTest()
    {
        handler = new CreateArticleCommandHandler(_unitOfWork.Object);
    }

    [Fact]
    public async Task ShouldCreateArticle()
    {
        // Arrange
        var command = new CreateArticleCommand
        {
            Title = "Test Article",
            Content = "This is a test article."
        };
        _unitOfWork.CallAddMock();
        _unitOfWork.CallSaveMock();
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        _unitOfWork.VerifyAddMock(Times.Once());
        _unitOfWork.VerifySaveMock(Times.Once());
        Assert.NotEqual(Guid.Empty, result);
    }
}

public class DeleteArticleTest 
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly DeleteArticleCommandHandler handler;
    public DeleteArticleTest()
    {
        handler = new(_unitOfWork.Object);
    }

    [Fact]
    public async Task ShouldDeleteArticle()
    {
        // Arrange

        var article = new ArticleFixture().Generate();
        _unitOfWork.CallGetByIdMock(article);
        _unitOfWork.CallDeleteMock();
        _unitOfWork.CallSaveMock();
        var command = new DeleteArticleCommand
        {
            Id = article.Id,
        };
        // Act
        await handler.Handle(command, CancellationToken.None);
        // Assert
        _unitOfWork.VerifyGetByIdMock(Times.Once());
        _unitOfWork.VerifyDeleteMock(Times.Once());
        _unitOfWork.VerifySaveMock(Times.Once());
    }
}

public class UpdateArticleTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly UpdateArticleCommandHandler handler;
    public UpdateArticleTest()
    {
        handler = new(_unitOfWork.Object);
    }
    [Fact]
    public async Task ShouldUpdateArticle()
    {
        // Arrange
        var article = new ArticleFixture().Generate();
        _unitOfWork.CallGetByIdMock(article);
        _unitOfWork.CallSaveMock();
        _unitOfWork.CallUpdateMock();
        var command = new UpdateArticleCommand
        {
            Id = article.Id,
            Title = "Updated Title",
            Content = "Updated Content"
        };
        // Act
        await handler.Handle(command, CancellationToken.None);
        // Assert
        _unitOfWork.VerifyGetByIdMock(Times.Once());
        _unitOfWork.VerifySaveMock(Times.Once());
        _unitOfWork.VerifyUpdateMock(Times.Once());
    }
}

public class GetArticleByIdTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly GetArticleByIdQueryHandler handler;
    public GetArticleByIdTest()
    {
        handler = new(_unitOfWork.Object);
    }
    [Fact]
    public async Task ShouldReadArticle()
    {
        // Arrange
        var article = new ArticleFixture().Generate();
        _unitOfWork.CallGetByIdMock(article);
        var command = new GetArticleByIdQuery
        {
            Id = article.Id,
        };
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        _unitOfWork.VerifyGetByIdMock(Times.Once());
        Assert.NotNull(result);
        Assert.Equal(article.Id, result.Id);
        Assert.Equal(article.Title, result.Title);
        Assert.Equal(article.Content, result.Content);
    }
}


public class GetArticleTest
{
    private readonly Mock<IUnitOfWork> _unitOfWork = new();
    private readonly GetArticleQueryHandler handler;
    public GetArticleTest()
    {
        handler = new(_unitOfWork.Object);
    }
    [Fact]
    public async Task ShouldReadArticle()
    {
        // Arrange
        var article = new ArticleFixture().Generate(10);
        _unitOfWork.CallGetMock(article);
        var command = new GetArticleQuery();
        // Act
        var result = await handler.Handle(command, CancellationToken.None);
        // Assert
        _unitOfWork.VerifyGetMock(Times.Once());

        Assert.NotNull(result);
        Assert.NotNull(result);
    }
}