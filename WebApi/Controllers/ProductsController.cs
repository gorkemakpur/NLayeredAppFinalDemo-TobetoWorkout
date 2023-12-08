using Business.Abstracts;
using Business.Dtos.Requests;
using Entities.Concretes;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : Controller
    {
        IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost] 
        public async Task<IActionResult> Add([FromBody] CreateProductRequest createProductRequest) 
        { 
            var result = await _productService.Add(createProductRequest); 
            return Ok(result);
        }
        [HttpGet] 
        public async Task<IActionResult> GetList() 
        { 
            var result = await _productService.GetListAsync();
            return Ok(result);
        }


    }
}
