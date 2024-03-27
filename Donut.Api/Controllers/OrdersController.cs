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
        /// <summary>
        /// Get all orders from the donut repository
        /// </summary>
        /// <returns></returns>
        [HttpGet("allopen")]
        public async Task<ActionResult<List<Orders>>> GetAllOpen() {
            return Ok(await _donutRepository.GetAllOpenOrders());
        }
        /// <summary>
        /// Add an  orders to the donut repository
        /// </summary>
        /// <param name="order"></param>
        [HttpPost]
        public async Task<ActionResult<Orders>> Add(Orders order) {
            return Ok(await _donutRepository.AddOrder(order));
        }
        /// <summary>
        /// Add an order item to the donut repository
        /// </summary>
        /// <param name="orderId"></param>
        /// <param name="items"></param>
        [HttpPost("{orderId}/item")]
        public async Task<ActionResult<Orders>> AddItem(int orderId, [FromBody] OrderItems items) {
            return Ok(await _donutRepository.AddOrderItem(orderId, items));
        }
        /// <summary>
        /// Update the status of an order in the donut repository to pending
        /// </summary>
        /// <param name="orderId"></param>
        [HttpPost("{orderId}/pending")]
        public async Task<ActionResult<Orders>> UpdateStatusPending(int orderId) {
            return Ok(await _donutRepository.UpdateOrderStatus(orderId, OrderStatus.Pending));
        }
        /// <summary>
        /// Update the status of an order in the donut repository to Completed 
        /// </summary>
        /// <param name="orderId"></param>
        [HttpPost("{orderId}/completed")]
        public async Task<ActionResult<Orders>> UpdateStatusCompleted(int orderId) {
            return Ok(await _donutRepository.UpdateOrderStatus(orderId, OrderStatus.Completed));
        }
        /// <summary>
        /// Update the status of an order in the donut repository to Cancelled
        /// </summary>
        /// <param name="orderId"></param>
        [HttpPost("{orderId}/cancelled")]
        public async Task<ActionResult<Orders>> UpdateStatusCancelled(int orderId) {
            return Ok(await _donutRepository.UpdateOrderStatus(orderId, OrderStatus.Cancelled));
        }
        /// <summary>
        /// Get all orders from the donut repository for a specific customer
        /// </summary>
        /// <param name="customerId"></param>
        [HttpGet("customer/{customerId}")]
        public async Task<ActionResult<List<Orders>>> GetCustomerOpenOrders( int customerId) {
            return Ok(await _donutRepository.GetCustomerOpenOrders( customerId));
        }
    }
}
