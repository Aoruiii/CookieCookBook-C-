// See https://aka.ms/new-console-template for more information

namespace CookieCookbook.DataAccess
{
    public abstract class StringsRepository : IStringsRepository
    {
        public List<string> ReadFromFile(string filePath)
        {
            string contentString = "";
            var data = new List<string>();
            if (File.Exists(filePath))
            {
                contentString = File.ReadAllText(filePath);
            }

            if (contentString != "")
            {
                data = TextToStrings(contentString);
            }
            return data;
        }

        public void WriteToFile(List<string> strings, string filePath)
        {
            string fileString = StringsToText(strings);
            File.WriteAllText(@filePath, fileString);
        }

        protected abstract List<string> TextToStrings(string @string);

        protected abstract string StringsToText(List<string> strings);

    }

}












