using Microsoft.AspNetCore.Mvc;
using Super4.Application.DataContract.Request.Customer;
using Super4.Application.Interfaces;


namespace Super4.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerApplication _customerApplication;

        public CustomerController(ICustomerApplication customerApplication)
        {
            _customerApplication = customerApplication;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var response = await _customerApplication.GetAllAsync();
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            try
            {
                var response = await _customerApplication.GetByIdAsync(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }

        }


        [HttpPost]
        public async Task<ActionResult> Post([FromBody] CreateCustomerRequest request)
        {
            /*var response = await _customerApplication.CreateAsync(request);

            if (!ModelState.IsValid)
            {
                return UnprocessableEntity(response);
            }
            return Ok(response);*/

            try
            {
                await _customerApplication.CreateAsync(request);
                return Ok(request);
            }
            catch(Exception ex)
            {
                return UnprocessableEntity(ex.Message);
            }
        }

    }
}
