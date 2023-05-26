
using Business.Handlers.Storages.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Storages.Queries.GetStorageQuery;
using Entities.Concrete;
using static Business.Handlers.Storages.Queries.GetStoragesQuery;
using static Business.Handlers.Storages.Commands.CreateStorageCommand;
using Business.Handlers.Storages.Commands;
using Business.Constants;
using static Business.Handlers.Storages.Commands.UpdateStorageCommand;
using static Business.Handlers.Storages.Commands.DeleteStorageCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class StorageHandlerTests
    {
        Mock<IStorageRepository> _storageRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _storageRepository = new Mock<IStorageRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Storage_GetQuery_Success()
        {
            //Arrange
            var query = new GetStorageQuery();

            _storageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Storage, bool>>>())).ReturnsAsync(new Storage()
//propertyler buraya yazılacak
//{																		
//StorageId = 1,
//StorageName = "Test"
//}
);

            var handler = new GetStorageQueryHandler(_storageRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.StorageId.Should().Be(1);

        }

        [Test]
        public async Task Storage_GetQueries_Success()
        {
            //Arrange
            var query = new GetStoragesQuery();

            _storageRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Storage, bool>>>()))
                        .ReturnsAsync(new List<Storage> { new Storage() { /*TODO:propertyler buraya yazılacak StorageId = 1, StorageName = "test"*/ } });

            var handler = new GetStoragesQueryHandler(_storageRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Storage>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Storage_CreateCommand_Success()
        {
            Storage rt = null;
            //Arrange
            var command = new CreateStorageCommand();
            //propertyler buraya yazılacak
            //command.StorageName = "deneme";

            _storageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Storage, bool>>>()))
                        .ReturnsAsync(rt);

            _storageRepository.Setup(x => x.Add(It.IsAny<Storage>())).Returns(new Storage());

            var handler = new CreateStorageCommandHandler(_storageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _storageRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Storage_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateStorageCommand();
            //propertyler buraya yazılacak 
            //command.StorageName = "test";

            _storageRepository.Setup(x => x.Query())
                                           .Returns(new List<Storage> { new Storage() { /*TODO:propertyler buraya yazılacak StorageId = 1, StorageName = "test"*/ } }.AsQueryable());

            _storageRepository.Setup(x => x.Add(It.IsAny<Storage>())).Returns(new Storage());

            var handler = new CreateStorageCommandHandler(_storageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Storage_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateStorageCommand();
            //command.StorageName = "test";

            _storageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Storage, bool>>>()))
                        .ReturnsAsync(new Storage() { /*TODO:propertyler buraya yazılacak StorageId = 1, StorageName = "deneme"*/ });

            _storageRepository.Setup(x => x.Update(It.IsAny<Storage>())).Returns(new Storage());

            var handler = new UpdateStorageCommandHandler(_storageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _storageRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Storage_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteStorageCommand();

            _storageRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Storage, bool>>>()))
                        .ReturnsAsync(new Storage() { /*TODO:propertyler buraya yazılacak StorageId = 1, StorageName = "deneme"*/});

            _storageRepository.Setup(x => x.Delete(It.IsAny<Storage>()));

            var handler = new DeleteStorageCommandHandler(_storageRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _storageRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

