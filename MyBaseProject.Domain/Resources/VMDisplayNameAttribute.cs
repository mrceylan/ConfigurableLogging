using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;

namespace MyBaseProject.Domain.Resources
{
  /// <summary>
  /// reading from resources
  /// </summary>
  public class VMDisplayNameAttribute : DisplayNameAttribute
  {
    public VMDisplayNameAttribute([CallerMemberName] string propertyName = null)
        : base(GetMessageFromResource(propertyName))
    { }

    private static string GetMessageFromResource(string resourceId)
    {
      ResourceManager rm = new ResourceManager(typeof(Resources.ViewModelResource));
      return rm.GetString(resourceId.Replace(" ", "").ToLowerInvariant(), CultureInfo.CurrentCulture ?? new System.Globalization.CultureInfo("en-US"))
        ?? CultureInfo.CurrentCulture.TextInfo.ToTitleCase(resourceId.ToLower());
    }
  }
}
