using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace Blog.Utility
{
    public class ServerTools
    {
        public static class Paths
        {
            public static string TempFolder
            {
                get
                {
                    return HttpContext.Current.Server.MapPath("~/Temp");
                }
            }
            public static string MediaFolderPath(params string[] subfolders)
            {
                return HttpContext.Current.Server.MapPath("~/MediaData/" + Path.Combine(subfolders)); 
            }

            public static string MediaFolderBlogsPath(params string[] subfolders)
            {
                return HttpContext.Current.Server.MapPath("~/MediaData/Blogs/" + Path.Combine(subfolders)); 
            }


            public static bool TempFolderContains(string fileName)
            {
                return !String.IsNullOrWhiteSpace(fileName) && File.Exists(Path.Combine(Paths.TempFolder, fileName));
            }
        }

        public static string RelativePath(string absolutePath)
        {
            return absolutePath.Replace(HttpContext.Current.Server.MapPath("~/"), "/").Replace(@"\", "/");
        }

        public static string GenerateUrlFriendlyString(string title)
        {
            // make it all lower case
            title = title.ToLower();
            // remove entities
            title = Regex.Replace(title, @"&\w+;", "");
            // remove anything that is not letters, numbers, dash, or space
            title = Regex.Replace(title, @"[^a-z0-9\-\s]", "");
            // replace spaces
            title = title.Replace(' ', '-');
            // collapse dashes
            title = Regex.Replace(title, @"-{2,}", "-");
            // trim excessive dashes at the beginning
            title = title.TrimStart(new[] { '-' });
            // remove trailing dashes
            title = title.TrimEnd(new[] { '-' });


            return title;
        }
    }
}
