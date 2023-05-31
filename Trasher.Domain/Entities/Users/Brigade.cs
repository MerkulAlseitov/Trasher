using Trasher.Domain.Common;
using Trasher.Domain.Entities.Orders;

namespace Trasher.Domain.Users
{
    public class Brigade : User
    {
        public string BrigadeName { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
