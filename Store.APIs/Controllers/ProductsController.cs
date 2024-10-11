using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Store.APIs.Errors;
using Store.Core.Dtos.Products;
using Store.Core.Helper;
using Store.Core.Services.Contract;
using Store.Core.Specifications.Products;

namespace Store.APIs.Controllers
{
    
    public class ProductsController : BaseApiController
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }


        [ProducesResponseType(typeof(PaginationResponse<ProductDto>), StatusCodes.Status200OK)]
        [HttpGet] // Get BaseUrl/api/Products
        public async Task<ActionResult<PaginationResponse<ProductDto>>> GetAllProducts([FromQuery] ProductSpecParams productSpec)
        {
            var result = await _productService.GetAllProductsAsync(productSpec);

            return Ok(result);
        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("brands")] // Get BaseUrl/api/Products/brands
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllBrands()
        {
            var result = await _productService.GetAllBrandsAsync();

            return Ok(result);
        }


        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [HttpGet("types")] // Get BaseUrl/api/Products/types
        public async Task<ActionResult<IEnumerable<TypeBrandDto>>> GetAllTypes()
        {
            var result = await _productService.GetAllTypesAsync();

            return Ok(result);
        }

        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(IEnumerable<TypeBrandDto>), StatusCodes.Status404NotFound)]
        [HttpGet("{id}")] // Get BaseUrl/api/Products
        public async Task<IActionResult> GetProductById(int? id)
        {
            if(id is null) return BadRequest(new ApiErrorResponse(400));

            var result = await _productService.GetProductByIdAsync(id.Value);

            if(result is null) return NotFound(new ApiErrorResponse(404 , $"The Product With Id : {id} not found at Db :("));

            return Ok(result);
        }
    }
}
