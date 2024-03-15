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

                if (errors.Count > 0)
                    return Ok(errors);

                DataContext.WriteData(customers);
                return Ok(new ApiResult
                {
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        Message = "ok",
                        AppStatusCode = AppStatusCode.Success
                    }

                });

            }
            catch (Exception ex)
            {
                return BadRequest(new ApiResult
                {
                    IsSuccess = false,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        AppStatusCode = AppStatusCode.LogicError
                    }
                });

            }

        }

        [HttpGet("GetCustomer")]
        public async Task<ActionResult<object>> GetCustomer()
        {

            try
            {
                var AllCustomers = await DataContext.ReadData();

                return Ok(new ApiResult<List<Customer>>
                {

                    Data = AllCustomers,
                    IsSuccess = true,
                    MetaData = new MetaData
                    {
                        AppStatusCode = AppStatusCode.Success,
                        Message = "ok"
                    }

                });
            }
            catch (Exception ex)
            {

                return BadRequest(new ApiResult
                {
                    IsSuccess = false,
                    MetaData = new MetaData
                    {
                        Message = ex.Message,
                        AppStatusCode = AppStatusCode.LogicError
                    }
                });
            }

        }
    }


}
