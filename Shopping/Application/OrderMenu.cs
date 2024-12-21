using System;
using System.Collections.Generic;
using Shopping.Logic;

namespace Shopping
{
    public static class OrderMenu
    {
        public static void Run(Order order, List<Product> products)
        {
            var menu = new Menu($"Order: {order.Id}", [
                new MenuOption("Add Product to the Order", () => AddProduct(order, products)),
                new MenuOption("Remove Product from the Order", () => RemoveProduct(order, products)),
                new MenuOption("Remove All Products from the Order", () => RemoveAllProducts(order)),
                new MenuOption("Get Total Price", () => GetTotalPrice(order)),
                new MenuOption("Get Order Details", () => GetOrderDetails(order)),
                new MenuOption("Exit", () => { }, true)
            ]);

            menu.Display();
        }

        private static void AddProduct(Order order, List<Product> products)
        {
            Console.WriteLine("\nAdd Product");
            Console.WriteLine("-----------");
            Console.WriteLine("Product Id");

            bool loop = true;
            Product product = null;
            while (loop)
            {
                int.TryParse(Console.ReadLine(), out int id);
                product = products.Find(p => p.Id == id);
                if (product == null)
                {
                    Console.WriteLine("\nProduct doesn't exist");
                }
                else
                {
                    loop = false;
                }
            }
            Console.WriteLine("Quantity");
            int.TryParse(Console.ReadLine(), out int quantity);

            order.AddOrUpdateProduct(product, quantity);
            Console.WriteLine($"Product '{product.Name}' added to the Order with quantity {quantity}");
        }

        private static void RemoveProduct(Order order, List<Product> products)
        {
            Console.WriteLine("\nRemove Product");
            Console.WriteLine("-----------");
            Console.WriteLine("Product Id");

            int.TryParse(Console.ReadLine(), out int id);
            bool res = order.RemoveProduct(id);
            if (res)
            {
                Console.WriteLine($"\nProduct removed from the Order");
            }
            else
            {
                Console.WriteLine("\nProduct doesn't exist in the Order");
            }
        }

        private static void RemoveAllProducts(Order order)
        {
            order.RemoveAllProducts();
            Console.WriteLine("\nAll Products removed from the Order");
        }

        private static void GetTotalPrice(Order order)
        {
            var menu = new Menu("Select Discount Type", [
                new MenuOption("Percentage Discount", () => ShowPrices(order, 1)),
                new MenuOption("Fixed Price Discount", () => ShowPrices(order, 2))
            ], 2, true);

            menu.Display();
        }

        private static void ShowPrices(Order order, int num)
        {
            var type = (DiscountType)num;
            Console.WriteLine($"\nTotal Price = {order.GetTotalPrice()}");
            Console.WriteLine($"DiscountedPrice = {order.GetDiscountedPrice(type)}");
        }

        private static void GetOrderDetails(Order order)
        {
            order.DisplayDetails();
        }
    }
}
