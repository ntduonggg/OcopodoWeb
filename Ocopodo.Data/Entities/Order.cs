using Ocopodo.Data.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ocopodo.Data.Entities
{
    public class Order
    {
        public int Id { get; set; }
        [Required]
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }
        public string CustomerAddress { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        // 0 = New, 1 = Processing, 2 = Completed, 3 = Cancelled
        public OrderStatus Status { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
