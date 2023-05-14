<%@ WebHandler Language="C#" Class="GetSessionId" %>

using System;
using System.Web;
using System.Web.SessionState;
public class GetSessionId : IHttpHandler, IRequiresSessionState {
    
    public void ProcessRequest (HttpContext context) {
      
        context.Response.ContentType = "text/plain";

        int companyId = 0;

        if (context.Session["CompanyId"] != null)
            companyId = Convert.ToInt16(context.Session["CompanyId"].ToString());
        context.Response.Write(companyId);
    }
 
    public bool IsReusable {
        get {
            return false;
        }
    }

}