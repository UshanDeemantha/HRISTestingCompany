<%@ Application Language="C#" %>

<script runat="server">

    void Application_Start(object sender, EventArgs e) 
    {
        Application.Add("UserCount", 0);

        List<SessionData> currentSession = new List<SessionData>();
        currentSession.Add(new SessionData { PayPeriodId = 0, IsPosted = false, IsArchive = false, IsPayHistory = false });
        Application.Add("Payroll", currentSession);
    }
    
    void Application_End(object sender, EventArgs e) 
    {
        //  Code that runs on application shutdown
        Session.Abandon();
    } 
        
    void Application_Error(object sender, EventArgs e) 
    {
        try
        {
            HttpContext ctx = HttpContext.Current;
                Exception exception = ctx.Server.GetLastError();

            try
            {
                Session["CURRENTURL"] = System.Web.HttpContext.Current.Request.Url.AbsolutePath;
            }
            catch
            {
                Session["CURRENTURL"] = string.Empty;
            }
            
            Session["APPERROROBJ"] = exception;
            // --------------------------------------------------
            // To let the page finish running we clear the error
            // --------------------------------------------------
            //ctx.Server.ClearError();
        }
        catch {}
    }

    void Session_Start(object sender, EventArgs e) 
    {
        int userCount = int.Parse(Application.Get("UserCount").ToString());
        userCount++;
        Application.Set("UserCount", userCount);

        Session["CURRENTURL"] = string.Empty;
        Session["APPERROROBJ"] = null;

        List<SessionData> currentSession = new List<SessionData>();
        currentSession.Add(new SessionData { PayPeriodId = 0, IsPosted = false, IsArchive = false, IsPayHistory = false });
        Session.Add("Payroll", currentSession);

        
    
    
    }

    void Session_End(object sender, EventArgs e) 
    {
        // Code that runs when a session ends. 
        // Note: The Session_End event is raised only when the sessionstate mode
        // is set to InProc in the Web.config file. If session mode is set to StateServer 
        // or SQLServer, the event is not raised.
        
        int userCount = int.Parse(Application.Get("UserCount").ToString());
        userCount--;
        Application.Set("UserCount", userCount);

        List<SessionData> currentSession = new List<SessionData>();
    }
       
</script>
