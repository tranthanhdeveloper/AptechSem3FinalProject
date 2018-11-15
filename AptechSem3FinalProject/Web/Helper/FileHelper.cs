using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace Web.Helper
{
    public static class FileHelper
    {
        public static bool DeleteFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                try
                {
                    System.IO.File.Delete(filePath);
                    return true;
                }
                catch(Exception ex)
                {
                    throw ex;
                }
            }
            return false;
        }

        public static bool StoreFile(string storedDirectory, string fileName,  HttpPostedFileBase file)
        {
            
            try
            {
                var storedPath = Path.Combine(storedDirectory, fileName);
                file.SaveAs(storedPath);
                return true;
            }catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}