
using Business.Handlers.Customers.Queries;
using DataAccess.Abstract;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using static Business.Handlers.Customers.Queries.GetCustomerQuery;
using Entities.Concrete;
using static Business.Handlers.Customers.Queries.GetCustomersQuery;
using static Business.Handlers.Customers.Commands.CreateCustomerCommand;
using Business.Handlers.Customers.Commands;
using Business.Constants;
using static Business.Handlers.Customers.Commands.UpdateCustomerCommand;
using static Business.Handlers.Customers.Commands.DeleteCustomerCommand;
using MediatR;
using System.Linq;
using FluentAssertions;


namespace Tests.Business.HandlersTest
{
    [TestFixture]
    public class CustomerHandlerTests
    {
        Mock<ICustomerRepository> _customerRepository;
        Mock<IMediator> _mediator;
        [SetUp]
        public void Setup()
        {
            _customerRepository = new Mock<ICustomerRepository>();
            _mediator = new Mock<IMediator>();
        }

        [Test]
        public async Task Customer_GetQuery_Success()
        {
            //Arrange
            var query = new GetCustomerQuery();

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>())).ReturnsAsync(new Customer()
//propertyler buraya yazılacak
//{																		
//CustomerId = 1,
//CustomerName = "Test"
//}
);

            var handler = new GetCustomerQueryHandler(_customerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            //x.Data.CustomerId.Should().Be(1);

        }

        [Test]
        public async Task Customer_GetQueries_Success()
        {
            //Arrange
            var query = new GetCustomersQuery();

            _customerRepository.Setup(x => x.GetListAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(new List<Customer> { new Customer() { /*TODO:propertyler buraya yazılacak CustomerId = 1, CustomerName = "test"*/ } });

            var handler = new GetCustomersQueryHandler(_customerRepository.Object, _mediator.Object);

            //Act
            var x = await handler.Handle(query, new System.Threading.CancellationToken());

            //Asset
            x.Success.Should().BeTrue();
            ((List<Customer>)x.Data).Count.Should().BeGreaterThan(1);

        }

        [Test]
        public async Task Customer_CreateCommand_Success()
        {
            Customer rt = null;
            //Arrange
            var command = new CreateCustomerCommand();
            //propertyler buraya yazılacak
            //command.CustomerName = "deneme";

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(rt);

            _customerRepository.Setup(x => x.Add(It.IsAny<Customer>())).Returns(new Customer());

            var handler = new CreateCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Added);
        }

        [Test]
        public async Task Customer_CreateCommand_NameAlreadyExist()
        {
            //Arrange
            var command = new CreateCustomerCommand();
            //propertyler buraya yazılacak 
            //command.CustomerName = "test";

            _customerRepository.Setup(x => x.Query())
                                           .Returns(new List<Customer> { new Customer() { /*TODO:propertyler buraya yazılacak CustomerId = 1, CustomerName = "test"*/ } }.AsQueryable());

            _customerRepository.Setup(x => x.Add(It.IsAny<Customer>())).Returns(new Customer());

            var handler = new CreateCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            x.Success.Should().BeFalse();
            x.Message.Should().Be(Messages.NameAlreadyExist);
        }

        [Test]
        public async Task Customer_UpdateCommand_Success()
        {
            //Arrange
            var command = new UpdateCustomerCommand();
            //command.CustomerName = "test";

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(new Customer() { /*TODO:propertyler buraya yazılacak CustomerId = 1, CustomerName = "deneme"*/ });

            _customerRepository.Setup(x => x.Update(It.IsAny<Customer>())).Returns(new Customer());

            var handler = new UpdateCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Updated);
        }

        [Test]
        public async Task Customer_DeleteCommand_Success()
        {
            //Arrange
            var command = new DeleteCustomerCommand();

            _customerRepository.Setup(x => x.GetAsync(It.IsAny<Expression<Func<Customer, bool>>>()))
                        .ReturnsAsync(new Customer() { /*TODO:propertyler buraya yazılacak CustomerId = 1, CustomerName = "deneme"*/});

            _customerRepository.Setup(x => x.Delete(It.IsAny<Customer>()));

            var handler = new DeleteCustomerCommandHandler(_customerRepository.Object, _mediator.Object);
            var x = await handler.Handle(command, new System.Threading.CancellationToken());

            _customerRepository.Verify(x => x.SaveChangesAsync());
            x.Success.Should().BeTrue();
            x.Message.Should().Be(Messages.Deleted);
        }
    }
}

