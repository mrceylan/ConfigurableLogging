using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MyBaseProject.Dal.Contexts;
using MyBaseProject.Dal.Repositories;

namespace MyBaseProject.Dal.UnitOfWork
{
  /// <summary>
  /// base unit for generating repositories and commiting changes
  /// </summary>
  public class UnitOfWork : IUnitOfWork
  {
    private readonly ProjectDbContext context;

    public UnitOfWork(ProjectDbContext context)
    {
      this.context = context;
    }

    public void Dispose()
    {
      context.Dispose();
    }

    public IRepository<T> GetRepository<T>() where T : class
    {
      return new Repository<T>(context);
    }

    public int Save()
    {
      return context.SaveChanges();
    }

    public async Task<int> SaveAsync()
    {
      return await context.SaveChangesAsync();
    }
  }
}
