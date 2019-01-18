using System.Collections.Generic;
using Data;

namespace Service
{
    public interface IDataRepository
    {
        void AddProduct(Product p);
        void UpdateProducts();
        List<Product> GetAllProducts();
        void DeleteProduct(Product p);

    }
}
