<%@ Page Title="HRIS Common | Company Profile" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="CompanyProfile.aspx.cs" Inherits="CompanyProfile_CompanyProfile" StylesheetTheme="Common" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <style type="text/css">
        .btn-outline-successpop {
    color: white;
    /* border-color: #28a745; */
    background-color: #1a8ac0;
}
        .gridstyle {
            font-weight: 500;
        }

        .style {
            font-weight: bold;
            text-align: center;
        }

        .checkboxlistformat label {
            margin-left: 20px;
        }

        .form-control-lg {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 250px;
        }

        .mb-5, .my-5 {
            margin-bottom: 15rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: -1.5rem !important;
        }

        .mt-2, .my-2 {
            margin-top: -0.5rem !important;
        }

        .pl-2, .px-2 {
            padding-left: 14.5rem !important;
        }

        .mb-4, .my-4 {
            margin-bottom: 0.5rem !important;
        }

        .mt-4, .my-4 {
            margin-top: -1.5rem !important;
        }

        .pr-2, .px-2 {
            padding-right: 2.5rem !important;
        }

        .col-md-4 {
            -ms-flex: 0 0 33.333333%;
            flex: 0 0 33.333333%;
            max-width: 26.333333%;
        }

        .mt-2, .my-2 {
            margin-top: 0.5rem !important;
        }

        .style2 {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            display: block;
            font-weight: 400;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .style3 {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            display: block;
            font-weight: 400;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .ml-2, .mx-2 {
            margin-left: -2.5rem !important;
        }

        .pt-2, .py-2 {
            padding-top: 2.5rem !important;
        }

        .ml-3, .mx-3 {
            margin-left: -5rem !important;
        }

        .mt-4, .my-4 {
            margin-top: 1.5rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: -3.5rem !important;
        }

        hr {
            box-sizing: content-box;
            height: 4px;
            overflow: visible;
            background-color: #097cbd;
            border-radius: 10PX;
        }

        .form-control-lg {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
        }

        .form-control:disabled {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            background-color:#f9fafb!important;
        }
        .mr-lg-5, .mx-lg-5 {
    margin-right: 0rem!important;
}
        .style4 {
            margin-right: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>

    <script type="">
        //$(document).ready(function () {
        //    alert("1111");
        //});
        //$("#downloadExcel").click(function() {
        //    alert("Handler for .click() called.");
        //});
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "/downloadExcelFromGrid",
                //data: JSON.stringify({ startDate: date._d.format('dd/MM/yyyy') }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {

                }
            });
        }
    </script>

    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
       <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row rm-margin">
                <div class="col head-container">
                    <h4 class="header">Company Profile</h4>
                    <span onclick="toggleProfileForm(585);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtCompanyCode" CssClass="form-label"
                                        ErrorMessage="a" ValidationGroup="a" ToolTip="Required Field!" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtCompanyCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtCompanyName" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtCompanyName" runat="server" CssClass="form-control form-control-lg" MaxLength="50" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label14" CssClass="form-label" runat="server" Text="Bank Name"></asp:Label>
                                    <telerik:RadTextBox ID="txtBankName" CssClass="form-control form-control-lg-disabled" runat="server" MaxLength="50" Width="250px" Enabled="false"
                                        Skin="Windows7" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="EPF Registration No"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtRegistrationNo" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtRegistrationNo" runat="server" MaxLength="50" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-3 ">
                                    <asp:Label ID="Label10" CssClass="form-label" runat="server" Text="Tax Registration No"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="txtTaxRegNo" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtTaxRegNo" runat="server" MaxLength="50" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label15" runat="server" CssClass="form-label" Text="Bank Branch"></asp:Label>
                                    <telerik:RadTextBox ID="txtBankBranch" CssClass="form-control form-control-lg" runat="server" Enabled="false">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Contact No 1"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactNo1" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtContactNo1" runat="server" MaxLength="10" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Contact No 2"></asp:Label>
                                    <telerik:RadTextBox ID="txtContactNo2" runat="server" MaxLength="10" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-4">
                                    <asp:Label ID="Label16" runat="server" CssClass="form-label" Text="Company A/C No"></asp:Label>
                                    <telerik:RadTextBox ID="radtxtCompanyAccountNo" CssClass="form-control form-control-lg" runat="server" Enabled="false">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-md-2  mt-5">
                                    <asp:ImageButton ID="btnPRVICOM" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/left-arrow.png" OnClick="btnPRVICOM_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnNEXTCOM" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/right-arrow.png" OnClick="btnNEXTCOM_Click"></asp:ImageButton>
                                    <asp:ImageButton ID="btnADDCOM" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btnADDCOM_Click"></asp:ImageButton>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Fax"></asp:Label>
                                    <telerik:RadTextBox ID="txtFax" runat="server" MaxLength="10" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Web"></asp:Label>
                                    <telerik:RadTextBox ID="txtWeb" runat="server" MaxLength="50" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="cbCustom" runat="server" class="style4" Checked="false" AutoPostBack="true" OnCheckedChanged="cbCustomize_CheckedChanged" />
                                    <asp:Label ID="Label20" CssClass="form-label" runat="server" Text="Custom Employee Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtTaxRegNo" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtPrefix" runat="server" MaxLength="3" Skin="Windows7" Enabled="false" CssClass="form-control form-control-lg" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Email"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" CssClass="form-label"
                                        ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="Red">Invalid 
                                    Email</asp:RegularExpressionValidator>
                                    <telerik:RadTextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label17" CssClass="form-label" runat="server" Text="Report Display Employee Name"></asp:Label>
                                    <dx:ASPxComboBox ID="ddlReportViewName" runat="server" CssClass="form-control form-control-lg" BackColor="Transparent"
                                        Skin="Windows7" SelectedIndex="0" OnSelectedIndexChanged="ddlReportViewName_SelectedIndexChanged">
                                        <Items>
                                            <dx:ListEditItem Text="Employee Code | First Name" Value="1" />
                                            <dx:ListEditItem Text="Employee Code | Last Name" Value="2" />
                                            <dx:ListEditItem Text="Employee Code | Calling Name" Value="3" />
                                            <dx:ListEditItem Text="Employee Code | Initials with Last Name" Value="4" />
                                            <dx:ListEditItem Text="EPF No | First Name" Value="5" />
                                            <dx:ListEditItem Text="EPF No | Last Name" Value="6" />
                                            <dx:ListEditItem Text="EPF No | Calling Name" Value="7" />
                                            <dx:ListEditItem Text="EPF No | Initials with Last Name" Value="8" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text=" Address"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtCompanyAddress" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtCompanyAddress" runat="server" MaxLength="100" Height="60px"
                                        TextMode="MultiLine" CssClass="form-control form-control-lg" />
                                </div>
                                <div class="col-md-6">
                                    <asp:CheckBox ID="NameStatus" runat="server" class="style4" Checked="false" AutoPostBack="true" OnCheckedChanged="NameStatus_CheckedChanged" />
                                    <asp:Label ID="Label18" CssClass="form-label" runat="server" Text="HRIS Display Employee Name"></asp:Label>
                                    <telerik:RadTextBox ID="txtCusCode" runat="server" MaxLength="3" Skin="Windows7" Enabled="false" CssClass="form-control form-control-lg" />
                                </div>
                            </div>
                             <div class="row mt-3">
                                 </div>
                             <div class="row mt-3">
                                 </div> 
                             <div class="row mt-3">
                                 </div>
                            <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                <div>
                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                                        ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                        ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                        Text="Delete" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <asp:HiddenField ID="hfCompanyId" runat="server" />
            <div class="row">
                <div class="row">
                    <div class="col">
                    </div>
                </div>
                <div class="row">
                    <div class="col mt-3">
                    </div>
                </div>
                <div class="row">
                    <div class="col  mt-3">
                    </div>
                </div>
                <div class="row mt-5">
                    <div class="col d-flex justify-content-end pr-2" style="grid-gap: 6px">
                    </div>
                </div>
                <div class="row">
                    <table style="width: 100%;">
                        <tr>
                            <td></td>
                            <td runat="server" visible="false">
                                <asp:Label ID="Label11" runat="server" CssClass="form-label" Text="Bank Name 2"></asp:Label>
                            </td>
                            <td runat="server" visible="false">
                                <telerik:RadComboBox ID="radcboBankName2" runat="server" AutoPostBack="True" Width="250px"
                                    DataTextField="BankName" DataValueField="BankId"
                                    Skin="Windows7" OnDataBound="radcboBankName2_DataBound">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" visible="false">
                                <asp:Label ID="Label12" runat="server" CssClass="form-label" Text="Bank Branch 2"></asp:Label>
                            </td>
                            <td runat="server" visible="false">
                                <asp:ObjectDataSource ID="objDataBankBranchInfo2" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetBankBranchDetails" TypeName="HRM.Payroll.BLL.BankAndBranch">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="radcboBankName2" Name="bankID" PropertyName="SelectedValue"
                                            Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>

                                <telerik:RadComboBox ID="radcboBankBranch2" runat="server" AutoPostBack="True" Width="250px"
                                    Skin="Windows7" DataTextField="Branch" DataValueField="BranchId"
                                    OnDataBound="radcboBankBranch2_DataBound">
                                </telerik:RadComboBox>
                            </td>
                        </tr>
                        <tr>
                            <td runat="server" visible="false">
                                <asp:Label ID="Label13" runat="server" CssClass="form-label" Text="Company A/C No 2"></asp:Label>
                            </td>
                            <td runat="server" visible="false">
                                <telerik:RadTextBox ID="radtxtCompanyAccountNo2" Width="250px" runat="server" Skin="Windows7">
                                </telerik:RadTextBox>
                            </td>
                        </tr>

                        <tr>
                            <td align="right">
                                <asp:UpdateProgress ID="UpdateProgress" runat="server">
                                    <ProgressTemplate>
                                        <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px"
                                            id="UpdateProgress" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                            <td>
                                <div id="toastrContainer">
                                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                                    <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                                </div>
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>

        <div class="row" style="position: relative; top: -1px; left: 4px;">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click2">
                <ContentTemplate>
                    <img alt="" src="../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>

            <div class="col table-scroll">
                <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsCompanyProfile" ForeColor="Black">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="CompanyID" Caption="PK" PropertiesEditType="TextBox" Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CompanyName" HeaderStyle-Font-Bold="false" Caption="Company Name" PropertiesEditType="TextBox" Width="300px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CompanyAddress" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Caption="Address" PropertiesEditType="TextBox" Width="420px" VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="CompanyContactNo1" HeaderStyle-Font-Bold="false" Caption="Contact No 1" PropertiesEditType="TextBox" Width="120px" VisibleIndex="3" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CompanyContactNo2" HeaderStyle-Font-Bold="false" Caption="Contact No 2" PropertiesEditType="TextBox" Width="120px" VisibleIndex="4" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"  Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyFax" HeaderStyle-Font-Bold="false" Caption="Fax" PropertiesEditType="TextBox" Width="100px" VisibleIndex="5" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CompanyEmail" HeaderStyle-Font-Bold="false" Caption="Email" PropertiesEditType="TextBox" Width="230px" VisibleIndex="6" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CompanyWeb" HeaderStyle-Font-Bold="false" Caption="Web" PropertiesEditType="TextBox" Width="100px" VisibleIndex="7" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyRegistrationNo" HeaderStyle-Font-Bold="false" Caption="Registration No" PropertiesEditType="TextBox"  VisibleIndex="8" Width="150px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="true" />
                        <dx:GridViewDataColumn FieldName="CompanyTaxRegistrationNo" HeaderStyle-Font-Bold="false" Caption="Tax Registration" PropertiesEditType="TextBox" Width="120px" VisibleIndex="9" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="BankId" HeaderStyle-Font-Bold="false" Caption="Bank Details 1" PropertiesEditType="TextBox" Width="100px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="BranchId" HeaderStyle-Font-Bold="false" Caption="Bank Details 2" PropertiesEditType="TextBox" Width="100px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyAccountNo" HeaderStyle-Font-Bold="false" Caption="Bank Details 2" PropertiesEditType="TextBox" Width="100px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="EmployeeNameStatus" HeaderStyle-Font-Bold="false" Caption="EmployeeNameStatus" PropertiesEditType="TextBox" Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CustomEmployeeCode" HeaderStyle-Font-Bold="false" Caption="CustomEmployeeCode" PropertiesEditType="Text" Width="100px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="Prefix" Caption="Prefix" HeaderStyle-Font-Bold="false" PropertiesEditType="Text" Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="EmployeeReportViewName" HeaderStyle-Font-Bold="false" Caption="EmployeeReportViewName" PropertiesEditType="Text" Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Width="35px"  CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" onclientclicked="showForm" OnClick="lkSelect_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                </asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="230" UseFixedTableLayout="true" />
                    <SettingsPager Mode="ShowPager" PageSize="10" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" FileName="Company_Profile" GridViewID="radgvDetails">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>

        <asp:ObjectDataSource ID="objdsCompanyProfile" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetCompanyProfile" TypeName="HRM.Common.BLL.CompanyProfile">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="companyId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
       <dx:ASPxPopupControl ID="ASPxPopupControl1" runat="server" CssClass="popup-container " PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
           <HeaderTemplate>
               <div style="display: flex; justify-content: space-between">

                   <h6 style="font-family: 'Source Sans Pro',sans-serif; font-size: 17px; font-weight: bold; color: white;">Company Bank Details</h6>
                   <asp:ImageButton runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" CssClass="btn-close-alignment" OnClick="Unnamed_Click"/>
               </div>

<%--               <div style="text-align: right;margin-top: -27px;">
                   <asp:ImageButton ID="btnClosePopup" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
               </div>--%>
           </HeaderTemplate>
           
          <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                    <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border mt-2" style="border-radius: 10px">
                                <div class="card-body shadow">
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label ID="lblbank" CssClass="form-label" runat="server">Bank</asp:Label>
                                            <dx:ASPxComboBox ID="dxPopCmbBank" CssClass="form-control form-control-lg" DataSourceID="objDataBankInfo" 
                                                required="" Width="100%" runat="server" DropDownStyle="DropDownList"
                                                ValueType="System.String" ValueField="BankId" TextFormatString="{0}" NullText="Please Select Bank Name" MaxLength="50" AutoPostBack="true">
                                                <Columns>
                                                    <dx:ListBoxColumn FieldName="BankName" Caption="Deduction Type" Width="100px" />
                                                </Columns>
                                                <InvalidStyle BackColor="LightPink" />
                                            </dx:ASPxComboBox>
                                            <asp:ObjectDataSource ID="objDataBankInfo" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetBankDetails" TypeName="HRM.Payroll.BLL.BankAndBranch"></asp:ObjectDataSource>
                                        </div>
                                        <div class="col-md-6">
                                                         <asp:Label ID="lblbranch" CssClass="form-label" runat="server">Branch</asp:Label>
                                            <dx:ASPxComboBox ID="dxPopCmbBranch" CssClass="form-control form-control-lg" DataSourceID="ObjectDataSource1" 
                                                required="" Width="100%" runat="server" DropDownStyle="DropDownList"
                                                ValueType="System.String" ValueField="BranchId" TextFormatString="{0}" NullText="Please Select Branch Name" MaxLength="50">
                                                <Columns>
                                                    <dx:ListBoxColumn FieldName="Branch" Caption="Select Branch" Width="100px" />
                                                </Columns>
                                                <InvalidStyle BackColor="LightPink" />
                                            </dx:ASPxComboBox>
                                            <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetBankBranchDetails" TypeName="HRM.Payroll.BLL.BankAndBranch">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="dxPopCmbBank" Name="bankID" PropertyName="Value"
                                                        Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            </div>
                                    </div>
                                 
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label ID="lblAccno" CssClass="form-label" runat="server">Account No</asp:Label>
                                            <dx:ASPxTextBox ID="dxPopTbAcNo"  runat="server" Height="20px" Width="100%" MaxLength="50" ClientInstanceName="txtWCNSerialFrom"
                                                CssClass="form-control form-control-lg">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right">
                                                    <RequiredField IsRequired="true" ErrorText="Amount is required" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="LightPink" />
                                            </dx:ASPxTextBox>
                                        </div>
                                         <div class="col-md-6">
                                                 <asp:Label ID="lbldes" CssClass="form-label" runat="server">Description</asp:Label>
                                            <dx:ASPxTextBox ID="dxTbDiscription" runat="server" Height="20px" Width="100%" MaxLength="50"
                                                lientInstanceName="txtWCNSerialFrom" CssClass="form-control form-control-lg">
                                                <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right">
                                                    <RequiredField IsRequired="true" ErrorText="Amount is required" />
                                                </ValidationSettings>
                                                <InvalidStyle BackColor="LightPink" />
                                            </dx:ASPxTextBox>
                                             </div>
                                    </div>
                                  
                                    <div class="row">
                                        <td>
                                            <asp:Label ID="lblResultPop" runat="server" ForeColor="Red"></asp:Label>
                                        </td>
                                        <div class="col-12 mt-5 mb-2">
                                            <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 3px">
                                                <div>
                                                    <asp:Button ID="btnAdd" runat="server" CssClass="btn btn-outline-successpop check-aspbtn" Text="Add" OnClick="btnAdd_Click" />
                                                </div>
                                                <div>
                                                    <telerik:RadButton ID="btnPopupAdd" runat="server" Text="Save" OnClick="btnPopupAdd_Click" />
                                                </div>
                                                <div>
                                                    <telerik:RadButton ID="btnPopupCancel" runat="server" Text="Cancel" OnClick="btnPopupCancel_Click" />
                                                </div>
                                            </div>
                                        </div>
                                    </div> 
                                    <asp:HiddenField ID="hfId" runat="server" />

                   
                            <dx:ASPxGridView ID="gvPopup" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStylepop"
                                ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue">
                               
                                <Columns>
                                    <dx:GridViewDataTextColumn FieldName="Id" Caption="Employee Code" HeaderStyle-Font-Bold="false" VisibleIndex="1" Width="100px" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="BankId" Caption="Employee Code" HeaderStyle-Font-Bold="false" Width="100px" VisibleIndex="1" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="BranchId" Caption="Employee Code" HeaderStyle-Font-Bold="false" Width="100px" VisibleIndex="1" Visible="false">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="BankName" Caption="Bank" Width="150px" HeaderStyle-Font-Bold="false" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Branch" Caption="Branch" Width="150px" HeaderStyle-Font-Bold="false" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="AccountNumber" Width="150px" Caption="Account No." HeaderStyle-Font-Bold="false" VisibleIndex="3" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn FieldName="Description" Width="250px" Caption="Description" HeaderStyle-Font-Bold="false" VisibleIndex="4" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left">
                                    </dx:GridViewDataTextColumn>
                                    <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" HeaderStyle-Font-Bold="false" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center">
                                        <DataItemTemplate>
                                            <asp:LinkButton ID="lkSelectPopup" runat="server" onclientclicked="showForm" OnClick="lkSelectPopup_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                </asp:LinkButton>
                                        </DataItemTemplate>
                                        <HeaderStyle HorizontalAlign="Center" />
                                        <CellStyle HorizontalAlign="Center" />
                                    </dx:GridViewDataTextColumn>
                                </Columns>
                                <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                    <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                    <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                </Styles>
                                <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                <SettingsPager Mode="ShowPager" PageSize="5"/>
                            </dx:ASPxGridView>
                      
                    
                                </div>
                            </div>
                        </div>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
</asp:Content>

