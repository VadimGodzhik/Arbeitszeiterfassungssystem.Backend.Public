using System;
using System.Collections.Generic;
using System.Linq;
using BRIT.Dal.EfStructures;
using BRIT.Models.Entities;
using BRIT.Models.Entities.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BRIT.Dal.Initialization
{
    public static class SampleDataInitializer
    {

        internal static void ClearData(ApplicationDbContext context)
        {
            var entitties = new[]
            {
                typeof(Angestellte).FullName,
                typeof(Arbeitsandauer).FullName,
                typeof(Arbeitsort).FullName,
                typeof(Arbeitszeiterfassung).FullName,
                typeof(Fundort).FullName,
                typeof(Hausanschrift).FullName,
                typeof(Kennwort).FullName,
                typeof(Rolle).FullName,
                typeof(Stadt).FullName,
            };

            foreach (var entityName in entitties)
            {
                var entity = context.Model.FindEntityType(entityName);
                var tableName = entity.GetTableName();
                var schemaName = entity.GetSchema();
                context.Database.ExecuteSqlRaw($"DELETE FROM {schemaName}.{tableName}");
                context.Database.ExecuteSqlRaw($"DBCC CHECKIDENT (\"{schemaName}.{tableName}\", RESEED, 1);");
            }
        }

        internal static void SeedData(ApplicationDbContext context)
        {
            try
            {
                ProcessInsert(context, context.Angestelltes!, SampleData.Angestelltes);
                ProcessInsert(context, context.Arbeitsorts!, SampleData.Arbeitsorte);
                //ProcessInsert(context, context.AngestellteArbeitsortViewModels!, SampleData.AngestellteArbeitsortViewModels);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
            static void ProcessInsert<TEntity>(
                ApplicationDbContext context, DbSet<TEntity> table, List<TEntity> records) where TEntity : BaseEntities
            {
                if (table.Any())
                {
                    return;
                }

                IExecutionStrategy strategy = context.Database.CreateExecutionStrategy();
                strategy.Execute(() =>
                {
                    using var transaction = context.Database.BeginTransaction();
                    try
                    {
                        var metaData = context.Model.FindEntityType(typeof(TEntity).FullName);
                        context.Database.ExecuteSqlRaw(
                            $"SET IDENTITY_INSERT {metaData.GetSchema()}.{metaData.GetTableName()} ON");
                        table.AddRange(records);
                        context.SaveChanges();
                        context.Database.ExecuteSqlRaw(
                            $"SET IDENTITY_INSERT {metaData.GetSchema()}.{metaData.GetTableName()} OFF");
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                });
            }

        }

        public static void DropAndCreateDatabase(ApplicationDbContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.Migrate();
        }

        public static void InitializeData(ApplicationDbContext context)
        {
            DropAndCreateDatabase(context);
            SeedData(context);
        }

        public static void ClearAndReseedDatabase(ApplicationDbContext context)
        {
            ClearData(context);
            SeedData(context);
        }
    }
}
