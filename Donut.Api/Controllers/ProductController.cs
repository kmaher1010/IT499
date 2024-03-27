using Library.WebApi.Services.DonutRepository;
using Microsoft.AspNetCore.Mvc;

namespace Donut.Api.Controllers {
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase {

        private readonly IDonutRepository _donutRepository;
        public ProductController(IDonutRepository donutRepository) {
            _donutRepository = donutRepository;
        }

        [HttpGet("all")]
        public async Task<ActionResult<List<Product>>> GetAll() {
            return Ok(await _donutRepository.GetAllProducts());
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id) {
            return Ok(await _donutRepository.GetProductById(id));
        }

        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product) {
            return Ok(await _donutRepository.AddProduct(product));
        }
        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product) {
            return Ok(await _donutRepository.UpdateProduct(product));
        }
        [HttpDelete]
        public async Task<ActionResult<Product>> Delete(int id) {
            return Ok(await _donutRepository.DeleteProduct(id));
        }

    }
}
