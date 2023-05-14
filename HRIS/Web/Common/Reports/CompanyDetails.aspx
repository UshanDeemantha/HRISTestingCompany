<%@ Page Title="HRIS Common | Company Details" Language="C#" AutoEventWireup="true" CodeFile="CompanyDetails.aspx.cs" Inherits="Common_Reports_CompanyDetails" MasterPageFile="~/Common/Common_MenuMasterPage.master" %>


<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v14.2.Web, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.13.110, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100px;
        }

        .style3 {
            width: 173px;
        }

        .style4 {
            width: 74px;
        }

        .style5 {
            width: 66px;
        }

        .style6 {
            width: 66px;
            height: 26px;
        }

        .style7 {
            height: 26px;
        }

        .auto-style1 {
            width: 119px;
        }

        .rounded-pill {
            border-radius: 0rem !important;
        }
        .mt-2, .my-2 {
    margin-top: 1.5rem!important;
}
    </style>
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/report.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="/App_Themes/NewTheme/script/button-style.js"></script>


    <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
        <div class="row">
            <div class="col head-container">
                <h4 class="header">Company Details </h4>

            </div>
        </div>
    </div>

    <div class="row" style="background-color: ghostwhite">
        <div class="col-md-12" style="border-top-color: black">
            <div class="border" style="border-radius: 10px">
                <div class="card-body shadow" >
                    <div class="row ">
                        <div class="col-md-6 ">
                            <asp:Label ID="lblApprove" CssClass="form-label" runat="server" Text="Report Name"></asp:Label>
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" DropDownStyle="DropDownList" ValueType="System.String" CssClass="form-control form-control-lg"
                                TextFormatString="{0}" Width="100%" OnTextChanged="ASPxComboBox1_TextChanged" AutoPostBack="true"> 
                                <Items>
                                    <dx:ListEditItem Text="Company Profile" Value="1" />
                                    <dx:ListEditItem Text="Company Branches Details" Value="2" />
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-6">
                            <div class="d-flex justify-content-end mr-lg-5 mt-2" style="grid-gap: 15px">
                                <div>
                                    <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View" CssClass="btn btn-outline-success check-aspbtn"
                                        ValidationGroup="a" />
                                </div>
                                <div>
                                    <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click" Text="Reset" CssClass="btn btn-outline-danger check-aspbtn"
                                        ValidationGroup="a" />
                                </div>
                                <div>
                                    <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click1" Text="Clear" CssClass="btn btn-outline-secondary check-aspbtn" />
                                </div>
                            </div>
                        </div>
                    </div>


                    <div class="row">
                        <div class="col-12">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </div>
                    </div>



                    <div>
                        <div class="row mt-3">
                            <div class="col-12">
                                <dx:ASPxDocumentViewer ID="ReportViewer1" Width="100%" CssClass="shadow viewer-container" runat="server" OnLoad="ReportViewer1_Load1">
                                    <SettingsParametersPanelCaption HorizontalAlign="Left" Position="Left" />
                                    <StylesViewer>
                                        <Paddings Padding="10px"></Paddings>
                                    </StylesViewer>

                                    <StylesReportViewer>
                                        <Paddings Padding="10px"></Paddings>
                                    </StylesReportViewer>
                                    <StylesParametersPanelParameterEditors>
                                        <CaptionCell Width="74px">
                                        </CaptionCell>
                                    </StylesParametersPanelParameterEditors>
                                </dx:ASPxDocumentViewer>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>

