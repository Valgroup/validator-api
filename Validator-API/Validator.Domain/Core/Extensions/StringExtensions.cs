namespace Validator.Domain.Core.Extensions
{
    public static class StringExtensions
    {
        public static string TrimOrDefault(this string value)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            return value.Trim();
        }
    }
}
