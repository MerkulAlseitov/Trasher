using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trasher.Domain.Entities.Orders;

namespace Trasher.Domain.DTOs
{
    public class OperatorDTO
    {
        public string? RefreshToken { get; set; }
        public DateTime RefreshTokenExpiredTime { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public virtual ICollection<Order>? Orders { get; set; }
    }
}
