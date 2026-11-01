using KASHOP.BLL;
using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {

        private readonly ICategoryService _service;

        public CategoriesController(ICategoryService service) 
        { 
            _service = service;
        }

        [HttpGet("")]
        public async  Task<IActionResult> Index(CancellationToken cancellationToken)
        {
            var categories = await _service.GetAllAsync(cancellationToken);
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id, CancellationToken cancellationToken)
        {
            var category = await _service.GetByIdAsync(id, cancellationToken);
            return Ok(category);
        }
        //[HttpPost("")]
        //public IActionResult Create(Category category)
        //{
        //    _service.Create(category);
        //    return Ok();
        //}

    }
}
