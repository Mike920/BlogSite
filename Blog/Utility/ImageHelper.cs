using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Blog.Utility
{
    public class ImageHelper
    {
       /* public virtual ActionResult CropImage(string imagePath, int? cropPointX, int? cropPointY, int? imageCropWidth, int? imageCropHeight)
        {
            if (string.IsNullOrEmpty(imagePath) || !cropPointX.HasValue || !cropPointY.HasValue || !imageCropWidth.HasValue || !imageCropHeight.HasValue)
            {
                return new HttpStatusCodeResult((int)HttpStatusCode.BadRequest);
            }

            byte[] imageBytes = System.IO.File.ReadAllBytes(Server.MapPath(imagePath));
            byte[] croppedImage = ImageHelper.CropImage(imageBytes, cropPointX.Value, cropPointY.Value, imageCropWidth.Value, imageCropHeight.Value);

            string tempFolderName = Server.MapPath("~/" + ConfigurationManager.AppSettings["Image.TempFolderName"]);
            string fileName = Path.GetFileName(imagePath);

            try
            {
                FileHelper.SaveFile(croppedImage, Path.Combine(tempFolderName, fileName));
            }
            catch (Exception ex)
            {
                //Log an error     
                return new HttpStatusCodeResult((int)HttpStatusCode.InternalServerError);
            }

            string photoPath = string.Concat("/", ConfigurationManager.AppSettings["Image.TempFolderName"], "/", fileName);
            return Json(new { photoPath = photoPath }, JsonRequestBehavior.AllowGet);
        }

        static class FileHelper
        {
            public static void SaveFile(byte[] content, string path)
            {
                string filePath = GetFileFullPath(path);
                if (!Directory.Exists(Path.GetDirectoryName(filePath)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(filePath));
                }

                //Save file
                using (FileStream str = File.Create(filePath))
                {
                    str.Write(content, 0, content.Length);
                }
            }

            public static string GetFileFullPath(string path)
            {
                string relName = path.StartsWith("~") ? path : path.StartsWith("/") ? string.Concat("~", path) : path;

                string filePath = relName.StartsWith("~") ? HostingEnvironment.MapPath(relName) : relName;

                return filePath;
            }
        }*/
    }
}
