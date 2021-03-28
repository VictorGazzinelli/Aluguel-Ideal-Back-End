using System;

namespace AluguelIdeal.Api.Utils.Extensions
{
    public static class StringExtensions
    {
        public static string Truncate(this string value, int maxLength)
        {
            if (string.IsNullOrEmpty(value)) 
                return value;

            return value.Length <= maxLength ? value : value.Substring(0, maxLength);
        }

        public static string ToCamelCase(this string value)
        {
            if (string.IsNullOrEmpty(value) || char.IsLower(value, 0))
                return value;

            return char.ToLowerInvariant(value[0]) + value[1..];
        }
    }
}
