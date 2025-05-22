using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineOrderManagement.Domain.Entities
{
    public class OrderStatusHistory: AuditableEntity
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public Order Order { get; set; } = null!;
        public string Status { get; set; } = null!;
        public DateTime Timestamp { get; set; }
    }
}
