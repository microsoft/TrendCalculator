using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace TrendsCalculator.Library.Helper
{
    public static class ExtensionMethods
    {
        public static void CopyPropertiesTo<T,Y>(this List<T> source, List<Y> dest)
        {
            var plist = from prop in typeof(T).GetProperties() where prop.CanRead && prop.CanWrite select prop;

            foreach (PropertyInfo prop in plist)
            {
                prop.SetValue(dest, prop.GetValue(source, null), null);
            }
        }
    }
}
