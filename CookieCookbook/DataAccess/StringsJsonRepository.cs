// See https://aka.ms/new-console-template for more information

using System.Text.Json;

namespace CookieCookbook.DataAccess
{
    public class StringsJsonRepository : StringsRepository
    {
        protected override List<string> TextToStrings(string @string)
        {
            return JsonSerializer.Deserialize<List<string>>(@string);
        }

        protected override string StringsToText(List<string> strings)
        {
            return JsonSerializer.Serialize(strings);
        }
    }

}












