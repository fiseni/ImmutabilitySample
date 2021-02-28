using ImmutabilitySample.DataAccess;
using ImmutabilitySample.Entities;
using ImmutabilitySample.ValueObjects;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImmutabilitySample
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext dbContext;
        private readonly IDateTime dateTimeService;
        private readonly IDocumentNoGenerator documentNoGenerator;

        public OrderService(AppDbContext dbContext,
                            IDateTime dateTimeService,
                            IDocumentNoGenerator documentNoGenerator)
        {
            this.dbContext = dbContext;
            this.dateTimeService = dateTimeService;
            this.documentNoGenerator = documentNoGenerator;
        }

        public Task<List<Order>> GetOrders()
        {
            return dbContext.Orders.Include(x=>x.OrderItems).ToListAsync();
        }

        public async Task DeleteOrder(int orderId)
        {
            var order = await dbContext.Orders.FindAsync(orderId);

            _ = order ?? throw new KeyNotFoundException($"The order with Id: {orderId} is not found!");

            dbContext.Orders.Remove(order);

            await dbContext.SaveChangesAsync();
        }

        public async Task<Order> AddOrderRandom()
        {
            var orderNo = await documentNoGenerator.GetNewOrderNo();

            var randomNo = new Random(5).Next(10, 1000);

            var order = OrderSeed.GenerateOrder(randomNo, orderNo, dateTimeService);

            await dbContext.Orders.AddAsync(order);

            await dbContext.SaveChangesAsync();

            return order;
        }
    }
}
