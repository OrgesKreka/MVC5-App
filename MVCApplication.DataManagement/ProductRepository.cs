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
                              @ProductName, @SupplierID, @CategoryID, @QuantityPerUnit, @UnitPrice, @UnitsInStock, @UnitsOnOrder, @ReorderLevel, @Discounted)";

            _connection.Execute(insertSql, model, transaction: _transaction);
        }

        public Products Get(int id)
        {
            var getSql = @"Select * From dbo.Products Where ProductID = @id ";

            return _connection.Query<Products>(getSql, param: new { id }, transaction: _transaction).FirstOrDefault();
        }

        public IEnumerable<Products> GetAll()
        {
            var getSql = @"Select * From dbo.Products";

            return _connection.Query<Products>(getSql, transaction: _transaction);
        }

        public IEnumerable<Products> GetProductsWithDiscount()
        {
            var getSql = @"Select * From dbo.Products Where Discounted = 1";

            return _connection.Query<Products>(getSql, transaction: _transaction);
        }

        public IEnumerable<Products> GetTopProductsInStock(int count)
        {
            var getSql = $@"Select TOP({count}) * From dbo.Products order by UnitsInStock desc";

            return _connection.Query<Products>(getSql, transaction: _transaction);
        }

        public void Remove(Products model)
        {
            var updateSql = $@" Delete from dbo.Products Where ProductID = @ProductID ";

            _connection.Query<Products>(updateSql, new { model.ProductID }, transaction: _transaction);
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
                             Discontinued = @Discounted
                             Where ProductID = @ProductID";
            _connection.Execute(updateSql, model, transaction: _transaction);
        }
    }
}
