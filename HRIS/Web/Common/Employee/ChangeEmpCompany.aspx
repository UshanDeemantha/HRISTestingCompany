<%@ Page Title="HRIS Employee | Employee's Company Change" Language="C#" AutoEventWireup="true" CodeFile="ChangeEmpCompany.aspx.cs" Inherits="Common_Employee_ChangeEmpCompany" MasterPageFile="~/HR/HR_MenuMasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/Common/ChangeCompany/OrganizationStucture.ascx" TagName="OrganizationStucture" TagPrefix="uc1" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        thead {
            background-color: #8cbcee54;
            font-family: "Hiragino Maru Gothic ProN";
            font-size: 12px;
            letter-spacing: 0.52px;
            line-height: 21px;
            background-color: white;
        }
        .check-aspbtn1 {
    height: 31.25px;
    padding-top: 4px !important;
    font-size: 14px !important;
    border-radius: 3px !important;
    width: 135px !important;
}
    </style>
<link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />
<link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
<script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    

    <div id="FormContentArea">
       <div class="form-container shadow" id="formContainer" runat="server" style="height: 60px;">
                <div class="row">
                    <div class="col head-container">
                        <h4 class="header">Employee's Company Change</h4>
                    </div>
                </div>
            </div>
        
        <div class="row">
            <div class="col-md-12" style="border-top-color: black">
                <div class="border" style="border-radius: 10px">
                    <div class="card-body shadow" style="border-radius: 10px">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="New Company"></asp:Label>
                                <dx:ASPxComboBox ID="radcbCompany" runat="server" CssClass="form-control form-control-lg" Width="100%" DataSourceID="sqldsCompany" OnSelectedIndexChanged="radcbCompany_SelectedIndexChanged" AutoPostBack="true"
                                    TextField="CompanyName" ValueField="CompanyID" Skin="Windows7" NullText=" << Select New Company >>">
                                </dx:ASPxComboBox>

                                <asp:SqlDataSource ID="sqldsCompany" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT CompanyID,CompanyName FROM CompanyProfile Where CompanyID != @CompanyId">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
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
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label24" runat="server" Text="Designation" CssClass="form-label"></asp:Label>
                            <dx:ASPxComboBox ID="cmbDesignation" runat="server" DataSourceID="GetDesignationforCombo" OnSelectedIndexChanged="cmbDesignation_SelectedIndexChanged"  TextField="DesignationName"
                                        ValueField="DesignationID" Width="100%" CssClass="form-control form-control-lg" >
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="GetDesignationforCombo" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                        SelectCommand="Emp_GetDesignationForCombo" SelectCommandType="StoredProcedure">
                                    </asp:SqlDataSource>
                                <asp:HiddenField ID="hfDesignationId" runat="server" />
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Employee"></asp:Label>
                                <dx:ASPxComboBox ID="ddlEmployee" runat="server" Width="100%" DataSourceID="sqldsEmployee" CssClass="form-control form-control-lg"
                                    TextField="FullName" ValueField="EmployeeID" Skin="Windows7" NullText=" << Select Employee >>" OnSelectedIndexChanged="ddlEmployee_SelectedIndexChanged">
                                </dx:ASPxComboBox>



                                <asp:SqlDataSource ID="sqldsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="
                                    SELECT        [EmployeeID], [EmployeeCode], CASE
		                            WHEN CompanyProfile.EmployeeReportViewName=1 THEN Employee.EmployeeCode+' | '+Employee.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=2 THEN Employee.EmployeeCode+' | '+Employee.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=3 THEN Employee.EmployeeCode+' | '+Employee.CallName
		                            WHEN CompanyProfile.EmployeeReportViewName=4 THEN Employee.EmployeeCode+' | '+Employee.NameWithInitial+' '+Employee.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=5 THEN Employee.EPFNo+' | '+Employee.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=6 THEN Employee.EPFNo+' | '+Employee.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=7 THEN Employee.EPFNo+' | '+Employee.CallName
		                            ELSE Employee.EPFNo+' | '+Employee.NameWithInitial+' '+Employee.LastName
		                            END AS FullName,  [OrgStructureID]
                                    FROM            dbo.Employee INNER JOIN
		                            CompanyProfile ON CompanyProfile.CompanyID=Employee.CompanyID     
                                   WHERE   Employee.CompanyId=@CompanyId AND Employee.InactiveStatus=1">

                                    <SelectParameters>
                                        <%--<asp:ControlParameter ControlID="hfOrganizationStructure" Name="OrgStructureID" PropertyName="Value" Type="Int32" />--%>
                                        <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Job Category"></asp:Label>

                                <dx:ASPxComboBox ID="dxcmbJobCategory" runat="server" Width="100%" DataSourceID="sqldsJobCategory" CssClass="form-control form-control-lg"
                                    TextField="JobCategoryName" ValueField="JobCategoryID" Skin="Windows7" NullText=" << Select Jobcategory >>">
                                </dx:ASPxComboBox>
                                <asp:SqlDataSource ID="sqldsJobCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand=" Select JobCategoryID,JobCategoryName From JobCategory Where CompanyId=@CompanyId">
                                    <SelectParameters>
                                        <asp:ControlParameter Name="CompanyId" ControlID="radcbCompany" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <asp:Label ID="Label7" runat="server" Text="New EPF No" CssClass="form-label" />
                                <telerik:RadTextBox ID="txtEpfNo" runat="server" MaxLength="50" Width="100%" Skin="Windows7" CssClass="form-control form-control-lg" />
                            </div>
                            <div class="col-md-6">
                                <asp:Label ID="Label3" runat="server" Text="Change Date" CssClass="form-label" />
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
                                <%-- <telerik:RadDatePicker ID="raddpFromDate" runat="server" Culture="en-US"
                                    FocusedDate="2012-08-10" MaxDate="2030-12-31" MinDate="2010-01-01"
                                    Skin="Windows7">
                                    <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                        runat="server" Skin="Windows7" Width="250px">
                                    </Calendar>
                                    <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy"
                                        runat="server">
                                    </DateInput>
                                    <DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                                </telerik:RadDatePicker>--%>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label5" runat="server" CssClass="form-label" Visible="false" Text="Pay Category"></asp:Label>
                                <dx:ASPxComboBox ID="ddlCategory" runat="server" Width="250px" AutoPostBack="true" Visible="false"
                                    TextField="PayPeriodCategory" ValueField="PayPeriodCategoryId" Skin="Windows7" NullText=" << Select New PayCategory >>">
                                </dx:ASPxComboBox>



                                <asp:SqlDataSource ID="sqldsCategory" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                    SelectCommand="SELECT PayPeriodCategoryId,PayPeriodCategory
                                                FROM dbo.Pay_PeriodCategory      
                                                WHERE   CompanyId=@NewCompanyId">
                                    <SelectParameters>
                                        <asp:SessionParameter Name="NewCompanyId" SessionField="NewCompanyId" Type="Int32" />
                                    </SelectParameters>
                                </asp:SqlDataSource>
                            </div>
                        </div>
                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px; margin-top: 20px">
                            <asp:Button ID="radbtnSave" runat="server" CssClass="btn btn-outline-primary check-aspbtn1" Text="Change Employee" OnClick="radbtnSave_Click" ValidationGroup="a" />
                            <asp:Button ID="radbtnCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="radbtnCancel_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="toastrContainer">
            <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
            <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
        </div>           
        <asp:HiddenField ID="hfEmploymentGradeId" runat="server" />
        <asp:HiddenField ID="hfPreviousSelectedCompany" runat="server" />
            <table style="width: 50%;">
</table>
<div class="ContentBlock">
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
                <div class="ContentProgressIcon">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
            </div>

        </div>
        <div class="ContentBlock">
            <asp:Label ID="Label4" runat="server" Height="10" Text=""></asp:Label>
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">

                <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </cc1:ToolkitScriptManager>
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentGridArea">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="Lev_GetLeaveEntitlements" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="CompanyId" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </div>
        </div>
        <div>
          <asp:Panel ID="Panel1" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical">
              <div class="style1">
                  <div style="float: right;margin-top: -10px;">
                      <asp:LinkButton ID="btnClosePopup" runat="server" 
                          > <img src="../../../App_Themes/NewTheme/img/close.png" /></asp:LinkButton>
                  </div>
                  <div id="org-stucture" >
                      <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                  </div>
              </div>
          </asp:Panel>
        </div>

<%--        <asp:Panel ID="Panel1" runat="server" CssClass="popstyle popstyle-2" ScrollBars="Vertical">
            <div class="style1">
                <div style="float: right">
                    <asp:ImageButton ID="btnClosePopup" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                </div>
                <div>
                    <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                </div>
            </div>
        </asp:Panel>--%>
   </div>
</asp:Content>
