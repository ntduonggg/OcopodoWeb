using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ocopodo.Application.Catalog.Products;
using Ocopodo.ViewModels.Catalog.ProductImages;
using Ocopodo.ViewModels.Catalog.Products;
using static System.Net.Mime.MediaTypeNames;

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

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] ProductCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var productId = await _productService.Create(request);
            if (productId == 0)
                return BadRequest();

            var product = await _productService.GetProductById(productId);

            return CreatedAtAction(nameof(GetProductById), new { id = productId }, product);
        }

        [HttpPut("{productId}")]
        public async Task<IActionResult> Update([FromRoute] int productId, [FromForm] ProductUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            productId = request.Id;
            var result = await _productService.Update(productId, request);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}")]
        public async Task<IActionResult> Delete(int productId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.Delete(productId);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpPatch("{productId}/{newPrice}")]
        public async Task<IActionResult> UpdatePrice(int productId, decimal newPrice)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.UpdatePrice(productId, newPrice);
            if (!result)
            {
                return BadRequest();
            }
            return Ok();
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

        //[HttpGet("paging")]
        //public async Task<IActionResult> GetProductsByKeyword(GetProductPagingRequest request)
        //{
        //    var products = await _productService.GetAllPaging(request);
        //    return Ok(products);
        //}

        [HttpPost("{productId}/images")]
        public async Task<IActionResult> AddImage(int productId, [FromForm] ProductImageCreateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var imageId = await _productService.AddImage(productId, request);
            if (imageId == 0)
                return BadRequest();
            var image = await _productService.GetImageById(imageId);

            return CreatedAtAction(nameof(GetImageById), new { id = imageId }, image);
        }

        [HttpPut("{productId}/images/{imageId}")]
        public async Task<IActionResult> UpdateImage(int imageId, [FromForm] ProductImageUpdateRequest request)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            imageId = request.Id;
            var result = await _productService.UpdateImage(imageId, request);
            if (result == 0)
            {
                return BadRequest();
            }
            return Ok();
        }

        [HttpDelete("{productId}/images/{imageId}")]
        public async Task<IActionResult> RemoveImage(int imageId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _productService.RemoveImage(imageId);
            if (result == 0)
                return BadRequest();

            return Ok();
        }

        [HttpGet("{productId}/images/{imageId}")]
        public async Task<IActionResult> GetImageById(int imageId)
        {
            var image = await _productService.GetImageById(imageId);
            if (image == null)
                return BadRequest("Không tìm thấy ảnh");
            return Ok(image);
        }

        [HttpGet("{productId}/images")]
        public async Task<IActionResult> GetListProductImage(int productId)
        {
            var image = await _productService.GetListImage(productId);
            if (image == null)
                return BadRequest("Không tìm thấy ảnh");
            return Ok(image);
        }
    }
}
