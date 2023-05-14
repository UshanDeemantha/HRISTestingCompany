<%@ Page Title="HRIS Employee | New Employee" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeAditionalInfo.aspx.cs" Inherits="Employee_EmployeeAditionalInfo" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/HR/Designation/DesignationStructureControl.ascx" TagName="DesignationStructureControl" TagPrefix="uc15" %>
<%@ Register Src="~/HR/Organization/OrganizationStucture.ascx" TagName="OrganizationStucture"
    TagPrefix="uc1" %>
<%@ Register Src="UserControls/EmployeeSummaryList.ascx" TagName="EmployeeSummaryList" TagPrefix="uc2" %>
<%@ Register Src="UserControls/EmployeeAditionalInfoContol.ascx" TagName="EmployeeAditionalInfoContol" TagPrefix="uc1" %>
<%@ Register Src="UserControls/EmployeeCertificationControl.ascx" TagName="EmployeeCertificationControl" TagPrefix="uc4" %>
<%@ Register Src="UserControls/EmployeeChildControl.ascx" TagName="EmployeeChildControl" TagPrefix="uc5" %>
<%@ Register Src="UserControls/EmoployeeMembershipControl.ascx" TagName="EmoployeeMembershipControl" TagPrefix="uc6" %>
<%@ Register Src="UserControls/EmployeeQulificationControl.ascx" TagName="EmployeeQulificationControl" TagPrefix="uc7" %>
<%@ Register Src="UserControls/EmployeeFluencyrControl.ascx" TagName="EmployeeFluencyrControl" TagPrefix="uc8" %>
<%@ Register Src="UserControls/EmployeeSpouseControl.ascx" TagName="EmployeeSpouseControl" TagPrefix="uc9" %>
<%@ Register Src="UserControls/EmoloyeeSportsControl.ascx" TagName="EmoloyeeSportsControl" TagPrefix="uc10" %>
<%@ Register Src="UserControls/EmployeeSkillsControl.ascx" TagName="EmployeeSkillsControl" TagPrefix="uc11" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>

<%@ Register Src="~/HR/Employee/UserControls/EmployeeChildControl.ascx" TagPrefix="uc1" TagName="EmployeeChildControl" %>


<%@ Register Assembly="DevExpress.Web.v14.2" Namespace="DevExpress.Web" TagPrefix="dx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-control1 {
            display: block;
            width: 100%;
            height: calc(1.5em + .75rem + 7px);
            padding: .375rem .75rem;
            font-size: 1.25rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .form-control2 {
            display: block;
            width: 250px;
            height: calc(1.5em + .75rem + 7px);
            padding: .375rem .75rem;
            font-size: 1rem;
            font-weight: 400;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: .25rem;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .hoverclass {
            margin-top: 31px;
        }

        .emptable {
            border: #333333 1px solid;
            border-radius: 5px;
            -moz-border-radius: 5px;
            -moz-box-shadow: 2px 2px 2px #888;
            -webkit-box-shadow: 2px 2px 2px #888;
            box-shadow: 2px 2px 2px #888;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 11px;
            color: #333333;
        }

            .emptable img {
                border: #666666 solid 2px;
                margin: 0 20px 0 0;
            }



        /*        .auto-style1 {
            width: 403px;
        }*/

        .auto-style2 {
            margin-left: 20px;
        }

        .auto-style3 {
            width: 250px;
        }

        .Contact {
            border-radius: 5px;
            -moz-border-radius: 5px;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #333333;
        }

        span.checkalign {
            margin-left: 46%;
        }

        table#ContentPlaceHolder1_EmployeeAditionalInfoWizard_rbEmpCard {
            margin-top: 3px;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_StepNavigationTemplateContainerID_StepPreviousButton {
            margin-right: 35px;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_StepNavigationTemplateContainerID_StepNextButton {
            margin-right: 19px;
        }

        .mt-3, .my-3 {
            margin-top: 0rem !important;
        }

        .mt-4, .my-4 {
            margin-top: 1rem !important;
        }

        .mt-x, .my-x {
            margin-top: 1.35rem !important;
        }

        .mt-3, .my-3 {
            margin-top: -0.5rem !important;
        }

        .form-containers {
            margin: 15px 5px 15px 20px;
            border-radius: 8px;
            overflow: hidden;
            transition: height 1s ease-in;
            padding-bottom: 34px;
        }

        .mt-1, .my-1 {
            margin-top: -0.5rem !important;
        }

        .mb-2, .my-2 {
            margin-bottom: -1rem !important;
        }

        .mr-lg-6, .mx-lg-6 {
            margin-right: 23rem !important;
        }

        .form-label {
            font-size: 14px;
            font-family: 'Source Sans Pro',sans-serif;
        }

        label.choose:before {
            background: none repeat scroll 0 0 #f7971f;
            border: 0 none;
            color: #FFFFFF;
            cursor: pointer;
            font-family: 'Altis_Book';
            font-size: 15px;
            padding: 3px 15px;
        }

        label.choose:before {
            content: 'Choose file';
            padding: 3px 6px;
            position: absolute;
        }

        .cus-radio > tbody > tr > td {
            display: flex;
            grid-gap: 6px;
            justify-content: center;
            align-items: center;
        }

        .cus-radio > tbody > tr {
            display: flex;
            height: 24px !important;
        }

            .cus-radio > tbody > tr > td > label {
                margin-bottom: 0px
            }

        .daytypecheck {
            margin-top: 10px !important;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_checkepf {
            margin-top: 30px;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_cbBankTranferRequired {
            margin-top: 30px;
        }

        label {
            margin-right: 15px;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_StepNavigationTemplateContainerID_StepPreviousButton {
            position: fixed;
            right: 50px;
            bottom: 10%;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_StepNavigationTemplateContainerID_StepNextButton {
            position: fixed;
            right: 40px;
            bottom: 10%;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_FinishNavigationTemplateContainerID_StepFinishButton {
            position: fixed;
            right: 40px;
            bottom: 10%;
        }

        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_FinishNavigationTemplateContainerID_StepPreviousButton {
            position: fixed;
            right: 160px;
            bottom: 10%;
        }
    </style>
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />

    <link rel="stylesheet" type="text/css" href="../../App_Themes/NewTheme/css/Common/wizard.css" />
    <%--<script type="text/javascript" src="/App_Themes/NewTheme/script/employeeList.js"></script>--%>
    <%--   <script type="text/javascript" src="/App_Themes/NewTheme/script/organization-structure.js"></script>--%>
    <link rel="stylesheet" href="/App_Themes/NewTheme/font-awesome-4.7.0/css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    &nbsp;&nbsp;
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-containers" id="formContainer" runat="server">
            <div class="row">
                <div class="col head-container">
                </div>
            </div>
            <div>
                <asp:HiddenField ID="hfEmloyeeId" runat="server" />
            </div>

            <asp:Wizard ID="EmployeeAditionalInfoWizard" runat="server" ActiveStepIndex="8" class="Contact"
                Width="100%" HeaderText="&nbsp;"
                OnActiveStepChanged="EmployeeAditionalInfoWizard_ActiveStepChanged"
                CssClass="wizard-main"
                StepNextButtonImageUrl="~/App_Themes/NewTheme/images/next-button.png"
                StepNextButtonType="Image"
                StepPreviousButtonImageUrl="~/App_Themes/NewTheme/images/next-button.png"
                StepPreviousButtonType="Image"
                OnFinishButtonClick="EmployeeAditionalInfoWizard_FinishButtonClick">
                <FinishNavigationTemplate>
                    <asp:ImageButton ID="StepPreviousButton" runat="server"
                        AlternateText="Previous" CausesValidation="False" CommandName="MovePrevious"
                        ImageUrl="~/App_Themes/HR/wizard-previous.png" />
                    <asp:ImageButton ID="StepFinishButton" runat="server" AlternateText="Finish"
                        CommandName="MoveComplete" ImageUrl="~/App_Themes/HR/wizard-finish.png" />
                </FinishNavigationTemplate>
                <HeaderStyle CssClass="wizard-hed" />
                <SideBarStyle CssClass="wizard-linkback" HorizontalAlign="Left"
                    VerticalAlign="Top" />
                <SideBarTemplate>
                    <asp:DataList ID="SideBarList" runat="server">
                        <ItemTemplate>
                            <asp:LinkButton ID="SideBarButton" runat="server"
                                CssClass="wizard-linkback-A"></asp:LinkButton>
                        </ItemTemplate>
                        <SelectedItemStyle Font-Bold="True" />
                    </asp:DataList>
                </SideBarTemplate>
                <StartNavigationTemplate>
                    <table class="Contact" width="100%">

                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: right; position: fixed; right: 60px; bottom: 10%;">
                                <asp:ImageButton ID="StartNextButton" runat="server" CommandName="MoveNext"
                                    ImageUrl="~/App_Themes/NewTheme/images/Skip.png" />
                            </td>
                        </tr>
                    </table>
                </StartNavigationTemplate>

                <StepNavigationTemplate>
                    <asp:ImageButton ID="StepPreviousButton" runat="server"
                        AlternateText="Previous" CausesValidation="true" CommandName="MovePrevious"
                        ImageUrl="~/App_Themes/NewTheme/images/previous.png" />
                    <%--                            </div>--%>
                    <%--                        <div style="position:fixed;margin-left: 1149px;margin-top: -464px;">--%>
                    <asp:ImageButton ID="StepNextButton" runat="server" AlternateText="Next" CausesValidation="true" ValidationGroup="a"
                        CommandName="MoveNext" ImageUrl="~/App_Themes/NewTheme/images/skip.png" />
                    <%--                            </div>--%>
                </StepNavigationTemplate>

                <WizardSteps>
                    <asp:WizardStep ID="WizardStep2" runat="server" Title="Employee's Details">
                        <div class="row">
                            <div class="col-md-12" style="border-top-color: black;">
                                <div class="border">
                                    <div class="card-body shadow" style="border-radius: 10px;">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="row" style="margin-left: -14px;">
                                                    <div class="col-md-6">
                                                        <div class="row mt-4">
                                                            <div class="col">
                                                                <asp:Label ID="Label27" CssClass="form-label" runat="server" Text="Employee Code" class="auto-style2"></asp:Label>

                                                                <telerik:RadTextBox ID="txtEmployeeCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="150px" Enabled="false"
                                                                    Skin="Windows7" />
                                                            </div>
                                                        </div>
                                                        <div class="row mt-4">
                                                            <div class="col">
                                                                <asp:Label ID="Label2" runat="server" Text="EPF Number" class="auto-style2" CssClass="form-label"></asp:Label>

                                                                <telerik:RadTextBox ID="txtEPFNo" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="150px" OnTextChanged="txtEPFNo_TextChanged" AutoPostBack="true" Enabled="true"
                                                                    Skin="Windows7" Visible="true" />
                                                            </div>
                                                        </div>
                                                        <div class="row mt-4">
                                                            <div class="col">
                                                                <asp:Label ID="Label28" CssClass="form-label" runat="server" Text="First Name" class="auto-style2"></asp:Label>
                                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator25" runat="server" ControlToValidate="txtFirstName"
                                                                    ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                                <telerik:RadTextBox ID="txtFirstName" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="200px"
                                                                    Skin="Windows7" />
                                                            </div>
                                                        </div>
                                                        <div class="row mt-3">
                                                        </div>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <div class="row">
                                                            <div class="col">
                                                                <asp:Image ID="Image2" runat="server" ImageUrl="~/App_Themes/Common/DefaultEmployee.png" />

                                                                <div id=" fileupload">
                                                                    <label class="choose">
                                                                        <asp:FileUpload ID="fuEmployeeImage" runat="server" Style="margin-left: 9px" />
                                                                    </label>

                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>

                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label29" CssClass="form-label" runat="server" Text="Last Name" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtLastName" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="200px"
                                                            Skin="Windows7" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtLastName"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col">
                                                        <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Initials" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtNameInitials" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="200px"
                                                            Skin="Windows7" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtNameInitials"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>

                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col">
                                                        <asp:Label ID="Label30" CssClass="form-label" runat="server" Text="Full Name" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtFullName" CssClass="form-control form-control-lg" runat="server"
                                                            Skin="Windows7" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" ControlToValidate="txtFullName"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col">
                                                        <asp:Label ID="Label150" CssClass="form-label" runat="server" Text="Call Name" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtCallName" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="100%"
                                                            Skin="Windows7" />
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtCallName"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label108" CssClass="form-label" runat="server" Text="Payroll Act" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="radCmbPayrollAct" runat="server" Width="100%" MaxHeight="80px" Skin="Windows7" EmptyMessage="Choose an Act">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="Shop & Office" Text="Shop & Office" />
                                                                <telerik:RadComboBoxItem Value="Wages Board" Text="Wages Board" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radCmbPayrollAct"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label111" CssClass="form-label" runat="server" Text="Cost Center" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="radCmbCostCenter" runat="server" Width="100%" MaxHeight="80px" Skin="Windows7" EmptyMessage="Choose a Cost Center"
                                                            DataSourceID="sqlDSCostCenter" DataTextField="CompanyName" DataValueField="CompanyID">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="radCmbCostCenter"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                        <asp:SqlDataSource ID="sqlDSCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="select CompanyID,CompanyName from CompanyProfile"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label113" CssClass="form-label" runat="server" Text="Direct/Indirect" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="radCmbDirectIndirect" runat="server" Width="100%" MaxHeight="80px" Skin="Windows7" EmptyMessage="Choose Direct/Indirect">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Value="Direct" Text="Direct" />
                                                                <telerik:RadComboBoxItem Value="Indirect" Text="Indirect" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="radCmbDirectIndirect"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label93" CssClass="form-label" runat="server" Text="Occupation Grade" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtOccupationGrade" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="100%" Skin="Windows7" Visible="true" />

                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label21" CssClass="form-label" runat="server" Text="Job Catogory" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlJobCatogory" runat="server" DataSourceID="odsJobCategorys"
                                                            DataTextField="JobCategoryForCombo" Width="100%" MaxHeight="80px" DataValueField="JobCategoryID"
                                                            Skin="Windows7" OnDataBound="ddlJobCatogory_DataBound">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" ControlToValidate="ddlJobCatogory"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label43" CssClass="form-label" runat="server" Text="Job Grade" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlJobGrade" runat="server" DataSourceID="odsJobGrads" DataTextField="JobGradeForCombo"
                                                            DataValueField="JobGradeID" Width="100%" MaxHeight="80px" OnDataBound="ddlJobGrade_DataBound"
                                                            Skin="Windows7">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlJobGrade"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label54" CssClass="form-label" runat="server" Text="Employment Type" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlEmployementType" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                                            DataSourceID="odsEmploymentTypes" Width="100%" DataTextField="EmploymentTypeForCombo"
                                                            DataValueField="EmploymentTypeID" OnDataBound="ddlEmployementType_DataBound" OnSelectedIndexChanged="ddlEmployementType_SelectedIndexChanged"
                                                            Skin="Windows7">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator19" runat="server" ControlToValidate="ddlEmployementType"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label44" CssClass="form-label" runat="server" Text="Employment Grade" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlEmploymentGrade" runat="server" Width="100%" MaxHeight="80px" DataSourceID="odsEmploymentGrades"
                                                            DataTextField="EmploymentGradeForCombo" DataValueField="EmploymentGradeID" OnDataBound="ddlEmploymentGrade_DataBound"
                                                            Skin="Windows7">
                                                        </telerik:RadComboBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" ControlToValidate="ddlEmploymentGrade"
                                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="tddpFromDate" CssClass="form-label" runat="server" Text="From Date" class="auto-style2" Visible="false"></asp:Label>
                                                        <telerik:RadDatePicker ID="dxEmpTypeFromDate" runat="server" Culture="en-US" Width="100%" Visible="false"
                                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar12" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput12" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                        <%--<dx:ASPxDateEdit ID="txtRecruitementDate" runat="server" Width="200px" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="tddpToDate" CssClass="form-label" runat="server" Text="To Date" class="auto-style2" Visible="false"></asp:Label>
                                                        <telerik:RadDatePicker ID="dxEmpTypeEndDate" runat="server" Culture="en-US" Width="100%" Visible="false"
                                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar13" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput13" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>

                                                        <%--  <dx:ASPxDateEdit ID="txtRetirementDate" runat="server" Width="200px" CssClass="form-control form-control-lg" Enabled="false"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="B Card Availability" class="auto-style2"></asp:Label>
                                                        <asp:RadioButtonList ID="rbEmpCard" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                                            <asp:ListItem>No</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 25px">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label31" CssClass="form-label" runat="server" Text="Gender"></asp:Label>

                                                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label32" CssClass="form-label" runat="server" Text="Status" class="auto-style2"></asp:Label>

                                                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                                            <asp:ListItem Selected="True" Value="0">Single</asp:ListItem>
                                                            <asp:ListItem Value="1">Married</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 23px">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label33" CssClass="form-label" runat="server" Text="Date Of Birth" class="auto-style2"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Culture="en-US" Width="100%" OnSelectedDateChanged="txtDateOfBirth_SelectedDateChanged"
                                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label" AutoPostBack="true"
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
                                                </div>
                                                <div class="row" style="margin-top: 9px">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label45" CssClass="form-label" runat="server" Text="Recruitment Date" class="auto-style2"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtRecruitementDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput3" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                        <%--<dx:ASPxDateEdit ID="txtRecruitementDate" runat="server" Width="200px" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label20" CssClass="form-label" runat="server" Text="Retirement Date" class="auto-style2"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtRetirementDate" runat="server" Culture="en-US" Width="100%" Enabled="false"
                                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar4" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput4" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>

                                                        <%--  <dx:ASPxDateEdit ID="txtRetirementDate" runat="server" Width="200px" CssClass="form-control form-control-lg" Enabled="false"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                </div>
                                                <div class="row" style="margin-top: 10px">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label11" CssClass="form-label" runat="server" Text="Confirmation Date" class="auto-style2"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtConfirmationDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar5" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput5" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                        <%-- <dx:ASPxDateEdit ID="txtConfirmationDate" runat="server" Width="100%" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label10" runat="server" Text="EPF Entitle Date" class="auto-style2" CssClass="form-label"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtEPFNoDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
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
                                                </div>
                                                <div class="row mb-3" style="margin-top: 12px">
                                                    <div class="col">
                                                        <asp:Label ID="lbldes" CssClass="form-label" runat="server">Designation</asp:Label>

                                                        <dx:ASPxComboBox ID="cmbDesignation" runat="server" DataSourceID="GetDesignationforCombo" TextField="DesignationName"
                                                            ValueField="DesignationID" Width="100%" CssClass="form-control form-control-lg">
                                                        </dx:ASPxComboBox>
                                                        <asp:SqlDataSource ID="GetDesignationforCombo" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetDesignationForCombo" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mb-3" style="margin-top: 35px">
                                                    <div class="col">
                                                        <asp:Button ID="btnOrganizationSelect" runat="server" CssClass="Buttonss" Text="Select Department"
                                                            BackColor="#f7971f" BorderColor="transparent" ForeColor="#FFFFFF" Font-Size="14px" />
                                                        <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                                                            CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="Panel1"
                                                            TargetControlID="btnOrganizationSelect">
                                                        </cc1:ModalPopupExtender>
                                                        &nbsp;&nbsp;<asp:Label ID="lblOrganization" runat="server" Font-Size="15px"></asp:Label>
                                                        <asp:HiddenField ID="hfOrganizationStructure" runat="server" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <div>
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 40px; height: 40px; float: right;" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                        </div>


                        <asp:Label ID="lblAddEmployee" runat="server" ForeColor="Red"></asp:Label>

                        <div>
                            <asp:ObjectDataSource ID="odsJobCategorys" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetJobCategoryCompanyWise" TypeName="HRM.Common.BLL.Reference">
                                <SelectParameters>
                                    <asp:SessionParameter DefaultValue="" Name="CompanyID" SessionField="CompanyId"
                                        Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsJobGrads" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetJobGrade" TypeName="HRM.Common.BLL.Reference">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="JobGradeId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsbranch" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetBranch" TypeName="HRM.Common.BLL.Reference">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="BranchId" Type="Int32" />
                                    <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>


                        <div>
                            <asp:ObjectDataSource ID="odsEmploymentGrades" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetEmploymentGrade" TypeName="HRM.Common.BLL.Employee">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="EmploymentGradeId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>

                            <asp:ObjectDataSource ID="odsEmploymentTypes" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetEmploymentType" TypeName="HRM.Common.BLL.Employee">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="EmploymentTypeId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                        <div>
                            <asp:Panel ID="Panel1" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical">
                                <div class="style1">
                                    <div style="float: right; margin-top: -10px;">
                                        <asp:LinkButton ID="btnClosePopup" runat="server"> <img src="../../../App_Themes/NewTheme/img/close.png" /></asp:LinkButton>
                                    </div>
                                    <div id="org-stucture">
                                        <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>
                        </div>
                        <%--                            <asp:Panel ID="Panel1" runat="server" CssClass="popstyles popstyle-2" ScrollBars="Vertical">
                                <div class="style1">
                                    <div style="float: right">
                                        <asp:ImageButton ID="btnClosePopup" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                                    </div>
                                    <div>
                                        <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>--%>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep3" runat="server" Title="Employee's Contact Details">
                        <div class="col-md-12" style="border-top-color: black;">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow" style="border-radius: 10px;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label16" runat="server" Text="E-Mail" CssClass="form-label"></asp:Label>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail"
                                                        CssClass="validator" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="a"> * Invalid Email</asp:RegularExpressionValidator>
                                                    <telerik:RadTextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label14" runat="server" Text="Contact No (Mobile 1)" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtMobileNo"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtMobileNo" runat="server" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label34" runat="server" Text="Contact No (Mobile 2)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtMobileNo2" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label39" runat="server" Text="Contact No (Home 1)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtHomeNo" runat="server" CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label89" runat="server" Text="Contact No (Home 2)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtHomeNo2" runat="server" MaxLength="10" CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label15" runat="server" Text="Office No (Mobile)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtOfficeNo" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label17" runat="server" Text="NIC" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtNIC"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtNIC" runat="server" CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label46" runat="server" Text="Passport No" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtPassport" runat="server" MaxLength="50" CssClass="form-control form-control-lg" AutoPostBack="true" Skin="Windows7" OnTextChanged="txtPassport_TextChanged" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label13" runat="server" Text="Expiry Date" CssClass="form-label"></asp:Label>
                                                    <telerik:RadDatePicker ID="raddpPasportExpiyDate" runat="server" Culture="en-US" Width="100%" Enabled="false"
                                                        FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                        Skin="Windows7">
                                                        <Calendar ID="Calendar6" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                            runat="server" Skin="Windows7" CssClass="calender shadow">
                                                        </Calendar>
                                                        <DateInput ID="DateInput6" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                            runat="server">
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                    <%-- <dx:ASPxDateEdit ID="raddpPasportExpiyDate" runat="server" CssClass="form-control form-control-lg" Enabled="false"></dx:ASPxDateEdit>--%>
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label40" runat="server" Text="Postal Code" CssClass="form-label"></asp:Label>

                                                    <telerik:RadTextBox ID="txtPostalCode" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label18" runat="server" Text="Emergency Contact Person" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmergencycontactPerson"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencycontactPerson" runat="server" MaxLength="50"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label151" runat="server" Text="Relationship" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRelationship"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtRelationship" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label42" runat="server" Text="Emergency Mobile No" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtEmergencyContactNo"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencyContactNo" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label90" runat="server" Text="Emergency Home No" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtEmergencyContactNoHome" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>

                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label35" runat="server" Text="Remarks" CssClass="form-label"></asp:Label>

                                                    <telerik:RadTextBox ID="txtRemark" runat="server" MaxLength="500"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>

                                        </div>
                                        <div class="col-md-6">
                                            <div class="row" style="margin-top: 11px">
                                                <div class="col">
                                                    <asp:Label ID="Label41" runat="server" Text="Address Line 1" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAddress"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtAddress" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label88" runat="server" Text="Address Line 2" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtAddress2"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtAddress2" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label79" runat="server" Text="City" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtcity"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtcity" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4" style="margin-top: 11px">
                                                <div class="col">
                                                    <asp:Label ID="Label81" runat="server" Text="Temporary Address Line 1" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txttempAddressline1" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4" style="margin-top: 11px">
                                                <div class="col">
                                                    <asp:Label ID="Label82" runat="server" Text="Temporary Address Line 2" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txttempAddressline2" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label83" runat="server" Text="Temporary City" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txttempAddresscity" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label92" runat="server" Text="Emergency Address Line1" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtEmergencyAddress"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencyAddress" runat="server" Height="35px" MaxLength="50" TextMode="MultiLine"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label48" runat="server" Text="Emergency Address Line2" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtEmergencyAddress2"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencyAddress2" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label77" runat="server" Text="Emergency City" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtEmergancycity"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergancycity" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <asp:Label ID="Label87" Visible="false" runat="server" Text="Contact No (Mobile 3)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtMobileNo3" Visible="false" CssClass="form-control form-control-lg" runat="server" MaxLength="10" Width="200px"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>

                                    <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">
                                        <div>
                                            <asp:Button ID="RadButton1" CssClass="btn btn-outline-success check-aspbtn" runat="server" OnClick="RadButton1_Click" Text="Finish"
                                                ToolTip="Add Information" />
                                        </div>
                                    </div>
                                </div>


                            </div>
                        </div>
                        <div>
                            <asp:Panel ID="PanelVisa" runat="server" Visible="False">
                                <table>
                                    <tr>
                                        <td>Visa No :</td>
                                        <td>
                                            <telerik:RadTextBox ID="txtVisa" runat="server" MaxLength="50"
                                                Skin="Windows7" Width="150px" EmptyMessage="Visa Number" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Expiry Date :</td>
                                        <td>
                                            <telerik:RadDatePicker ID="raddpVisaExpiyDate" runat="server" Skin="WebBlue" Width="130px">
                                                <Calendar Skin="WebBlue" UseColumnHeadersAsSelectors="False"
                                                    UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                </Calendar>
                                                <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                </DateInput>
                                                <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                            </telerik:RadDatePicker>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>


                        <asp:UpdateProgress ID="UpdateProgress3" runat="server">
                            <ProgressTemplate>
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 40px; height: 40px; float: right;" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                        <div>
                            <asp:Label ID="Label19" runat="server" ForeColor="Red"></asp:Label>
                        </div>

                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep12" runat="server" Title="Employee's Salary Detail">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label95" runat="server" CssClass="form-label" Text="Basic Salary "></asp:Label>
                                                        <telerik:RadNumericTextBox ID="RN_txtBasicsalary" runat="server"
                                                            Culture="en-US" Skin="Windows7" Value="0" CssClass="form-control form-control-lg">
                                                            <NumberFormat ZeroPattern="n"></NumberFormat>
                                                        </telerik:RadNumericTextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                            ControlToValidate="RN_txtBasicsalary" ErrorMessage="*" ForeColor="Red"
                                                            ValidationGroup="Add"></asp:RequiredFieldValidator>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:CheckBox ID="checkepf" runat="server" Checked="true" CssClass="daytypecheck" />
                                                        <asp:Label ID="Label24" CssClass="form-label" runat="server" Text="EPF Calculate" />
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label96" runat="server" CssClass="form-label" Text="Daily Wage "></asp:Label>
                                                        <telerik:RadNumericTextBox ID="RN_txtDailyWadge" runat="server"
                                                            Culture="en-US" Skin="Windows7" CssClass="form-control form-control-lg" Value="0">
                                                            <NumberFormat ZeroPattern="n"></NumberFormat>
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="cbBankTranferRequired" runat="server" Checked="false" OnCheckedChanged="cbBankTranferRequired_CheckedChanged" AutoPostBack="true" />
                                                        <asp:Label ID="Label106" CssClass="form-label" runat="server" Text="Bank Tranfer Required" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label97" runat="server" CssClass="form-label" Text="Fixed Amount "></asp:Label>

                                                        <telerik:RadNumericTextBox ID="RN_txtFixedAmmount" runat="server"
                                                            Culture="en-US" Skin="Windows7" CssClass="form-control form-control-lg" Value="0">
                                                            <NumberFormat ZeroPattern="n"></NumberFormat>
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col">
                                                        <asp:Label ID="Label101" CssClass="form-label" runat="server" Text="Bank "></asp:Label>
                                                        <dx:ASPxComboBox ID="RcmbBank" runat="server" DataSourceID="objDataBankDetails" TextField="BankName" NullText=" Please Select the Bank Name" AutoPostBack="True"
                                                            ValueField="BankId" Width="100%" CssClass="form-control form-control-lg" Enabled="false">
                                                        </dx:ASPxComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label99" runat="server" CssClass="form-label" Text="Currency Code "></asp:Label>

                                                        <telerik:RadComboBox ID="RcmbCurencyCode" runat="server"
                                                            DataSourceID="SqlDataSource3" DataTextField="CurrencyName"
                                                            DataValueField="CurrencyCode" Skin="Windows7"
                                                            OnDataBound="RcmbCurencyCode_DataBound" Width="100%">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col">
                                                        <asp:Label ID="Label102" CssClass="form-label" runat="server" Text="Branch "></asp:Label>
                                                        <dx:ASPxComboBox ID="RcmbBranches" runat="server" DataSourceID="objDataBankBranchDetails" TextField="Branch" NullText=" Please Select the Branch Name"
                                                            ValueField="BranchId" Width="100%" CssClass="form-control form-control-lg" Enabled="false">
                                                        </dx:ASPxComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label98" runat="server" CssClass="form-label" Text="Max Advance Percentage "></asp:Label>

                                                        <telerik:RadNumericTextBox ID="RN_txtMaxAdvance" runat="server" Culture="en-US"
                                                            Skin="Windows7" Type="Percent" CssClass="form-control form-control-lg" Value="0">
                                                            <NumberFormat ZeroPattern="n%"></NumberFormat>
                                                        </telerik:RadNumericTextBox>
                                                    </div>
                                                    <div class="col">
                                                        <asp:Label ID="Label103" CssClass="form-label" runat="server" Text="Account No "></asp:Label>

                                                        <telerik:RadTextBox ID="RdTxtAccountNo" runat="server" Enabled="false"
                                                            CssClass="form-control form-control-lg" Skin="Windows7">
                                                        </telerik:RadTextBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">

                                                        <asp:Label ID="Label100" CssClass="form-label" runat="server" Text="Pay Catagory "></asp:Label>

                                                        <telerik:RadComboBox ID="RcmbPayPeriodCatogory" runat="server"
                                                            DataSourceID="SqlDataSource2" DataTextField="PayPeriodCategory"
                                                            DataValueField="PayPeriodCategoryId" Skin="Windows7" Width="100%"
                                                            OnDataBound="RcmbPayPeriodCatogory_DataBound">
                                                        </telerik:RadComboBox>

                                                    </div>
                                                    <div class="col">
                                                        <asp:Label ID="Label104" CssClass="form-label" runat="server" Text="Name As Per Bank "></asp:Label>

                                                        <telerik:RadTextBox ID="RdTxtNameAsPerBank" runat="server" Enabled="false"
                                                            CssClass="form-control form-control-lg" Skin="Windows7">
                                                        </telerik:RadTextBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px; margin-top: 16px;">
                                            <div>
                                                <asp:Button ID="RadButton2" CssClass="btn btn-outline-success check-aspbtn" runat="server" OnClick="RadButton2_Click" Text="Finish"
                                                    ToolTip="Add Information" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table>
                            <tr runat="server" visible="false">
                                <td>&nbsp;</td>
                                <td>
                                    <telerik:RadButton ID="cbStopPay" runat="server" ButtonType="ToggleButton"
                                        OnCheckedChanged="cbStopPay_CheckedChanged" Skin="Simple" Text="Stop Pay"
                                        ToggleType="CheckBox">
                                    </telerik:RadButton>
                                    &nbsp;<asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <asp:SqlDataSource ID="SqlDataSource2" runat="server"
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            OldValuesParameterFormatString="original_{0}"
                            SelectCommand="SELECT [PayPeriodCategoryId], [PayPeriodCategory] FROM [Pay_PeriodCategory] WHERE CompanyId=@CompanyID ">
                            <SelectParameters>

                                <asp:SessionParameter DefaultValue="" Name="CompanyID" SessionField="CompanyId"
                                    Type="Int32" />
                            </SelectParameters>
                        </asp:SqlDataSource>


                        <asp:SqlDataSource ID="SqlDataSource3" runat="server"
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            OldValuesParameterFormatString="original_{0}"
                            SelectCommand="SELECT [CurrencyCode], [CurrencyName] FROM [Pay_CurrencyType]"></asp:SqlDataSource>
                        <asp:ObjectDataSource ID="objDataBankDetails" runat="server"
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetBankDetails"
                            TypeName="HRM.Payroll.BLL.BankAndBranch"></asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="objDataBankBranchDetails" runat="server"
                            OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetBankBranchDetails" TypeName="HRM.Payroll.BLL.BankAndBranch">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="RcmbBank" Name="bankID"
                                    PropertyName="Value" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:ObjectDataSource ID="objdsEmployee" runat="server"
                            SelectMethod="GetEmployee" TypeName="HRM.Common.BLL.Employee"
                            OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfEmloyeeId" DefaultValue="-99"
                                    Name="EmployeeId" PropertyName="Value" Type="Int64" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep13" runat="server" Title="Employee's Time Detail">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label120" CssClass="form-label" runat="server" Text="Shift Group" />

                                                        <telerik:RadComboBox ID="radcboRosterGroup" runat="server" AutoPostBack="true" DataSourceID="objdsRosterGroup" DataTextField="Remarks" DataValueField="RosterGroupId" OnItemDataBound="RcmbRosterGroup_ItemDataBound" Skin="Windows7" Width="100%">
                                                        </telerik:RadComboBox>
                                                        <asp:ObjectDataSource ID="objdsRosterGroup" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetRosterGroupsList" TypeName="HRM.Time.BLL.Roster">
                                                            <SelectParameters>
                                                                <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label119" CssClass="form-label" runat="server" Text="Default Shift" />

                                                        <telerik:RadComboBox ID="radcboShiftCode" runat="server" DataSourceID="objdsShiftTypes" DataTextField="Shift" DataValueField="ShiftId" OnItemDataBound="RcmbShiftCode_ItemDataBound" Skin="Windows7" Width="100%">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Owner="radcboShiftCode" Text="select" Value="select" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                        <asp:ObjectDataSource ID="objdsShiftTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetShiftTypeByCompany" TypeName="HRM.Time.BLL.Shift">
                                                            <SelectParameters>
                                                                <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:ObjectDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-10">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        &nbsp;
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Time related Entitlements" Font-Bold="true" ForeColor="#f7971f" />
                                                    </div>
                                                </div>
                                                <hr style="border-top-color: #f7971f; border-top-width: 2px; margin-bottom: 2rem;" />
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:CheckBox ID="CheckBox6" runat="server" Checked="true" />
                                                        <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="  Is Attendance Consider" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="CheckBox1" runat="server" />
                                                        <asp:Label ID="Label71" CssClass="form-label" runat="server" Text="  Post OT Entitlement" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="CheckBox2" runat="server" />
                                                        <asp:Label ID="Label94" CssClass="form-label" runat="server" Text="  Early OT Entitlement" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="cbNightOTEntitle" runat="server" />
                                                        <asp:Label ID="Label105" CssClass="form-label" runat="server" Text="  Night OT Entitlement" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="CheckBox3" runat="server" />
                                                        <asp:Label ID="Label109" CssClass="form-label" runat="server" Text="  Late Calculation" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="CheckBox4" runat="server" />
                                                        <asp:Label ID="Label110" CssClass="form-label" runat="server" Text="  Early Departure Calculation" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:CheckBox ID="CheckBox5" runat="server" />
                                                        <asp:Label ID="Label112" CssClass="form-label" runat="server" Text="  Liev Leaves Entitlement" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:CheckBox ID="CheckBox7" runat="server" Text="Pay Cut Calculation" Visible="false" />
                                                        <asp:Label ID="Label107" CssClass="form-label" runat="server" Text="Pay Cut Calculation" Visible="false" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <div>
                                                <asp:Button ID="RadButton3" runat="server" CssClass="btn btn-outline-success check-aspbtn" OnClick="RadButton3_Click" Text="Finish"
                                                    ToolTip="Add Information" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>

                        <table class="Contact" width="75%">
                            <tr>
                                <td></td>
                            </tr>
                            <tr id="LateAtt" runat="server" visible="false">
                                <td>&nbsp; </td>
                                <td class="auto-style1">
                                    <table style="background-color: whitesmoke; width: 300%;">
                                        <tr>
                                            <td class="auto-style3">
                                                <asp:Label ID="Label121" runat="server" Text="Late Attendance Deduct option" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td class="auto-style3">
                                                <dx:ASPxCheckBoxList ID="CBOption" runat="server" RepeatColumns="3" RepeatLayout="Table" TextField="OptionDescription" Theme="Office2010Blue" ValueField="LateSetupId" Width="100%">
                                                    <RootStyle VerticalAlign="Top">
                                                    </RootStyle>
                                                    <RootStyle BackColor="Salmon">
                                                    </RootStyle>
                                                </dx:ASPxCheckBoxList>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                        </table>
                        <%--  <table class="Contact" width="75%">
                                <tr>
                                    <td>
                                        <table style="width: 100%;">
                                             <tr>
                                                <td>
                                                    <asp:Label ID="Label109" runat="server" Text="Default Shift" />
                                                </td>
                                                <td class="auto-style1">
                                                    <telerik:RadComboBox ID="radcboShiftCode" runat="server" DataSourceID="objdsShiftTypes" DataTextField="Shift" DataValueField="ShiftId" OnItemDataBound="RcmbShiftCode_ItemDataBound" Skin="Windows7" Width="200px">
                                                        <Items>
                                                            <telerik:RadComboBoxItem runat="server" Owner="radcboShiftCode" Text="select" Value="select" />
                                                        </Items>
                                                    </telerik:RadComboBox>
                                                    <asp:ObjectDataSource ID="objdsShiftTypes" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetShiftTypeByCompany" TypeName="HRM.Time.BLL.Shift">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label110" runat="server" Text="Shift Group" />
                                                </td>
                                                <td>
                                                    <telerik:RadComboBox ID="radcboRosterGroup" runat="server" AutoPostBack="true" DataSourceID="objdsRosterGroup" DataTextField="Remarks" DataValueField="RosterGroupId" OnItemDataBound="RcmbRosterGroup_ItemDataBound"  Skin="Windows7" Width="200px">
                                                    </telerik:RadComboBox>
                                                    <asp:ObjectDataSource ID="objdsRosterGroup" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetRosterGroupsList" TypeName="HRM.Time.BLL.Roster">
                                                        <SelectParameters>
                                                            <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                                        </SelectParameters>
                                                    </asp:ObjectDataSource>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>Entitlements </td>
                                                <td class="auto-style1">
                                                    <table style="width: 200%; margin-left: 10px">
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox6" runat="server" Checked="true" Text="Is Attendance Consider" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox1" runat="server" Text="Post OT Entitlement" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox2" runat="server" Text="Early OT Entitlement" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="cbNightOTEntitle" runat="server" Text="Night OT Entitlement" Visible="false"/>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox3" runat="server" Text="Late Calculation" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox4" runat="server" Text="Early Departure Calculation" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox5" runat="server" Text=" Liev Leaves Entitlement" />
                                                            </td>
                                                            <td>
                                                                <asp:CheckBox ID="CheckBox7" runat="server" Text="Pay Cut Calculation" Visible="false" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr id="LateAtt" runat="server" visible="false">
                                                <td>&nbsp; </td>
                                                <td class="auto-style1">
                                                    <table style="background-color: whitesmoke; width: 300%;">
                                                        <tr>
                                                            <td class="auto-style3">
                                                                <asp:Label ID="Label112" runat="server" Text="Late Attendance Deduct option" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td class="auto-style3">
                                                                <dx:ASPxCheckBoxList ID="CBOption" runat="server" RepeatColumns="3" RepeatLayout="Table" TextField="OptionDescription" Theme="Office2010Blue" ValueField="LateSetupId" Width="100%">
                                                                    <RootStyle VerticalAlign="Top">
                                                                    </RootStyle>
                                                                    <RootStyle BackColor="Salmon">
                                                                    </RootStyle>
                                                                </dx:ASPxCheckBoxList>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="text-align: right">&nbsp;</td>
                                                <td>
                                                   
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>--%>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep1" runat="server" Title="Additional Information">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label1" runat="server" Text="Blood Group" CssClass="form-label"></asp:Label>
                                                        <telerik:RadComboBox ID="ddlBloodGroup" runat="server" Width="100%"
                                                            Skin="Windows7">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Please Select the Blood Group"
                                                                    Value="0" />
                                                                <telerik:RadComboBoxItem runat="server" Text="A+"
                                                                    Value="A+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="A-"
                                                                    Value="A-" />
                                                                <telerik:RadComboBoxItem runat="server" Text="B+"
                                                                    Value="B+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="B-"
                                                                    Value="B-" />
                                                                <telerik:RadComboBoxItem runat="server" Text="AB+"
                                                                    Value="AB+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="AB-"
                                                                    Value="AB-" />
                                                                <telerik:RadComboBoxItem runat="server" Text="O+"
                                                                    Value="O+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="O-"
                                                                    Value="O-" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label91" runat="server" Text="Nationality" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlNationality" runat="server"
                                                            DataSourceID="odsNationality" DataTextField="NationalityName"
                                                            DataValueField="NationalityID" Width="100%" Skin="Windows7">
                                                        </telerik:RadComboBox>

                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label3" runat="server" Text="Race" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlRace" runat="server"
                                                            DataSourceID="odsRace" DataTextField="RaceName"
                                                            DataValueField="RaceID" Width="100%" Skin="Windows7">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label115" runat="server" Text="Transport Route" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtTransportRoute" runat="server" Skin="Windows7" Width="100%" DataSourceID="dstransport" ValueField="TrId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="transportplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="transportplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dstransport" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetTransportRtforcombo" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label118" runat="server" Text="Distance For Permanent Address" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtDistanceforPermanentAddress" CssClass="form-control form-control-lg" runat="server" Skin="Windows7" Width="100%" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label114" runat="server" Text="Vaccination" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="cmbvaccine" runat="server" Skin="Windows7" Width="100%" DataSourceID="DSVACCINE" ValueField="VcId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btnvcplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btnvcplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="DSVACCINE" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GETVCFORCOMBO" SelectCommandType="StoredProcedure">

                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label4" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtDistrict" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsdistrict" ValueField="DstrictId" TextField="District" AutoPostBack="true" CssClass="form-control form-control-lg" />
                                                        <asp:SqlDataSource ID="dsdistrict" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetDistrictforcombo" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-x">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label12" runat="server" Text="Province" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtProvince" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsprovince" ValueField="PrId" TextField="Province" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <asp:SqlDataSource ID="dsprovince" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                        SelectCommand="Emp_GetProvince" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label116" runat="server" Text="GS Division" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtGSDivision" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsgsdivisioans" ValueField="GSId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btngsplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btngsplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dsgsdivisioans" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetAllGsDivisionsforcombo" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label128" runat="server" Text="Electoral District" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtElectoralDistrict" runat="server" Skin="Windows7" Width="100%" DataSourceID="dselect" ValueField="GSId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btnelectplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btnelectplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dselect" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetElectoralforcombo" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label125" runat="server" Text="Divisional Secretariats" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtDivisionalSecretariats" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsdivision" ValueField="VcId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btndivisionplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btndivisionplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dsdivision" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emmp_Getdivisionfog" SelectCommandType="StoredProcedure">

                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label5" Visible="false" runat="server" Text="Coller Size" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtxCollorSize" Visible="false" runat="server" Skin="Windows7" Width="100%" CssClass="form-control form-control-lg" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                                                            ControlToValidate="txtxCollorSize" ErrorMessage="*" ForeColor="Red"
                                                            ValidationExpression="[0-9]+(\.[0-9][0-9]?)?" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <div>
                                                &nbsp;
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>


                        <div>
                            <asp:ObjectDataSource ID="odsRace" runat="server"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetRaceList"
                                TypeName="HRM.Common.BLL.Race">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="RaceId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                            <asp:ObjectDataSource ID="odsNationality" runat="server"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetNationalityList"
                                TypeName="HRM.Common.BLL.Reference">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="NationalityId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>


                    </asp:WizardStep>

                    <asp:WizardStep ID="WizardStep4" runat="server" Title="Qualification Details">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label22" runat="server" CssClass="form-label" Text="Institute" class="auto-style2" />
                                                        <telerik:RadComboBox ID="ddlInstitute" runat="server" AutoPostBack="True" DataSourceID="SqlDsInstitution"
                                                            Skin="Windows7" DataTextField="InstituteCode" DataValueField="InstituteId" TextFormatString="{0}" Width="100%">
                                                        </telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="SqlDsInstitution" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="SELECT [InstituteId], [InstituteCode], [InstituteName], [Active] FROM [Institute]"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label23" CssClass="form-label" runat="server" Text="Course" class="auto-style2"></asp:Label>
                                                        <telerik:RadComboBox ID="ddlCourse" runat="server"
                                                            DataSourceID="sqlDsCourse" DataTextField="CourseName" TextFormatString="{0}" ValueType="System.String"
                                                            DataValueField="CourseID" OnDataBound="ddlCourse_DataBound1" Skin="Windows7" Width="100%">
                                                        </telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="sqlDsCourse" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="SELECT [CourseID], [CourseName] FROM [RecognizeCourse] WHERE ([InstituteID] = @InstituteID)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlInstitute" Name="InstituteID"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label26" CssClass="form-label" runat="server" Text="Year" class="auto-style2"></asp:Label>
                                                        <telerik:RadTextBox ID="txtYear" runat="server" MaxLength="4" Width="100%" CssClass="form-control1 form-control-lg" EmptyMessage="YYYY"
                                                            Skin="Windows7" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <asp:Button ID="btnQuliSave" runat="server" OnClick="btnQuliSave_Click" Text="Add"
                                                ToolTip="Add Information" ValidationGroup="a" CssClass="btn btn-outline-success check-aspbtn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table>
                            <tr>
                                <td style="text-align: right">
                                    <asp:UpdateProgress ID="UpdateProgress4" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblResultQulification" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div class="row" style="position: relative">
                            <div class="col table-scroll">
                                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="EmployeeQulificationID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EmployeeQulificationID" Caption="LoanTypeId" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="CourseID" Visible="false" VisibleIndex="1" Caption="Loan Type Code" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="InstituteCode" VisibleIndex="2" Caption="Institute Name" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="CourseName" VisibleIndex="3" Caption="Certification Name" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Year" VisibleIndex="4" Caption="Date" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click1" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep5" runat="server" Title="Certification Details">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label36" runat="server" Text="Institute" CssClass="form-label" />

                                                        <telerik:RadComboBox ID="ddlCerInstitute" runat="server" AutoPostBack="True" DataSourceID="SqlDscertInstitution"
                                                            Skin="Windows7" Width="100%" DataTextField="InstituteCode" DataValueField="InstituteId" TextFormatString="{0}">
                                                        </telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="SqlDscertInstitution" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="SELECT [InstituteId], [InstituteCode], [InstituteName], [Active] FROM [Institute]"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label37" runat="server" Text="Certification" CssClass="form-label" />

                                                        <telerik:RadComboBox ID="ddlCertification" runat="server" AutoPostBack="True" Skin="Windows7" Width="100%" DataSourceID="sqldsCertificationType"
                                                            DataValueField="CertificationID" DataTextField="CertificationName" ValueType="System.String" TextFormatString="{0}">
                                                        </telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="sqldsCertificationType" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="SELECT [CertificationID], [CertificationCode],[CertificationName] FROM [Certifications] WHERE ([InstituteId] = @InstituteId)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlCerInstitute" Name="InstituteId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label38" runat="server" Text="Date" CssClass="form-label"></asp:Label>

                                                        <telerik:RadDatePicker ID="txtDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar7" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput7" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-2">

                                                        <asp:Label ID="Label47" runat="server" Text="Grade" CssClass="form-label" />

                                                        <telerik:RadTextBox ID="txtGrade" runat="server" CssClass="form-control form-control-lg" Width="100%"
                                                            Skin="Windows7" />

                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <asp:Button ID="btnCertiSave" runat="server" OnClick="btnCertiSave_Click" Text="Add" CssClass="btn btn-outline-success check-aspbtn"
                                                ToolTip="Add Information" />
                                            <div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table>

                            <tr>
                                <td>
                                    <asp:UpdateProgress ID="UpdateProgress5" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblCerticication" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <asp:HiddenField ID="hfCertificationID" runat="server" />
                        <div class="row" style="position: relative">
                            <div class="col table-scroll">

                                <dx:ASPxGridView ID="RadGrid2" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="CertificationID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="CertificationID" Caption="LoanTypeId" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="EmployeeCertificationID" Visible="false" VisibleIndex="1" Caption="Loan Type Code" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="InstituteCode" VisibleIndex="2" Caption="Institute Name" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="CertificationName" VisibleIndex="3" Caption="Certification Name" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Date" VisibleIndex="4" Caption="Date" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Grade" VisibleIndex="5" Caption="Grade" Width="30%" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep6" runat="server" Title="Membership Details">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label49" runat="server" Text="Membership" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlMembership" runat="server" Skin="Windows7" Width="100%"
                                                            OnDataBound="ddlMembership_DataBound">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label50" runat="server" Text="From Date" CssClass="form-label"></asp:Label>

                                                        <telerik:RadDatePicker ID="txtFromDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar8" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput8" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label51" runat="server" Text="To Date" CssClass="form-label"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtToDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label" Enabled="true"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar9" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput9" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                        <%--<dx:ASPxDateEdit ID="txtToDate" runat="server" Width="250px" CssClass="form-control form-control-lg" Enabled="false"></dx:ASPxDateEdit>--%>
                                                        <asp:CheckBox ID="cbIsToDate" runat="server" AutoPostBack="True" Text="Yes" CssClass="checkalign" Visible="false"
                                                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label52" runat="server" Text="Grade" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtmemGrade" runat="server" Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label53" runat="server" Text="Remark" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtmemremark" runat="server"
                                                            TextMode="MultiLine" Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <asp:Button ID="btnMembeSave" runat="server" OnClick="btnMembeSave_Click" Text="Add"
                                                ToolTip="Add Information" CssClass="btn btn-outline-success check-aspbtn" />
                                            <div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="Contact" width="75%">

                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblMembership" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div class="row" style="position: relative">
                            <div class="col table-scroll">

                                <dx:ASPxGridView ID="RadGrid3" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="EmployeeMembershipID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EmployeeMembershipID" Caption="EmployeeMembershipID" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Right" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SportID" Visible="false" VisibleIndex="1" Caption="SportID" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Right" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Membership" VisibleIndex="2" Caption="Membership" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Right" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="FromDate" VisibleIndex="3" Caption="FromDate" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Right" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="ToDate" VisibleIndex="4" Caption="ToDate" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Right" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Right" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Grade" VisibleIndex="5" Caption="Grade" Width="30%" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Right" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Right" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Remark" VisibleIndex="5" Caption="Remark" Width="30%" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Right" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">


                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Right" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>


                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click2" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter2" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep7" runat="server" Title="Language Ability Details">
                        <table class="Contact" width="75%">
                            <tr>
                                <td style="text-align: right">
                                    <asp:UpdateProgress ID="UpdateProgress6" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFluency" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label55" runat="server" CssClass="form-label" Text="Language Ability" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlLanguge" runat="server" DataSourceID="sqldsLanguage" DataTextField="LanguageName" DataValueField="LanguageID" OnDataBound="ddlLanguge_DataBound1"
                                                            Skin="Windows7" Width="100%">
                                                        </telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="sqldsLanguage" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="SELECT [LanguageID], [LanguageName] FROM [Languages]"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label56" runat="server" CssClass="form-label" Text="Reading" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlReading" runat="server" OnDataBound="ddlReading_DataBound1" Skin="Windows7" Width="100%">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Beginner" Value="1" />
                                                                <telerik:RadComboBoxItem Text="Average" Value="2" />
                                                                <telerik:RadComboBoxItem Text="Skilled" Value="3" />
                                                                <telerik:RadComboBoxItem Text="Specialist" Value="4" />
                                                                <telerik:RadComboBoxItem Text="Expert" Value="5" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label57" runat="server" CssClass="form-label" Text="Writing" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlWriting" runat="server" OnDataBound="ddlWriting_DataBound1" Skin="Windows7" Width="100%">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Beginner" Value="1" />
                                                                <telerik:RadComboBoxItem Text="Average" Value="2" />
                                                                <telerik:RadComboBoxItem Text="Skilled" Value="3" />
                                                                <telerik:RadComboBoxItem Text="Specialist" Value="4" />
                                                                <telerik:RadComboBoxItem Text="Expert" Value="5" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label58" runat="server" CssClass="form-label" Text="Speaking" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlSpeaking" runat="server" OnDataBound="ddlSpeaking_DataBound1" Skin="Windows7" Width="100%">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Beginner" Value="1" />
                                                                <telerik:RadComboBoxItem Text="Average" Value="2" />
                                                                <telerik:RadComboBoxItem Text="Skilled" Value="3" />
                                                                <telerik:RadComboBoxItem Text="Specialist" Value="4" />
                                                                <telerik:RadComboBoxItem Text="Expert" Value="5" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label59" runat="server" CssClass="form-label" Text="Listening" class="auto-style2"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlListening" runat="server" OnDataBound="ddlListening_DataBound1" Skin="Windows7" Width="100%">
                                                            <Items>
                                                                <telerik:RadComboBoxItem Text="Beginner" Value="1" />
                                                                <telerik:RadComboBoxItem Text="Average" Value="2" />
                                                                <telerik:RadComboBoxItem Text="Skilled" Value="3" />
                                                                <telerik:RadComboBoxItem Text="Specialist" Value="4" />
                                                                <telerik:RadComboBoxItem Text="Expert" Value="5" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <asp:Button ID="btnFluenSave" runat="server" OnClick="btnFluenSave_Click" Text="Add" CssClass="btn btn-outline-success check-aspbtn"
                                                ToolTip="Add Information" />
                                            <div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="row" style="position: relative">
                            <div class="col table-scroll">

                                <dx:ASPxGridView ID="RadGrid4" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="EmployeeMembershipID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="LanguId" Caption="LanguageID" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="LanguageID" Visible="false" VisibleIndex="1" Caption="LanguageID" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="LanguageName" VisibleIndex="2" Caption="Language Name" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Writing" VisibleIndex="3" Caption="Writing" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Speaking" VisibleIndex="4" Caption="Speaking" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Listening" VisibleIndex="5" Caption="Listening" Width="30%" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>

                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click3" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter3" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep8" runat="server" Title="Spouse's Details">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label62" runat="server" Text="First Name" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtSpousFirstName" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label63" runat="server" Text="Last Name" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtSpousLastName" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label64" runat="server" Text="Full Name" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtSpousFullName" runat="server" MaxLength="500"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label70" runat="server" Text="Date of Birth" CssClass="form-label"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtSpouseDateOfBirth" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar10" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput10" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>

                                                        <%-- <dx:ASPxDateEdit ID="txtSpouseDateOfBirth" runat="server" Width="250px" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label117" CssClass="form-label" runat="server" Text="Gender"></asp:Label>

                                                        <asp:RadioButtonList ID="rbGender" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label67" runat="server" Text="Contact No" CssClass="form-label"></asp:Label>
                                                        <telerik:RadTextBox ID="txtContactNo" runat="server" MaxLength="10"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label68" runat="server" Text="Spouse's Email" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtSpouceEmail" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server"
                                                            ControlToValidate="txtSpouceEmail" ErrorMessage="Invalid Email Address"
                                                            ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                            ValidationGroup="a" ForeColor="Red"></asp:RegularExpressionValidator>
                                                        <cc1:ValidatorCalloutExtender ID="RegularExpressionValidator1_ValidatorCalloutExtender"
                                                            runat="server" Enabled="True" TargetControlID="RegularExpressionValidator1">
                                                        </cc1:ValidatorCalloutExtender>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label65" runat="server" Text="Company" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtCompany" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label66" runat="server" Text="Designation" CssClass="form-label"></asp:Label>
                                                        <telerik:RadTextBox ID="txtDesignation" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>

                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <div>
                                                <asp:Button ID="btnSpouseSave" runat="server" OnClick="btnSpouseSave_Click" Text="Add"
                                                    ToolTip="Add Information" CssClass="btn btn-outline-success check-aspbtn" />
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div id="toastrContainer">
                            <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                            <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                        </div>
                        <table>
                            <tr>
                                <td style="text-align: right">
                                    <asp:UpdateProgress ID="UpdateProgress7" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSpouse" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="row" style="position: relative">
                            <div class="col table-scroll">

                                <dx:ASPxGridView ID="RadGrid5" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="EmployeeMembershipID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="SpouseID" Caption="SpouseID" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="FirstName" Visible="true" VisibleIndex="1" Caption="LanguageID" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="FullName" VisibleIndex="2" Visible="false" Caption="Full Name" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Company" VisibleIndex="3" Caption="Company" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Designation" VisibleIndex="4" Caption="Designation" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Left" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="ContactNo" VisibleIndex="5" Caption="ContactNo" Width="30%" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Email" VisibleIndex="5" Caption="Email" Width="30%" Visible="false" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Gender" VisibleIndex="5" Caption="Gender" Width="30%" Visible="false" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DateOfBirth" VisibleIndex="5" Caption="Date Of Birth" Width="30%" Visible="false" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click4" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter4" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep runat="server" Title="Children's Details" ID="DDD">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label72" CssClass="form-label" runat="server" Text="First Name" class="auto-style2"></asp:Label>
                                                        <telerik:RadTextBox ID="txtChildFName" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label73" CssClass="form-label" runat="server" Text="Last Name" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtChildLName" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label74" CssClass="form-label" runat="server" Text="Full Name" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtChildFullName" runat="server" MaxLength="50" TextMode="MultiLine" Height="40px"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label76" CssClass="form-label" runat="server" Text="Date of Birth" class="auto-style2"></asp:Label>

                                                        <telerik:RadDatePicker ID="txtChildDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar11" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput11" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>

                                                        <%--<dx:ASPxDateEdit ID="txtChildDate" runat="server" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label69" CssClass="form-label" runat="server" Text="Gender"></asp:Label>

                                                        <asp:RadioButtonList ID="RBChildGender" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <asp:Button ID="btnChildSave" runat="server" OnClick="btnChildSave_Click" Text="Add" CssClass="btn btn-outline-success check-aspbtn"
                                                ToolTip="Add Information" />
                                            <div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="Contact" width="75%">

                            <tr>
                                <td style="text-align: right">
                                    <asp:UpdateProgress ID="UpdateProgress8" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblChild" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div class="row" style="position: relative">
                            <div class="col table-scroll">

                                <dx:ASPxGridView ID="RadGrid6" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="ChildID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="ChildID" Caption="ChildID" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="FirstName" Visible="true" VisibleIndex="1" Caption="First Name" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="FullName" VisibleIndex="2" Caption="Full Name" Width="30%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="Gender" VisibleIndex="3" Caption="Gender" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="DateOfBirth" VisibleIndex="5" Caption="Date Of Birth" Width="30%" Visible="false" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click5" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter5" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>

                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep9" runat="server" Title="Extra Skills Details">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label61" runat="server" Text="Employee Skill" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlSkill" runat="server" AutoPostBack="True"
                                                            Skin="Windows7" Width="100%"
                                                            OnDataBound="ddlSkill_DataBound">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label78" runat="server" Text="Skill Grade" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlSkillGrade" runat="server" AutoPostBack="True"
                                                            Skin="Windows7" Width="100%"
                                                            OnDataBound="ddlSkillGrade_DataBound">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <asp:Button ID="btnSkillSave" runat="server" OnClick="btnSkillSave_Click" Text="Add" CssClass="btn btn-outline-success check-aspbtn"
                                                ToolTip="Add Information" />
                                            <div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <table class="Contact" width="75%">

                            <tr>
                                <td style="text-align: right">
                                    <asp:UpdateProgress ID="UpdateProgress9" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSkill" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div class="row" style="position: relative">
                            <div class="col table-scroll">

                                <dx:ASPxGridView ID="RadGrid7" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="EmployeeSkillID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EmployeeSkillID" Caption="EmployeeSkillID" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SkillID" Visible="false" VisibleIndex="1" Caption="SkillID" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SkillName" VisibleIndex="3" Caption="Skill Name" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SkillGradeID" VisibleIndex="5" Caption="SkillGradeID" Width="30%" Visible="false" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click6" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter6" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep10" runat="server" Title="Sports Skill Details">
                        <div class="row" style="background-color: ghostwhite">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-12">
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label80" runat="server" Text="Employee Sport" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlSport" runat="server" Width="100%"
                                                            DataSourceID="oobjsport" DataTextField="SportName"
                                                            DataValueField="SportID" OnDataBound="RadComboBox1_DataBound" Skin="Windows7">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                            <asp:Button ID="btnSportSave" runat="server" OnClick="btnSportSave_Click" Text="Add"
                                                ToolTip="Add Information" CssClass="btn btn-outline-success check-aspbtn" />

                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <table class="Contact" width="75%">
                            <tr>
                                <td style="width: 180px"></td>
                                <td>
                                    <asp:ObjectDataSource ID="oobjsport" runat="server"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSport"
                                        TypeName="HRM.HR.BLL.Sport">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="SportId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">
                                    <asp:UpdateProgress ID="UpdateProgress10" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                            </tr>
                            <tr>
                                <td style="text-align: right">&nbsp;</td>
                                <td></td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="lblSport" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                        </table>

                        <div class="row" style="position: relative">
                            <div class="col table-scroll">

                                <dx:ASPxGridView ID="RadGrid8" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue" Visible="false"
                                    KeyFieldName="EmployeeSkillID" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EmployeeSportActivityID" Caption="EmployeeSportActivityID" PropertiesEditType="TextBox" Visible="false" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SportID" Visible="false" VisibleIndex="1" Caption="SkillID" Width="40%" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataColumn FieldName="SportName" VisibleIndex="3" Caption="Sport Name" PropertiesEditType="TextBox" Width="20%" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
                                        </dx:GridViewDataColumn>
                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click7" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/trash.png" alt="delete group" /></asp:LinkButton>
                                            </DataItemTemplate>

                                            <HeaderStyle CssClass="headerStyle" Font-Bold="False"></HeaderStyle>

                                            <CellStyle HorizontalAlign="Center" CssClass="cellStyle"></CellStyle>
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
                                <dx:ASPxGridViewExporter ID="ASPxGridViewExporter7" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>

                    </asp:WizardStep>
                    <asp:WizardStep ID="WizardStep11" runat="server" Title="Profile Completion">

                        <table class="Contact" width="75%">
                            <tr>
                                <td style="width: 180px">
                                    <asp:Label ID="Label60" runat="server" ForeColor="Red"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                            </tr>

                            <tr>
                                <td></td>
                                <td style="text-align: left">
                                    <asp:UpdateProgress ID="UpdateProgress11" runat="server">
                                        <ProgressTemplate>
                                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                        </ProgressTemplate>
                                    </asp:UpdateProgress>
                                </td>
                                <td>&nbsp;
                                </td>
                            </tr>


                        </table>
                    </asp:WizardStep>
                </WizardSteps>
            </asp:Wizard>
            <table>
                <tr>
                    <td>
                        <telerik:RadNotification ID="RadNotification1" runat="server" Animation="Fade" AutoCloseDelay="6000"
                            Position="Center" Skin="Web20" Width="500px">
                            <NotificationMenu ID="NotificationMenu1">
                            </NotificationMenu>
                        </telerik:RadNotification>
                        <br />
                    </td>
                </tr>
            </table>
            <br />
        </div>
    </div>

    <table style="width: 100%;">

        <tr>
            <td></td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="Label25" Visible="false" runat="server" Text=""></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td colspan="2">&nbsp;
            </td>
        </tr>

        <tr>
            <td>&nbsp;
            </td>
            <td colspan="2">&nbsp;
            </td>
        </tr>
    </table>


    <%-- GS Devision Popup --%>

    <dx:ASPxPopupControl ID="GsDevisionPopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <div style="display: flex; justify-content: space-between">
                <h6 style="font-family: 'Source Sans Pro',sans-serif; font-size: 17px; font-weight: bold; color: white;">GS Division</h6>
                <asp:ImageButton runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" CssClass="btn-close-alignment" OnClick="Unnamed_Click1" />
            </div>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow">
                                <div class="row mt-4">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblbank" CssClass="form-label" runat="server">GS Code</asp:Label>
                                        <telerik:RadTextBox ID="txtgscode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblbranch" CssClass="form-label" runat="server">Name</asp:Label>
                                        <telerik:RadTextBox ID="txtgsname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btngsAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btngsAdd_Click" />
                                    <asp:Button ID="btngsupdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btngsupdate_Click" Enabled="false" />
                                    <asp:Button ID="btngscancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btngscancel_Click" />
                                    <asp:Button ID="btngsdelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btngsdelete_Click" Enabled="false" />

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="gvPopup" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="GET_GsDivisions">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="GSId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
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
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
    <asp:SqlDataSource ID="GET_GsDivisions" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="Emp_GetAllGsDivisions" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="hfgsId" runat="server" />

    <%-- Elect District Popup --%>

    <dx:ASPxPopupControl ID="ElectPopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <div style="display: flex; justify-content: space-between">
                <h6 style="font-family: 'Source Sans Pro',sans-serif; font-size: 17px; font-weight: bold; color: white;">Electoral District</h6>
                <asp:ImageButton runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" CssClass="btn-close-alignment" OnClick="Unnamed_Click2" />
            </div>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-radius: 10px">
                                <div class="row mt-4">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label84" CssClass="form-label" runat="server">Elect Code</asp:Label>
                                        <telerik:RadTextBox ID="txtelcode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label85" CssClass="form-label" runat="server">Elect District</asp:Label>
                                        <telerik:RadTextBox ID="txteldistrict" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btneladd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btneladd_Click" />
                                    <asp:Button ID="btnelupdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnelupdate_Click" Enabled="false" />
                                    <asp:Button ID="btnelcancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btnelcancel_Click" />
                                    <asp:Button ID="btneldelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btneldelete_Click" Enabled="false" />

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="gvelectoral" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="GET_electoral">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="ELId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
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
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
    <asp:SqlDataSource ID="GET_electoral" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="Emp_GetElectoralDsForgrid" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="hfElectoral" runat="server" />
    <%-- Division Popup --%>
    <dx:ASPxPopupControl ID="Divisianalpopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <div style="display: flex; justify-content: space-between">
                <h6 style="font-family: 'Source Sans Pro',sans-serif; font-size: 17px; font-weight: bold; color: white;">Divisional Secretariats</h6>
                <asp:ImageButton runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" CssClass="btn-close-alignment" OnClick="Unnamed_Click3" />
            </div>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-radius: 10px">
                                <div class="row mt-4">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label86" CssClass="form-label" runat="server">Division Code</asp:Label>
                                        <telerik:RadTextBox ID="txtdvcode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label122" CssClass="form-label" runat="server">Division Name</asp:Label>
                                        <telerik:RadTextBox ID="txtdvname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btndvAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btndvAdd_Click" />
                                    <asp:Button ID="btndvUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btndvUpdate_Click" Enabled="false" />
                                    <asp:Button ID="btndvCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btndvCancel_Click" />
                                    <asp:Button ID="btndvDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btndvDelete_Click" Enabled="false" />

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="gvdivision" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="dsdivisiongrid">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="DvId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click1" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
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
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
    <asp:SqlDataSource ID="dsdivisiongrid" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="Emp_Getdisionalsec" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>
    <asp:HiddenField ID="hfDvId" runat="server" />

    <dx:ASPxPopupControl ID="transportpopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <div style="display: flex; justify-content: space-between">
                <h6 style="font-family: 'Source Sans Pro',sans-serif; font-size: 17px; font-weight: bold; color: white;">Transport Route</h6>
                <asp:ImageButton runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" CssClass="btn-close-alignment" OnClick="Unnamed_Click4" />
            </div>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-radius: 10px">
                                <div class="row mt-4">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label123" CssClass="form-label" runat="server">Rout number</asp:Label>
                                        <telerik:RadTextBox ID="txtrtnumber" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label124" CssClass="form-label" runat="server">Rout Name</asp:Label>
                                        <telerik:RadTextBox ID="txtrtname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btntrAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btntrAdd_Click" />
                                    <asp:Button ID="btntrUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btntrUpdate_Click" Enabled="false" />
                                    <asp:Button ID="btntrCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btntrCancel_Click" />
                                    <asp:Button ID="btntrDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btntrDelete_Click" Enabled="false" />

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="transportgv" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="dstransportsroute">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TrId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Number" Caption="Number" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Route Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click2" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
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
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
    <asp:SqlDataSource ID="dstransportsroute" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="Emp_Gettransportroutforgv" SelectCommandType="StoredProcedure">
        <SelectParameters>
            <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                Type="Int32" />
        </SelectParameters>
    </asp:SqlDataSource>

    <asp:HiddenField ID="hfTrId" runat="server" />

    <dx:ASPxPopupControl ID="vaccinepopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <div style="display: flex; justify-content: space-between">
                <h6 style="font-family: 'Source Sans Pro',sans-serif; font-size: 17px; font-weight: bold; color: white;">Vaccination Details</h6>
                <asp:ImageButton runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" CssClass="btn-close-alignment" OnClick="Unnamed_Click5" />
            </div>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-radius: 10px">
                                <div class="row mt-4">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label126" CssClass="form-label" runat="server">Vacciene Code</asp:Label>
                                        <telerik:RadTextBox ID="txtvccode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label127" CssClass="form-label" runat="server">Vacciene Name</asp:Label>
                                        <telerik:RadTextBox ID="txtvcname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btnvcAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btnvcAdd_Click" />
                                    <asp:Button ID="btnvcUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnvcUpdate_Click" Enabled="false" />
                                    <asp:Button ID="btnvcCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btnvcCancel_Click" />
                                    <asp:Button ID="btnvcDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btnvcDelete_Click" Enabled="false" />

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="vaccienegv" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="VcId" Theme="MetropolisBlue" DataSourceID="dsvaccienefrgrid">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="VcId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Vacciene Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Vacciene Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click3" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
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
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
    <asp:SqlDataSource ID="dsvaccienefrgrid" runat="server"
        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
        SelectCommand="Emp_Gettvaccienefrvcgrid" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

    <asp:HiddenField ID="hfvcId" runat="server" />
</asp:Content>

