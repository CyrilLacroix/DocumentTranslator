using System.Text.RegularExpressions;


namespace TranslationAssistantCore.Business
{
    static class ReplaceLanguageCode
    {
        public static string Replace(string text, string fromlangcode, string tolangcode)
        {
            return Regex.Replace(text, fromlangcode + "-{,1}[a-z]{2,4}", tolangcode, RegexOptions.IgnoreCase);
        }
    }
}
