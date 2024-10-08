using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.Core.Dtos.Products;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;

namespace Store.APIs.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet] // Get BaseUrl/api/Products
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var result = await _productService.GetAllProductsAsync(productSpec);

            return Ok(result);
        }

        [HttpGet("brands")] // Get BaseUrl/api/Products/brands
        public async Task<IActionResult> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);
        }

        [HttpGet("types")] // Get BaseUrl/api/Products/types
        public async Task<IActionResult> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);
        }
        [HttpGet("{id}")] // Get BaseUrl/api/Products

        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null) return BadRequest("Invalid id !!");

            var result = await _productService.GetProductByIdAsync(id.Value);

            if(result is null) return NotFound($"The Product With Id : {id} not found at Db :(");

            return Ok(result);
        }
    }
}
