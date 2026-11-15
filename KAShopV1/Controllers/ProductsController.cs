using KASHOP.BLL;
using KASHOP.DAL.DTO.Requests;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAShop.PL.Controllers
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

        [HttpPost("")]
        public async Task<IActionResult> CreateAsync([FromForm] ProductRequest request, CancellationToken cancellationToken)
        {
            var result = await _productService.CreateAsync(request, cancellationToken);
            return result ? Ok() : BadRequest("Invalid product data");
        }


        [HttpGet("")]
        public async Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var result = await _productService.GetAllAsync(cancellationToken);
            return Ok(result);
        }
    }
}
