// See https://aka.ms/new-console-template for more information

namespace CookieCookbook.FileAccess
{
    public static class FileFormatExtensions
    {
        public static string AsFileExtension(this FileFormat format)
        {
            return format == FileFormat.Json ? "json" : "txt";
        }
    }
}
