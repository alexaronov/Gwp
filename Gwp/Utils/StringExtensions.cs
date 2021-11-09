namespace Gwp.Utils
{
    public static class StringExtensions
    {
        public static string ToCamelCase(this string s)
        {
            if (string.IsNullOrWhiteSpace(s))
            {
                return s;
            }
            if (s.Length > 1)
            {
                return char.ToLowerInvariant(s[0]) + s.Substring(1);
            }
            return char.ToLowerInvariant(s[0]).ToString();
        }
    }
}