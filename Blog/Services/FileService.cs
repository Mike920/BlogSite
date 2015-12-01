using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Blog.Utility;

namespace Blog.Services
{
    class FileService
    {
        private ModelStateDictionary _modelState;

        public FileService(ModelStateDictionary modelState)
        {
            _modelState = modelState;
        }

        public IEnumerable<string> UploadToTempDir(HttpFileCollectionBase files)
        {
            var fileNames = new List<string>();
            
            if (files.Count > 0)
            {
                foreach (string file in files)
                {
                    var postedFile = files[file];
                    string uniqueName = GenerateUniqueName(postedFile.FileName);
                    var filePath = Path.Combine(ServerTools.Paths.TempFolder, uniqueName);
                    postedFile.SaveAs(filePath);
                    fileNames.Add(uniqueName);
                    // NOTE: To store in memory use postedFile.InputStream
                }
            }
            return fileNames;
        }

        private string GenerateUniqueName(string fileName)
        {
            return System.IO.Path.GetRandomFileName() + "$temp$" + fileName;
        }
    }
}
