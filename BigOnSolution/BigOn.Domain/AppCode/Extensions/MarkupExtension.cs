using System.Text.RegularExpressions;

namespace BigOn.Domain.AppCode.Extensions
{
    public static partial class Extension
    {
        static public string ToPlainText(this string text)
        {
            text = Regex.Replace(text, "<[^>]*>", "");
            return text;
        }
    }
}
