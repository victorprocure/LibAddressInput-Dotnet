using System;

namespace LibAddressInput.Utils
{
    public static class StringUtils
    {
        internal static string CheckNotNull(string? value)
        {
            if(string.IsNullOrEmpty(value))
                throw new InvalidOperationException($"{nameof(value)} cannot be null");

            return value;
        }

        internal static string? TrimToNull(string? value)
        {
            if(value is null)
                return null;
            
            value = value.Trim();

            if(string.IsNullOrEmpty(value))
                return null;

            return value;
        }
    }
}