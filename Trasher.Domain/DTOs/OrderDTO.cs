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
    public class OrderDTO
    {
        public OrderStatus OrderStatus { get; set; }
        public OrderType OrderType { get; set; }
        public string Comments { get; set; }

        public int? ReviewId { get; set; }
        public Review? Review { get; set; }
        public string ClientId { get; set; }
        public Client Client { get; set; }
        public string? BrigadeId { get; set; }
        public Brigade? Brigade { get; set; }
        public string? OperatorId { get; set; }
        public Operator? Operator { get; set; }

    }
}
