using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TCR.Lib.Utility
{
    public static class StringUtils
    {
        public static string FirstWords(this String input, int numberWords, bool appendEllips = true)
        {
            if (input.WordCount() <= numberWords)
                return input;

            // Number of words we still want to display.
            int words = numberWords;
            // Loop through entire summary.
            for (int i = 0; i < input.Length; i++)
            {
                // Increment words on a space.
                if (input[i] == ' ')
                {
                    words--;
                }
                // If we have no more words to display, return the substring.
                if (words == 0)
                {
                    if (appendEllips)
                        return input.Substring(0, i) + "...";
                    else
                        return input.Substring(0, i) + "...";
                }
            }
            return string.Empty;
        }

        public static int WordCount(this String str)
        {
            return str.Split(new char[] { ' ', '.', '?' }, StringSplitOptions.RemoveEmptyEntries).Length;
        }

        public static List<string> FindAllUrls(this String str)
        {
            if (String.IsNullOrWhiteSpace(str))
                return new List<string>();

            List<string> result = new List<string>();
            Regex regx = new Regex("http://([\\w+?\\.\\w+])+([a-zA-Z0-9\\~\\!\\@\\#\\$\\%\\^\\&amp;\\*\\(\\)_\\-\\=\\+\\\\\\/\\?\\.\\:\\;\\'\\,]*)?", RegexOptions.IgnoreCase);
            MatchCollection mactches = regx.Matches(str);
            foreach (Match match in mactches)
            {
                if(!result.Contains(match.Value))
                  result.Add(match.Value);
            }
            return result;
        }

        public static string ToHtmlParagraphs(this String input, bool splitWithBrs = false)
        {
            string result = "";
            foreach(string s in input.Split("\n".ToCharArray()))
            {
                if (splitWithBrs)
                {
                    if (!String.IsNullOrWhiteSpace(result))
                        result = result + "<br />" + s;
                    else
                        result = s;
                }
                else
                {
                    result = result + "<p>" + s + "</p>";
                }
            }
            return result;
        }
    }
}
