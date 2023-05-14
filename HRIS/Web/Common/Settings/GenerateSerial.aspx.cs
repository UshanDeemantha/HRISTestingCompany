using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
    
    

public partial class Common_Settings_GenerateSerial : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["AuthCode"] == null)
        {
            Response.Redirect("~/Login.aspx");
        }
    }
    protected void radbtnGenerateSerial_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.QueryString["AuthCode"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            
            else
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Invalid Credentials Specified!";
            }
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }

    protected void radbtnCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    protected void radbtnRegister_Click(object sender, EventArgs e)
    {
        if (radtxtSerial.Text.Trim() != string.Empty)
        {
            Response.Redirect("~/Common/Settings/EnterRegisrationInformation.aspx?RegCode=" + radtxtSerial.Text);
        }
    }
}