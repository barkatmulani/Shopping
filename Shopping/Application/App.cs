using Shopping.Logic;
using System;
using System.Collections.Generic;

namespace Shopping
{
    public class App
    {
        private ShoppingCart _shoppingCart;
        private List<Order> _orders;
        private List<Product> _products;

        public App()
        {
            _shoppingCart = new ShoppingCart();
            _orders = new List<Order>();
            _products = new List<Product>
            {
                new Product(1, "Engine Oil", 50),
                new Product(2, "Filter", 20),
                new Product(3, "Mat", 40),
                new Product(4, "Brake Fluid", 30),
                new Product(5, "Radiator Coolant", 25)
            };
        }

        public void Run()
        {
            var menu = new Menu("Main Menu", [
                new MenuOption("Product", () => ProductMenu.Run(_products)),
                new MenuOption("Shopping Cart", () => ShoppingCartMenu.Run(_shoppingCart, _products)),
                new MenuOption("Order", () => OrdersMenu.Run(_orders, _products)),
                new MenuOption("Exit", () => { }, true)
            ]);

            menu.Display();
        }
    }
}
