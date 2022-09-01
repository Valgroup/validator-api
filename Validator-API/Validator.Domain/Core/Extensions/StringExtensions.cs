using System.Text;

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

        public static string ClearCaracters(this string value, params char[] caracteres)
        {
            if (string.IsNullOrEmpty(value))
                return value;

            var sb = new StringBuilder();

            foreach (var c in value)
            {
                bool adicionar = true;
                for (int j = 0; j < caracteres.Length; j++)
                {
                    if (caracteres[j] == c)
                    {
                        adicionar = false;
                        break;
                    }
                }

                if (adicionar)
                    sb.Append(c);

            }

            if (string.IsNullOrEmpty(sb.ToString()))
                return null;

            return sb.ToString().Trim();
        }



    }
}
