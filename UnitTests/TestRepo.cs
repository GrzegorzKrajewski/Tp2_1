using System;
using System.Collections.Generic;
using Data;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Service;

namespace UnitTests
{
    [TestClass]
    public class TestRepo : IDataRepository
    {
        private List<Product> products = new List<Product>(4);

        public TestRepo()
        {
            Product p;
            for (int i = 0; i < products.Capacity; i++)
            {
                p = new Product();
                p.ProductID = i;
                p.Name = "Name" + i;
                p.ProductNumber = "Number" + i;
                p.Color = "Color" + i;
                p.ModifiedDate = new DateTime(2000, 1, 1, 12, 0, 0);
                products.Add(p);
            }
        }

        public void AddProduct(Product p)
        {
            products.Add(p);
        }

        public void DeleteProduct(Product p)
        {
            products.Remove(p);
        }

        public List<Product> GetAllProducts()
        {
            return new List<Product>(products);
        }

        public void UpdateProducts()
        {
        }
    }
}