// See https://aka.ms/new-console-template for more information

namespace CookieCookbook.DataAccess
{
    public class StringsTextualRepository : StringsRepository
    {
        protected override List<string> TextToStrings(string @string)
        {
            return @string.Split(Environment.NewLine).ToList();
        }

        protected override string StringsToText(List<string> strings)
        {
            return string.Join(Environment.NewLine, strings);
        }
    }

}












