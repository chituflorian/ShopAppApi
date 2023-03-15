 using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;
using ShopApp.WebAPI.Models.DTOs;

namespace ShopApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ProductController(IProductRepository productRepository, IMapper mapper, LinkGenerator linkGenerator)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _linkGenerator = linkGenerator;
        }
        [HttpGet]
        public ActionResult<ProductDTO[]> Get()
        {
            try
            {
                var result = _productRepository.GetAll();
                return _mapper.Map<ProductDTO[]>(result);
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        public ActionResult<ProductDTO> Post(ProductDTO productDTO)
        {
            try
            {
                var location = _linkGenerator.GetPathByAction("Get", "Product", new { name = productDTO.Name });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current product!");
                }

                var product = _mapper.Map<Product>(productDTO);
                _productRepository.Add(product);
                if (_productRepository.SaveChanges())
                {
                    return Created(location, _mapper.Map<ProductDTO>(product));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }
    }
}
