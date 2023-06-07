using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.Domain.Entities.Orders;

namespace Trasher.Domain.DTOs
{
    public class ReviewDTO : BaseEntityDTO
    {
        public string? ReviewText { get; set; }
        public int Score { get; set; }
        public int orderId { get; set; }
    }
}