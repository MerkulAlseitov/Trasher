using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trasher.API.MODELS.Response;
using Trasher.BLL.Interfaces;
using Trasher.Domain.DTOs;
using Trasher.Domain.Users;

namespace Trasher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperatorController : ControllerBase
    {
        private readonly IOperatorService _ioperatorService;

        public OperatorController(IOperatorService operatorService)
        {
            _ioperatorService = operatorService;
        }

        [HttpGet("GetActiveOrders")]
        public async Task<ActionResult<IResponse<IEnumerable<OrderDTO>>>> GetActiveOrders(string id)
        {
            var response = await _ioperatorService.GetActiveOrders(id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMassage);
        }

        [HttpGet("GetClosedOrders")]
        public async Task<ActionResult<IResponse<IEnumerable<OrderDTO>>>> GetClosedOrders(string id)
        {
            var response = await _ioperatorService.GetClosedOrders(id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("AcceptOrder")]
        public async Task<ActionResult<IResponse<bool>>> AcceptOrder([FromForm] int orderId, [FromForm] string id)
        {
            var response = await _ioperatorService.AcceptOrder(orderId, id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("MarkOrderAsCompleted")]
        public async Task<ActionResult<IResponse<bool>>> MarkOrderAsCompleted([FromForm] int orderId)
        {
            var response = await _ioperatorService.MarkOrderAsCompleted(orderId);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMassage);
        }

        [HttpGet("GetAllOperators")]
        public async Task<ActionResult<IResponse<IEnumerable<Operator>>>> GetAllOperators()
        {
            var response = await _ioperatorService.GetAllAsync();
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMassage);
        }

        [HttpPut("UpdateOperator")]
        public async Task<ActionResult<IResponse<bool>>> UpdateOperator([FromBody] Operator user)
        {
            var response = await _ioperatorService.UpdateAsync(user);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("CloseOrderByOperator")]
        public async Task<ActionResult<IResponse<bool>>> CloseOrderByOperator([FromForm] int orderId, [FromForm] string id)
        {
            var response = await _ioperatorService.CloseOrderByOperator(orderId, id);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }
            return BadRequest(response.ErrorMassage);
        }
    }
}

