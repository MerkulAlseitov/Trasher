using Trasher.Domain.Common;
using Trasher.Domain.Entities.Orders;

namespace Trasher.Domain.Users
{
    public class Client : User
    {
        public string? CompanyName { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }

    }
}
