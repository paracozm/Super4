using Microsoft.AspNetCore.Mvc;
using Super4.Application.DataContract.Request.Order;
using Super4.Application.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Super4.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderApplication _orderApplication;

        public OrderController(IOrderApplication orderApplication)
        {
            _orderApplication = orderApplication;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _orderApplication.GetAllAsync();
            return Ok(new
            {
                Message = "All orders returned.",
                response

            });
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult> Get(string id)
        {
            var response = await _orderApplication.GetByIdAsync(id);
            return Ok(new
            {
                Message = "Order returned.",
                response

            });
        }

        // POST api/<OrderController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateOrderRequest request)
        {
            try
            {
                await _orderApplication.CreateAsync(request);
                return Ok(new
                {
                    Message = "Order created.",
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
