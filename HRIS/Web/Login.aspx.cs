using System;
using System.Web.UI;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        lbMessage.ForeColor = System.Drawing.Color.Black;
        if (!IsPostBack)
        {
           
        }

    }
    protected void btSignIn_Click(object sender, EventArgs e)
    {

        try
        {
            //Session.Clear();
            //Session.RemoveAll();
            //Session.Abandon();
            Session["UserName"] = null;
            Session["CompanyId"] = null;
            Session["CompanyName"] = null;
            Session["UserTypeId"] = null;
            Security objSecurity = new Security();
            objSecurity.GetUserAuthentication(txtUserName.Text, txtpass.Text, Convert.ToInt32(dxdDCompany.Value));
           if(DateTime.Now.Date >= DateTime.Now.Date)
            {
                if (objSecurity.IsSuccess)
                {

                    Session["UserName"] = objSecurity.UserName;
                    Session["CompanyId"] = objSecurity.CompanyId.ToString();
                    Session["CompanyName"] = objSecurity.CompanyName;
                    Session["UserTypeId"] = Convert.ToInt32(objSecurity.UserTypeId);

                    if (Request.QueryString["DoRedirect"] != null)
                    {
                        Response.Redirect(Request.QueryString["DoRedirect"] as string);
                    }
                    else
                    {
                        Response.Redirect("Home.html");
                    }
                }
                else
                {
                    lbMessage.Text = "Invalide login details";
                    lbMessage.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff0033");
                }
            }

            else
            {
                Response.Redirect("~/NoPermissions.aspx");
                return;
            }

        }
        catch { }
    }

}