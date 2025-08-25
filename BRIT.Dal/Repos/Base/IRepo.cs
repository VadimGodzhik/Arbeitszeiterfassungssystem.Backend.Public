using System;
using System.Collections.Generic;


namespace BRIT.Dal.Repos.Base
{
    public interface IRepo<T> : IDisposable
    {
        //....................................................... 1 - ok
        int Add(T entity, bool persist = true);
        Task<int> AddAsync(T entity, bool persist = true);

        
        //....................................................... 2
        int AddRange(IEnumerable<T> entities, bool persist = true);

        
        //....................................................... 3
        int Update(T entity, bool persist = true);
        Task<int> UpdateAsync(T entity, int id, CancellationToken cancellationToken, bool persist = true);


        //....................................................... 4
        int UpdateRange(IEnumerable<T> entities, bool persist = true);


        //....................................................... 5
        int Delete(int id, byte[] timeStamp, bool persist = true);



        //....................................................... 6
        int Delete(T entity, bool persist = true);
        Task<int> DeleteAsync(T entity, int id, CancellationToken cancellationToken, bool persist = true);


        //....................................................... 7
        int DeleteRange(IEnumerable<T> entities, bool persist = true);



        //....................................................... 8 - ok
        T? Find(int? id);
        Task<T?> FindAsync(int? id);


        //....................................................... 9 - ok
        T? FindAsNoTracking(int id);
        Task<T?> FindAsNoTrackingAsync(int id);


        //....................................................... 10
        T? FindIgnoreQueryFilters(int id);


        //....................................................... 11 - ok
        IEnumerable<T> GetAll();



        //....................................................... 12
        IEnumerable<T> GetAllIgnoreQueryFilters();


        //....................................................... 13
        void ExecuteQuery(string sql, object[] sqlParametersObjects);



        //....................................................... 14 - ok
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
        
    }
}
