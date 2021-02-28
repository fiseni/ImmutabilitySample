using ImmutabilitySample.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ImmutabilitySample
{
    public interface IOrderService
    {
        Task<Order> AddOrderRandom();
        Task DeleteOrder(int orderId);
        Task<List<Order>> GetOrders();
    }
}
