using ADO_NET_Task.Models;
using ADO_NET_Task.Repositories.Interfaces;
using System.Data;
using System.Data.Common;

namespace ADO_NET_Task.Repositories
{
    public class ProductRepository : IGenericRepository<Product>
    {
        private readonly DbProviderFactory providerFactory;
        private readonly string connectionString;

        public ProductRepository(string connectionString, string dbProvider)
        {
            providerFactory = DbProviderFactories.GetFactory(dbProvider);
            this.connectionString = connectionString;
        }

        public void Delete(object id)
        {
            using (var connection = providerFactory.CreateConnection())
            {
                if (connection is null)
                {
                    throw new ArgumentNullException(nameof(connection));
                }

                connection.ConnectionString = connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "DELETE FROM dbo.Products WHERE dbo.Products.Id = @id";
                    command.CommandType = CommandType.Text;

                    CreateCommandParameter(id, "id", command);

                    command.ExecuteNonQuery();
                }
            }
        }

        public IEnumerable<Product> GetAll()
        {
            var products = new List<Product>();

            using (var connection = providerFactory.CreateConnection())
            {
                if (connection is null)
                {
                    throw new ArgumentNullException(nameof(connection));
                }

                connection.ConnectionString = connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, Name, Description, " +
                      "Weight, Height, Width, Length FROM dbo.Products";
                    command.CommandType = CommandType.Text;

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            var product = new Product()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Weight = reader.GetInt32(3),
                                Height = reader.GetInt32(4),
                                Width = reader.GetInt32(5),
                                Length = reader.GetInt32(6),
                            };

                            products.Add(product);
                        };
                    }
                }
            }

            return products;
        }

        public IEnumerable<Product> GetAllWithFilter(string storedProcedure)
        {
            throw new NotImplementedException();
        }

        public Product? GetById(object id)
        {
            using (var connection = providerFactory.CreateConnection())
            {
                if (connection is null)
                {
                    throw new ArgumentNullException(nameof(connection));
                }

                connection.ConnectionString = connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "SELECT Id, Name, Description, " +
                      "Weight, Height, Width, Length FROM dbo.Products WHERE dbo.Products.Id = @id";
                    command.CommandType = CommandType.Text;

                    CreateCommandParameter(id, "id", command);

                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Product product = new Product()
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                                Description = reader.GetString(2),
                                Weight = reader.GetInt32(3),
                                Height = reader.GetInt32(4),
                                Width = reader.GetInt32(5),
                                Length = reader.GetInt32(6),
                            };

                            return product;
                        };
                    }
                }
            }

            return null;
        }

        public void Insert(Product obj)
        {
            using (var connection = providerFactory.CreateConnection())
            {
                if (connection is null)
                {
                    throw new ArgumentNullException(nameof(connection));
                }

                connection.ConnectionString = connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "INSERT INTO dbo.Products(Name, Description, Weight, Height, Width, Length) " +
                        "VALUES (@name, @description, @weight, @height, @width, @length)";
                    command.CommandType = CommandType.Text;

                    CreateCommandParameter(obj.Name ?? "NotSet", "name", command);
                    CreateCommandParameter(obj.Description ?? "NotSet", "description", command);
                    CreateCommandParameter(obj.Weight, "weight", command);
                    CreateCommandParameter(obj.Height, "height", command);
                    CreateCommandParameter(obj.Width, "width", command);
                    CreateCommandParameter(obj.Length, "length", command);

                    command.ExecuteNonQuery();
                }
            }
        }

        public void Update(Product obj)
        {
            using (var connection = providerFactory.CreateConnection())
            {
                if (connection is null)
                {
                    throw new ArgumentNullException(nameof(connection));
                }

                connection.ConnectionString = connectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "UPDATE dbo.Products SET Name=@name, Description=@description, " +
                        "Weight=@weight, Height=@height, Width=@width, Length=@length " +
                        "WHERE dbo.Products.Id = @id";
                    command.CommandType = CommandType.Text;

                    CreateCommandParameter(obj.Id, "id", command);
                    CreateCommandParameter(obj.Name ?? "NotSet", "name", command);
                    CreateCommandParameter(obj.Description ?? "NotSet", "description", command);
                    CreateCommandParameter(obj.Weight, "weight", command);
                    CreateCommandParameter(obj.Height, "height", command);
                    CreateCommandParameter(obj.Width, "width", command);
                    CreateCommandParameter(obj.Length, "length", command);

                    command.ExecuteNonQuery();
                }
            }
        }

        private void CreateCommandParameter(object obj, string parameterName, DbCommand command)
        {
            var parameter = command.CreateParameter();
            parameter.ParameterName = $"@{parameterName}";
            parameter.Value = obj;
            command.Parameters.Add(parameter);
        }
    }
}
