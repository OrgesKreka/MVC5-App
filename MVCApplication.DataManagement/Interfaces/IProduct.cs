using MVCApplication.DataManagement.Entities;
using System.Collections.Generic;

namespace MVCApplication.DataManagement.Interfaces
{
    public interface IProduct : IRepository<Products>
    {
        IEnumerable<Products> GetProductsWithDiscount();
        IEnumerable<Products> GetTopProductsInStock(int count);
    }
}
