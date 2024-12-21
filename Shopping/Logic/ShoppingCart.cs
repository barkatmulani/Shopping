using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;

namespace Shopping.Logic
{
    public class ShoppingCart
    {
        private string fileName = "../../../shopping-cart.json";

        public ShoppingCart()
        {
        }

        public bool AddProduct(Product product)
        {
            var products = ReadFromFile();

            try
            {
                if (products.Any(x => x.Id == product.Id)) {
                    throw new DuplicateProductException(product);
                }
                products.Add(product);
                SaveToFile(products);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
            return true;
        }

        public bool RemoveProduct(int id)
        {
            var products = ReadFromFile();

            var product = products.Find(p => p.Id == id);
            if (product != null)
            {
                products.Remove(product);
                SaveToFile(products);
                return true;
            }
            return false;
        }

        public void RemoveAllProducts()
        {
            var products = GetProducts();
            products.Clear();
            SaveToFile(products);
        }

        public float GetTotalPrice()
        {
            return GetProducts().Sum(p => p.Price);
        }

        public float GetDiscountedPrice(DiscountType type)
        {
            var price = GetTotalPrice();
            var discManager = new DiscountManager(price, type);
            return discManager.GetPriceAfterDiscount();
        }

        public List<Product> GetProductsByKeyword(string keyword)
        {
            return GetProducts().Where(p => p.Name.ToLower().Contains(keyword.ToLower())).ToList();
        }

        public List<Product> GetProductsByPriceRange(float minPrice = 0, float maxPrice = -1) {
            return GetProducts().Where(p => (p.Price >= minPrice) && (maxPrice == -1 || p.Price <= maxPrice)).ToList();
        }

        public void DisplayDetails()
        {
            Console.WriteLine("\nShopping Cart Details");
            Console.WriteLine("---------------------");

            GetProducts().ForEach(p =>
            {
                Console.WriteLine($"Id: {p.Id} - Name: {p.Name} - Price: {p.Price}");
            });
        }

        private List<Product> GetProducts()
        {
            return ReadFromFile();
        }

        private List<Product> ReadFromFile()
        {
            if (File.Exists(fileName))
            {
                var jsonText = File.ReadAllText(fileName);
                return JsonConvert.DeserializeObject<List<Product>>(jsonText);
            }
            else
            {
                return new List<Product>();
            }
        }

        private void SaveToFile(List<Product> products)
        {
            string json = JsonConvert.SerializeObject(products.ToArray());
            using (StreamWriter outputFile = new StreamWriter(fileName))
            {
                outputFile.WriteLine(json);
            }
        }
    }

    internal class DuplicateProductException : Exception
    {
        public DuplicateProductException(Product product) : this($"Product with Id '{product.Id}' already exists.")
        {
        }

        private DuplicateProductException(string message) : base(message)
        {
        }

    }
}
