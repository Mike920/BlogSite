using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Blog.Utility
{
  /*  public class Sanitizer
    {
        private static AjaxControlToolkit.HtmlEditor.Sanitizer.DefaultHtmlSanitizer sanitizer = new AjaxControlToolkit.HtmlEditor.Sanitizer.DefaultHtmlSanitizer();
        
        private static Dictionary<string, string[]> elementWhitelist = new Dictionary<string, string[]>
{
    {"b"            , new string[] { "style" }},
    {"strong"       , new string[] { "style" }},
    {"i"            , new string[] { "style" }},
    {"em"           , new string[] { "style" }},
    {"u"            , new string[] { "style" }},
    {"strike"       , new string[] { "style" }},
    {"sub"          , new string[] { "style" }},
    {"sup"          , new string[] { "style" }},
    {"p"            , new string[] { "align" }},
    {"div"          , new string[] { "style", "align" }},
    {"ol"           , new string[] { }},
    {"li"           , new string[] { }},
    {"ul"           , new string[] { }},
    {"a"            , new string[] { "href" }},
    {"font"         , new string[] { "style", "face", "size", "color" }},
    {"span"         , new string[] { "style" }},
    {"blockquote"   , new string[] { "style", "dir" }},
    {"hr"           , new string[] { "size", "width", "id" }},
    {"img"          , new string[] { "src" }},
    {"h1"           , new string[] { "style" }},
    {"h2"           , new string[] { "style" }},
    {"h3"           , new string[] { "style" }},
    {"h4"           , new string[] { "style" }},
    {"h5"           , new string[] { "style" }},
    {"h6"           , new string[] { "style" }}
};

        private static Dictionary<string, string[]> attributeWhitelist = new Dictionary<string, string[]>
{
    {"style"    , new string[] {}},
    {"align"    , new string[] {}},
    {"href"     , new string[] {}},
    {"face"     , new string[] {}},
    {"size"     , new string[] {}},
    {"color"    , new string[] {}},
    {"dir"      , new string[] {}},
    {"width"    , new string[] {}},
    {"id"       , new string[] {}},
    {"src"      , new string[] {}}
};

        public string SanitizeHtmlInput(string unsafeStr)
        {
            return sanitizer.GetSafeHtmlFragment(unsafeStr, elementWhitelist, attributeWhitelist);
        }
    }*/
}
