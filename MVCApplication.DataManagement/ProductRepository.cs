using Dapper;
using MVCApplication.DataManagement.Entities;
using MVCApplication.DataManagement.Interfaces;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MVCApplication.DataManagement
{
    public class ProductRepository : IProduct
    {
        private IDbTransaction _transaction;
        private IDbConnection _connection => _transaction.Connection;

        public ProductRepository(IDbTransaction transaction)
        {
            _transaction = transaction;
        }

        public void Add(Products model)
        {
            var insertSql = @"INSERT Into dbo.Products( ProductName, SupplierID, CategoryID, QuantityPerUnit, UnitPrice, UnitsInStock, UnitsOnOrder, ReorderLevel, Discontinued ) VALUES(
                              @ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discontinued)";

            _connection.Execute(insertSql, model);
        }

        public Products Get(int id)
        {
            var getSql = @"Select * dbo.Products Where id = @id ";

            return _connection.Query<Products>(getSql, id).FirstOrDefault();
        }

        public IEnumerable<Products> GetAll()
        {
            var getSql = @"Select * dbo.Products";

            return _connection.Query<Products>(getSql);
        }

        public IEnumerable<Products> GetProductsWithDiscount()
        {
            var getSql = @"Select * dbo.Products Where Discounted = 1";

            return _connection.Query<Products>(getSql);
        }

        public IEnumerable<Products> GetTopProductsInStock(int count)
        {
            var getSql = $@"Select TOP({count}) * dbo.Products order by UnitsInStock desc";

            return _connection.Query<Products>(getSql);
        }

        public void Remove(Products model)
        {
            var updateSql = $@" Delete from dbo.Products Where ProductID = @ProductID ";

            _connection.Query<Products>(updateSql, model.ProductID);
        }

        public void Update(Products model)
        {
            var updateSql = @"Update dbo.Products Set 
                             ProductName = @ProductName,
                             SupplierID = @SupplierID,
                             CategoryID = @CategoryID,
                             QuantityPerUnit = @QuantityPerUnit,
                             UnitPrice = @UnitPrice,
                             UnitsInStock = @UnitsInStock,
                             UnitsOnOrder = @UnitsOnOrder,
                             ReorderLevel = @ReorderLevel,
                             Discounted = @Discounted";
            _connection.Execute(updateSql, model);
        }
    }
}
