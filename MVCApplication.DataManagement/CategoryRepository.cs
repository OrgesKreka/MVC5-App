using Dapper;
using MVCApplication.DataManagement.Entities;
using MVCApplication.DataManagement.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MVCApplication.DataManagement
{
    public class CategoryRepository : ICategoryRepository
    {

        private IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public CategoryRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(Categories model)
        {
            var insertSql = @"Insert Into dbo.Categories (CategoryName,Description,Picture ) Values(
                              @CategoryName, @Description, @Picture )";

            _connection.Execute(insertSql, model);
        }

        public Categories Get(int id)
        {
            var getSql = @"Select * From dbo.Categories Where CategoryID = @id";
            return _connection.Query<Categories>(getSql, id).FirstOrDefault();
        }

        public IEnumerable<Categories> GetAll()
        {
            var getSql = @"Select * From dbo.Categories";
            return _connection.Query<Categories>(getSql);
        }

        public IEnumerable<Categories> GetCategoriesOrderedByName()
        {
            var getSql = @"Select * From dbo.Categories Order By CategoryName";
            return _connection.Query<Categories>(getSql);
        }

        public IEnumerable<Categories> GetTopCategories(int count)
        {
            var getSql = $@"Select TOP({count}) * From dbo.Categories Order By CategoryName";
            return _connection.Query<Categories>(getSql);
        }

        public void Remove(Categories model)
        {
            var deleteSql = "Delete From dbo.Categories Where CategoryID = @CategoryID";
            _connection.Execute(deleteSql, model.CategoryID);
        }

        public void Update(Categories model)
        {
            var updateSql = @"Update dbo.Categories Set 
                              CategoryName = @CategoryName,
                              Description = @Description,
                              Picture = @Picture Where CategoryID = @CategoryID";
            _connection.Execute(updateSql, model);
        }
    }
}
