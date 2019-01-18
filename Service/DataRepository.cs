using System;
using System.Collections.Generic;
using System.Linq;
using Data;

namespace Service
{
    public class DataRepository : IDataRepository, IDisposable
    {
        private readonly DataDBDataContext _context = new DataDBDataContext();

        public void AddProduct(Product p)
        {
            _context.Products.InsertOnSubmit(p);
            _context.SubmitChanges();
        }

        public List<Product> GetAllProducts()
        {
            var products = (
                    from p in _context.Products
                    select p)
                .ToList();
            return products;
        }

        public void DeleteProduct(Product p)
        {
            _context.Products.DeleteOnSubmit(p);
            _context.SubmitChanges();
        }

        public void UpdateProducts()
        {
            _context.SubmitChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}