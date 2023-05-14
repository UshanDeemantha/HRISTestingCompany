<%@ WebHandler Language="C#" Class="Handler" %>

using System;
using System.Web;
using System.IO;
public class Handler : IHttpHandler {
    
  public void ProcessRequest(HttpContext context)
    {
        foreach (string key in context.Request.Files)
        {
            HttpPostedFile postedFile = context.Request.Files[key];
            string folderPath = context.Server.MapPath("~/Uploads/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }
            postedFile.SaveAs(folderPath + postedFile.FileName);
        }
        context.Response.StatusCode = 200;
        context.Response.ContentType = "text/plain";
        context.Response.Write("Success");
    }
 
    public bool IsReusable
    {
        get
        {
            return false;
        }
    }
}