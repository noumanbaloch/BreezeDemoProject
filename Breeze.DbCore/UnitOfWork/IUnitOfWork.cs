using Breeze.DbCore.GenericRepository;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Breeze.DbCore.UnitOfWork
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> GetRepository<T>() where T : class;
        List<TEntity> DapperSpListWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class;
        List<TEntity> DapperSpListWithoutParams<TEntity>(string spName) where TEntity : class;
        TEntity DapperSpSingleWithParams<TEntity>(string spName, DynamicParameters parameters) where TEntity : class;
        TEntity DapperSpSingleWithoutParams<TEntity>(string spName) where TEntity : class;
        int Commit();
        Task<int> CommitAsync();
    }
}
