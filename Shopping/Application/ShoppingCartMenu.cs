using System;
using System.Collections.Generic;
using Shopping.Logic;

namespace Shopping
{
    public static class ShoppingCartMenu
    {
        public static void Run(ShoppingCart cart, List<Product> products)
        {
            var menu = new Menu("Shopping Cart Menu", [
                new MenuOption("Add Product to Cart", () => AddProduct(cart, products)),
                new MenuOption("Remove Product from Cart", () => RemoveProduct(cart, products)),
                new MenuOption("Remove All Products from Cart", () => RemoveAllProducts(cart)),
                new MenuOption("Get Total Price", () => GetTotalPrice(cart)),
                new MenuOption("Get Shopping Cart Details", () => GetCartDetails(cart)),
                new MenuOption("Get Products By Keyword", () => GetProductsByKeyword(cart)),
                new MenuOption("Get Products By Price Range", () => GetProductsByPriceRange(cart)),
                new MenuOption("Exit", () => { }, true)
            ]);

            menu.Display();
        }

        private static void AddProduct(ShoppingCart cart, List<Product> products)
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

            var res = cart.AddProduct(product);
            if (res)
            {
                Console.WriteLine($"Product '{product.Name}' added to Shopping Cart");
            }
        }

        private static void RemoveProduct(ShoppingCart cart, List<Product> products)
        {
            Console.WriteLine("\nRemove Product");
            Console.WriteLine("-----------");
            Console.WriteLine("Product Id");

            int.TryParse(Console.ReadLine(), out int id);
            bool res = cart.RemoveProduct(id);
            if (res)
            {
                Console.WriteLine($"\nProduct removed from Shopping Cart");
            }
            else
            {
                Console.WriteLine("\nProduct doesn't exist in the Shopping Cart");
            }
        }

        private static void RemoveAllProducts(ShoppingCart cart)
        {
            cart.RemoveAllProducts();
            Console.WriteLine("\nAll Products removed from the Shopping Cart");
        }


        private static void GetTotalPrice(ShoppingCart cart)
        {
            var menu = new Menu("Select Discount Type", [
                new MenuOption("Percentage Discount", () => ShowPrices(cart, 1)),
                new MenuOption("Fixed Price Discount", () => ShowPrices(cart, 2))
            ], 2, true);

            menu.Display();
        }

        private static void ShowPrices(ShoppingCart cart, int num)
        {
            var type = (DiscountType)num;
            Console.WriteLine($"\nTotal Price = {cart.GetTotalPrice()}");
            Console.WriteLine($"DiscountedPrice = {cart.GetDiscountedPrice(type)}");
        }

        private static void GetCartDetails(ShoppingCart cart)
        {
            cart.DisplayDetails();
        }

        private static void GetProductsByKeyword(ShoppingCart cart)
        {
            Console.WriteLine("\nProducts by Keyword");
            Console.WriteLine("-------------------");

            Console.WriteLine("Enter Keyword: ");
            string keyword = Console.ReadLine();

            if (!keyword.Equals(""))
            {
                var products = cart.GetProductsByKeyword(keyword);

                Console.WriteLine($"\nProducts containing '{keyword}'");
                Console.WriteLine("----------------------" + new string('-', keyword.Length));
                products.ForEach(p =>
                {
                    Console.WriteLine($"Id: {p.Id} - Name: {p.Name} - Price: {p.Price}");
                });
            }
        }


        private static void GetProductsByPriceRange(ShoppingCart cart)
        {
            Console.WriteLine("\nProducts by Price Range");
            Console.WriteLine("-----------------------");

            Console.WriteLine("Enter Min Price:");
            float.TryParse(Console.ReadLine(), out float minPrice);

            Console.WriteLine("Enter Max Price:");
            float.TryParse(Console.ReadLine(), out float maxPrice);

            var products = cart.GetProductsByPriceRange(minPrice, maxPrice);

            Console.WriteLine($"\nProducts in Price Range");
            Console.WriteLine("------------------------");
            products.ForEach(p =>
            {
                Console.WriteLine($"Id: {p.Id} - Name: {p.Name} - Price: {p.Price}");
            });
        }
    }
}
