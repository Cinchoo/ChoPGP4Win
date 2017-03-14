using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChoPGP4Win
{
    public static class ChoExtensions
    {
        public static string FormatString(this string format, params object[] args)
        {
            return string.Format(format, args);
        }

        public static bool IsEmpty(this string text)
        {
            if (text != null)
                return text.Length == 0;
            return false;
        }

        public static bool IsNull(this string text)
        {
            return text == null;
        }

        public static bool IsNullOrEmpty(this string text)
        {
            return string.IsNullOrEmpty(text);
        }

        public static bool IsNullOrWhiteSpace(this string text)
        {
            return string.IsNullOrWhiteSpace(text);
        }

        public static string ToNString<T>(this T target, T defaultValue = default(T))
        {
            return target == null ? defaultValue == null ? String.Empty : defaultValue.ToString() : target.ToString();
        }

        public static T CastTo<T>(this object @this, T defaultValue = default(T))
        {
            if (@this == null || @this == DBNull.Value)
                return defaultValue == null ? default(T) : defaultValue;
            else if (@this is string && ((string)@this).IsNullOrWhiteSpace())
                return defaultValue == null ? default(T) : defaultValue;
            else
            {
                Type targetType = typeof(T);
                if (targetType == typeof(object))
                    return (T)@this;

                try
                {
                    if (targetType.IsEnum)
                    {
                        if (@this is string)
                            return (T)Enum.Parse(targetType, @this as string);
                        else
                            return (T)Enum.ToObject(targetType, @this);
                    }
                    else if (targetType == typeof(Type))
                    {
                        if (@this is string)
                            return (T)Convert.ChangeType(Type.GetType(@this as string), typeof(T));
                        else
                            return (T)Convert.ChangeType(@this, typeof(T));
                    }
                    else
                        return (T)Convert.ChangeType(@this, typeof(T));
                }
                catch
                {
                    if (defaultValue != null)
                        return defaultValue;

                    throw;
                }
            }
        }
    }
}
