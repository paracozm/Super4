using Microsoft.AspNetCore.Mvc;
using Super4.Application.Application;
using Super4.Application.DataContract.Request.Product;
using Super4.Application.DataContract.Request.Stock;
using Super4.Application.Interfaces;


namespace Super4.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StockController : ControllerBase
    {
        private readonly IStockApplication _stockApplication;

        public StockController(IStockApplication stockApplication)
        {
            _stockApplication = stockApplication;
        }


        [HttpGet]
        public async Task<ActionResult> Get()
        {
            var response = await _stockApplication.GetAllAsync();
            return Ok(new
            {
                Message = "All stock returned.",
                response

            });
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            try
            {
                var response = await _stockApplication.GetByIdAsync(id);
                return Ok(new
                {
                    Message = "Stock returned.",
                    response

                });
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateStockRequest request)
        {
            try
            {
                await _stockApplication.CreateAsync(request);
                return Ok(new
                {
                    Message = "Stock created.",
                    request

                });
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }


        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, UpdateStockRequest request)
        {
            try
            {
                await _stockApplication.UpdateAsync(id, request);
                return Ok(new
                {
                    Message = "Stock updated.",
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
