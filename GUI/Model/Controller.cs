using System;
using System.Collections.Generic;
using Data;
using Service;

namespace GUI.Model
{
    public class Controller
    {
        private readonly IDataRepository _repository;
        public Controller(IDataRepository repo)
        {
            _repository = repo ?? throw new ArgumentNullException();
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }

        public void AddProduct(Product p)
        {
            DateTime now = DateTime.Now;
            p.MakeFlag = false;
            p.FinishedGoodsFlag = false;
            p.SafetyStockLevel = 400;
            p.ReorderPoint = 275;
            p.StandardCost = 0.0M;
            p.ListPrice = 0.0M;
            p.Size = null;
            p.SizeUnitMeasureCode = null;
            p.WeightUnitMeasureCode = null;
            p.DaysToManufacture = 0;
            p.ProductLine = null;
            p.Class = null;
            p.Style = null;
            p.ProductSubcategoryID = null;
            p.ProductModelID = null;
            p.SellStartDate = now;
            p.SellEndDate = null;
            p.DiscontinuedDate = null;
            p.ModifiedDate = now;
            p.rowguid = Guid.NewGuid();
            _repository.AddProduct(p);
        }

        public void DeleteProduct(Product p)
        {
            _repository.DeleteProduct(p);
        }

        public void UpdateProduct()
        {
            _repository.UpdateProducts();
        }
    }
}
