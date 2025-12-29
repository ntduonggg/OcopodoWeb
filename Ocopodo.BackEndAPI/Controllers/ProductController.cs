using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocopodo.Application.Catalog.Products;
using Ocopodo.ViewModels.Catalog.Products;

namespace Ocopodo.BackEndAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var products = await _productService.GetAll();
            return Ok(products);
        }

        [HttpGet("{productId}")]
        public async Task<IActionResult> GetProductById(int productId)
        {
            var product = await _productService.GetProductById(productId);
            return Ok(product);
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetProductsByKeyword(GetProductPagingRequest request)
        {
            var products = await _productService.GetAllPaging(request);
            return Ok(products);
        }
    }
}
