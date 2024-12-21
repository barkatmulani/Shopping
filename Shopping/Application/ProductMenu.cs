using Shopping.Logic;
using System;
using System.Collections.Generic;

namespace Shopping
{
    public static class ProductMenu
    {
        public static void Run(List<Product> products)
        {
            var menu = new Menu("Products Menu", [
                new MenuOption("List all products", () => {
                    Console.WriteLine("\nProducts");
                    Console.WriteLine("--------");

                    foreach (var prod in products)
                    {
                        prod.DisplayDetails();
                    }
                }),
                new MenuOption("Exit", () => { }, true)
            ]);

            menu.Display();
        }
    }
}
