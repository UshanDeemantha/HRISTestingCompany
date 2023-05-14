<%@ Page Title="" Language="C#" MasterPageFile="~/EmptyTemplateMasterPage.master" AutoEventWireup="true" CodeFile="UploadFormats.aspx.cs" Inherits="Common_Settings_UploadFormats" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <style type="text/css">
        .infoBox {
            font-size: 13px;
            font-style: italic;
            border-radius: 15px;
            -moz-border-radius: 15px;
            margin: 20px 0;
            border-width: 2px;
            border-color: Green;
            border-style: solid;
            width: 100%;
        }

        .infoInnerMargin {
            margin-top: 10px;
            margin-bottom: 10px;
            margin-right: 20px;
        }

        .hierarchyAlignment {
            margin-left: 75px;
        }

        .style1 {
            width: 100%;
        }

        h4.header {
            font-size: 20px;
            color: #0c65a5;
            font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
        }
        .h4\.header {
    font-size: 15px;
    font-size: 16px;
    /* color: #f7971f; */
    font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
}

        table {
    font-size: 15px;
    font-size: 16px;
    /* color: #f7971f; */
    font-family: -apple-system,BlinkMacSystemFont,"Segoe UI",Roboto,"Helvetica Neue",Arial,"Noto Sans",sans-serif,"Apple Color Emoji","Segoe UI Emoji","Segoe UI Symbol","Noto Color Emoji";
    margin-left: 100px;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row rm-margin">
                <div class="col head-container">
                    <h4 class="header" style="font-size: 16px;">HRIS Related Excel Templates</h4>
                    <span onclick="toggleProfileForm(585);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <table class="style1">
                <tr>
                    <td>
                        <div style="display: grid; grid-gap: 10px; grid-template-columns: 1fr 1fr; place-content: center; margin-top: 35px;">
                            <div style="font-size: 16px;" class="h4.header">
                                <ul >
                                    <li>Uploading Excel file must be in <b style="color: #0c65a5">Microsoft Office 97-2003 Workbook or Excel Workbook</b> format.</li>
                                    <li>Only one excel file can be uploaded at once; no limit of records in file.</li>
                                    <li>All templates come with sample data. So, please make sure you remove them before using the template.</li>
                                    <li>The relevant template is highlighted in green.</li>
                                </ul>
                            </div>
                            </div>
                    </td>
                </tr>
            </table>
            <br />
             <table >
        <tr>
            <td>
                Employee Master Data :</td>
            <td>
                <asp:HyperLink ID="HyperLink1" runat="server" 
                    NavigateUrl="~/Common/FileUploadFormats/EmployeeMaster.xls">Employee Master</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="imgEmpMaster" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                Employee Additional Details :</td>
            <td>
                <asp:HyperLink ID="HyperLink11" runat="server" 
                    NavigateUrl="~/Common/FileUploadFormats/EmployeeAdditionalDetails.xls">Employee Additional Details</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImgEmpAddData" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                Employee Payroll Data :</td>
            <td>
                <asp:HyperLink ID="HyperLink2" runat="server" 
                    NavigateUrl="~/Common/FileUploadFormats/EmployeePayroll.xls">Employee Payroll</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="imgEmpPayroll" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
            </td>
        </tr>
      <tr>
            <td>
                Employee Bank Data :</td>
            <td>
                <asp:HyperLink ID="HyperLink12" runat="server" 
                    NavigateUrl="~/Common/FileUploadFormats/BankDetails.xls">Employee Bank Details</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="imgEmpBank" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
            </td>
        </tr>
           <tr>
            <td>
                Employee Fix Data :</td>
            <td>
                <asp:HyperLink ID="HyperLink7" runat="server" 
                    NavigateUrl="~/Common/FileUploadFormats/EmployeeFixData.xls">Employee Pay</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="imgEmpPay" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                Employee Mobile Bill Data :</td>
            <td>
                <asp:HyperLink ID="HyperLink9" runat="server" 
                    NavigateUrl="~/Common/FileUploadFormats/EmployeeMobileBillData.xls">Employee Mobile Bill</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImgMobBill" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                Employee Time  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Common/FileUploadFormats/DailyLog.xls">Employee Time</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="imgEmpTime" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
           <tr>
            <td>
                Leave Entitlement  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Common/FileUploadFormats/LeaveEntitlement.xls">Leave Entitlement</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImgLeaveEtitl" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                Leave Bulk Entry  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink13" runat="server" NavigateUrl="~/Common/FileUploadFormats/LeaveBulkDetails.xls">Leave Bulk Entry</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImgLeaveBulk" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
           <tr>
            <td>
               Employee Variable Data  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Common/FileUploadFormats/VariableData.xls">Employee Transaction</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImgTransaction" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
         <tr>
            <td>
               Employee Advances  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Common/FileUploadFormats/EmployeeAdvances.xls">Employee Advances</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImgAdvances" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
               Employee Increment  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Common/FileUploadFormats/EmployeeIncrement.xls">Employee Increment</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImgIncrement" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
               Employee Loans  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Common/FileUploadFormats/EmployeeLoan.xls">Employee Loans</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="ImageLoan" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
                <tr>
            <td>
               Daily Log  :
            </td>
            <td>
                <asp:HyperLink ID="HyperLink14" runat="server" NavigateUrl="~/Common/FileUploadFormats/DailyLog.xls">Daily Log</asp:HyperLink>
            </td>
            <td>
                <asp:Image ID="Imgcoverin" runat="server" 
                    ImageUrl="~/App_Themes/Common/UpladSelect.png"
                    Visible="False" />
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadButton ID="RadButton1" runat="server" onclick="RadButton1_Click" 
                    Skin="WebBlue" Text="Back">
                </telerik:RadButton>
            </td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
    </table>



        </div>
    </div>
</asp:Content>

