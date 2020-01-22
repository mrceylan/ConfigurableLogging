using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace MyBaseProject.Domain.Models
{
  public class LogField : BaseModel
  {
    [Key]
    public long Id { get; set; }
    [Required]
    public string TableName { get; set; }
    [Required]
    public string ColumnName { get; set; }
  }
}
