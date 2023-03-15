using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApp.DataAccess.DataInterface;
using ShopApp.WebAPI.Models.DTOs;

namespace ShopApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserController(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public ActionResult<UserDTO[]> Get(bool includeOrders = false)
        {
            try
            {
                var result = _userRepository.GetUsers(includeOrders);
                return _mapper.Map<UserDTO[]>(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

        }

        [HttpGet("{name}")]
        public ActionResult<UserDTO[]> Get(string name)
        {
            try
            {
                var result = _userRepository.Find(u => u.Name.Equals(name));

                if (result == null || !result.Any())
                {
                    return NotFound("User not found!");
                }

                return _mapper.Map<UserDTO[]>(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
    }
}