using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trasher.API.MODELS.Response;
using Trasher.BLL.IClientService;
using Trasher.BLL.Implementations;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Users;
using Trasher.BLL.Interfaces;



namespace Trasher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrigadeController : ControllerBase
    {
        private readonly IBrigadeService _ibrigadeService;

        public BrigadeController(IBrigadeService brigadeService)
        {
            _ibrigadeService = brigadeService;
        }


        [HttpGet("GetActiveOrders")]
        public async Task<ActionResult<IResponse<IEnumerable<OrderDTO>>>> GetActiveOrders(string id)
        {
            var response = await _ibrigadeService.GetActiveOrders(id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpGet("GetClosedOrders")]
        public async Task<ActionResult<IResponse<IEnumerable<OrderDTO>>>> GetClosedOrders(string id)
        {
            var response = await _ibrigadeService.GetClosedOrders(id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("AcceptOrder")]
        public async Task<ActionResult<IResponse<bool>>> AcceptOrder([FromForm] int orderId, [FromForm] string id)
        {
            var response = await _ibrigadeService.AcceptOrder(orderId, id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("MarkOrderAsCompleted")]
        public async Task<ActionResult<IResponse<bool>>> MarkOrderAsCompleted([FromForm] int orderId)
        {
            var response = await _ibrigadeService.MarkOrderAsCompleted(orderId);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpGet("GetAllBrigades")]
        public async Task<ActionResult<IResponse<IEnumerable<Brigade>>>> GetAllBrigades()
        {
            var response = await _ibrigadeService.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpPut("UpdateBrigade")]
        public async Task<ActionResult<IResponse<bool>>> UpdateBrigade([FromBody] Brigade user)
        {
            var response = await _ibrigadeService.UpdateAsync(user);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }
    }

}
