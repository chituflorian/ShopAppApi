using Microsoft.AspNetCore.Mvc;
using ShopApp.DataAccess.DataInterface;

namespace ShopApp.WebAPI.Controllers
{
    [Route("api/[controller]")]
    public class OrdersController : Controller
    {
        private readonly IOrderRepository _orderRepsoitory;

        public OrdersController(IOrderRepository orderRepsoitory)
        {
            _orderRepsoitory = orderRepsoitory;
        }

        [HttpGet(Name = "GetOrders")]
        public IActionResult Get()
        {
            try
            {
                var orders = _orderRepsoitory.GetAll();

                if (orders.Any())
                {
                    return Ok(orders);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        
        [HttpGet("{id}", Name = "GetOrder")]
        public IActionResult Get(int id)
        {
            try
            {
                var order = _orderRepsoitory.GetById(id);

                if (order != null)
                {
                    return Ok(order);
                }
                return NotFound();
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }
        [Route("[action]/{active}")]
        [HttpGet("{id}", Name = "GetIfOrderIsActive")]
        public IActionResult GetActive(int id)
        {
            try
            {
                var order = _orderRepsoitory.GetById(id);

                if (order == null)
                {
                    return NotFound("Order not found");
                }

                if (order.Active == true)
                {
                    return Ok(new { isActive = true});
                }
                else
                {
                    return Ok(new { isActive = false });
                }

            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var old = _orderRepsoitory.GetById(id);

                if (old == null)
                {
                    return NotFound("Could not find this order.");
                }

                _orderRepsoitory.Remove(old);

                if (_orderRepsoitory.SaveChanges())
                {
                    return Ok();
                }
            }
            catch (Exception e)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Database Failure");
            }

            return BadRequest("Failed to delete the order");
        }
    }
}
