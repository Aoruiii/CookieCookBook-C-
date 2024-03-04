// See https://aka.ms/new-console-template for more information

namespace CookieCookbook.FileAccess
{

    public class FileMetaData
    {
        public string Name { get; }
        public FileFormat Format { get; }
        public FileMetaData(string name, FileFormat format)
        {
            Name = name;
            Format = format;
        }

        public string ToPath()
        {
            return $"{Name}.{Format.AsFileExtension()}";
        }
    }
}
