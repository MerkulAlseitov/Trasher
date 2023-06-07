using Trasher.Domain.Common;
using Trasher.Domain.Enums;
using Trasher.Domain.Users;

namespace Trasher.Domain.Entities.Orders
{
    public class Order : BaseEntity
    {
        public OrderStatus OrderStatus { get; set; }
        public OrderType OrderType { get; set; }
        public string Comments { get; set; }
  
        public int? ReviewId { get; set; }
        public Review? Review { get; set; }
        public string? ClientId { get; set; }
        public Client? Client { get; set; }  
        public string? BrigadeId { get; set; }
        public Brigade? Brigade { get; set; }
        public string? OperatorId { get; set; }
        public Operator? Operator { get; set; }
    }
}
