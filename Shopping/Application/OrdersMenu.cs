using System;
using System.Collections.Generic;
using System.Diagnostics;
using Shopping.Logic;

namespace Shopping
{
    public static class OrdersMenu
    {
        public static void Run(List<Order> orders, List<Product> products)
        {
            var menu = new Menu("Orders Menu", [
                new MenuOption("Add New Order", () => AddOrder(orders, products)),
                new MenuOption("Edit Existing Order", () => EditOrder(orders, products)),
                new MenuOption("Exit", () => { }, true)
            ]);

            menu.Display();
        }

        private static void AddOrder(List<Order> orders, List<Product> products)
        {
            var order = new Order();
            orders.Add(order);
            OrderMenu.Run(order, products);
        }

        private static void EditOrder(List<Order> orders, List<Product> products)
        {
            Console.WriteLine("\nEdit Order");
            Console.WriteLine("----------");

            for (int i = 1; i <= orders.Count; i++)
            {
                var order = orders[i - 1];
                Console.WriteLine($"{i} - {order.Id} ({order.ProductCount} products)");
            }

            int.TryParse(Console.ReadLine(), out int num);
            if (num < 1 || num > orders.Count)
            {
                Console.WriteLine("Invalid selection");
            }
            else
            {
                OrderMenu.Run(orders[num - 1], products);
            }
        }
    }
}
