using System;
using System.Collections.Generic;
using System.Linq;

namespace Shopping.Logic
{
    public class Order
    {
        private Guid _id;
        private List<OrderProduct> _orderProducts;

        public Order()
        {
            _id = Guid.NewGuid();
            _orderProducts = new List<OrderProduct>();
        }

        public Guid Id {
            get { return _id; }
        }

        public int ProductCount
        {
            get { return _orderProducts.Count; }
        }

        public void AddOrUpdateProduct(Product product, int quantity = 1)
        {
            var index = _orderProducts.FindIndex(p => p.Product.Id == product.Id);
            if (index != -1)
            {
                var prod = _orderProducts[index];
                prod.Quantity = quantity;
                _orderProducts[index] = prod;
            }
            else
            {
                _orderProducts.Add(new OrderProduct(product, quantity));
            }
        }

        public bool RemoveProduct(int id)
        {
            var prod = _orderProducts.Find(p => p.Product.Id == id);

            if (prod.Product != null)
            {
                _orderProducts.Remove(prod);
                return true;
            }

            return false;
        }

        public void RemoveAllProducts()
        {
            _orderProducts.Clear();
        }

        public float GetTotalPrice()
        {
            return _orderProducts.Select(p => p.Product.Price * p.Quantity).Sum();
        }

        public float GetDiscountedPrice(DiscountType type)
        {
            var price = GetTotalPrice();
            var discManager = new DiscountManager(price, type);
            return discManager.GetPriceAfterDiscount();
        }
        
        public void DisplayDetails()
        {
            Console.WriteLine("\nOrder Details");
            Console.WriteLine("-------------");

            _orderProducts.ForEach(p =>
            {
                Console.WriteLine($"Id: {p.Product.Id} - Name: {p.Product.Name} - Price: {p.Product.Price} - Quantity: {p.Quantity}");
            });
        }

        public struct OrderProduct
        {
            public Product Product { get; set; }
            public int Quantity { get; set; }

            public OrderProduct(Product product, int quantity)
            {
                Product = product;
                Quantity = quantity;
            }
        }
    }
}
