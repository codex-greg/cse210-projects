using System;
using System.Collections.Generic;

namespace ProductOrderSystem
{
    public class Address
    {
        private string _streetAddress;
        private string _city;
        private string _stateProvince;
        private string _country;

        public Address(string streetAddress, string city, string stateProvince, string country)
        {
            _streetAddress = streetAddress;
            _city = city;
            _stateProvince = stateProvince;
            _country = country;
        }

        public bool IsInUSA()
        {
            return _country == "USA";
        }

        public string GetFullAddress()
        {
            return $"{_streetAddress}\n{_city}, {_stateProvince}\n{_country}";
        }
    }

    public class Customer
    {
        private string _name;
        private Address _address;

        public Customer(string name, Address address)
        {
            _name = name;
            _address = address;
        }

        public string GetName()
        {
            return _name;
        }

        public Address GetAddress()
        {
            return _address;
        }

        public bool IsInUSA()
        {
            return _address.IsInUSA();
        }
    }

    public class Product
    {
        private string _name;
        private string _productId;
        private double _price;
        private int _quantity;

        public Product(string name, string productId, double price, int quantity)
        {
            _name = name;
            _productId = productId;
            _price = price;
            _quantity = quantity;
        }

        public string GetName()
        {
            return _name;
        }

        public string GetProductId()
        {
            return _productId;
        }

        public double GetPrice()
        {
            return _price;
        }

        public int GetQuantity()
        {
            return _quantity;
        }

        public double GetTotalCost()
        {
            return _price * _quantity;
        }
    }

    public class Order
    {
        private List<Product> _products;
        private Customer _custoAmer;

        public Order(Customer customer)
        {
            _customer = customer;
            _products = new List<Product>();
        }

        public void AddProduct(Product product)
        {
            _products.Add(product);
        }

        public double CalculateTotalCost()
        {
            double totalProductCost = 0;
            foreach (Product product in _products)
            {
                totalProductCost += product.GetTotalCost();
            }

            double shippingCost = _customer.IsInUSA() ? 5 : 35;
            return totalProductCost + shippingCost;
        }

        public string GetPackingLabel()
        {
            string packingLabel = "Packing Label:\n";
            foreach (Product product in _products)
            {
                packingLabel += $"{product.GetName()} (ID: {product.GetProductId()})\n";
            }
            return packingLabel;
        }

        public string GetShippingLabel()
        {
            return "Shipping Label:\n" +
                   $"{_customer.GetName()}\n" +
                   $"{_customer.GetAddress().GetFullAddress()}";
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            // Create addresses
            Address address1 = new Address("123 Main St", "Anytown", "CA", "USA");
            Address address2 = new Address("456 Elm St", "Toronto", "ON", "Canada");

            // Create customers
            Customer customer1 = new Customer("John Doe", address1);
            Customer customer2 = new Customer("Alice Smith", address2);

            // Create products
            Product product1 = new Product("Laptop", "LP123", 1200, 1);
            Product product2 = new Product("Mouse", "MS456", 25, 2);
            Product product3 = new Product("Keyboard", "KB789", 75, 1);
            Product product4 = new Product("Monitor", "MN012", 300, 1);
            Product product5 = new Product("Webcam", "WC345", 50, 1);

            // Create orders
            Order order1 = new Order(customer1);
            order1.AddProduct(product1);
            order1.AddProduct(product2);
            order1.AddProduct(product3);

            Order order2 = new Order(customer2);
            order2.AddProduct(product4);
            order2.AddProduct(product5);

            // Display order 1 information
            Console.WriteLine("Order 1:");
            Console.WriteLine(order1.GetPackingLabel());
            Console.WriteLine(order1.GetShippingLabel());
            Console.WriteLine($"Total cost: ${order1.CalculateTotalCost()}");
            Console.WriteLine();

            // Display order 2 information
            Console.WriteLine("Order 2:");
            Console.WriteLine(order2.GetPackingLabel());
            Console.WriteLine(order2.GetShippingLabel());
            Console.WriteLine($"Total cost: ${order2.CalculateTotalCost()}");
        }
    }
}
