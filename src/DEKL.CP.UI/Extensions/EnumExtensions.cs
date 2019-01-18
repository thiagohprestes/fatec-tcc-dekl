using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Web.Mvc;

namespace DEKL.CP.UI.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum val)
            => val.GetType()
                  .GetMember(val.ToString())
                  .FirstOrDefault()?
                  .GetCustomAttribute<DisplayAttribute>(false)?
                  .Name ?? val.ToString();

        public static string GetDescription(this Enum val) 
            => val.GetType()
                  .GetMember(val.ToString())
                  .FirstOrDefault()?
                  .GetCustomAttribute<DescriptionAttribute>(false)?
                  .Description ?? string.Empty;

        public static SelectList ToSelectList<TEnum>(this TEnum obj)
         where TEnum : struct, IComparable, IFormattable, IConvertible 
            => new SelectList(Enum.GetValues(typeof(TEnum)).OfType<Enum>()
                .Select(x => new SelectListItem
                {
                    Text = x.GetDescription(),
                    Value = (Convert.ToInt32(x)).ToString()
                }), "Value", "Text");
    }
}
