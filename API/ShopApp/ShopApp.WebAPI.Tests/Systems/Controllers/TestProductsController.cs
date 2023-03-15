using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;
using ShopApp.WebAPI.Controllers;
using ShopApp.WebAPI.Models.DTOs;

namespace ShopApp.WebAPI.Systems.Controllers
{
    [TestFixture]
    public class TestProductsController
    {
        private ProductsController _sut;
        private Mock<IProductRepository> _productRepositoryMock;
       // private Mock<AutoMapper> _autoMapper;
        [SetUp]
        public void Setup()
        {
            //Arrange -> obiectul pe care il testam
            _productRepositoryMock = new Mock<IProductRepository>();    
            _productRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<Product>()
                {
                    new()
                    {
                        ProductId = 1,
                        Name ="Product",
                        Description = "dfada",
                        Price = 10,
                        Stock = 10,
                        OrderProducts = null,
                    }
                });
            _sut = new ProductsController(_productRepositoryMock.Object);

        }
        /// <summary>
        /// Testez cazul in care requestul este ok si returneaza 200
        /// </summary>
        /// <returns></returns>
        [Test]
        public void Get_OnSuccess_ShouldReturnStatusCode200()
        {
            //Act -> actiunea a carui rezultat il punem sub test
            var result = (OkObjectResult) _sut.Get();

            //Assert
            result.StatusCode.Should().Be(200);
        }

        /// <summary>
        /// Testez dependinta cu repository-ul meu
        /// </summary>
        /// <returns></returns>
        [Test]
        public void Get_OnSucces_InvokesProductRepositoryExactlyOnce()
        {
            //Act -> actiunea a carui rezultat il punem sub test
            var result = _sut.Get();

            //Assert
            _productRepositoryMock.Verify(repo => repo.GetAll(), Times.Once());
        }

        [Test]
        public void Get_OnSuccess_ReturnsListOfProducts()
        {
            //Act -> actiunea a carui rezultat il punem sub test
            var result = _sut.Get();

            //Assert
           result.Should().BeOfType<OkObjectResult>();
           var objectResult = (OkObjectResult)result;
           objectResult.Value.Should().BeOfType<List<Product>>();
        }

        [Test]
        public void Get_OnNoProductFound_ShouldReturnStatusCode404()
        {
            //Arrange -> obiectul pe care il testam
             _productRepositoryMock
                .Setup(repo => repo.GetAll())
                .Returns(new List<Product>());

            _sut = new ProductsController(_productRepositoryMock.Object);

            //Act -> actiunea a carui rezultat il punem sub test
            var result = _sut.Get();

            //Assert
            result.Should().BeOfType<NotFoundResult>();
            var objectResult = (NotFoundResult) result;
            objectResult.StatusCode.Should().Be(404);
        }

        [Test]
        public void Get_OnError_ShouldReturnCode500()
        {
            //Arrange -> obiectul pe care il testam
            var _productRepositoryMock = new Mock<IProductRepository>();
            _productRepositoryMock
                .Setup(repo => repo.GetAll())
                .Throws(new Exception());

            _sut = new ProductsController(_productRepositoryMock.Object);

            //Act -> actiunea a carui rezultat il punem sub test
            var result = _sut.Get();

            //Assert
            result.Should().BeOfType<ObjectResult>();
            var objectResult = (ObjectResult)result;
            objectResult.StatusCode.Should().Be(500);
        }
    }
}