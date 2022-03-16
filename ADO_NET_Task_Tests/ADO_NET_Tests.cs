#pragma warning disable CS8602
#pragma warning disable CS8618

using ADO_NET_Task.Models;
using ADO_NET_Task.Repositories;
using ADO_NET_Task.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System.Collections.Generic;

namespace ADO_NET_Task_Tests
{
    public class ADO_NET_Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void GetAllProductsTest()
        {
            IGenericRepository<Product> productRepository = Mock.Of<IGenericRepository<Product>>(x => x.GetAll() == new List<Product>());

            var sequence = productRepository.GetAll();

            Assert.That(sequence, Is.EqualTo(new List<Product>()));
        }

        [Test]
        public void GetProductByIdTest()
        {
            var product = new Product();
            IGenericRepository<Product> productRepository =
                Mock.Of<IGenericRepository<Product>>(x => x.GetById(It.IsAny<int>()) == product);

            var actual = productRepository.GetById(5);

            Assert.That(actual, Is.EqualTo(product));
        }

        //IEnumerable<T> GetAll();
        //T? GetById(object id);
        //void Insert(T obj);
        //void Update(T obj);
        //void Delete(object id);
        //IEnumerable<T> GetAllWithFilter(string storedProcedure, Func<T, bool> filter);
        //int DeleteInBulkWithFilter(string storedProcedure, string filter);
    }
}