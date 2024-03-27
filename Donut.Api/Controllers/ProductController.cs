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
        /// <summary>
        /// get all products from the donut repository
        /// </summary>
        [HttpGet("all")]
        public async Task<ActionResult<List<Product>>> GetAll() {
            return Ok(await _donutRepository.GetAllProducts());
        }
        /// <summary>
        /// Get a single product by its primary key
        /// </summary>
        /// <param name="id"></param>
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetById(int id) {
            return Ok(await _donutRepository.GetProductById(id));
        }
        /// <summary>
        /// Add a new product to the donut repository
        /// </summary>
        /// <param name="product"></param>
        [HttpPost]
        public async Task<ActionResult<Product>> Add(Product product) {
            return Ok(await _donutRepository.AddProduct(product));
        }
        /// <summary>
        /// Update an existing product in the donut repository
        /// </summary>
        /// <param name="product"></param>
        [HttpPut]
        public async Task<ActionResult<Product>> Update(Product product) {
            return Ok(await _donutRepository.UpdateProduct(product));
        }
        /// <summary>
        /// Delete a product from the donut repository
        /// </summary>
        /// <param name="id"></param>
        [HttpDelete]
        public async Task<ActionResult<Product>> Delete(int id) {
            return Ok(await _donutRepository.DeleteProduct(id));
        }

    }
}
