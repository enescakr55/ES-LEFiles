
using LEFiles.Core.Entities.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace LEFiles.Core.DataAccess
{
  public interface ICrudOperations<T> where T : class, IEntity, new()
  {
    T Add(T entity);
    bool Delete(T entity);
    T Update(T entity);
    T Get(Expression<Func<T, bool>> predicate);
    List<T> GetAll(Expression<Func<T,bool>> predicate = null!);
  }
}
