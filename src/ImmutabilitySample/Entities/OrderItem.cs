using ImmutabilitySample.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutabilitySample.Entities
{
    public class OrderItem
    {
        public int Id { get; }
        public string Name { get; }
        public decimal Price { get; }

        public int OrderId { get; }

        private OrderItem(int id, string name, decimal price, int orderId)
        {
            this.Id = id;
            this.Name = name;
            this.Price = price;
            this.OrderId = orderId;
        }

        public OrderItem(string name, decimal price)
        {
            if (string.IsNullOrEmpty(name)) throw new ArgumentNullException(nameof(name));

            this.Name = name;
            this.Price = price;
        }
    }
}
