using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Blog.Utility
{
    public static class HtmlHelpers
    {
        public static string Trim(this HtmlHelper helper, string text, int charactersLimit)
        {
            return text.Length > charactersLimit ? text.Substring(0, charactersLimit) + "..." : text;

        }
    }
}
