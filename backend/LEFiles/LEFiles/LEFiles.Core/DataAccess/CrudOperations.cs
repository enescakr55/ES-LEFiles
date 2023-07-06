using LEFiles.Core.Entities.Abstract;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Core.DataAccess
{
  
  public class CrudOperations<TEntity, TContext> : ICrudOperations<TEntity> where TEntity:class, IEntity,new() where TContext : DbContext,new()
  {
    DbContext context;
    public CrudOperations()
    {
      context = new TContext();
    }
    public CrudOperations(DbContext myContext)
    {
      context = myContext;
    }
    public TEntity Add(TEntity entity)
    {
      var addedEntity = context.Entry(entity);
      addedEntity.State = EntityState.Added;
      return addedEntity.Entity;
    }

    public bool Delete(TEntity entity)
    {
      var deletedEntity = context.Entry(entity);
      deletedEntity.State = EntityState.Deleted;
      return true;
    }

    public TEntity Get(Expression<Func<TEntity, bool>> predicate)
    {
      var entry = context.Set<TEntity>().FirstOrDefault(predicate);
      return entry;
    }

    public List<TEntity> GetAll(Expression<Func<TEntity, bool>> predicate = null)
    {
      var entries = predicate == null ? context.Set<TEntity>().ToList() : context.Set<TEntity>().Where(predicate).ToList();
      return entries;
    }

    public TEntity Update(TEntity entity)
    {
      var updatedEntry = context.Entry(entity);
      updatedEntry.State = EntityState.Modified;
      return updatedEntry.Entity;
    }
  }
}
