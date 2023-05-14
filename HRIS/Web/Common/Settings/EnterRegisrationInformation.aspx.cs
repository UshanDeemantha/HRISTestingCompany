using System;
using System.Drawing;

public partial class Common_Settings_EnterRegisrationInformation : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["RegCode"] != null)
        {
            string[] serialNumber = Request.QueryString["RegCode"].ToString().Split(new string[] { "-" }, StringSplitOptions.None);
            if (serialNumber.Length == 7)
            {
                radtxtSerialP1.Text = serialNumber[0];
                radtxtSerialP2.Text = serialNumber[1];
                radtxtSerialP3.Text = serialNumber[2];
                radtxtSerialP4.Text = serialNumber[3];
                radtxtSerialP5.Text = serialNumber[4];
                radtxtSerialP6.Text = serialNumber[5];
                radtxtSerialP7.Text = serialNumber[6];
            }
            else
            {
                radtxtSerialP1.Text = string.Empty;
                radtxtSerialP2.Text = string.Empty;
                radtxtSerialP3.Text = string.Empty;
                radtxtSerialP4.Text = string.Empty;
                radtxtSerialP5.Text = string.Empty;
                radtxtSerialP6.Text = string.Empty;
                radtxtSerialP7.Text = string.Empty;
            }
        }
    }

    protected void radtxtValidateAndSave_Click(object sender, EventArgs e)
    {
        try
        {
            lblMessage.Text = string.Empty;

            if (radtxtSerialP1.Text.Trim().Length != 5 || 
                radtxtSerialP2.Text.Trim().Length != 5 || 
                radtxtSerialP3.Text.Trim().Length != 5 || 
                radtxtSerialP4.Text.Trim().Length != 5 || 
                radtxtSerialP5.Text.Trim().Length != 5 || 
                radtxtSerialP6.Text.Trim().Length != 5 || 
                radtxtSerialP7.Text.Trim().Length != 5)
            {
                lblMessage.ForeColor = Color.Red;
                lblMessage.Text = "Serial number cannot contain any white spaces!";

                return;
            }

           
            
        }
        catch (Exception ex)
        {
            lblMessage.ForeColor = Color.Red;
            lblMessage.Text = ex.Message;
        }
    }

    protected void radtxtCancel_Click(object sender, EventArgs e)
    {
        Response.Redirect("~/Login.aspx");
    }

    protected void radtxtSerialP1_TextChanged(object sender, EventArgs e)
    {
        if (radtxtSerialP1.Text.Trim().Length == 5)
        {
            radtxtSerialP2.Text = "asda";
        }
    }
}