using Microsoft.AspNetCore.Mvc;
using Super4.Application.DataContract.Request.Product;
using Super4.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Super4.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductApplication _productApplication;

        public ProductController(IProductApplication productApplication)
        {
            _productApplication = productApplication;
        }

        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _productApplication.GetAllAsync();
            return Ok(new
            {
                Message = "All products returned.",
                response
            });
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var response = await _productApplication.GetByIdAsync(id);
                return Ok(new
                {
                    Message = "Product returned.",
                    response
                });
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateProductRequest request)
        {
            try
            {
                await _productApplication.CreateAsync(request);
                return Ok(new
                {
                    Message = "Product created.",
                    request
                });
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, UpdateProductRequest request)
        {
            try
            {
                await _productApplication.UpdateAsync(id, request);
                return Ok(new
                {
                    Message = "Product updated.",
                    request
                });
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }
    }
}
