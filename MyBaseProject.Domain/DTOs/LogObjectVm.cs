using System;
using System.Collections.Generic;
using System.Text;

namespace MyBaseProject.Domain.DTOs
{
  public class LogObjectVm
  {
    public string UserId { get; set; }
    public string UserName { get; set; }
    public string TableName { get; set; }
    public object RowId { get; set; }
    public string ColumnName { get; set; }
    public string OldValue { get; set; }
    public string NewValue { get; set; }
    public DateTime Timestamp { get; set; }
  }
}
