using ADO_NET_Task.Models;
using ADO_NET_Task.Repositories;
using ADO_NET_Task.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System.Data.Common;
using System.Threading.Tasks;

namespace ADO_NET_ConsoleApp
{
    public class Program
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=master;Trusted_Connection=True;";

        static async Task Main()
        {
            DbProviderFactories.RegisterFactory("Microsoft.Data.SqlClient", SqlClientFactory.Instance);
            IGenericRepository<Product> productRepository = new ProductRepository(connectionString, "Microsoft.Data.SqlClient");
            IGenericRepository<Order> orderRepository = new OrderRepository(connectionString, "Microsoft.Data.SqlClient");

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand();

                //command.CommandText = "CREATE DATABASE adonetdb";

                //command.CommandText = "CREATE TABLE Products (Id INT PRIMARY KEY IDENTITY, " +
                //    "Name NVARCHAR(100) NOT NULL, " +
                //    "Description NVARCHAR(100) NOT NULL, " +
                //    "Weight INT NOT NULL, " +
                //    "Height INT NOT NULL, " +
                //    "Width INT NOT NULL, " +
                //    "Length INT NOT NULL)";

                //command.CommandText = "CREATE TABLE Orders (Id INT PRIMARY KEY IDENTITY, " +
                //    "Status NVARCHAR(30) NOT NULL, " +
                //    "CreatedDate DATE NOT NULL, " +
                //    "UpdatedDate DATE NOT NULL, " +
                //    "ProductId INT NOT NULL REFERENCES Products (Id))";

                //command.Connection = connection;

                //await command.ExecuteNonQueryAsync();
            }

            //var product = new Product()
            //{
            //    Name = "TV new model",
            //    Description = "Smart TV Panasonic 65",
            //    Weight = 9,
            //    Height = 150,
            //    Width = 8,
            //    Length = 200,
            //};

            //productRepository.Insert(product);

            //productRepository.Delete(1003);

            var product = productRepository.GetById(2002);
            product.Name = "Laptop Asus";
            product.Description = "Workstation portative Asus";

            productRepository.Update(product);

            var products = productRepository.GetAll();
        }
    }
}
