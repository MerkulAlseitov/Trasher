using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.Domain.Entities.Orders;
using Trasher.Domain.Enums;
using Trasher.Domain.Users;

namespace Trasher.Domain.DTOs
{
    public class OrderDTO : BaseEntityDTO
    {
        public OrderStatus OrderStatus { get; set; }
        public OrderType OrderType { get; set; }
        public string Comments { get; set; }

        public string? ClientId { get; set; }
    }
}
