using System;
using System.Collections.Generic;
using System.Reflection;

namespace TaskManager.Repository.Converter
{
    internal static class ModelConvertorHelper
    {
        public static T ConvertModel<T, TS>(TS source)
            where T : class, new()
            where TS : class, new()
        {
            return ConvertModel<T, TS>(source, null);
        }

        public static T ConvertModel<T, TS>(TS source, HashSet<string> ignore)
            where T : class, new()
            where TS : class, new()
        {
            if (source == null)
            {
                return null;
            }
            var target = new T();

            Type sType = typeof(TS);
            Type tType = typeof(T);
            PropertyInfo[] sPropertyInfo = sType.GetProperties();

            foreach (PropertyInfo item in sPropertyInfo)
            {
                if (ignore != null && ignore.Contains(item.Name))
                    continue;
                object tempValue = item.GetValue(source, null);
                tType.GetProperty(item.Name).SetValue(target, tempValue, null);
            }
            return target;
        }
    }
}
