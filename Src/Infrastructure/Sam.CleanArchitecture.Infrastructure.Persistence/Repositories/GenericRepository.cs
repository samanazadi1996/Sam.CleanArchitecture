using Sam.CleanArchitecture.Application.Interfaces.Repositories;
using Sam.CleanArchitecture.Application.Parameters;
using Sam.CleanArchitecture.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;

namespace Sam.CleanArchitecture.Infrastructure.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        public GenericRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public virtual async Task<T> GetByIdAsync(long id)
        {
            return await _dbContext.Set<T>().FindAsync(id);
        }

        public async Task<T> AddAsync(T entity)
        {
            await _dbContext.Set<T>().AddAsync(entity);
            return entity;
        }

        public void Update(T entity)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
        }

        public void Delete(T entity)
        {
            _dbContext.Set<T>().Remove(entity);
        }

        public async Task<IReadOnlyList<T>> GetAllAsync()
        {
            return await _dbContext
                 .Set<T>()
                 .ToListAsync();
        }

        protected IQueryable<T> Paged(IQueryable<T> query, PagenationRequestParameter requestParameter)
        {
            if (requestParameter.OrderParameters is not null)
                foreach (var item in requestParameter.OrderParameters)
                    query = OrderByColumn(query, item.PropertyName, item.Descending);

            query = query
                .Skip((requestParameter.PageNumber - 1) * requestParameter.PageSize)
                .Take(requestParameter.PageSize);

            return query;
            IQueryable<T> OrderByColumn(IQueryable<T> collection, string columnName, bool desk)
            {
                PropertyInfo prop = typeof(T).GetProperty(columnName);

                if (prop is null) return collection;

                ParameterExpression param = Expression.Parameter(typeof(T), "x");

                MemberExpression propertyExpression = Expression.Property(param, prop);

                LambdaExpression lambda = Expression.Lambda(propertyExpression, param);

                string methodName = desk ? "OrderByDescending" : "OrderBy";

                MethodCallExpression orderByExpression = Expression.Call(
                    typeof(Queryable),
                    methodName,
                    new[] { typeof(T), prop.PropertyType },
                    collection.Expression,
                    Expression.Quote(lambda)
                );

                return collection.Provider.CreateQuery<T>(orderByExpression);

            }
        }
    }
}
