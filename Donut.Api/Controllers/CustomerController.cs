using Library.WebApi.Services.DonutRepository;
using Microsoft.AspNetCore.Mvc;

namespace Donut.Api.Controllers {
    [Route("api/customer")]
    [ApiController]
    public class CustomerController : ControllerBase {
        private readonly IDonutRepository _donutRepository;
        public CustomerController(IDonutRepository donutRepository) {
            _donutRepository = donutRepository;
        }

        /// <summary>
        /// Get All Customers from the donut repository
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<Customer>>> GetAll() {
            return Ok(await _donutRepository.GetAllCustomers());
        }
        /// <summary>
        /// Get a single customer by its primary key
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id) {
            return Ok(await _donutRepository.GetCustomerById(id));
        }

        /// <summary>
        /// Add a new customer to the donut repository
        /// </summary>
        /// <param name="customer"></param>
        [HttpPost]
        public async Task<ActionResult<Customer>> Add(Customer customer) {
            return Ok(await _donutRepository.AddCustomer(customer));
        }
        /// <summary>
        /// Update an existing customer in the donut repository
        /// </summary>
        /// <param name="customer"></param>
        [HttpPut]
        public async Task<ActionResult<Customer>> Update(Customer customer) {
            return Ok(await _donutRepository.UpdateCustomer(customer));
        }
        /// <summary>
        /// Delete a customer from the donut repository
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public async Task<ActionResult<Customer>> Delete(int id) {
            return Ok(await _donutRepository.DeleteCustomer(id));
        }
    }
}
