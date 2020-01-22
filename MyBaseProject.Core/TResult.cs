using System;
using System.Collections.Generic;
using System.Text;

namespace MyBaseProject.Core
{
  public class TResult<T>
  {
    public TResult()
    {
      this.Error = false;
    }

    public bool Error { get; set; }
    public string ErrorMessage { get; set; }
    public int Count { get; set; }
    public T Data { get; set; }
    public string DevErrorMessage { get; set; }
  }
}
