<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Reports_Common.aspx.cs" Inherits="HR_Reports_Reports_Common" MasterPageFile="~/HR/HR_MenuMasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="DevExpress.XtraReports.v14.2.Web, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%--<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik1" %>--%>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.13.110, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
<%--         <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.1/css/bootstrap.min.css" />--%>
         <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
         <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />
         <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/report.css" />
       <%--  <link rel="stylesheet" type="text/css" href="../../App_Themes/NewTheme/css/Common/form.css" />--%>

    <style>
        .btn:hover, .btn:focus, .btn.focus {
    color: white!important;
    text-decoration: none;
}
        .modal-header .close {
    padding: 1rem 1rem;
     margin: 0rem 0rem 0rem auto!important; 
}
        .btn-outline-secondary {
            color: none !important;
            border-color: none !important;
        }
        .btn-outline-warning {
            color: #ffc107;
            border-color: #ffc107 !important;
        }
        .btn-outline-dark {
            /* color: white; */
            border-color: #343a40 !important;
        }
        ContentPlaceHolder1_btnView:hover {
            color: red;
        }
.btn-outline-danger {
    color: #dc3545!important;
    border-color: #dc3545!important;
}
        .row {
     margin-right: 0px!important; 
     margin-left: 0px!important; 
}
/*        .btn-outline-secondary {
            color: #6c757d !important;
            border-color: #6c757d !important;
        }*/
        .modal {
            position: fixed;
            top: 168px;
            right: 40px;
            bottom: 0;
            left: 483px;
            z-index: 1050;
            display: none;
            overflow: hidden;
            -webkit-overflow-scrolling: touch;
            outline: 0;
        }
        .col-md-7.d-flex.justify-content-end {
            height: 30px;
        }
        .border {
            border: none !important;
            /* border: 1px solid #dee2e6!important; */
        }
        .modal-header {
            padding: 0px !important;
            border-bottom: 1px solid #e5e5e5;
        }

         
    </style>
</asp:Content>
 
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script type="text/javascript" src="../../App_Themes/NewTheme/script/form.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/2.1.1/jquery.min.js"></script>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script type="text/javascript" src="https://maxcdn.bootstrapcdn.com/bootstrap/3.4.1/js/bootstrap.min.js"></script>
    <script type="text/javascript">
        $('#myModal').on('shown.bs.modal', function () {
            $('#myInput').trigger('focus')
        }
    
        //$('#ddlActiveStatus').change(function () {
           
        //});

    </script>
   
    <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
        <div class="row">
            <div class="col head-container">
                <h4 class="header">Employee Details Reports </h4>

            </div>
        </div>
    </div>
    <div class="row" style="background-color: ghostwhite">
        <div class="col-md-12" style="border-top-color: black">
            <div class="border" style="border-radius: 10px">
                <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                    <div class="row">
                        <div class="col-md-4">
                            <asp:Button ID="btnOrganizationSelect" runat="server" CssClass="Buttons" Text="Select Department"
                                BackColor="WhiteSmoke" BorderColor="WindowFrame" ForeColor="Black" Font-Size="14px" />
                            <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                                CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="Panel1"
                                TargetControlID="btnOrganizationSelect">
                            </cc1:ModalPopupExtender>
                            &nbsp;&nbsp;
                            <asp:Label ID="lblOrganization" runat="server"></asp:Label>
                                            <asp:HiddenField ID="hfOrganizationStructure" runat="server" />
                            <asp:HiddenField ID="hfPreviousOrgId" runat="server" />
                        </div>
                        <div class="col-md-4">
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
                        <div class="col-md-4">
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
                    </div>
                    <div class="row">
                        <div class="col-md-4">
                            <dx:ASPxComboBox ID="ddlActiveStatus" AutoPostBack="true" runat="server" DropDownStyle="DropDownList" OnTextChanged="ddlActiveStatus_TextChanged"
                                ValueType="System.String" TextFormatString="{0}" Width="150px" Enabled="true" SelectedIndex="0" EnableSynchronization="False">
                                <Items>
                                    <dx:ListEditItem Text="All Employee" Value="0" />
                                    <dx:ListEditItem Text="Active Employee" Value="1" />
                                    <dx:ListEditItem Text="Inactive Employee" Value="2" />
                                    <dx:ListEditItem Text="Resigned Employee" Value="3" />
                                    <dx:ListEditItem Text="Terminated Employee" Value="4" />
                                </Items>
                                <%--                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }" />--%>
                            </dx:ASPxComboBox>
                        </div>
                        <div class="col-md-4">
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
                        <div class="col-md-4">
                            <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Employee"></asp:Label>
                            <dx:ASPxComboBox ID="ddlEmployee" runat="server" Width="100%" DataSourceID="sqldsEmployee" AutoPostBack="true" BackColor="Transparent"
                                TextField="EmployeeReportViewName" ValueField="EmployeeId" Skin="Windows7" NullText="Employee's Name" CssClass="form-control form-control-lg">
                            </dx:ASPxComboBox>

                            <asp:SqlDataSource ID="sqldsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                SelectCommand="COM_GetEmployeeList" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="radcbJobCategory" Name="CatID" DefaultValue="0" Type="Int32" />
                                    <asp:ControlParameter ControlID="hfOrganizationStructure" Name="OrgStructureID" PropertyName="Value" Type="Int32" />
                                    <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                    <asp:ControlParameter ControlID="ddlActiveStatus" Name="ActiveStatus" Type="Int32" />
                                </SelectParameters>
                            </asp:SqlDataSource>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-5">
                            <asp:Label ID="lblApprove" CssClass="form-label" runat="server" Text="Report Name"></asp:Label>
                            <dx:ASPxComboBox ID="ASPxComboBox1" runat="server" DropDownStyle="DropDownList" ValueType="System.String" CssClass="form-control form-control-lg"
                                TextFormatString="{0}" Width="100%" OnTextChanged="ASPxComboBox1_TextChanged" AutoPostBack="true">
                                <Items>
                                    <dx:ListEditItem Text="Employee Details" Value="1" />
                                    <dx:ListEditItem Text="New Employee Details" Value="2" />
                                    <dx:ListEditItem Text="Active Employee Details" Value="3" />
                                    <dx:ListEditItem Text="Resigned Employee Details" Value="4" />
                                    <dx:ListEditItem Text="Inactive Employee Details" Value="5" />
                                    <dx:ListEditItem Text="Terminated Employee Details" Value="6" />
                                </Items>
                            </dx:ASPxComboBox>


                        </div>
                  <%--      <div class="col-md-7 d-flex justify-content-end ">
                            <button type="button" class="btn btn-primary" data-toggle="modal" data-target="#exampleModal">
                                Search Criteria
                            </button>
                        </div>--%>
                    </div>

                    <div class="d-flex justify-content-end mr-lg-5 mt-3" style="grid-gap: 15px">
                        <div>
                            <asp:Button ID="btnView" runat="server" OnClick="btnView_Click" Text="View"
                                ValidationGroup="a" Skin="WebBlue" CssClass="btn btn-outline-warning check-aspbtn" />
                        </div>
                        <div>
                            <asp:Button ID="btnReset" runat="server" OnClick="btnReset_Click"
                                Text="Reset" Skin="WebBlue" CssClass="btn btn-outline-danger check-aspbtn" />
                        </div>
                        <div>
                            <asp:Button ID="btnClear" runat="server" OnClick="btnClear_Click1" Text="Clear"
                                Skin="WebBlue" CssClass="btn btn-outline-secondary check-aspbtn" />
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
                    <%--                    <div class="ContentBlock">
                        
                            <asp:Panel ID="Panel1" runat="server" CssClass="popstyle popstyle-2" ScrollBars="Vertical">
                                <div class="style1">
                                    <div style="float: right">
                                        <asp:ImageButton ID="btnClosePopup" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                                    </div>
                                    <div>
                                        <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>

                    </div>--%>
                </div>
            </div>
        </div>
    </div>

 <!-- Modal -->
    <div  class="modal fade" id="exampleModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
        <div class="modal-dialog" role="document">
        
            <div class="modal-content">
                <div class="modal-header">
                    <h5 class="modal-title" id="exampleModalLabel"></h5>
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span>
                    </button>
                </div>
                <div class="modal-body">
                    <div class="d-flex justify-content-end">
                        <contentcollection>
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border mt-2" style="border-radius: 10px">
                             
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Status"></asp:Label>
                                    <%--     <dx:ASPxComboBox ID="ddlActiveStatus" AutoPostBack="true" runat="server" DropDownStyle="DropDownList" CssClass="form-control form-controllg" 
                                                ValueType="System.String" TextFormatString="{0}" Width="150SelectedIndex="0"px"  OnTextChanged="ddlActiveStatus_TextChangd1" >
                                                <Items>
                                                 <dx:ListEditItem Text="All Employee" Value="0" />
                                                <dx:ListEditItem Text="Active Employee" Value="1" />
                                                  <dx:ListEditItem Text="Inactive Employee" Value="2" />
                                                 <dx:ListEditItem Text="Terminated Employee" Value="4" />
                                                </Items>
                                          </dx:ASPxComboBox>--%>

                                           <%-- <dx:ASPxTextBox ID="ASPxTextBox1" runat="server" Text="Choose the country" ClientInstanceName="tb"></dx:ASPxTextBox>
                                            <dx:ASPxComboBox ID="ASPxComboBox3" runat="server" AutoPostBack="true" processOnServer="true">
                                                <Items>
                                                    <dx:ListEditItem Text="Afghanistan" Value="1" />
                                                    <dx:ListEditItem Text="sss" Value="1" />
                                                    <dx:ListEditItem Text="ddd" Value="1" />
                                                   
                                                </Items>
                                                <ClientSideEvents SelectedIndexChanged="our_function" />
                                            </dx:ASPxComboBox>--%>

                           <%--                 <dx:ASPxComboBox runat="server" ID="CmbCountry" DropDownStyle="DropDownList" IncrementalFilteringMode="StartsWith"
                                                TextField="CountryName" ValueField="CountryName" Width="100%" DataSourceID="CountriesDataSource"
                                                EnableSynchronization="False">
                                                <ClientSideEvents SelectedIndexChanged="function(s, e) { OnCountryChanged(s); }" />
                                            </dx:ASPxComboBox>

                                            <dx:ASPxComboBox runat="server" ID="CmbCity" ClientInstanceName="cmbCity" OnCallback="CmbCity_Callback"
                                                DropDownStyle="DropDown" TextField="CityName" Width="100%"
                                                ValueField="CityName" IncrementalFilteringMode="StartsWith" EnableSynchronization="False">
                                                <ClientSideEvents EndCallback=" OnEndCallback" />
                                            </dx:ASPxComboBox>--%>

                            
                                        </div>
                                        <div class="col-md-6">
                                        
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                

                                        <div class="col-md-6" style="display: none">
                                            <asp:Label ID="lblActiveStatus" runat="server" CssClass="form-label" Text="Status" Font-Names="Tahoma"></asp:Label>

                                            <dx:ASPxComboBox ID="ASPxComboBox2" AutoPostBack="true" runat="server" DropDownStyle="DropDownList" OnTextChanged="ddlActiveStatus_TextChanged"
                                                ValueType="System.String" TextFormatString="{0}" Width="150px" Enabled="false" SelectedIndex="0">
                                                <Items>
                                                    <dx:ListEditItem Text="All Employee" Value="0" />
                                                    <dx:ListEditItem Text="Active Employee" Value="1" />
                                                    <dx:ListEditItem Text="Inactive Employee" Value="2" />
                                                    <dx:ListEditItem Text="Resigned Employee" Value="3" />
                                                    <dx:ListEditItem Text="Terminated Employee" Value="4" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </div>
                                    </div>
                                    <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                        <div>
                                            <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Set"
                                                ValidationGroup="a" CssClass="btn btn-outline-success check-aspbtn" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </contentcollection>
                      </div>
                    <%--   <div class="modal-footer">
                    <button type="button" class="btn btn-secondary" data-dismiss="modal">Close</button>
                    <button type="button" class="btn btn-primary">Save changes</button>
                </div>--%>
                </div>
        </div>
    </div>
       


    </div>
        </div>
</asp:Content>
