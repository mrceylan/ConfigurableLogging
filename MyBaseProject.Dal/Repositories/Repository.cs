using MyBaseProject.Dal.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace MyBaseProject.Dal.Repositories
{
  /// <summary>
  /// Generic repository for database crud
  /// </summary>
  /// <typeparam name="T"></typeparam>
  public class Repository<T> : IRepository<T> where T : class
  {
    private readonly ProjectDbContext context;
    private readonly DbSet<T> dbSet;

 
    public Repository(ProjectDbContext context)
    {
      this.context = context;
      this.dbSet = context.Set<T>();
    }

    public void Add(T entity)
    {
      dbSet.Add(entity);
    }

    public void AddRange(IEnumerable<T> entities)
    {
      dbSet.AddRange(entities);
    }

    public IQueryable<T> Find(Expression<Func<T, bool>> predicate)
    {
      return dbSet.Where(predicate);
    }

    public T FirstOrDefault(Expression<Func<T, bool>> predicate)
    {
      return dbSet.FirstOrDefault(predicate);
    }

    public async Task<T> FirstOrDefaultAsync(Expression<Func<T, bool>> predicate)
    {
      return await dbSet.FirstOrDefaultAsync(predicate);
    }

    public T Get(int id)
    {
      return dbSet.Find(id);
    }

    public IQueryable<T> GetAll()
    {
      return dbSet;
    }

    public async Task<T> GetAsync(int id)
    {
      return await dbSet.FindAsync(id);
    }

    public IQueryable<T> GetPaged(out int totalRecord, int skip, int pageSize, string sortColumn, string sortDirection, Expression<Func<T, bool>> predicate = null)
    {
      IQueryable<T> query;
      if (predicate != null)
        query = dbSet.Where(predicate);
      else
        query = dbSet;
      totalRecord = query.Count();
      if (string.IsNullOrEmpty(sortColumn))
        return query.Skip(skip).Take(pageSize);
      else
        return query.OrderBy(sortColumn + " " + sortDirection).Skip(skip).Take(pageSize);
    }

    public void Remove(T entity)
    {
      dbSet.Remove(entity);
    }

    public void RemoveRange(IEnumerable<T> entities)
    {
      dbSet.RemoveRange(entities);
    }

    public void Update(T entity)
    {
      dbSet.Attach(entity);
      context.Entry(entity).State = EntityState.Modified;
    }

    public void UpdateRange(IEnumerable<T> entities)
    {
      dbSet.AttachRange(entities);
      context.Entry(entities).State = EntityState.Modified;
    }
  }
}
