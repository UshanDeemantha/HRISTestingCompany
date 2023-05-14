<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewEmployeeAdd.ascx.cs" Inherits="HR_Employee_UserControls_NewEmployeeAdd" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/HR/Organization/OrganizationStucture.ascx" TagName="OrganizationStucture" TagPrefix="uc5" %>
<%@ Register Src="EmployeeSummaryList.ascx" TagName="EmployeeSummaryList" TagPrefix="uc3" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc2" %>
<%@ Register Src="~/HR/Designation/DesignationStructureControl.ascx" TagName="DesignationStructureControl" TagPrefix="uc15" %>
<%@ Register Src="~/HR/Organization/OrganizationStucture.ascx" TagName="OrganizationStucture" TagPrefix="uc1" %>

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
        width: 98px;
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

    .col-md-12 {
        margin: 15px 5px 15px 20px;
        border-radius: 8px;
        overflow: hidden;
        transition: height 1s ease-in;
        padding-bottom: 34px;
    }

    td.wizard-linkback {
        margin-left: 40px;
    }

    label {
        margin-top: 3px;
        margin-left: 15px;
        font-size: 14px;
    }
</style>
<link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />
        <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
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
                                                <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Employee Code" class="auto-style2"></asp:Label>
                                                <telerik:RadTextBox ID="txtEmployeeCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="150px" Enabled="false"
                                                    Skin="Windows7" />
                                            </div>
                                        </div>
                                        <div class="row mt-4">
                                            <div class="col">
                                                <asp:Label ID="Label8" runat="server" Text="EPF Number" class="auto-style2" CssClass="form-label"></asp:Label>

                                                <telerik:RadTextBox ID="txtEPFNo" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="150px" OnTextChanged="txtEPFNo_TextChanged"
                                                    AutoPostBack="true" Enabled="true"
                                                    Skin="Windows7" Visible="true" />
                                            </div>
                                        </div>
                                        <div class="row mt-4">
                                            <div class="col">
                                                <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="First Name" class="auto-style2"></asp:Label>
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
                                                <asp:Image ID="Image1" runat="server" ImageUrl="~/App_Themes/Common/DefaultEmployee.png" />

                                                <div id=" fileupload">
                                                    <label class="choose">
                                                        <asp:FileUpload ID="fuEmployeeImage" runat="server" Style="margin-left: 9px" />
                                                    </label>
                                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="row mt-4">
                                    <div class="col">
                                        <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="Last Name" class="auto-style2"></asp:Label>

                                        <telerik:RadTextBox ID="txtLastName" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="200px"
                                            Skin="Windows7" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtLastName"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col">
                                        <asp:Label ID="Label11" CssClass="form-label" runat="server" Text="Initials" class="auto-style2"></asp:Label>

                                        <telerik:RadTextBox ID="txtNameInitials" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="200px"
                                            Skin="Windows7" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtNameInitials"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>

                                </div>
                                <div class="row mt-3">
                                    <div class="col">
                                        <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Full Name" class="auto-style2"></asp:Label>

                                        <telerik:RadTextBox ID="txtFullName" CssClass="form-control form-control-lg" runat="server"
                                            Skin="Windows7" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtFullName"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col">
                                        <asp:Label ID="Label12" CssClass="form-label" runat="server" Text="Call Name" class="auto-style2"></asp:Label>

                                        <telerik:RadTextBox ID="txtCallName" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="100%"
                                            Skin="Windows7" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtCallName"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label13" CssClass="form-label" runat="server" Text="Payroll Act" class="auto-style2"></asp:Label>

                                        <telerik:RadComboBox ID="radCmbPayrollAct" runat="server" Width="100%" MaxHeight="80px" Skin="Windows7" EmptyMessage="Choose an Act">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="Shop & Office" Text="Shop & Office" />
                                                <telerik:RadComboBoxItem Value="Wages Board" Text="Wages Board" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radCmbPayrollAct"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label14" CssClass="form-label" runat="server" Text="Cost Center" class="auto-style2"></asp:Label>

                                        <telerik:RadComboBox ID="radCmbCostCenter" runat="server" Width="100%" MaxHeight="80px" Skin="Windows7" EmptyMessage="Choose a Cost Center"
                                            DataSourceID="sqlDSCostCenter" DataTextField="CompanyName" DataValueField="CompanyID">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="radCmbCostCenter"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                        <asp:SqlDataSource ID="sqlDSCostCenter" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                            SelectCommand="select CompanyID,CompanyName from CompanyProfile"></asp:SqlDataSource>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label15" CssClass="form-label" runat="server" Text="Direct/Indirect" class="auto-style2"></asp:Label>

                                        <telerik:RadComboBox ID="radCmbDirectIndirect" runat="server" Width="100%" MaxHeight="80px" Skin="Windows7" EmptyMessage="Choose Direct/Indirect">
                                            <Items>
                                                <telerik:RadComboBoxItem Value="Direct" Text="Direct" />
                                                <telerik:RadComboBoxItem Value="Indirect" Text="Indirect" />
                                            </Items>
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radCmbDirectIndirect"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label16" CssClass="form-label" runat="server" Text="Occupation Grade" class="auto-style2"></asp:Label>

                                        <telerik:RadTextBox ID="txtOccupationGrade" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="100%" Skin="Windows7" Visible="true" />

                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label17" CssClass="form-label" runat="server" Text="Job Catogory" class="auto-style2"></asp:Label>

                                        <telerik:RadComboBox ID="ddlJobCatogory" runat="server" DataSourceID="odsJobCategorys"
                                            DataTextField="JobCategoryForCombo" Width="100%" MaxHeight="80px" DataValueField="JobCategoryID"
                                            Skin="Windows7" OnDataBound="ddlJobCatogory_DataBound">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="ddlJobCatogory"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label18" CssClass="form-label" runat="server" Text="Job Grade" class="auto-style2"></asp:Label>

                                        <telerik:RadComboBox ID="ddlJobGrade" runat="server" DataSourceID="odsJobGrads" DataTextField="JobGradeForCombo"
                                            DataValueField="JobGradeID" Width="100%" MaxHeight="80px" OnDataBound="ddlJobGrade_DataBound"
                                            Skin="Windows7">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="ddlJobGrade"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label19" CssClass="form-label" runat="server" Text="Employment Type" class="auto-style2"></asp:Label>

                                        <telerik:RadComboBox ID="ddlEmployementType" runat="server" AppendDataBoundItems="True" AutoPostBack="true"
                                            DataSourceID="odsEmploymentTypes" Width="100%" DataTextField="EmploymentTypeForCombo"
                                            DataValueField="EmploymentTypeID" OnDataBound="ddlEmployementType_DataBound" OnSelectedIndexChanged="ddlEmployementType_SelectedIndexChanged"
                                            Skin="Windows7">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator8" runat="server" ControlToValidate="ddlEmployementType"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label20" CssClass="form-label" runat="server" Text="Employment Grade" class="auto-style2"></asp:Label>

                                        <telerik:RadComboBox ID="ddlEmploymentGrade" runat="server" Width="100%" MaxHeight="80px" DataSourceID="odsEmploymentGrades"
                                            DataTextField="EmploymentGradeForCombo" DataValueField="EmploymentGradeID" OnDataBound="ddlEmploymentGrade_DataBound"
                                            Skin="Windows7">
                                        </telerik:RadComboBox>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator10" runat="server" ControlToValidate="ddlEmploymentGrade"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="tdlblFromDate" CssClass="form-label" runat="server" Text="From Date" class="auto-style2" Visible="false"></asp:Label>
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
                                        <asp:Label ID="tdlblToDate" CssClass="form-label" runat="server" Text="To Date" class="auto-style2" Visible="false"></asp:Label>
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
                                        <asp:Label ID="Label21" CssClass="form-label" runat="server" Text="B Card Availability" class="auto-style2"></asp:Label>
                                        <asp:RadioButtonList ID="rbEmpCard" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                            <asp:ListItem Selected="True">Yes</asp:ListItem>
                                            <asp:ListItem>No</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 25px">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label22" CssClass="form-label" runat="server" Text="Gender"></asp:Label>

                                        <asp:RadioButtonList ID="rblGender" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                            <asp:ListItem>Female</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label23" CssClass="form-label" runat="server" Text="Status" class="auto-style2"></asp:Label>

                                        <asp:RadioButtonList ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                            <asp:ListItem Selected="True" Value="0">Single</asp:ListItem>
                                            <asp:ListItem Value="1">Married</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 23px">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Date Of Birth" class="auto-style2"></asp:Label>
                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Culture="en-US" Width="100%" OnSelectedDateChanged="txtDateOfBirth_SelectedDateChanged"
                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label" AutoPostBack="true"
                                            Skin="Windows7">
                                            <Calendar ID="Calendar6" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                            </Calendar>
                                            <DateInput ID="DateInput6" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                runat="server">
                                            </DateInput>
                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                        </telerik:RadDatePicker>

                                    </div>
                                </div>
                                <div class="row" style="margin-top: 9px">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label24" CssClass="form-label" runat="server" Text="Recruitment Date" class="auto-style2"></asp:Label>
                                        <telerik:RadDatePicker ID="txtRecruitementDate" runat="server" Culture="en-US" Width="100%"
                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
                                            Skin="Windows7">
                                            <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                            </Calendar>
                                            <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                runat="server">
                                            </DateInput>
                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                        </telerik:RadDatePicker>
                                        <%--<dx:ASPxDateEdit ID="txtRecruitementDate" runat="server" Width="200px" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label25" CssClass="form-label" runat="server" Text="Retirement Date" class="auto-style2"></asp:Label>
                                        <telerik:RadDatePicker ID="txtRetirementDate" runat="server" Culture="en-US" Width="100%" Enabled="false"
                                            FocusedDate="2022-01-01" MaxDate="2999-12-31" MinDate="1900-01-01" CssClass="form-label"
                                            Skin="Windows7">
                                            <Calendar ID="Calendar7" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                            </Calendar>
                                            <DateInput ID="DateInput7" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                runat="server">
                                            </DateInput>
                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                        </telerik:RadDatePicker>

                                        <%--  <dx:ASPxDateEdit ID="txtRetirementDate" runat="server" Width="200px" CssClass="form-control form-control-lg" Enabled="false"></dx:ASPxDateEdit>--%>
                                    </div>
                                </div>
                                <div class="row" style="margin-top: 10px">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblConfirmationdate" CssClass="form-label" runat="server" Text="Confirmation Date" class="auto-style2"></asp:Label>
                                        <telerik:RadDatePicker ID="txtConfirmationDate" runat="server" Culture="en-US" Width="100%"
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
                                        <%-- <dx:ASPxDateEdit ID="txtConfirmationDate" runat="server" Width="100%" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label26" runat="server" Text="EPF Entitle Date" class="auto-style2" CssClass="form-label"></asp:Label>
                                        <telerik:RadDatePicker ID="txtEPFNoDate" runat="server" Culture="en-US" Width="100%"
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
                                    </div>
                                </div>
                                <div class="row mb-3" style="margin-top: 12px">
                                    <div class="col">
                                        <asp:Label ID="Label10" CssClass="form-label" runat="server">Designation</asp:Label>

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
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="col -md-6">
                                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Employee Status"></asp:Label><br />

                                    <asp:CheckBox ID="cbIsActive" runat="server" OnCheckedChanged="cbIsResign_CheckedChanged"
                                        Text="Is Active" TextAlign="Right" Checked="True" AutoPostBack="True" />
                                    <asp:RadioButtonList ID="rblInactiveStatus" runat="server" DataSourceID="odsInactiveEmployeeStatus" AutoPostBack="true"
                                        DataTextField="StatusName" DataValueField="StatusId" Visible="False" OnSelectedIndexChanged="rblInactiveStatus_SelectedIndexChanged">
                                    </asp:RadioButtonList>
                                    <asp:ObjectDataSource ID="odsInactiveEmployeeStatus" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetInactiveEmployeeStatus" TypeName="HRM.Common.BLL.CompanyProfile">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="StatusId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                                <div class="col-md-6">

                                    <asp:Label ID="lblInactiveDate" runat="server" Text="Changed Date" CssClass="form-label" Visible="False"></asp:Label>

                                    <telerik:RadDatePicker ID="txtResignDate" runat="server" Culture="en-US" Width="100%"
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

                                </div>

                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="col-md-6">
                                    &nbsp;
                                </div>
                                <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                    <asp:Panel ID="btnPanelUpdate" runat="server" Width="233px">
                                        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn btn-outline-success check-aspbtn"
                                            ValidationGroup="Add" />
                                        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="btn btn-outline-danger check-aspbtn"
                                            OnClientClick="return confirm('Are you sure you want to Delete this Record?');" />
                                        <cc1:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server"
                                            ConfirmText="Are you sure you want to delete ?" Enabled="True"
                                            TargetControlID="btnDelete">
                                        </cc1:ConfirmButtonExtender>
                                        <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn btn-outline-secondary check-aspbtn" />
                                    </asp:Panel>
                                </div>
                            </div>


                        </div>
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
        <table class="Contact" style="width: 100%">
            <tr>
                <td>&nbsp;
                </td>
                <td>&nbsp;
                                 
                                <telerik:RadNotification ID="RadNotification1" runat="server" Animation="Fade" AutoCloseDelay="6000"
                                    Position="Center" Skin="Web20" Width="500px">
                                    <NotificationMenu ID="TitleMenu">
                                    </NotificationMenu>
                                </telerik:RadNotification>
                    <br />
                </td>
            </tr>

            <tr>
                <td>&nbsp;
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                <ProgressTemplate>
                                    <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px;" />
                                </ProgressTemplate>
                            </asp:UpdateProgress>
                </td>
                <td>

                    <asp:Panel ID="btnPanel" runat="server" Width="174px" Visible="False">
                        <telerik:RadButton ID="btnApprove" runat="server" OnClientClick="return confirm('Are you sure you want to Approve this Record?');"
                            Text="Approve" Skin="WebBlue" />
                        <cc1:ConfirmButtonExtender ID="btnApprove_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to Approve this Record ?"
                            Enabled="True" TargetControlID="btnApprove">
                        </cc1:ConfirmButtonExtender>
                        &nbsp;&nbsp;<telerik:RadButton ID="btnReject" runat="server" OnClientClick="return confirm('Are you sure you want to Reject this Record?');"
                            Text="Reject" Skin="WebBlue" />
                        <cc1:ConfirmButtonExtender ID="btnReject_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to Reject this Record ?"
                            Enabled="True" TargetControlID="btnReject">
                        </cc1:ConfirmButtonExtender>
                    </asp:Panel>
                    <br />
                    <asp:LinkButton ID="lbtnModify" runat="server"
                        OnClick="btnUpdatePersonalInfoEditRequest_Click"
                        PostBackUrl='<%# "../Employee/EditEmployee.aspx?EmployeeId="+Eval("EmployeeID") %>'
                        Visible="False">Personal Info Change Request is Pending.</asp:LinkButton>
                </td>
                <td>&nbsp;
                </td>
            </tr>

            <tr>
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
            </tr>
            <tr>
                <asp:ObjectDataSource ID="odsbranch" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetBranch" TypeName="HRM.Common.BLL.Reference">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="BranchId" Type="Int32" />
                        <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
            </tr>
        </table>
        </td>
                <td></td>
        </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>&nbsp;
                             <asp:ObjectDataSource ID="odsEmploymentGrades" runat="server" OldValuesParameterFormatString="original_{0}"
                                 SelectMethod="GetEmploymentGrade" TypeName="HRM.Common.BLL.Employee">
                                 <SelectParameters>
                                     <asp:Parameter DefaultValue="0" Name="EmploymentGradeId" Type="Int32" />
                                 </SelectParameters>
                             </asp:ObjectDataSource>
                            </td>
                            <td>&nbsp;
                               <asp:ObjectDataSource ID="odsEmploymentTypes" runat="server" OldValuesParameterFormatString="original_{0}"
                                   SelectMethod="GetEmploymentType" TypeName="HRM.Common.BLL.Employee">
                                   <SelectParameters>
                                       <asp:Parameter DefaultValue="0" Name="EmploymentTypeId" Type="Int32" />
                                   </SelectParameters>
                               </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="hfCompanyId" runat="server" />
                                <asp:HiddenField ID="hfDesignationId" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
      <div>
          <asp:Panel ID="Panel1" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical">
              <div class="style1">
                  <div style="float: right; margin-top: -10px;">
                      <asp:LinkButton ID="btnClosePopup" runat="server" OnClick="btnClosePopup_Click"> <img src="../../../App_Themes/NewTheme/img/close.png" /></asp:LinkButton>
                  </div>
                  <div id="org-stucture">
                      <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                  </div>
              </div>
          </asp:Panel>
      </div>

    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="btnUpdate" />
    </Triggers>

</asp:UpdatePanel>



