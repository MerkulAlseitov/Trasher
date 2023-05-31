using Trasher.Domain.Common;
using Trasher.Domain.Entities.Orders;

namespace Trasher.Domain.Users
{
    public class Operator : User
    {
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
