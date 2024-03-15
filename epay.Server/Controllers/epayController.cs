using epay.Server.data;
using epay.Server.Models;
using epay.Server.Validation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace epay.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class epayController : ControllerBase
    {

        [HttpPost("InsertCustomer")]
        public async Task<ActionResult<object>> InsertCustomer(List<Customer> customers)
        {

            try
            {
                var errors = new List<Error>();
                foreach (var customer in customers)
                    customer.Validate(ref errors);

                if (errors.Count > 0) { return Ok(errors); }
                DataContext.WriteData(customers);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
                
            }

        }

        [HttpGet("GetCustomer")]
        public async Task<ActionResult<object>> GetCustomer(List<Customer> customers)
        {

            var errors = new List<Error>();
            foreach (var customer in customers)
                customer.Validate(ref errors);

            if (errors.Count > 0) { return Ok(errors); }
            DataContext.WriteData(customers);

            return Ok();

        }
    }


}
