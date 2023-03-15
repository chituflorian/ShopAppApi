 using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShopApp.Core.Domain;
using ShopApp.DataAccess.DataInterface;
using ShopApp.WebAPI.Models.DTOs;

namespace ShopApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly LinkGenerator _linkGenerator;

        public ProductsController(IProductRepository productRepository, LinkGenerator linkGenerator, IMapper mapper)
        {
            _linkGenerator = linkGenerator;
            _mapper = mapper;
            _productRepository = productRepository;
        }

        [HttpGet(Name = "GetProducts")]
        public IActionResult Get()
        {
            try
            {
                var products = _productRepository.GetAll();

                if (products.Any())
                {
                    return Ok(products);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        public ActionResult<ProductDTO[]> Post(ProductDTO[] productDTO)
        {
            try
            {
                var location = _linkGenerator.GetPathByAction("Get", "Products", new { name = productDTO[0].Name });

                if (string.IsNullOrWhiteSpace(location))
                {
                    return BadRequest("Could not use current product!");
                }

                var product = _mapper.Map<Product[]>(productDTO);
                foreach(var elem in product)
                {
                    _productRepository.Add(elem);
                }
                if (_productRepository.SaveChanges())
                {
                    return Created(location, _mapper.Map<ProductDTO[]>(product));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest();
        }

        [HttpPut("{name}", Name = "PutProduct")]
        public IActionResult Put(string name, ProductDTO productDTO)
        {
            try
            {
                var old = _productRepository.Find(u => u.Name.Equals(name)).FirstOrDefault();

                if (old == null)
                {
                    return NotFound("Could not find this product.");
                }

                _mapper.Map(productDTO, old);

                if (_productRepository.SaveChanges())
                {
                    return Ok(_mapper.Map<ProductDTO>(old));
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
            return NotFound();
        }

        [HttpGet("{name}" ,Name = "GetIfProductInStock")]
        public IActionResult Get(string name)
        {
            try
            {
                var product = _productRepository.Find(u => u.Name.Equals(name)).FirstOrDefault();

                if (product == null)
                {
                    return NotFound("Product not found");
                }

                if (product.Stock > 0)
                {
                    return Ok(new {inStock = true, numberOfPieces = product.Stock});
                }
                else
                {
                    return Ok(new { inStock = false });
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        [HttpDelete("{name}")]
        public IActionResult Delete(string name)
        {
            try
            {
                var old = _productRepository.Find(u => u.Name.Equals(name)).FirstOrDefault();

                if (old == null)
                {
                    return NotFound("Could not find this product.");
                }

                _productRepository.Remove(old);

                if (_productRepository.SaveChanges())
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the product");
        }
    }
}
