using MyBaseProject.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MyBaseProject.Engine.Interfaces
{
  public interface ILogEngine
  {
    Task LogAll(IEnumerable<LogObjectVm> logs);
  }
}
