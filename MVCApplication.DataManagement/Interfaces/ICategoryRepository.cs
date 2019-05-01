using MVCApplication.DataManagement.Entities;
using System.Collections.Generic;

namespace MVCApplication.DataManagement.Interfaces
{
    public interface ICategoryRepository : IRepository<Categories>
    {
        IEnumerable<Categories> GetCategoriesOrderedByName();
        IEnumerable<Categories> GetTopCategories(int count);
    }
}
