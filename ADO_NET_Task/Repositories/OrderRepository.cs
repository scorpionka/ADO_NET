using ADO_NET_Task.Models;
using ADO_NET_Task.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            throw new NotImplementedException();
        }

        public IEnumerable<Order> GetAll()
        {
            throw new NotImplementedException();
        }

        public Order GetById(object id)
        {
            throw new NotImplementedException();
        }

        public void Insert(Order obj)
        {
            throw new NotImplementedException();
        }

        public void Update(Order obj)
        {
            throw new NotImplementedException();
        }
    }
}
