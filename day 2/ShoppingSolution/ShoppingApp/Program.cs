using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoppingApp
{
    internal class ShoppingHome
    {
        ManageProduct manageProduct;
        public ShoppingHome()
        {
            manageProduct = new ManageProduct();
        }
        void DisplayAdminMenu()
        {
            Console.WriteLine("1. Add Product");
            Console.WriteLine("2. Update Product Price");
            Console.WriteLine("3. Delete Product");
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
                        manageProduct.GetProductDetailsFromUser();
                        break;
                    case 2:
                        UpdatePrice();
                        break;
                    case 3:
                        Console.WriteLine("Product Deleted");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Try again");
                        break;
                }
            } while (choice != 0);
        }

        private void UpdatePrice()
        {
            manageProduct.PrintProductDetails();
            Console.WriteLine("Please enter the new price");
            float price = Convert.ToSingle(Console.ReadLine());
            var result = manageProduct.UpdatePrice(price);
            if (result == true)
            {
                Console.WriteLine("Price updated");
                manageProduct.PrintProductDetails();
            }
            else
                Console.WriteLine("Unable to update price");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to my Shopping App");
            ShoppingHome home = new ShoppingHome();
            home.StartAdminActivities();
        }
    }
}