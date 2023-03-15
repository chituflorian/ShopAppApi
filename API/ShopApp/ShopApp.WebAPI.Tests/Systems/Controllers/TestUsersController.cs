using Moq;
using ShopApp.DataAccess.DataInterface;
using ShopApp.WebAPI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopApp.WebAPI.Systems.Controllers
{
    [TestFixture]
    public class TestUsersController
    {
        private UsersController _sut;
        private Mock<IUserRepository> _userRepositoryMock;
        //[SetUp]
        //public void Setup()
        //{
        //    _userRepositoryMock = new Mock<IUserRepository>();
        //    _userRepositoryMock.Setup(repo => repo.)
        //}

    }
}
