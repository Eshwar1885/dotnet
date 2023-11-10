using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    internal class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public float Price { get; set; }
        public string Picture { get; set; }
        public string Description { get; set; }
        public double Rating { get; set; }
        public float Discount { get; set; }
        public Product()
        {
            Price = 0.0f;
            Discount = 0.5f;
            Quantity = 1;
            Rating = 0;
        }

        public Product(int id, string name, int quantity, float price, string picture, string description, double rating, float discount)
        {
            Id = id;
            Name = name;
            Quantity = quantity;
            Price = price;
            Picture = picture;
            Description = description;
            Rating = rating;
            Discount = discount;
        }
        public override string ToString()
        {
            float netPrice = Price - (Price * Discount / 100);
            return $"Product Id : {Id}\nProduct Name : {Name}\nProduct Price : Rs. {Price}\nProduct Quantity In Hand : {Quantity}" +
                $"\nDiscount offered : {Discount}%\nRating : {Rating}\nNet Price : {netPrice}";
        }
    }
}
//-----------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    internal class ProductRepository
    {
        List<Product> products;

        public ProductRepository()
        {
            products = new List<Product>();
        }
        int GetNextId()
        {
            if (products.Count == 0)
                return 1;
            int id = products[products.Count - 1].Id;
            return ++id;
        }
        void TakeRemainingProductDetails(Product product)
        {
            Console.WriteLine("Please enter the product name");
            product.Name = Console.ReadLine();
            Console.WriteLine("Please enter the product price");
            product.Price = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Please enter the product quantity");
            product.Quantity = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Please enter the product description");
            product.Description = Console.ReadLine();
            Console.WriteLine("Please enter the product discount that you can offer");
            product.Discount = Convert.ToSingle(Console.ReadLine());
            Console.WriteLine("Please enter the product picture path");
            product.Picture = Console.ReadLine();
        }
        public Product Add()
        {
            int id = GetNextId();
            Product product = new Product();
            product.Id = id;
            //Getting the product details from theuser
            TakeRemainingProductDetails(product);
            //Adding to the collection
            products.Add(product);
            return product;
        }
        public List<Product> GetProducts()
        {
            return products;
        }
        public Product GetProductById(int id)
        {
            for (int i = 0; i < products.Count; i++)
            {
                if (products[i].Id == id)
                    return products[i];
            }
            return null;
        }
        public Product Update(int id, Product product, string choice)
        {
            Product myProduct = GetProductById(id);
            if (myProduct != null)
            {

                if (choice == "price")
                {
                    if (product.Price > 0)
                    {
                        myProduct.Price = product.Price;
                        return myProduct;
                    }
                }
                else if (choice == "quanity")
                {
                    if (product.Quantity > 0)
                    {
                        myProduct.Quantity -= product.Quantity;
                        return myProduct;
                    }
                }
                else
                {
                    Console.WriteLine("Invalid choice");
                }
            }
            Console.WriteLine("Could not update");
            return null;
        }
        public Product Delete(int id)
        {
            Product myProduct = GetProductById(id);
            if (myProduct != null)
            {
                products.Remove(myProduct);
                Console.WriteLine("Product deleted");
                return myProduct;
            }
            return null;

        }
    }
}

//-------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    internal class ShoppingHome
    {

        ProductRepository productRepository;
        public ShoppingHome()
        {
            productRepository = new ProductRepository();
        }
        void DisplayAdminMenu()
        {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product Price");
            Console.WriteLine("3. Delete Product");
            Console.WriteLine("4. Print All Products");
            Console.WriteLine("0. Exit");
        }
        void StartAdminActivities()
        {
            int choice;
            do
            {
                DisplayAdminMenu();
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 0:
                        Console.WriteLine("Bye bye");
                        break;
                    case 1:
                        productRepository.Add();
                        break;
                    case 2:
                        UpdatePrice();
                        break;
                    case 3:
                        DeleteProduct();
                        break;
                    case 4:
                        PrintAllProducts();
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again");
                        break;
                }
            } while (choice != 0);
        }

        private void PrintAllProducts()
        {
            Console.WriteLine("***********************************");
            var products = productRepository.GetProducts();
            foreach (var item in products)
            {
                Console.WriteLine(item);
                Console.WriteLine("-------------------------------");
            }
            Console.WriteLine("***********************************");
        }

        int GetProductIdFromUser()
        {
            int id;
            Console.WriteLine("Please enter the product id");
            id = Convert.ToInt32(Console.ReadLine());
            return id;
        }
        private void DeleteProduct()
        {
            int id = GetProductIdFromUser();
            if (productRepository.Delete(id) != null)
                Console.WriteLine("Product deleted");
        }

        private void UpdatePrice()
        {
            var id = GetProductIdFromUser();
            Console.WriteLine("Please enter the new price");
            float price = Convert.ToSingle(Console.ReadLine());
            Product product = new Product();
            product.Price = price;
            product.Id = id;
            var result = productRepository.Update(id, product, "price");
            if (result != null)
                Console.WriteLine("Update success");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my Shopping App");
            ShoppingHome home = new ShoppingHome();
            home.StartAdminActivities();
        }
    }
}

