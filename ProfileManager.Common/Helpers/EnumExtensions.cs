using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ProfileManager.Common.Helpers
{
    public static class EnumExtensions
    {
        private static string DisplayName(this Enum enumValue)
        {
            return enumValue.GetType()
                            ?.GetMember(enumValue.ToString())
                            ?.First()
                            ?.GetCustomAttribute<DisplayAttribute>()
                            ?.GetName();
        }


        public static string GetDisplayName(this Enum val)
        {
            string name = "";



            if (NullablEnum(val) == null)
            {
                return "";
            }

            string displayName  = val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.GetCustomAttribute<DisplayAttribute>(false)
                      ?.Name
                      ?? DisplayName(val);
            if (string.IsNullOrEmpty(displayName))
            {
                displayName = val.GetType()
                      .GetMember(val.ToString())
                      .FirstOrDefault()
                      ?.Name;
                     
            }

            return displayName;
        }

        private static string NullablEnum(Enum val)
        {
            return val?.GetType()
                  ?.GetMember(val.ToString())
                  ?.FirstOrDefault()
                  ?.Name;
        }

      
    }
}
