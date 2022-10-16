using Breeze.DbCore.Context;
using Breeze.DbCore.GenericRepository;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace Breeze.DbCore.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private IDatabaseContext _dbContext;
        private readonly string _connectionString;
        private bool disposed = false;

        public UnitOfWork(IDatabaseContext dbContext, DatabaseContext dbConnectionContext)
        {
            _dbContext = dbContext;
            _connectionString = dbConnectionContext.Database.GetConnectionString();
        }

        public Dictionary<Type, object> _repos = new();

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if(_repos == null)
            {
                _repos = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repos.ContainsKey(type))
            {
                IGenericRepository<TEntity> repo = new GenericRepository<TEntity>(_dbContext);
                _repos.Add(type, repo);
            }

            return _repos[type] as IGenericRepository<TEntity>;
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync(CancellationToken.None);
        }

        public List<TEntity> DapperSpListWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class
        {
            using(var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, parameters, commandTimeout: 60).ToList();
            }
        }

        public List<TEntity> DapperSpListWithoutParams<TEntity>(string spName) where TEntity : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, commandTimeout: 60).ToList();
            }
        }

        public TEntity DapperSpSingleWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, parameters, commandTimeout: 60).FirstOrDefault();
            }
        }

        public TEntity DapperSpSingleWithoutParams<TEntity>(string spName) where TEntity : class
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();
                return connection.Query<TEntity>(spName, commandTimeout: 60).FirstOrDefault();
            }
        }

        protected virtual void Dispose(bool disposing)
        {
            if(!disposed && disposing && _dbContext != null)
            {
                _dbContext.Dispose();
                _dbContext = null;
                disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
     
    }
}
