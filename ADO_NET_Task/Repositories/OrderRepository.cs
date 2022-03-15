using ADO_NET_Task.Enums;
using ADO_NET_Task.Models;
using ADO_NET_Task.Repositories.Interfaces;
using System.Data;
using System.Data.Common;

namespace ADO_NET_Task.Repositories
{
    public class OrderRepository : IGenericRepository<Order>
    {
        private readonly DbProviderFactory providerFactory;
        private readonly string connectionString;

        public OrderRepository(string connectionString, string dbProvider)
        {
            providerFactory = DbProviderFactories.GetFactory(dbProvider);
            this.connectionString = connectionString;
        }

        public void Delete(object id)
        {
            try
            {
                DbConnection? connection = providerFactory.CreateConnection();

                if (connection == null)
                {
                    return;
                }

                connection.ConnectionString = connectionString;

                using (connection)
                {
                    string queryString =
                        "SELECT * FROM dbo.Orders";

                    DbCommand? command = providerFactory.CreateCommand();

                    if (command == null)
                    {
                        return;
                    }

                    command.CommandText = queryString;
                    command.Connection = connection;

                    DbDataAdapter? adapter = providerFactory.CreateDataAdapter();

                    if (adapter == null)
                    {
                        return;
                    }

                    adapter.SelectCommand = command;

                    DbCommandBuilder? builder = providerFactory.CreateCommandBuilder();

                    if (builder == null)
                    {
                        return;
                    }

                    builder.DataAdapter = adapter;

                    adapter.DeleteCommand = builder.GetDeleteCommand();

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    DataRow[] deleteRow = table.Select($"Id = {id}");
                    foreach (DataRow row in deleteRow)
                    {
                        row.Delete();
                    }

                    adapter.Update(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = new List<Order>();

            try
            {
                using (var connection = providerFactory.CreateConnection())
                {
                    if (connection != null)
                    {
                        connection.ConnectionString = connectionString;

                        using (connection)
                        {
                            string queryString = "SELECT * FROM dbo.Orders";

                            DbCommand? command = providerFactory.CreateCommand();

                            if (command == null)
                            {
                                return orders;
                            }

                            command.CommandText = queryString;
                            command.Connection = connection;

                            DbDataAdapter? adapter = providerFactory.CreateDataAdapter();

                            if (adapter == null)
                            {
                                return orders;
                            }

                            adapter.SelectCommand = command;

                            DataTable table = new DataTable("Orders");
                            adapter.Fill(table);

                            orders = (from DataRow row in table.Rows
                                      select new Order()
                                      {
                                          Id = Convert.ToInt32(row["Id"]),
                                          Status = ConvertStrToEnum(row["Status"].ToString() ?? string.Empty),
                                          CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                                          UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]),
                                          ProductId = Convert.ToInt32(row["ProductId"]),
                                      }).ToList();

                            return orders;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return orders;
        }

        public Order? GetById(object id)
        {
            var order = new Order();

            try
            {
                using (var connection = providerFactory.CreateConnection())
                {
                    if (connection != null)
                    {
                        connection.ConnectionString = connectionString;

                        using (connection)
                        {
                            string queryString = "SELECT * FROM dbo.Orders";

                            DbCommand? command = providerFactory.CreateCommand();

                            if (command == null)
                            {
                                return order;
                            }

                            command.CommandText = queryString;
                            command.Connection = connection;

                            DbDataAdapter? adapter = providerFactory.CreateDataAdapter();

                            if (adapter == null)
                            {
                                return order;
                            }

                            adapter.SelectCommand = command;

                            DataTable table = new DataTable("Orders");
                            adapter.Fill(table);

                            order = (from DataRow row in table.Rows
                                     select new Order()
                                     {
                                         Id = Convert.ToInt32(row["Id"]),
                                         Status = ConvertStrToEnum(row["Status"].ToString() ?? string.Empty),
                                         CreatedDate = Convert.ToDateTime(row["CreatedDate"]),
                                         UpdatedDate = Convert.ToDateTime(row["UpdatedDate"]),
                                         ProductId = Convert.ToInt32(row["ProductId"]),
                                     })
                                      .Where(x => x.Id == Convert.ToInt32(id))
                                      .Single();

                            return order;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return order;
        }

        public void Insert(Order obj)
        {
            try
            {
                DbConnection? connection = providerFactory.CreateConnection();

                if (connection == null)
                {
                    return;
                }

                connection.ConnectionString = connectionString;

                using (connection)
                {
                    string queryString =
                        "SELECT * FROM dbo.Orders";

                    DbCommand? command = providerFactory.CreateCommand();

                    if (command == null)
                    {
                        return;
                    }

                    command.CommandText = queryString;
                    command.Connection = connection;

                    DbDataAdapter? adapter = providerFactory.CreateDataAdapter();

                    if (adapter == null)
                    {
                        return;
                    }

                    adapter.SelectCommand = command;

                    DbCommandBuilder? builder = providerFactory.CreateCommandBuilder();

                    if (builder == null)
                    {
                        return;
                    }

                    builder.DataAdapter = adapter;

                    adapter.InsertCommand = builder.GetInsertCommand();
                    adapter.UpdateCommand = builder.GetUpdateCommand();
                    adapter.DeleteCommand = builder.GetDeleteCommand();

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    DataRow newRow = table.NewRow();
                    newRow["Status"] = obj.Status;
                    newRow["CreatedDate"] = obj.CreatedDate;
                    newRow["UpdatedDate"] = obj.UpdatedDate;
                    newRow["ProductId"] = obj.ProductId;
                    table.Rows.Add(newRow);

                    adapter.Update(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Order obj)
        {
            try
            {
                DbConnection? connection = providerFactory.CreateConnection();

                if (connection == null)
                {
                    return;
                }

                connection.ConnectionString = connectionString;

                using (connection)
                {
                    string queryString =
                        "SELECT * FROM dbo.Orders";

                    DbCommand? command = providerFactory.CreateCommand();

                    if (command == null)
                    {
                        return;
                    }

                    command.CommandText = queryString;
                    command.Connection = connection;

                    DbDataAdapter? adapter = providerFactory.CreateDataAdapter();

                    if (adapter == null)
                    {
                        return;
                    }

                    adapter.SelectCommand = command;

                    DbCommandBuilder? builder = providerFactory.CreateCommandBuilder();

                    if (builder == null)
                    {
                        return;
                    }

                    builder.DataAdapter = adapter;

                    adapter.UpdateCommand = builder.GetUpdateCommand();

                    DataTable table = new DataTable();
                    adapter.Fill(table);

                    DataRow[] editRow = table.Select($"Id = {obj.Id}");

                    editRow[0]["Status"] = obj.Status;
                    editRow[0]["CreatedDate"] = obj.CreatedDate;
                    editRow[0]["UpdatedDate"] = obj.UpdatedDate;
                    editRow[0]["ProductId"] = obj.ProductId;

                    adapter.Update(table);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private static Status ConvertStrToEnum(string status)
        {
            return status switch
            {
                "NotStarted" => Status.NotStarted,
                "Loading" => Status.Loading,
                "InProgress" => Status.InProgress,
                "Arrived" => Status.Arrived,
                "Unloading" => Status.Unloading,
                "Cancelled" => Status.Cancelled,
                "Done" => Status.Done,
                _ => Status.NotSet
            };
        }
    }
}
