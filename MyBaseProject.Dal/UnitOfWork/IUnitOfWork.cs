using MyBaseProject.Dal.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBaseProject.Dal.UnitOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    IRepository<T> GetRepository<T>() where T : class;
    int Save();
    Task<int> SaveAsync();
  }
}
