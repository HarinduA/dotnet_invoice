using System;
using System.Data;
using System.Data.SqlClient;

namespace StoreInventoryApp
{
    class Program
    {
        // SQL Server connection string
        static string connectionString = "Server=localhost,1433;Database=store_inventory_db_2024;User Id=sa;Password=MyOwnDemo123Pwd;";

        static void Main(string[] args)
        {
            Console.WriteLine("Store Items");

            // Display existing items
            DisplayItems();

            // Add a new item
            AddNewItem();

            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        // Method to display items from the database
        static void DisplayItems()
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT itemCode, description, qty, price, amount FROM items";

                SqlCommand command = new SqlCommand(query, connection);
                connection.Open();

                SqlDataReader reader = command.ExecuteReader();

                Console.WriteLine("Item Code\tDescription\tQuantity\tPrice\tAmount");
                while (reader.Read())
                {
                    Console.WriteLine($"{reader["itemCode"]}\t{reader["description"]}\t{reader["qty"]}\t{reader["price"]}\t{reader["amount"]}");
                }

                reader.Close();
            }
        }

        // Method to add a new item to the database
        static void AddNewItem()
        {
            Console.WriteLine("\nAdd New Item");
            Console.Write("Enter Item Code: ");
            string itemCode = Console.ReadLine();
            Console.Write("Enter Description: ");
            string description = Console.ReadLine();
            Console.Write("Enter Quantity: ");
            int qty = int.Parse(Console.ReadLine());
            Console.Write("Enter Price: ");
            double price = double.Parse(Console.ReadLine());
            double amount = qty * price;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "INSERT INTO items (itemCode, description, qty, price, amount) " +
                               "VALUES (@itemCode, @description, @qty, @price, @amount)";

                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@itemCode", itemCode);
                command.Parameters.AddWithValue("@description", description);
                command.Parameters.AddWithValue("@qty", qty);
                command.Parameters.AddWithValue("@price", price);
                command.Parameters.AddWithValue("@amount", amount);

                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                Console.WriteLine($"{rowsAffected} row(s) affected.");
            }
        }
    }
}
