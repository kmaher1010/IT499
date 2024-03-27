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

        [HttpGet("all")]
        public async Task<ActionResult<List<Customer>>> GetAll() {
            return Ok(await _donutRepository.GetAllCustomers());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Customer>> GetById(int id) {
            return Ok(await _donutRepository.GetCustomerById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Customer>> Add(Customer customer) {
            return Ok(await _donutRepository.AddCustomer(customer));
        }
        [HttpPut]
        public async Task<ActionResult<Customer>> Update(Customer customer) {
            return Ok(await _donutRepository.UpdateCustomer(customer));
        }
        [HttpDelete]
        public async Task<ActionResult<Customer>> Delete(int id) {
            return Ok(await _donutRepository.DeleteCustomer(id));
        }
    }
}
