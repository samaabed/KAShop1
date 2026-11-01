using KASHOP.DAL.Data;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KAShop.PL.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandsController : ControllerBase
    {
        //GenericRepository<Brand> repo = new GenericRepository<Brand>();

        //[HttpGet("")]
        //public IActionResult Index()
        //{
        //    var brands = repo.Get();
        //    return Ok(brands);
        //}


        //[HttpPost("")]
        //public IActionResult Create(Brand brand)
        //{
        //    repo.Create(brand);
        //    return Ok();
        //}
    }
}
