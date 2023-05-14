<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployementStatusDetails.aspx.cs" Inherits="HR_Reports_EmployementStatusDetails" MasterPageFile="~/HR/HR_MenuMasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v14.2.Web, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik1" %>--%>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.13.110, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
        <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />
        <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/report.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
        <div class="row">
            <div class="col head-container">
                <h4 class="header">Employment Status Reports </h4>

            </div>
        </div>
    </div>

<div class="row" style="background-color: ghostwhite">
        <div class="col-md-12" style="border-top-color: black">
            <div class="border" style="border-radius: 10px">
                <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                    <div class="row mt-3">
                        <div class="col-md-6">
                     <asp:Button ID="btnOrganizationSelect" runat="server" CssClass="Buttons" Text="Select Department"
                                                BackColor="WhiteSmoke" BorderColor="WindowFrame" ForeColor="Black" Font-Size="14px" />
                                            <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                                                CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="Panel1"
                                                TargetControlID="btnOrganizationSelect">
                                            </cc1:ModalPopupExtender>
                                            &nbsp;&nbsp;<asp:Label ID="lblOrganization" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hfOrganizationStructure" runat="server" />
                            <asp:HiddenField ID="hfPreviousOrgId" runat="server" />
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-2">
                            <asp:Label ID="FromDate" CssClass="form-label" runat="server" Text="From Date"></asp:Label>
                            <telerik:RadDatePicker ID="raddpFromDate" runat="server" Culture="en-US" Width="100%"
                                FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                Skin="Windows7">
                                <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                    runat="server" Skin="Windows7" CssClass="calender shadow">
                                </Calendar>
                                <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                    runat="server">
                                </DateInput>
                                <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="ToDate" CssClass="form-label" runat="server" Text="To Date"></asp:Label>
                            <telerik:RadDatePicker ID="raddpToDate" runat="server" Culture="en-US" Width="100%"
                                FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                Skin="Windows7">
                                <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                    runat="server" Skin="Windows7" CssClass="calender shadow">
                                </Calendar>
                                <DateInput ID="DateInput2" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                    runat="server">
                                </DateInput>
                                <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                            </telerik:RadDatePicker>
                        </div>
                        <div class="col-md-2">
                            <asp:Label ID="lblActiveStatus" CssClass="form-label" runat="server" Text="Status"></asp:Label>
                            <dx:ASPxComboBox ID="ddlActiveStatus" AutoPostBack="true" runat="server" DropDownStyle="DropDownList" CssClass="form-control form-control-lg"
                                ValueType="System.String" TextFormatString="{0}" Width="150px" SelectedIndex="0">
                                <Items>
                        <dx:ListEditItem Text="All Employee" Value="0" />
                        <dx:ListEditItem Text="Active Employee" Value="1" />
                        <dx:ListEditItem Text="Inactive Employee" Value="2" />
                        <dx:ListEditItem Text="Resigned Employee" Value="3" />
                        <dx:ListEditItem Text="Terminated Employee" Value="4" />
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Job Category"></asp:Label>
                            <dx:ASPxComboBox ID="radcbJobCategory" runat="server" DataSourceID="SqlJobCategory" TextField="JobCategoryName" ValueField="JobCategoryID" DropDownStyle="DropDownList" AutoPostBack="true"
                                ValueType="System.String" TextFormatString="{0}" BackColor="Transparent" Width="100%" OnTextChanged="radcbJobCategory_TextChanged" 
                                NullText="Job Category" Enabled="false" CssClass="form-control form-control-lg">
                            </dx:ASPxComboBox>
                <asp:SqlDataSource ID="SqlJobCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="Select JobCategoryID,JobCategoryName from JobCategory Where CompanyId=@CompanyId ">
                    <SelectParameters>
                    <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                </SelectParameters>
                </asp:SqlDataSource>
                        </div>
                        <div class="col-md-3">
                            <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Employee"></asp:Label>
                <dx:ASPxComboBox ID="ddlEmployee" runat="server" BackColor="Transparent" Width="250px" DataSourceID="sqldsEmployee" Enabled="false"
                    TextField="EmployeeReportViewName" ValueField="EmployeeID" Skin="Windows7" NullText="<< Employee >>">
                </dx:ASPxComboBox>
            <asp:SqlDataSource ID="sqldsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="COM_GetEmployeeList" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:ControlParameter ControlID="radcbJobCategory" Name="JobCategoryID" DefaultValue="0" Type="Int32" />
                    <asp:ControlParameter ControlID="hfOrganizationStructure" Name="OrgStructureID" PropertyName="Value" Type="Int32" />
                    <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                    <asp:ControlParameter ControlID="ddlActiveStatus" Name="ActiveStatus" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
                        </div>
                    </div>
                    <div class="row mt-3">
                        <div class="col-md-6">
                            <asp:Label ID="lblApprove" CssClass="form-label" runat="server" Text="Report Name"></asp:Label>
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" DropDownStyle="DropDownList" ValueType="System.String" CssClass="form-control form-control-lg"
                                TextFormatString="{0}" Width="100%" OnTextChanged="ASPxComboBox1_TextChanged" AutoPostBack="true">
                                <Items>
                        <dx:ListEditItem Text="Employment Status Details" Value="1" />
                        <dx:ListEditItem Text="Employee Retirement Details" Value="2" />
                                </Items>
                            </dx:ASPxComboBox>
                        </div>
                         <div class="col-md-6">
           
                <asp:Label ID="lblEmplymentStatus" runat="server" Text="Employment Status" CssClass="form-label" Font-Names="Tahoma" />
          
                <dx:ASPxComboBox ID="ddlEmplymentStatus" runat="server" DataSourceID="SqlEmplymentStatus" TextField="EmploymentTypeName" ValueField="EmploymentTypeID" DropDownStyle="DropDownList" SelectedIndex="1"
                    ValueType="System.String" TextFormatString="{0}" BackColor="Transparent" Width="180px" OnTextChanged="radcbJobCategory_TextChanged" NullText="<< Employment Type >>" Enabled="false">
                </dx:ASPxComboBox>
                <asp:SqlDataSource ID="SqlEmplymentStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="Select EmploymentTypeID,EmploymentTypeCode,EmploymentTypeName from EmploymentType"></asp:SqlDataSource>
           
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
                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View"
                                ValidationGroup="a" Skin="WebBlue" CssClass="btn btn-outline-warning check-aspbtn"/>
                        </div>
                        <div>
                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click"
                                Text="Reset" Skin="WebBlue" CssClass="btn btn-outline-danger check-aspbtn"/>
                        </div>
                        <div>
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click1" Text="Clear"
                                Skin="WebBlue" CssClass="btn btn-outline-secondary check-aspbtn"/>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-12">
                            <asp:ScriptManager ID="ScriptManager1" runat="server">
                            </asp:ScriptManager>
                        </div>
                    </div>



                    <div>
                        <div class="row mb-5">
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
                    <asp:Panel ID="Panel1" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical">
                                <div class="style1">
                                    <div style="float: right; margin-top: -10px;">
                                        <asp:ImageButton ID="btnClosePopup" runat="server"
                                            ImageUrl="~/App_Themes/NewTheme/img/close.png" />
                                    </div>
                                    <div id="org-stucture">
                                    </div>
                                </div>
                            </asp:Panel>
                
            </div>
        </div>
    </div>
    </div>
</asp:Content>
