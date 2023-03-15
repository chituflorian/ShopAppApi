using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;
using ShopApp.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.WebAPI.Systems.Controllers
{
    public class TestOrdersController
    {
        private OrdersController _sut;
        private Mock<IOrderRepository> _orderRepositoryMock;

        [SetUp]
        public void Setup()
        {
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<Order>()
                {
                    new()
                    {
                        OrderId = 1,
                        DateCreated = new DateTime(2020,2,3),
                        TotalAmount = 10,
                        Active = true,
                        UserId = 1,
                        User = null,
                        OrderProducts = null
                    }
                });
            _sut = new OrdersController(_orderRepositoryMock.Object);
        }

        [Test]
        public void GetOrders_OnSucces_ShouldReturnStatusCode200()
        {
            //Arrange

            //Act
            var result = (ObjectResult)_sut.Get();
            //Assert
            result.StatusCode.Should().Be(200);
        }

        [Test]
        public void GetOrders_OnNoOrderFound_ShouldReturnStatusCode404()
        {
            //Arrange
            _orderRepositoryMock
               .Setup(repo => repo.GetAll())
               .Returns(new List<Order>());

            //Act
            var result = _sut.Get();
            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Test]
        public void GetOrders_OnError_ShouldReturnCode500()
        {
            //Arrange
            _orderRepositoryMock
               .Setup(repo => repo.GetAll())
               .Throws(new Exception());
            //Act
            var result = _sut.Get();
            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult)result;
            objectResult.StatusCode.Should().Be(500);
        }

        [Test]
        public void GetOrders_OnSucces_InvokesOrderRepositoryExactlyOnce()
        {
            //Arrange

            //Act
            var result = _sut.Get();
            //Assert
            _orderRepositoryMock.Verify(repo => repo.GetAll(), Times.Once);
        }

        [Test]
        public void GetOrders_OnSuccess_ReturnsListOfOrders()
        {
            //Arrange

            //Act
            var result = _sut.Get();
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<List<Order>>();
        }

        [Test]
        public void GetOrder_OnSuccess_ReturnsStatusCode200()
        {
            //Arrange
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderRepositoryMock
                .Setup(repo => repo.GetById(1))
                .Returns(new Order
                {
                    OrderId = 1,
                    DateCreated = new DateTime(2020, 2, 3),
                    TotalAmount = 10,
                    Active = true,
                    UserId = 1,
                    User = null,
                    OrderProducts = null
                });
            _sut = new OrdersController(_orderRepositoryMock.Object);
            //Act
            var result =_sut.Get(1);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.StatusCode.Should().Be(200);
        }

        [Test]
        public void GetOrder_OnNoOrderFound_ShouldReturnStatusCode404()
        {
            //Arrange
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderRepositoryMock
                .Setup(repo => repo.GetById(2))
                .Returns(new Order
                {
                    OrderId = 2,
                    DateCreated = new DateTime(2020, 2, 3),
                    TotalAmount = 10,
                    Active = true,
                    UserId = 1,
                    User = null,
                    OrderProducts = null
                });
            _sut = new OrdersController(_orderRepositoryMock.Object);
            //Act
            var result = _sut.Get(3);
            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Test]
        public void GetOrder_OnNoOrderFoundSecondCase_ShouldReturnStatusCode404()
        {
            //Arrange
            _orderRepositoryMock = new Mock<IOrderRepository>();
           
            _sut = new OrdersController(_orderRepositoryMock.Object);
            //Act
            var result = _sut.Get(3);
            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult)result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Test]
        public void GetOrder_OnError_ShouldReturnCode500()
        {
            //Arrange
            _orderRepositoryMock
               .Setup(repo => repo.GetById(2))
               .Throws(new Exception());
            //Act
            var result = _sut.Get(2);
            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult)result;
            objectResult.StatusCode.Should().Be(500);
        }

        [Test]
        public void GetOrder_OnSucces_InvokesOrderRepositoryExactlyOnce()
        {
            //Arrange
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderRepositoryMock
                .Setup(repo => repo.GetById(2))
                .Returns(new Order
                {
                    OrderId = 2,
                    DateCreated = new DateTime(2020, 2, 3),
                    TotalAmount = 10,
                    Active = true,
                    UserId = 1,
                    User = null,
                    OrderProducts = null
                });
            _sut = new OrdersController(_orderRepositoryMock.Object);
            var result = _sut.Get(2);
            //Assert
            _orderRepositoryMock.Verify(repo => repo.GetById(2), Times.Once);
        }

        [Test]
        public void GetOrder_OnSuccess_ReturnsOrder()
        {    //Arrange
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _orderRepositoryMock
                .Setup(repo => repo.GetById(2))
                .Returns(new Order
                {
                    OrderId = 2,
                    DateCreated = new DateTime(2020, 2, 3),
                    TotalAmount = 10,
                    Active = true,
                    UserId = 1,
                    User = null,
                    OrderProducts = null
                });
            _sut = new OrdersController(_orderRepositoryMock.Object);
            //Act
            var result = _sut.Get(2);
            //Assert
            result.Should().BeOfType<OkObjectResult>();
            var objectResult = (OkObjectResult)result;
            objectResult.Value.Should().BeOfType<Order>();
        }

    }
}
