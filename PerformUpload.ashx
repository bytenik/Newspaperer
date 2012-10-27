<%@ WebHandler Language="C#" Class="PerformUpload" %>

using System;
using System.IO;
using System.Web;

public class PerformUpload : IHttpHandler
{
    #region IHttpHandler Members

    public void ProcessRequest(HttpContext context)
    {
        context.Response.ContentType = "text/plain";
        HttpRequest Request = context.Request;
        HttpResponse Response = context.Response;
        HttpFileCollection myFileCollection = Request.Files;
        if (myFileCollection.Count > 0)
        {
            HttpPostedFile myFile = myFileCollection[0];

            var input = new byte[myFile.ContentLength];

            // Initialize the stream.
            Stream MyStream = myFile.InputStream;
            string path = context.Server.MapPath("Binary");
            string fileName = Path.GetFileName(myFile.FileName);
            //cli.Uploading += new FTPFileTransferEventHandler(cli_Uploading);
            try
            {
                var bufferSize = (int) myFile.InputStream.Length;
                var buffer = new byte[bufferSize];

                //  write the byte to disk
                using (var fs = new FileStream(Path.Combine(path, fileName), FileMode.Create))
                {
                    //  fill the buffer from the input stream
                    int bytes = MyStream.Read(buffer, 0, bufferSize);
                    //  write the bytes to the file stream
                    fs.Write(buffer, 0, bytes);
                }

                context.Response.Write("{success:true}");
            }
            catch (Exception ex)
            {
                context.Response.Write("{success:false,message:'" + ex.Message + "'}");
            }
        }
    }

    public bool IsReusable
    {
        get { return false; }
    }

    #endregion
}