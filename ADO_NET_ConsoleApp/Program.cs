using ADO_NET_Task.Enums;
using ADO_NET_Task.Models;
using ADO_NET_Task.Repositories;
using ADO_NET_Task.Repositories.Interfaces;
using Microsoft.Data.SqlClient;
using System;
using System.Data.Common;
using System.Threading.Tasks;

namespace ADO_NET_ConsoleApp
{
    public class Program
    {
        private const string connectionString = "Server=(localdb)\\mssqllocaldb;Database=adonetdb;Trusted_Connection=True;";

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

                //command.CommandText = @"USE adonetdb;
                //                        CREATE TABLE Products
                //                            (
                //                             Id INT PRIMARY KEY IDENTITY,
                //                             Name NVARCHAR(100) NOT NULL,
                //                             Description NVARCHAR(100) NOT NULL,
                //                             Weight INT NOT NULL,
                //                             Height INT NOT NULL,
                //                             Width INT NOT NULL,
                //                             Length INT NOT NULL
                //                            )";

                //command.CommandText = @"USE adonetdb;
                //                        CREATE TABLE Orders
                //                            (
                //                             Id INT PRIMARY KEY IDENTITY,
                //                             Status NVARCHAR(30) NOT NULL,
                //                             CreatedDate DATE NOT NULL,
                //                             UpdatedDate DATE NOT NULL,
                //                             ProductId INT NOT NULL REFERENCES Products (Id)
                //                            )";

                //command.Connection = connection;

                //await command.ExecuteNonQueryAsync();
            }

            //var product = new Product()
            //{
            //    Name = "Laptop new model",
            //    Description = "HP Pavilion",
            //    Weight = 2,
            //    Height = 20,
            //    Width = 2,
            //    Length = 30,
            //};

            //productRepository.Insert(product);

            //productRepository.Delete(1003);

            //var product = productRepository.GetById(2002);
            //product.Name = "Laptop Asus";
            //product.Description = "Workstation portative Asus";

            //productRepository.Update(product);

            var products = productRepository.GetAll();

            //var order = new Order()
            //{
            //    Status = Status.Arrived,
            //    CreatedDate = new DateTime(2019, 9, 11),
            //    UpdatedDate = new DateTime(2020, 7, 8),
            //    ProductId = 2,
            //};

            //orderRepository.Insert(order);

            //orderRepository.Delete(3);

            //var order = orderRepository.GetById(2);
            //order.Status = Status.InProgress;
            //order.UpdatedDate = DateTime.Now;

            //orderRepository.Update(order);

            var orders = orderRepository.GetAll();

            string proc = @"CREATE PROCEDURE [dbo].[sp_GetOrders]
                                AS
                                    SELECT * FROM Orders 
                                GO";

            _ = AddStoredProcedure(proc);

            var ordersFiltered = orderRepository.GetAllWithFilter("sp_GetOrders", x => x.Status == Status.Done);

            var numberOfDeletedOrders = orderRepository.DeleteInBulkWithFilter("sp_GetOrders", "");
        }

        private static async Task AddStoredProcedure(string storedProcedure)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                SqlCommand command = new SqlCommand(storedProcedure, connection);

                await command.ExecuteNonQueryAsync();
            }
        }
    }
}
