using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyBaseProject.Dal.Repositories
{
  public interface IRepository<T> where T : class
  {
    T Get(int id);
    Task<T> GetAsync(int id);
    IQueryable<T> GetAll();
    IQueryable<T> Find(Expression<Func<T, bool>> predicate);
    T FirstOrDefault(Expression<Func<T, bool>> predicate);
    Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate);

    void Add(T entity);
    void AddRange(IEnumerable<T> entities);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
    void Update(T entity);
    void UpdateRange(IEnumerable<T> entities);

    IQueryable<T> GetPaged(out int totalRecord, int skip, int pageSize, string sortColumn, string sortDirection, Expression<Func<T, bool>> predicate = null);
  }
}
