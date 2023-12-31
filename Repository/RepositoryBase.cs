using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using BackendApi2.Contracts;
using BackendApi2.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackendApi2.Repository
{
    public abstract class RepositoryBase<T> : IRepositoryBase<T> where T : class
    {
       protected RepositoryContext RepositoryContext { get; set; } 
        public RepositoryBase(RepositoryContext repositoryContext) 
        {
            RepositoryContext = repositoryContext; 
        }
        public IQueryable<T> FindAll() => RepositoryContext.Set<T>().AsNoTracking();
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression) => 
            RepositoryContext.Set<T>().Where(expression).AsNoTracking();
        public void Create(T entity) => RepositoryContext.Set<T>().Add(entity);
        public void Update(T entity) => RepositoryContext.Set<T>().Update(entity);
        public void Delete(T entity) => RepositoryContext.Set<T>().Remove(entity);

        public  IQueryable<T> FindALLwithRelatedEntities(string Navigationproperty)
        {
           return  RepositoryContext.Set<T>().Include(Navigationproperty).AsNoTracking();
        }

        public IQueryable<T> FindByConditionWithRelatedEntities(string Navigationproperty,
         Expression<Func<T, bool>> expression)
         {
            return RepositoryContext.Set<T>().Include(Navigationproperty).Where(expression).AsNoTracking();
         }

         public Task<bool> CheckExists(Expression<Func<T, bool>> expression)
         {
            return RepositoryContext.Set<T>().AnyAsync(expression);
         }
    }
}