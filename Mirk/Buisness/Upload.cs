using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;

namespace Mirk.Buisness
{
    public class Upload
    {
        private static Upload instance;

        public static Upload getInstance()
        {
            if (instance == null)
            {
                instance = new Upload();
            }
            return instance;
        }

        public bool TryUpload(HttpPostedFileBase fileUpload1, string path)
        {
            bool u;
            try
            {
                fileUpload1.SaveAs(path);
                u = false;
            }
            catch (Exception e)
            {
                u = true;
            }
            return u;
        }

        public bool UploadProfileImg(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = HostingEnvironment.MapPath(@"~\Ressources\Group_Pictures\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += file.FileName;
                return TryUpload(file, path);
            }
            else
            {
                return true;
            }
        }

        public bool UploadUserImg(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = HostingEnvironment.MapPath(@"~\Ressources\User_Pictures\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += file.FileName;
                return TryUpload(file, path);
            }
            else
            {
                return true;
            }
        }

        public bool UploadSharedDocument(HttpPostedFileBase file)
        {
            if (file != null)
            {
                string path = HostingEnvironment.MapPath(@"~\Ressources\Shared_Files\");
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                path += file.FileName;
                return TryUpload(file, path);
            }
            else
            {
                return true;
            }
        }
    }
}