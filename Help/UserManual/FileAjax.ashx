<%@ WebHandler Language="C#" Class="FileAjax" %>

using System;
using System.Web;
using System.IO;
using System.Collections.Generic;
using System.Web.Script.Serialization;

public class FileAjax : IHttpHandler {
    
    public void ProcessRequest (HttpContext context) {
        
        string path = context.Request.Path;
        path = context.Server.MapPath(path);
        path = Path.GetDirectoryName(path);
        path = Path.Combine(path, "..\\__docs");

        if (Directory.Exists(path))
        {
            List<FileDescription> files = new List<FileDescription>();
            foreach (string file in Directory.GetFiles(path, "*.*", SearchOption.TopDirectoryOnly))
            {
                FileInfo fi = new FileInfo(file);

                FileDescription fd = new FileDescription();
                fd.FileName = fi.Name;
                decimal size = (decimal)fi.Length;
                if (size > 1024)
                {
                    size = size / 1024;
                    if (size > 1024)
                    {
                        size = size / 1024;
                        if (size > 1024)
                        {
                            size = size / 1024;
                            fd.Size = Math.Round(size, 2).ToString() + " GB";
                        }
                        else
                        {
                            fd.Size = Math.Round(size, 2).ToString() + " MB";
                        }
                    }
                    else
                    {
                        fd.Size = Math.Round(size, 2).ToString() + " KB";
                    }
                }
                else
                {
                    fd.Size = size.ToString() + " 字节";
                }

                fd.LastModify = fi.LastWriteTime.ToString("yyyy-MM-dd");


                files.Add(fd);
            }

            JavaScriptSerializer seri = new JavaScriptSerializer();
            context.Response.Write(seri.Serialize(files));
        }
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

    public class FileDescription
    {
        private string _fileName;
        private string _size;
        private string _lastModify;

        public string FileName
        {
            get { return _fileName; }
            set { _fileName = value; }
        }
        public string Size
        {
            get { return _size; }
            set { _size = value; }
        }
        public string LastModify
        {
            get { return _lastModify; }
            set { _lastModify = value; }
        }
    }
}

