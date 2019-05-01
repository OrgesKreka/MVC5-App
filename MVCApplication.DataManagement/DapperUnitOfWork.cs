using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace MVCApplication.DataManagement
{
    public class DapperUnitOfWork : IDisposable
    {
        private IDbConnection _connection;
        private IDbTransaction _transaction;

        private CategoryRepository _categoryRepository;
        private ProductRepository _productsRepository;

        private readonly string ConnectionString;

        public DapperUnitOfWork(string connectionString = "")
        {
            ConnectionString = connectionString;

            if (string.IsNullOrEmpty(ConnectionString))
                ConnectionString = ConfigurationManager.ConnectionStrings["MVCApplication"].ConnectionString;

            _connection = new SqlConnection(ConnectionString);
            _connection.Open();
            _transaction = _connection.BeginTransaction();
        }


        public CategoryRepository CategoryRepository => _categoryRepository ?? (_categoryRepository = new CategoryRepository(_transaction));
        public ProductRepository ProductRepository => _productsRepository ?? (_productsRepository = new ProductRepository(_transaction));

        public void Commit()
        {
            try
            {
                _transaction.Commit();
            }
            catch
            {
                _transaction.Rollback();
                throw;
            }
            finally
            {
                _transaction.Dispose();
                _transaction = _connection.BeginTransaction();
                ResetRepositories();
            }
        }

        public void Dispose()
        {

            _transaction?.Dispose();
            _transaction = null;

            _connection?.Dispose();
            _connection = null;

        }

        private void ResetRepositories()
        {
            _productsRepository = null;
            _categoryRepository = null;
        }
    }
}
