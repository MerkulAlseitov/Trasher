using Microsoft.AspNetCore.Mvc;
using Trasher.BLL.Implementations;
using Trasher.BLL.Interfaces;
using Trasher.Domain.DTOs;
using Trasher.Domain.Enums;



namespace Trasher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _iorderService;

        public OrderController(OrderService orderService)
        {
            _iorderService = orderService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrder(OrderDTO order)
        {
            var response = await _iorderService.CreateOrder(order);

            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }

        [HttpPut("{orderId}/status")]
        public async Task<IActionResult> ChangeOrderStatus(int orderId, OrderStatus newStatus)
        {
            var response = await _iorderService.ChangeOrderStatus(orderId, newStatus);

            if (response.IsSuccess)
            {
                return Ok();
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }

        [HttpGet("unassigned")]
        public async Task<IActionResult> GetUnassignedOrders()
        {
            var response = await _iorderService.GetUnassignedOrder();

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }
        [HttpPut]
        public async Task<IActionResult> Update(OrderDTO model)
        {
            var response = await _iorderService.UpdateOrder(model);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }


        [HttpPost]
        [Route("AssignOrderToBrigade")]
        public async Task<IActionResult> AssignOrderToBrigade(int orderId, string brigadeId)
        {
            var response = await _iorderService.AssignOrderToBrigade(orderId, brigadeId);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
         }

        [HttpPost]
        [Route("AssignOrderToOperator")]
        public async Task<IActionResult> AssignOrderToOperator(int orderId, string brigadeId)
        {
            var response = await _iorderService.AssignOrderToOperator(orderId, brigadeId);
            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return StatusCode(response.StatusCode, response.ErrorMassage);
        }
    }
}
