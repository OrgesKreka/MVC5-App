using System.Collections.Generic;

namespace MVCApplication.DataManagement.Interfaces
{
    public interface IRepository<TModel> where TModel : class
    {
        TModel Get(int id);
        IEnumerable<TModel> GetAll();
        void Update(TModel model);
        void Add(TModel model);
        void Remove(TModel model);
    }
}
