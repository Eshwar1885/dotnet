using ShoppingBLLibrary;
using ShoppingDALLibrary;
using System.Diagnostics;
using System.Xml.Linq;

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
//---------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShoppingModelLibrary;

namespace ShoppingDALLibrary
{
    public interface IRepository
    {
        public Product Add(Product product);
        public Product Update(Product product);
        public Product Delete(int id);
        public Product GetById(int id);
        public List<Product> GetAll();
    }
}
//---------------------------------------------------------------
using ShoppingModelLibrary;

namespace ShoppingApp
{
    internal class Program
    {
        static int Main(string[] args)
        {
            int num1 = 0;
            try
            {
                Console.WriteLine("Please enter a number");
                num1 = Convert.ToInt32(Console.ReadLine());
                int num2 = 100;
                num2 = num2 / num1;
                Console.WriteLine($"The result is {num2}");
                return 1;
            }
            catch (ArithmeticException e)
            {
                Console.WriteLine("There is an arthmatical problem");
                Console.WriteLine(e.Message);
            }
            catch (FormatException e)
            {
                Console.WriteLine("The input was not a number.");
                Console.WriteLine(e.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine("Oops something went wrong");
                Console.WriteLine(e.StackTrace);
            }
            finally
            {
                Console.WriteLine("Connection closed");
            }
            Console.WriteLine("Hello, World!");
            return 0;
        }
    }
}
//---------------------------------------------------------------
using ShoppingModelLibrary;

namespace ShoppingDALLibrary
{
    public class ProductRepository : IRepository
    {
        Dictionary<int, Product> products = new Dictionary<int, Product>();
        /// <summary>
        /// c
        /// </summary>
        /// <param name="product">Product object that has to be added</param>
        /// <returns>The product that has been added</returns>
        public Product Add(Product product)
        {
            int id = GetTheNextId();
            try
            {
                products.Add(product.Id, product);
                return product;
            }
            catch (ArgumentException e)
            {
                Console.WriteLine("The product Id already exists");
                Console.WriteLine(e.Message);
            }
            return null;

        }

        private int GetTheNextId()
        {
            if (products.Count == 0)
                return 1;
            int id = products.Keys.Max();
            return ++id;
        }

        /// <summary>
        /// Deletes the product from teh dictionary using the id as key
        /// </summary>
        /// <param name="id">The Id of the product to be deleted</param>
        /// <returns>The deleted product</returns>
        public Product Delete(int id)
        {
            products.Remove(id);
            return products[id];
        }

        public List<Product> GetAll()
        {
            var productList = products.Values.ToList();
            return productList;
        }

        public Product GetById(int id)
        {
            if (products.ContainsKey(id))
                return products[id];
            return null;
        }

        public Product Update(Product product)
        {
            products[product.Id] = product;
            return products[product.Id];
        }
    }
}
//--------------------------------------------------------------------------------------
using System.Runtime.Serialization;

namespace ShoppingBLLibrary
{
    [Serializable]
    public class InValidUpdateActionException : Exception
    {
        string message;
        public InValidUpdateActionException()
        {
            message = "The action you have specified is not valid";
        }
        public override string Message => message;

    }
}
//--------------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace ShoppingBLLibrary
{
    [Serializable]
    public class NoProductsAvailableException : Exception
    {
        string message;
        public NoProductsAvailableException()
        {
            message = "No products are available currently";
        }

        public override string Message => message;
    }
}

//--------------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace ShoppingBLLibrary
{
    [Serializable]
    public class NoSuchProductException : Exception
    {
        string message;
        public NoSuchProductException()
        {
            message = "The product with teh given id is not present";
        }
        public override string Message => message;


    }
}

//--------------------------------------------------------------------------------------

using System.Runtime.Serialization;

namespace ShoppingBLLibrary
{
    [Serializable]
    public class NotAddedException : Exception
    {
        string message;
        public NotAddedException()
        {
            message = "Product was not addedd.";
        }
        public override string Message => message;

    }
}

//--------------------------------------------------------------------------------------
using ShoppingDALLibrary;
using ShoppingModelLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary
{
    public interface IProductService
    {
        public Product AddProduct(Product product);
        public Product UpdateProductPrice(int id, float price);
        public Product GetProduct(int id);
        public List<Product> GetProducts();
        public Product UpdateProductQuantity(int id, int quantity, string action);
        public Product Delete(int id);
    }
}




//--------------------------------------------------------------------------------------


using ShoppingDALLibrary;
using ShoppingModelLibrary;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingBLLibrary
{
    public class ProductService : IProductService
    {
        IRepository repository;
        public ProductService()
        {
            repository = new ProductRepository();
        }
        /// <summary>
        /// Adds the product to the collection using the repository
        /// </summary>
        /// <param name="product">The product to be added</param>
        /// <returns></returns>
        /// <exception cref="NotAddedException">Product Id duplicated</exception>
        public Product AddProduct(Product product)
        {
            var result = repository.Add(product);
            if (result != null)
                return result;
            throw new NotAddedException();
        }

        public Product Delete(int id)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                repository.Delete(id);
                return product;
            }
            throw new NoSuchProductException();
        }
        /// <summary>
        /// Returns the product for teh given Id
        /// </summary>
        /// <param name="id">Id of the product to be returned</param>
        /// <returns></returns>
        /// <exception cref="NoSuchProductException">No product with the given Id</exception>
        public Product GetProduct(int id)
        {
            var result = repository.GetById(id);
            //if (result != null) 
            //    return result;
            //throw new NoSuchProductException();

            //null collasing operator
            //return result ?? throw new NoSuchProductException();

            return result == null ? throw new NoSuchProductException() : result;
        }

        public List<Product> GetProducts()
        {
            var products = repository.GetAll();
            if (products.Count != 0)
                return products;
            throw new NoProductsAvailableException();
        }

        public Product UpdateProductPrice(int id, float price)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                product.Price = price;
                var result = repository.Update(product);
                return result;
            }
            throw new NoSuchProductException();
        }

        public Product UpdateProductQuantity(int id, int quantity, string action)
        {
            var product = GetProduct(id);
            if (product != null)
            {
                if (action == "add")
                {
                    product.Quantity += quantity;
                }
                else if (action == "remove")
                {
                    product.Quantity -= quantity;
                }
                else
                    throw new InValidUpdateActionException();
                var result = repository.Update(product);
                return result;
            }
            throw new NoSuchProductException();
        }
    }
}


//--------------------------------------------------------------------------------------

using ShoppingModelLibrary;
using ShoppingBLLibrary;
using ShoppingDALLibrary;

namespace ShoppingApp
{
    internal class Program
    {
        IProductService productService;
        public Program()
        {
            productService = new ProductService();
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
                        AddProduct();
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
            var products = productService.GetProducts();
            foreach (var item in products)
            {
                Console.WriteLine(item);
                Console.WriteLine("-------------------------------");
            }
            Console.WriteLine("***********************************");
        }
        void AddProduct()
        {
            try
            {
                Product product = TakeProductDetails();
                var result = productService.AddProduct(product);
                if (result != null)
                {
                    Console.WriteLine("Product added");
                }
            }
            catch (FormatException e)
            {
                Console.WriteLine(e.Message);

            }
            catch (NotAddedException e)
            {
                Console.WriteLine(e.Message);
            }

        }
        Product TakeProductDetails()
        {
            Product product = new Product();
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
            return product;
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
            try
            {
                int id = GetProductIdFromUser();
                if (productService.Delete(id) != null)
                    Console.WriteLine("Product deleted");
            }
            catch (NoSuchProductException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        private void UpdatePrice()
        {
            var id = GetProductIdFromUser();
            Console.WriteLine("Please enter the new price");
            float price = Convert.ToSingle(Console.ReadLine());
            Product product = new Product();
            product.Price = price;
            product.Id = id;
            try
            {
                var result = productService.UpdateProductPrice(id, price);
                if (result != null)
                    Console.WriteLine("Update success");
            }
            catch (NoSuchProductException e)
            {
                Console.WriteLine(e.Message);
            }
        }
        static int Main(string[] args)
        {
            Program program = new Program();
            program.StartAdminActivities();
            Console.WriteLine("Hello, World!");
            return 0;
        }
    }
}


