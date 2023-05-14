using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Settings_UploadFormats : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        imgEmpMaster.Visible = false;
        imgEmpPayroll.Visible = false;
        //imgEmpTxn.Visible = false;
        if (Page.IsPostBack == false)
        {
            
            if (Request.QueryString["TempateName"] != null)
            {
                ViewState["TempateName"] = Request.QueryString["TempateName"];
            }
            if (Request.QueryString["PagePath"] != null)
            {
                ViewState["PagePath"] = Request.QueryString["PagePath"];
            }
            else
            {
                ViewState["PagePath"] = null;
            }

            if (ViewState["TempateName"] != null)
            {
                switch (ViewState["TempateName"].ToString())
                {
                    case "EmpMaster":
                        imgEmpMaster.Visible = true;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;
                    case "EmpAdditionalDetails":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = true;
                        ImgLeaveBulk.Visible = false;
                        break;
                    case "EmpPayroll":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = true;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;
                    case "EmpBankDetails":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = true;
                        imgEmpTime.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;
                    case "EmpTxn":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgTransaction.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;
                    case "DailyLog":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        Imgcoverin.Visible = true;
                        break;
                    case "LeaveEntitlement":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgLeaveEtitl.Visible = true;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;
                    case "EmployeeTransaction":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgTransaction.Visible = true;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;
                    case "EmployeeFixData":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = true;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;

                    case "TravellingData":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;

                    case "EmployeeIncrement":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgIncrement.Visible = true;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;

                    case "EmployeeMobileBillData":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = true;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;

                    case "EmpLoan":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = true;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;

                    case "AdvanceBulkEntry":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = true;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;

                    case "LeaveBulkEntry":
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        ImgTransaction.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = true;
                        break;

                    default:
                        imgEmpMaster.Visible = false;
                        imgEmpPayroll.Visible = false;
                        imgEmpBank.Visible = false;
                        imgEmpTime.Visible = false;
                        ImgTransaction.Visible = false;
                        ImgLeaveEtitl.Visible = false;
                        imgEmpPay.Visible = false;
                        ImgAdvances.Visible = false;
                        ImgMobBill.Visible = false;
                        ImageLoan.Visible = false;
                        ImgEmpAddData.Visible = false;
                        ImgLeaveBulk.Visible = false;
                        break;

                }

            }
        }
    }
    protected void RadButton1_Click(object sender, EventArgs e)
    {
        if (ViewState["PagePath"] != null)
        {
            Response.Redirect("~/" + ViewState["PagePath"].ToString());
        }
        else
        {
            Response.Redirect("~/SessionTimeout.aspx");
        }
    }
}