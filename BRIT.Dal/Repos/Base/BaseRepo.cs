using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using BRIT.Dal.EfStructures;
using BRIT.Dal.Exceptions;
using BRIT.Models.DtoEntities.Base;
using BRIT.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace BRIT.Dal.Repos.Base
{
    public abstract class BaseRepo<T> : IRepo<T> where T : BaseEntities, new()
    {
        private readonly bool _disposeContext;
        public DbSet<T> Table1 { get; }
        public ApplicationDbContext Context { get; }

        protected BaseRepo(ApplicationDbContext context)
        {
            Context = context;
            Table1 = Context.Set<T>();
            _disposeContext = false;
        }

        protected BaseRepo(DbContextOptions<ApplicationDbContext> options) : this(new ApplicationDbContext(options))
        {
            _disposeContext = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private bool _isDisposed;

        protected virtual void Dispose(bool disposing)
        {
            if (_isDisposed)
            {
                return;
            }

            if (disposing)
            {
                if (_disposeContext)
                {
                    Context.Dispose();
                }
            }

            _isDisposed = true;
        }

        ~BaseRepo()
        {
            Dispose(false);
        }


        // ....................................................... 1
        public virtual int Add(T entity, bool persist = true)
        {
            Table1.Add(entity);
            return persist ? SaveChanges() : 0;

        }

        public virtual async Task<int> AddAsync(T entity, bool persist = true)
        {
            CancellationToken cancellationToken = new CancellationToken();

            try
            {
                if (Table1 != null)
                {
                    Table1?.AddAsync(entity);
                    return persist ? await SaveChangesAsync(cancellationToken) : 0;
                }
                throw new CustomException($"An error occurred: Entity: {nameof(entity)} is null.\n");

            }
            catch (CustomException ex)
            {
                //Should handle intelligently - already logged
                throw;
            }
            catch (Exception ex)
            {
                //Should log and handle intelligently
                throw new CustomException("An error occurred adding into the database", ex);
            }
        }


        // ....................................................... 2
        public virtual int AddRange(IEnumerable<T> entities, bool persist = true)
        {
            Table1.AddRange(entities);
            return persist ? SaveChanges() : 0;
        }



        // ....................................................... 3
        public virtual int Update(T entity, bool persist = true)
        {
            Table1.Update(entity);
            return persist ? SaveChanges() : 0;
        }


        public virtual async Task<int> UpdateAsync(T entity, int id, CancellationToken cancellationToken, bool persist = true)
        {

            try
            {
                if (Table1 != null)
                {
                    if (persist)
                    {
                        var dbEntity = await Table1.FindAsync(id, cancellationToken);
                        if (dbEntity != null)
                        {
                            var typeT = dbEntity.GetType();
                            Type typeEntity = entity.GetType();

                            PropertyInfo[] propertiesT = typeT.GetProperties(BindingFlags.Instance | BindingFlags.Public);
                            PropertyInfo[] propertiesEntity = typeEntity.GetProperties(BindingFlags.Instance | BindingFlags.Public);

                            Console.WriteLine($"\n\nType of T:");
                            foreach (PropertyInfo propertyT in propertiesT)
                            {
                                Console.WriteLine($"Name property: {propertyT.PropertyType.Name}, {propertyT.Name}");
                            }

                            Console.WriteLine($"\n\nType of Entity:");
                            foreach (PropertyInfo propertyEntity in propertiesEntity)
                            {
                                Console.WriteLine($"Name property: {propertyEntity.PropertyType.Name}, {propertyEntity.Name}");
                            }


                            Console.WriteLine($"\n\nMatching od Property of T and of Entity:");
                            foreach (PropertyInfo propertyT in propertiesT)
                            {
                                foreach (PropertyInfo propertyEntity in propertiesEntity)
                                {
                                    if (propertyEntity.Name == propertyT.Name)
                                    {
                                        Console.WriteLine($"PropertyT: {propertyT.Name} = {propertyT.GetValue(dbEntity)}, PropertyEntity: {propertyEntity.Name} = {propertyEntity.GetValue(entity)} are completely coincided");
                                        if (propertyT.Name.ToString() != "Id" && propertyT.Name.ToString() != "TimeStamp" && propertyT.PropertyType.Name.ToString() != "List`1")
                                        {
                                            propertyT.SetValue(dbEntity, propertyEntity.GetValue(entity));
                                            Console.BackgroundColor = ConsoleColor.Green;
                                            Console.ForegroundColor = ConsoleColor.Black;
                                            Console.WriteLine($"PropertyT: {propertyT.Name} = {propertyT.GetValue(dbEntity)}, PropertyEntity: {propertyEntity.Name} = {propertyEntity.GetValue(entity)} are completely coincided");
                                            Console.ForegroundColor = ConsoleColor.White;
                                            Console.BackgroundColor = ConsoleColor.Black;
                                        }

                                    }

                                }
                            }
                            Console.WriteLine("\n\n");
                            
                            int result = default;
                            string[]? args = default;
                            var newContext = new ApplicationDbContextFactory().CreateDbContext(args);

                            using var transaction = await newContext.Database.BeginTransactionAsync(cancellationToken);
                            result = persist ? await SaveChangesAsync(cancellationToken) : 0;
                            // Commit transaction if all commands succeed, transaction will auto-rollback
                            // when disposed if either commands fails
                            transaction.Commit();
                            return result;
                        }
                        else
                        {
                            return 0;
                        }
                    }
                    else
                    {
                        return 0;
                    }
                }
                throw new CustomException($"An error occurred: Entity: {nameof(entity)} is null.\n");
            }
            catch (CustomException ex)
            {

                //Should handle intelligently - already logged
                throw;
            }
            catch (Exception ex)
            {
                //Should log and handle intelligently
                throw new CustomException($"An error occurred: Entity: {nameof(entity)} is null.\n{ex}");
            }
        }


        // ....................................................... 4
        public virtual int UpdateRange(IEnumerable<T> entities, bool persist = true)
        {
            Table1.UpdateRange(entities);
            return persist ? SaveChanges() : 0;
        }




        // ....................................................... 5
        public int Delete(int id, byte[] timeStamp, bool persist = true)
        {
            var entity = new T { Id = id, TimeStamp = timeStamp };
            Context.Entry(entity).State = EntityState.Deleted;
            return persist ? SaveChanges() : 0;
        }




        // ....................................................... 6
        public virtual int Delete(T entity, bool persist = true)
        {
            Table1.Remove(entity);
            return persist ? SaveChanges() : 0;
        }

        public virtual async Task<int> DeleteAsync(T entity, int íd, CancellationToken cancellationToken, bool persist = true)
        {

            try
            {
                if (Table1 != null)
                {
                    var stadtToDelete = await Table1.FindAsync(entity.Id, cancellationToken);
                    if (stadtToDelete != null)
                    {
                        Table1.Remove(stadtToDelete);
                        return persist ? await SaveChangesAsync(cancellationToken) : 0;
                    }
                }
                throw new CustomException($"An error occurred: Entity: {nameof(entity)} is null.\n");
            }
            catch (CustomException ex)
            {
                //Should handle intelligently - already logged
                throw;
            }
            catch (Exception ex)
            {
                //Should log and handle intelligently
                throw new CustomException("An error occurred adding into the database", ex);
            }
        }
        // ....................................................... 7
        public virtual int DeleteRange(IEnumerable<T> entities, bool persist = true)
        {
            Table1.RemoveRange(entities);
            return persist ? SaveChanges() : 0;
        }




        // ....................................................... 8
        public virtual T? Find(int? id) => Table1.Find(id);
        public virtual async Task<T?> FindAsync(int? id) => await Table1.FindAsync(id);



        // ....................................................... 9
        public virtual T? FindAsNoTracking(int id)
            => Table1.AsNoTrackingWithIdentityResolution().FirstOrDefault(x => x.Id == id);

        public virtual async Task<T?> FindAsNoTrackingAsync(int id)
        => await Table1.AsNoTrackingWithIdentityResolution().FirstOrDefaultAsync(x => x.Id == id);



        // ....................................................... 10
        public T? FindIgnoreQueryFilters(int id)
            => Table1.IgnoreQueryFilters().FirstOrDefault(x => x.Id == id);



        // ....................................................... 11
        public virtual IEnumerable<T> GetAll() => Table1;


        // ....................................................... 12
        public virtual IEnumerable<T> GetAllIgnoreQueryFilters() => Table1.IgnoreQueryFilters();



        // ....................................................... 13
        public void ExecuteQuery(string sql, object[] sqlParametersObjects)
            => Context.Database.ExecuteSqlRaw(sql, sqlParametersObjects);




        // ....................................................... 14
        public int SaveChanges()
        {
            try
            {
                return Context.SaveChanges();
            }
            catch (CustomException ex)
            {
                //Should handle intelligently - already logged
                throw;
            }
            catch (Exception ex)
            {
                //Should log and handle intelligently
                throw new CustomException("An error occurred updating the database", ex);
            }
        }
        public async Task<int> SaveChangesAsync(CancellationToken cancellationToken)
        {
            try
            {
                return await Context.SaveChangesAsync(cancellationToken).ConfigureAwait(false);
            }
            catch (CustomException ex)
            {
                //Should handle intelligently - already logged
                throw;
            }
            catch (Exception ex)
            {
                //Should log and handle intelligently
                throw new CustomException("An error occurred updating the database", ex);
            }
        }

    }
}
