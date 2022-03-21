#pragma warning disable CS8602
#pragma warning disable CS8618

using ADO_NET_Task.Models;
using ADO_NET_Task.Repositories.Interfaces;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

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
            var products = new List<Product>();
            products.Add(new Product());

            IGenericRepository<Product> productRepositoryStub = Mock.Of<IGenericRepository<Product>>(x => x.GetAll() == products);

            var sequence = productRepositoryStub.GetAll();

            Assert.That(sequence, Is.EqualTo(products));
        }

        [Test]
        public void GetProductByIdTest()
        {
            var product = new Product();
            IGenericRepository<Product> productRepositoryStub =
                Mock.Of<IGenericRepository<Product>>(x => x.GetById(It.IsAny<int>()) == product);

            var actual = productRepositoryStub.GetById(5);

            Assert.That(actual, Is.EqualTo(product));
        }

        [Test]
        public void GetAllOrdersTest()
        {
            var orders = new List<Order>();
            orders.Add(new Order());

            IGenericRepository<Order> orderRepositoryStub = Mock.Of<IGenericRepository<Order>>(x => x.GetAll() == orders);

            var sequence = orderRepositoryStub.GetAll();

            Assert.That(sequence, Is.EqualTo(orders));
        }

        [Test]
        public void GetOrderByIdTest()
        {
            var order = new Order();
            IGenericRepository<Order> orderRepositoryStub =
                Mock.Of<IGenericRepository<Order>>(x => x.GetById(It.IsAny<int>()) == order);

            var actual = orderRepositoryStub.GetById(5);

            Assert.That(actual, Is.EqualTo(order));
        }

        [Test]
        public void GetAllOrdersWithFilterTest()
        {
            List<Order> orders = new List<Order>();
            orders.Add(new Order());

            var orderRepositoryStub = new Mock<IGenericRepository<Order>>();

            orderRepositoryStub
                .Setup(x => x.GetAllWithFilter(It.IsAny<string>(), It.IsAny<Func<Order, bool>>()))
                .Returns(orders);

            var orderRepository = orderRepositoryStub.Object;

            Assert.That(orderRepository.GetAllWithFilter("anything", x => x.Id > 7), Is.EqualTo(orders));
        }

        [Test]
        public void DeleteOrdersInBulkWithFilterTest()
        {
            var orderRepositoryStub = new Mock<IGenericRepository<Order>>();

            orderRepositoryStub
                .Setup(x => x.DeleteInBulkWithFilter(It.IsAny<string>(), It.IsAny<string>()))
                .Returns(5);

            var orderRepository = orderRepositoryStub.Object;

            Assert.That(orderRepository.DeleteInBulkWithFilter("anything", "anything"), Is.EqualTo(5));
        }
    }
}