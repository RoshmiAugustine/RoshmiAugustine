// -----------------------------------------------------------------------
// <copyright file="BaseRepository.cs" company="Naicoits">
// Copyright (c) Naicoits. All rights reserved.
// </copyright>
// -----------------------------------------------------------------------
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Opeeka.PICS.Domain.DTO;
using Opeeka.PICS.Domain.Entities;
using Opeeka.PICS.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Opeeka.PICS.Infrastructure.Data.Repositories.Base
{
    /// <summary>
    /// 
    /// https://blogs.msdn.microsoft.com/pfxteam/2012/04/13/should-i-expose-synchronous-wrappers-for-asynchronous-methods/
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IAsyncRepository<T> where T : BaseEntity
    {
        private readonly OpeekaDBContext _dbContext;

        public BaseRepository(OpeekaDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext.Set<T>().ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).ToListAsync();
        }

        public async Task<int> CountAsync(ISpecification<T> spec)
        {
            return await ApplySpecification(spec).CountAsync();
        }

        private IQueryable<T> ApplySpecification(ISpecification<T> spec)
        {
            return SpecificationEvaluator<T>.GetQuery(_dbContext.Set<T>().AsQueryable(), spec);
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().AsNoTracking().Where(predicate).ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (!string.IsNullOrWhiteSpace(includeString))
            {
                query = query.Include(includeString);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public async Task<IReadOnlyList<T>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
            IQueryable<T> query = _dbContext.Set<T>();
            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (includes != null)
            {
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }

            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(dynamic id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> GetRowAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbContext.Set<T>().Where(predicate).FirstOrDefaultAsync();
        }

        public async Task<T> AddAsync(T entity)
        {
            try
            {
                _dbContext.Set<T>().Add(entity);
                await _dbContext.SaveChangesAsync();
                return entity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task AddBulkAsync(List<T> entityList)
        {
            try
            {
                if (entityList.Count > 0)
                {
                    using (var sqlBulkCopy = GetSqlBulkCopy(this._dbContext, this._dbContext.Database.CurrentTransaction))
                    {
                        sqlBulkCopy.BatchSize = 10000;
                        sqlBulkCopy.BulkCopyTimeout = 1800;
                        var dataTable = GetDataTable(entityList, sqlBulkCopy);
                        await sqlBulkCopy.WriteToServerAsync(dataTable);
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<T> UpdateAsync(T entity)
        {
            try
            {
                entity = UpdateForAudit(entity);
                _dbContext.Entry(entity).State = EntityState.Modified;
                await _dbContext.SaveChangesAsync();
                _dbContext.Entry(entity).State = EntityState.Detached;
                return entity;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteAsync(T entity)
        {
            try
            {
                entity = UpdateForAudit(entity);
                _dbContext.Entry(entity).State = EntityState.Deleted;
                await _dbContext.SaveChangesAsync();
                _dbContext.Entry(entity).State = EntityState.Detached;
            }
            catch (Exception)
            {
                throw;
            }

        }

        public async Task DeleteBulkAsync(List<T> entityList)
        {
            try
            {
                foreach (T item in entityList)
                {
                    var entity = UpdateForAudit(item);
                    _dbContext.Set<T>().Remove(entity);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }

        }

        /// <summary>
        /// Update the entity for EF bulit-in update
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private T UpdateForAudit(T entity)
        {
            try
            {
                var keyName = _dbContext.Model.FindEntityType(typeof(T)).FindPrimaryKey().Properties
                .Select(x => x.Name).Single();
                var entityKey = entity.GetType().GetProperty(keyName).GetValue(entity, null);
                var oldEntity = _dbContext.Set<T>().FindAsync(entityKey).Result;
                var newProperties = entity.GetType().GetProperties();
                var oldProperties = oldEntity.GetType().GetProperties();
                foreach (PropertyInfo newProps in newProperties)
                {
                    foreach (PropertyInfo oldProps in oldProperties)
                    {
                        if (newProps.Name == oldProps.Name)
                        {
                            oldProps.SetValue(oldEntity, newProps.GetValue(entity));
                        }
                    }
                }
                return oldEntity;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<T> ExecuteSqlQuery<T>(string query, Func<DbDataReader, T> map, List<QueryParameterDTO> sqlParameterKeyValues = null)
        {
            try
            {
                var entities = new List<T>();
                var context = _dbContext;
                DbCommand command;
                if (context.Database.CurrentTransaction != null)
                {
                    command = context.Database.CurrentTransaction.GetDbTransaction().Connection.CreateCommand();
                    command.Transaction = context.Database.CurrentTransaction.GetDbTransaction();
                }
                else
                    command = context.Database.GetDbConnection().CreateCommand();
                command.CommandText = query;
                command.CommandType = CommandType.Text;
                command.CommandTimeout = 30;
                if (sqlParameterKeyValues != null && sqlParameterKeyValues.Count() > 0)
                {
                    foreach (var parameter in sqlParameterKeyValues)
                    {
                        command.Parameters.Add(new SqlParameter(parameter.Parameter, parameter.ParameterValue));
                    }
                }
                context.Database.OpenConnection();
                using (var result = command.ExecuteReader())
                {
                    while (result.Read())
                    {
                        entities.Add(map(result));
                    }
                }
                context.Database.CloseConnection();
                return entities;
            }
            catch (Exception)
            {
                throw;
            }

        }



        internal DataTable GetDataTable<T>(IList<T> entities, SqlBulkCopy sqlBulkCopy)
        {
            var dataTable = new DataTable();
            var columnsDict = new Dictionary<string, object>();
            var ownedEntitiesMappedProperties = new HashSet<string>();

            var type = entities[0].GetType();
            var entityType = this._dbContext.Model.FindEntityType(type);
            //var entityPropertiesDict = entityType.GetProperties().Where(a => tableInfo.PropertyColumnNamesDict.ContainsKey(a.Name)).ToDictionary(a => a.Name, a => a);
            var entityPropertiesDict = entityType.GetProperties().ToDictionary(a => a.Name, a => a);
            var entityNavigationOwnedDict = entityType.GetNavigations().Where(a => a.GetTargetType().IsOwned()).ToDictionary(a => a.Name, a => a);
            var properties = type.GetProperties();
            // var discriminatorColumn = tableInfo.ShadowProperties.Count == 0 ? null : tableInfo.ShadowProperties.ElementAt(0);

            foreach (var property in properties)
            {
                if (entityPropertiesDict.ContainsKey(property.Name))
                {
                    var propertyEntityType = entityPropertiesDict[property.Name];
                    string columnName = propertyEntityType.GetColumnName();

                    // var isConvertible = tableInfo.ConvertibleProperties.ContainsKey(columnName);
                    var propertyType = property.PropertyType;

                    var underlyingType = Nullable.GetUnderlyingType(propertyType);
                    if (underlyingType != null)
                    {
                        propertyType = underlyingType;
                    }

                    dataTable.Columns.Add(columnName, propertyType);
                    columnsDict.Add(property.Name, null);
                }
            }


            foreach (var entity in entities)
            {
                foreach (var property in properties)
                {
                    if (entityPropertiesDict.ContainsKey(property.Name))
                    {
                        var propertyValue = property.GetValue(entity, null);
                        if (property.PropertyType == typeof(Guid) && (Guid)propertyValue == default(Guid))
                        {
                            propertyValue = Guid.NewGuid();
                        }

                        columnsDict[property.Name] = propertyValue;
                    }
                }
                var record = columnsDict.Values.ToArray();
                dataTable.Rows.Add(record);
            }

            foreach (DataColumn item in dataTable.Columns)  //Add mapping
            {
                sqlBulkCopy.ColumnMappings.Add(item.ColumnName, item.ColumnName);
            }
            string schema = entityType.GetSchema() != null ? entityType.GetSchema() : "dbo";
            dataTable.TableName = schema + "." + entityType.GetTableName();
            sqlBulkCopy.DestinationTableName = dataTable.TableName;
            return dataTable;
        }

        /// <summary>
        /// Get Sql connection for bulk copy
        /// </summary>
        /// <param name="sqlConnection"></param>
        /// <param name="transaction"></param>
        /// <returns></returns>
        private SqlBulkCopy GetSqlBulkCopy(DbContext dbContext, IDbContextTransaction transaction)
        {
            var sqlConnection = dbContext.Database.GetDbConnection().ConnectionString;
            if (transaction == null)
            {
                //return new SqlBulkCopy(sqlConnection, sqlBulkCopyOptions, null);
                return new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.CheckConstraints);
            }
            else
            {
                var sqlTransaction = (SqlTransaction)transaction.GetDbTransaction();
                //return new SqlBulkCopy(sqlConnection, sqlBulkCopyOptions, sqlTransaction);
                return new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.CheckConstraints);
            }
        }

        /// <summary>
        /// Update Bulk Async.
        /// </summary>
        /// <param name="entityList">entityList.</param>
        /// <returns>Task.</returns>
        public async Task UpdateBulkAsync(List<T> entityList)
        {
            try
            {
                if (entityList.Count > 0)
                {
                    _dbContext.Set<T>().UpdateRange(entityList);
                    await _dbContext.SaveChangesAsync();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}