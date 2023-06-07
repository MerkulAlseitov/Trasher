﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Trasher.API.MODELS.Response;
using Trasher.BLL.IClientService;
using Trasher.Domain.DTOs;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Users;

namespace Trasher.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientService _iclientService;

        public ClientController(IClientService clientService)
        {
            _iclientService = clientService;
        }

        [HttpGet("active-orders/{id}")]
        public async Task<IActionResult> GetActiveOrders(string id)
        {
            IResponse<IEnumerable<OrderDTO>> response = await _iclientService.GetActiveOrders(id);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpGet("closed-orders/{id}")]
        public async Task<IActionResult> GetClosedOrders(string id)
        {
            IResponse<IEnumerable<OrderDTO>> response = await _iclientService.GetClosedOrders(id);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("accept-order/{orderId}/{id}")]
        public async Task<IActionResult> AcceptOrder(int orderId, string id)
        {
            IResponse<bool> response = await _iclientService.AcceptOrder(orderId, id);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("mark-order-completed/{orderId}")]
        public async Task<IActionResult> MarkOrderAsCompleted(int orderId)
        {
            IResponse<bool> response = await _iclientService.MarkOrderAsCompleted(orderId);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpGet("clients")]
        public async Task<IActionResult> GetAllClients()
        {
            IResponse<IEnumerable<Client>> response = await _iclientService.GetAllAsync();

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpPut("update-client")]
        public async Task<IActionResult> UpdateClient([FromBody] Client client)
        {
            IResponse<bool> response = await _iclientService.UpdateAsync(client);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }

        [HttpPost("create-client")]
        public async Task<IActionResult> CreateClient([FromBody] ClientDTO client)
        {
            IResponse<bool> response = await _iclientService.CreateClient(client);

            if (response.IsSuccess)
            {
                return Ok(response.Data);
            }

            return BadRequest(response.ErrorMassage);
        }
    }
}