using Microsoft.AspNetCore.Mvc;
using OnlineOrderManagement.Application.Services;
using OnlineOrderManagement.Application;
using OnlineOrderManagement.Domain.Entities;

namespace OnlineOrderManagement.Api.Controller
{

    [ApiController]
    [Route("api/orders")]
    public class OrdersController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrdersController(IOrderService orderService) => _orderService = orderService;

        [HttpPost]
        public async Task<IActionResult> Create(CreateOrderDto dto)
        {
            try
            {
                var createdOrder = await _orderService.CreateOrderAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = createdOrder.Id }, createdOrder);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null) return NotFound();
            return Ok(order);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] string newStatus)
        {
            var updated = await _orderService.UpdateOrderStatusAsync(id, newStatus);
            if (!updated) return NotFound();
            return NoContent();
        }
      
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Order>>> GetOrders()
            => Ok(await _orderService.GetAllOrdersAsync());

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            if (!await _orderService.DeleteOrderAsync(id))
                return NotFound();
            return NoContent();
        }


    }
}
