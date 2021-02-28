using ImmutabilitySample.Entities;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ImmutabilitySample
{
    public class MyApp
    {
        private readonly IServiceProvider serviceProvider;

        public MyApp(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public async Task Run()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine();
                    Console.WriteLine("----------------------");
                    Console.WriteLine("Order Information!");
                    Console.WriteLine();
                    Console.WriteLine("Available actions:");
                    Console.WriteLine("1 => List orders");
                    Console.WriteLine("2 => Add random order");
                    Console.WriteLine("3 => Delete order");
                    Console.WriteLine("0 => Exit application");
                    Console.WriteLine();
                    Console.Write("Select option: ");
                    var response = Console.ReadLine();

                    if (response.Equals("0")) break;
                    else if (response.Equals("1")) await ListOrders();
                    else if (response.Equals("2")) await AddOrder();
                    else if (response.Equals("3"))
                    {
                        Console.Write("Enter order Id: ");
                        var id = Console.ReadLine();
                        await DeleteOrder(id);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"An error ocurred: {ex.Message}");
                    Console.WriteLine("Exception details:");
                    Console.WriteLine(ex.StackTrace);
                }
            }
        }

        private async Task ListOrders()
        {
            List<Order> orders = new List<Order>();

            using (var scope = serviceProvider.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                orders = await orderService.GetOrders();
            }

            Console.WriteLine("List of orders:");
            Console.WriteLine();

            foreach (var order in orders)
            {
                Console.WriteLine(JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true }));
            }
        }

        private async Task AddOrder()
        {
            Order order;

            using (var scope = serviceProvider.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                order = await orderService.AddOrderRandom();
            }

            Console.WriteLine();
            Console.WriteLine("----------------------");
            Console.WriteLine("Added order:");
            Console.WriteLine();
            Console.WriteLine(JsonSerializer.Serialize(order, new JsonSerializerOptions { WriteIndented = true }));
        }

        private async Task DeleteOrder(string orderId)
        {
            var id = Convert.ToInt32(orderId);

            using (var scope = serviceProvider.CreateScope())
            {
                var orderService = scope.ServiceProvider.GetRequiredService<IOrderService>();

                await orderService.DeleteOrder(id);
            }

            Console.WriteLine();
            Console.WriteLine("----------------------");
            Console.WriteLine("Order deleted succesfully");
            Console.WriteLine();
        }
    }
}
