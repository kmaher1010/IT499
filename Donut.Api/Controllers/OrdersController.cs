using Library.WebApi.Services.DonutRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Donut.Api.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase {
        private readonly IDonutRepository _donutRepository;
        public OrdersController(IDonutRepository donutRepository) {
            _donutRepository = donutRepository;
        }
        [HttpGet("allopen")]
        public async Task<ActionResult<List<Orders>>> GetAllOpen() {
            return Ok(await _donutRepository.GetAllOpenOrders());
        }
        [HttpPost]
        public async Task<ActionResult<Orders>> Add(Orders order) {
            return Ok(await _donutRepository.AddOrder(order));
        }
        [HttpPost("{orderId}/pending")]
        public async Task<ActionResult<Orders>> UpdateStatusPending(int orderId) {
            return Ok(await _donutRepository.UpdateOrderStatus(orderId, OrderStatus.Pending));
        }
        [HttpPost("{orderId}/completed")]
        public async Task<ActionResult<Orders>> UpdateStatusCompleted(int orderId) {
            return Ok(await _donutRepository.UpdateOrderStatus(orderId, OrderStatus.Completed));
        }
        [HttpPost("{orderId}/cancelled")]
        public async Task<ActionResult<Orders>> UpdateStatusCancelled(int orderId) {
            return Ok(await _donutRepository.UpdateOrderStatus(orderId, OrderStatus.Cancelled));
        }
    }
}
