// See https://aka.ms/new-console-template for more information

namespace CookieCookbook.DataAccess
{

    public interface IStringsRepository
    {
        List<string> ReadFromFile(string filePath);

        public void WriteToFile(List<string> strings, string filePath);

    }

}












