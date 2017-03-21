using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Web.SessionState;

namespace misSystem.HR.personnel
{
    /// <summary>
    /// showImage 的摘要描述
    /// </summary>
    public class showImage : IHttpHandler, IRequiresSessionState
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");


            if (context.Session["myFile"] != null)
            {
                HttpPostedFile myFile = (HttpPostedFile)context.Session["myFile"];
                int nFileLen = myFile.ContentLength;
                // Allocate a buffer for reading of the file

                byte[] myData = new byte[nFileLen];
                myFile.InputStream.Read(myData, 0, nFileLen);
                context.Response.Clear();
                context.Response.ContentType = "image/jpeg";
                context.Response.BinaryWrite(myData);
            }

            //string filename = context.Request.QueryString["filename"];
            //string extension = Path.GetExtension(filename).ToLowerInvariant();
            //string[] ss = extension.Split('.');
            //context.Response.ContentType = "image/" + ss[2]; //副檔名

            //var filepath = @"D:\FileViewDemo\" + filename;

            ////Ensure you have permissions else the below line will throw exception.
            //context.Response.WriteFile(filepath);

           

        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}