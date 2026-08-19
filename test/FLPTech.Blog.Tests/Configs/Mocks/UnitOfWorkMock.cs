using FLPTech.Blog.Domain.Entities;
using FLPTech.Blog.Domain.Services.Repositories;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace FLPTech.Blog.Tests.Configs.Mocks;

internal static class UnitOfWorkMock
{
    public static Mock<IUnitOfWork> CallGetMock(this Mock<IUnitOfWork> mock, IEnumerable< Article> articles)
    {
        mock.Setup(uow => uow.Articles.GetAsync( It.IsAny<CancellationToken>()))
            .ReturnsAsync(articles)
            .Verifiable();
        return mock;
    }
    public static Mock<IUnitOfWork> CallGetByIdMock(this Mock<IUnitOfWork> mock, Article article)
    {
        mock.Setup(uow => uow.Articles.GetByIdAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(article)
            .Verifiable();
        return mock;
    }
    public static Mock<IUnitOfWork> CallAddMock(this Mock<IUnitOfWork> mock)
    {
        mock.Setup(uow => uow.Articles.Add(It.IsAny<Article>()))
            .Verifiable();
        return mock;
    }
    public static Mock<IUnitOfWork> CallUpdateMock(this Mock<IUnitOfWork> mock)
    {
        mock.Setup(uow => uow.Articles.Update(It.IsAny<Article>()))
            .Verifiable();
        return mock;
    }
    public static Mock<IUnitOfWork> CallDeleteMock(this Mock<IUnitOfWork> mock)
    {
        mock.Setup(uow => uow.Articles.Delete(It.IsAny<Article>()))
            .Verifiable();
        return mock;
    }

    public static Mock<IUnitOfWork> CallSaveMock(this Mock<IUnitOfWork> mock)
    {
        mock.Setup(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()))
            .ReturnsAsync(1)
            .Verifiable();
        return mock;
    }

    public static Mock<IUnitOfWork> VerifyGetMock(this Mock<IUnitOfWork> mock, Times times)
    {
        mock.Verify(uow => uow.Articles.GetAsync(), times);
        return mock;
    }

    public static Mock<IUnitOfWork> VerifyGetByIdMock(this Mock<IUnitOfWork> mock, Times times)
    {
        mock.Verify(uow => uow.Articles.GetByIdAsync(It.IsAny<Guid>()), times);
        return mock;
    }

    public static Mock<IUnitOfWork> VerifyAddMock(this Mock<IUnitOfWork> mock, Times times)
    {
        mock.Verify(uow => uow.Articles.Add(It.IsAny<Article>()), times);
        return mock;
    }
    public static Mock<IUnitOfWork> VerifyUpdateMock(this Mock<IUnitOfWork> mock, Times times)
    {
        mock.Verify(uow => uow.Articles.Update(It.IsAny<Article>()), times);
        return mock;
    }
    public static Mock<IUnitOfWork> VerifyDeleteMock(this Mock<IUnitOfWork> mock, Times times)
    {
        mock.Verify(uow => uow.Articles.Delete(It.IsAny<Article>()), times);
        return mock;
    }
    public static Mock<IUnitOfWork> VerifySaveMock(this Mock<IUnitOfWork> mock, Times times)
    {
        mock.Verify(uow => uow.SaveChangesAsync(It.IsAny<CancellationToken>()), times);
        return mock;
    }


}
