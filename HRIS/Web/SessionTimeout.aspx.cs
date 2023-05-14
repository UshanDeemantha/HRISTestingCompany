using System;
using System.Web.UI;
using HRM.Common.BLL;
using Telerik.Web.UI;

public partial class SessionTimeout : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
    protected void btSignIn_Click(object sender, EventArgs e)
    {

        try
        {
            Security objSecurity = new Security();
            objSecurity.GetUserAuthentication(txtUserName.Text, txtpass.Text, Convert.ToInt32(dxdDCompany.Value));

            if (objSecurity.IsSuccess)
            {

                /* if ((new SaveSerialInformation()).isValidRegisration() == false)
                 {
                      Response.Redirect("~/Common/Settings/EnterRegisrationInformation.aspx");
                  }
                  if ((new SaveSerialInformation()).isValidRegisration() == false)
                  {
                      Response.Redirect("~/Common/Settings/EnterRegisrationInformation.aspx");
                  }
                 * */


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
                lbError.Text = "Invalide Login Details";
                lbError.ForeColor = System.Drawing.ColorTranslator.FromHtml("#ff0033");
            }
        }
        catch { }
    }

}