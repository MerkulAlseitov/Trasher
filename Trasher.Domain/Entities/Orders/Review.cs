using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.Domain.Common;

namespace Trasher.Domain.Entities.Orders
{
    public class Review : BaseEntity
    {
        public string? ReviewText { get; set; }
        public int Score { get; set; }
        public Order? Order { get; set; }
    }
}
