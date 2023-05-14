<%@ Page Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeExcelUpdate.aspx.cs" Inherits="HR_Employee_EmployeeExcelUpdate"   %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/HR/Organization/OrganizationStucture.ascx" TagName="OrganizationStucture"
    TagPrefix="uc1" %>

<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        input.ruButton.ruBrowse {
    display: none;
}
        input#ctl00_ContentPlaceHolder1_radUploadEmpExcelTextBox0 {
    display: none;
}
        input#ctl00_ContentPlaceHolder1_radUploadEmpExcelfile0 {
    width: 222px;
    margin-left: -49px;
}
        input#ctl00_ContentPlaceHolder1_cmbempColumn_Header_RadTextBox1 {
    width: 100%;
}
        input#ctl00_ContentPlaceHolder1_RadComboBox2_Header_RadTextBox1 {
    width: 100%;
}
        .rcbCheckAllItems {
    font-size: initial;
    background-color: white;
    margin-left: -1px;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    
     <ContentTemplate>
     <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>

  <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />

                <script type="text/javascript">
                    var $T = Telerik.Web.UI;
                    function OnLoad(sender, args) {
                        // #region Add ComboBox reference to TextBox
                        function getComboBoxReference() {
                            var comboBox = $telerik.$(sender.get_element()).closest(".RadComboBox")[0].control;
                            //add a reference of the containing combo box to the RadTextBox instance
                            sender.__containingComboBox = comboBox;

                            //add a reference of the RadTextBox instance to the containing combo box
                            comboBox.__filterTextBox = sender;

                            Sys.Application.remove_load(getComboBoxReference);
                        }
                        Sys.Application.add_load(getComboBoxReference);
                        // #endregion

                        $telerik.$(sender.get_element()).on("keyup", function (e) {
                            sender.__containingComboBox.highlightAllMatches(sender.get_textBoxValue());
                        })
                    }

                    function OnClientLoadComboBox(sender, args) {
                        var originalFunction = $T.RadComboBox.prototype.highlightAllMatches;
                        // override the highlightAllMatches only on this RadComboBox instance
                        sender.highlightAllMatches = function (text) {
                            this.set_filter($T.RadComboBoxFilter.Contains);
                            originalFunction.call(this, text);
                            this.set_filter($T.RadComboBoxFilter.None);
                        };
                    }

                    function OnClientDropDownClosing(sender, args) {
                        sender.get_items().forEach(function (item) {
                            item.clearEmTags();
                        });

                        sender.setAllItemsVisible(true);
                        sender.__filterTextBox.clear();
                    }
                </script>

    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server">
          <asp:HiddenField ID="hfEmloyeeId" runat="server" />
        
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Export Employee's Excel Data</h4>
                   
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="lblbank" CssClass="form-label" runat="server">Company</asp:Label>
                                    <dx:ASPxComboBox ID="dxPopcompany" CssClass="form-control form-control-lg" DataSourceID="objcompany"
                                        required="" Width="100%" runat="server" DropDownStyle="DropDownList" TextField="CompanyName" OnSelectedIndexChanged="dxPopcompany_SelectedIndexChanged"
                                        ValueType="System.String" ValueField="CompanyID" TextFormatString="{0}" MaxLength="50" AutoPostBack="true">

                                        <InvalidStyle BackColor="LightPink" />
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="objcompany" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                        SelectCommand="HR_GetCompaniesForExcelExport" SelectCommandType="StoredProcedure">
                                        <%--<SelectParameters>
                                                              <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                Type="Int32" />
                                                        </SelectParameters>--%>
                                    </asp:SqlDataSource>
                                </div>
                                <div class="col-md-6">
                                    <div class="row m-0">
                                        <div class="col-md-6 pr-0" style="margin-top: 22px">
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
                                            <asp:Label ID="lbldes" CssClass="form-label" runat="server">Designation</asp:Label>
                                            <dx:ASPxComboBox ID="cmbDesignation" runat="server" DataSourceID="GetDesignationforCombo" AutoPostBack="true" OnSelectedIndexChanged="cmbDesignation_SelectedIndexChanged" OnTextChanged="cmbDesignation_TextChanged" TextField="DesignationName"
                                                ValueField="DesignationID" Width="100%" CssClass="form-control form-control-lg">
                                            </dx:ASPxComboBox>
                                            <asp:SqlDataSource ID="GetDesignationforCombo" runat="server"
                                                ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                SelectCommand="Emp_GetDesignationForCombo" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                        </div>
                                    </div>
                              
                                </div>
                            </div>

                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Employees" />
                                    <telerik:RadComboBox ID="RadComboBox2" runat="server" CheckBoxes="true" CheckedItemsTexts="DisplayAllInInput" OnClientLoad="OnClientLoadComboBox" AutoPostBack="false"
                                        DataTextField="Names" DataValueField="EmployeeId" Width="100%" Enabled="true" EmptyMessage="<< Select Employee Name >>" EnableCheckAllItemsCheckBox="true">
                                        <HeaderTemplate>
                                            <telerik:RadTextBox runat="server" ID="RadTextBox1" Width="100%" ClientEvents-OnLoad="OnLoad">
                                            </telerik:RadTextBox>
                                        </HeaderTemplate>
                                    </telerik:RadComboBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="lblbranch" CssClass="form-label" runat="server">Status</asp:Label>
                                    <dx:ASPxComboBox ID="dxPopstatus" CssClass="form-control form-control-lg" DataSourceID="objstatus"
                                        required="" Width="100%" runat="server" DropDownStyle="DropDownList"
                                        ValueType="System.String" ValueField="StatusId" TextField="StatusName" TextFormatString="{0}" MaxLength="50">

                                        <InvalidStyle BackColor="LightPink" />
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="objstatus" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                        SelectCommand="HR_GetStatusForExcelExport" SelectCommandType="StoredProcedure">
                                        <%--<SelectParameters>
                                            <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                             Type="Int32" />
                                            </SelectParameters>--%>
                                    </asp:SqlDataSource>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label3" runat="server" Text="Column Name" CssClass="form-label"></asp:Label>
                                    <telerik:RadComboBox ID="cmbempColumn" runat="server" CheckBoxes="true" CheckedItemsTexts="DisplayAllInInput" OnClientLoad="OnClientLoadComboBox" AutoPostBack="false"
                                        DataTextField="ColumnName" DataValueField="Value" Width="250px" Height="25px" Enabled="true" EmptyMessage="<< Select Column Name >>" EnableCheckAllItemsCheckBox="true" DataSourceID="dsGetColumnName">
                                        <HeaderTemplate>
                                            <telerik:RadTextBox runat="server" ID="RadTextBox1" Height="100%" Width="100%" ClientEvents-OnLoad="OnLoad">
                                            </telerik:RadTextBox>
                                        </HeaderTemplate>
                                       
                                    </telerik:RadComboBox>
                                    <asp:SqlDataSource ID="dsGetColumnName" runat="server"
                                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                        SelectCommand="Emp_GetTableColumnData" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

                                </div>
                            </div>

                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label4" runat="server" Text="Upload file" CssClass="form-label"/>
                                    <telerik:RadUpload ID="radUploadEmpExcel" runat="server" InputSize="50" MaxFileInputsCount="1"
                                        OverwriteExistingFiles="True" Skin="WebBlue" MaxFileSize="5242880" TargetFolder="~/Payroll/RawDataUploads/"
                                        OnFileExists="radUploadEmpExcel_FileExists" ControlObjectsVisibility="None" AllowedFileExtensions=".xls,.xlsx">
                                    </telerik:RadUpload>
                                </div>
                                <div class="col-md-6">
                                    <telerik:RadButton ID="radbtnUploadFile" runat="server" Skin="WebBlue" Text="Upload File"
                                        OnClick="radbtnUploadFile_Click" Style="top: 0px; left: 0px">
                                    </telerik:RadButton>
                                </div>
                            </div>
                            <div>
                                                            <telerik:RadButton ID="RadButton3" runat="server" Skin="WebBlue" Text="Upload File"
                                OnClick="radbtnUploadFile_Click" style="top: 0px; left: 0px">
                            </telerik:RadButton>
                            <%--<cc1:ConfirmButtonExtender ID="radbtnUploadFile_ConfirmButtonExtender" runat="server"
                                ConfirmText="Are you sure to import the selected file's employee data to master data? (If any errors on uploading file, please make sure to set relevent 'Error Correction Options'!)"
                                Enabled="True" TargetControlID="radbtnUploadFile">
                            </cc1:ConfirmButtonExtender>--%>
                            <dx:ASPxPopupControl ID="confirmpopup" runat="server" HeaderText="Confirm Message" Height="80px" PopupHorizontalAlign="WindowCenter" Width="380px" PopupVerticalAlign="WindowCenter">
                                    <HeaderTemplate>
                                        Confirm Message
                                    </HeaderTemplate>
                                    <ContentCollection>
                                        <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td class="auto-style10" colspan="3">&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style10" colspan="3" style="font-size: small; font-weight: bold; text-align: center; font-family: Arial, Helvetica, sans-serif; font-style: normal;">Are you sure to upload the selected file? </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style10">&nbsp;</td>
                                                    <td></td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                         
                                            <table style="width: 100%;">
                                                <tr>
                                                  <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td></td>
                                                    <td>
                                                        <asp:Button ID="popOkbtn" runat="server" Height="20px" Width="80px" Text="OK" OnClick="popOkbtn_Click" CssClass="btn btn-block btn-success btn-large" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="popCancelbtn" runat="server" Height="20px" Width="80px" Text="Cancel" OnClick="popCancelbtn_Click" CssClass="btn btn-block btn-warning btn-large" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="auto-style7">&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                    <td>&nbsp;</td>
                                                </tr>
                                            </table>
                                        </dx:PopupControlContentControl>
                                    </ContentCollection>
                                </dx:ASPxPopupControl>
                            </div>
                            <div class="d-flex justify-content-end mr-lg-5 mt-3" style="grid-gap: 15px">
                                <div>
                                    <asp:Button ID="btnsearch" runat="server" CssClass="btn btn-outline-info check-aspbtn" Text="Search" OnClick="btnAdd_Click" />
                                </div>
                                <div>
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-outline-info check-aspbtn" Text="Update" OnClick="btnUpdate_Click" />
                                </div>
                                <div>
                                    <asp:Button ID="btnPopupCancel" runat="server" CssClass="btn btn-outline-warning check-aspbtn" Text="Cancel" OnClick="btnPopupCancel_Click" />
                                </div>
                            </div>
                            <div class="row" style="position: relative">

                                <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                                    <ContentTemplate>
                                        <img alt="" src="../../App_Themes/NewTheme/img/exceldwn.png" />
                                    </ContentTemplate>
                                </telerik:RadButton>

                                <div class="col table-scroll3">
                                    <telerik:RadButton ID="RadButton2" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                                        <ContentTemplate>
                                            <img alt="" src="../../App_Themes/Images/exceldwn.png" />
                                        </ContentTemplate>
                                    </telerik:RadButton>
                                    <dx:ASPxGridView ID="EMPGRID" ClientInstanceName="grid" runat="server" ForeColor="Black" order-bordercolor="Black" Theme="MetropolisBlue" Width="100%"
                                        AutoGenerateColumns="true" ClientIDMode="AutoID" CssClass="tableStyle">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />

                                    </dx:ASPxGridView>

                                    <dx:ASPxGridViewExporter ID="ASPxGridViewExporter1" runat="server" GridViewID="EMPGRID" FileName="Employee_Master">
                                        <Styles>
                                            <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridViewExporter>

                                    <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="EMPGRID">
                                        <Styles>
                                            <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                            </Header>
                                        </Styles>
                                    </dx:ASPxGridViewExporter>
                                </div>
                            </div>
                             <table style="width:100%">
        <tr>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
  
        <tr>
            <td>
                <asp:Label ID="Label5" runat="server" Text="Error Log:" />&nbsp;&nbsp;&nbsp;
                <telerik:RadButton ID="radbtnClearLog" runat="server" Text="Clear Log" OnClick="radbtnClearLog_Click"
                    Skin="WebBlue">
                </telerik:RadButton>
                <cc1:ConfirmButtonExtender ID="radbtnClearLog_ConfirmButtonExtender" runat="server"
                    ConfirmText="Are you sure to clear all events in Event Log?" Enabled="True" TargetControlID="radbtnClearLog">
                </cc1:ConfirmButtonExtender>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label6" runat="server" Text="Filter events by" />
                &nbsp;&nbsp;
                <telerik:RadComboBox ID="radcmbFilterEvents" runat="server" Skin="Windows7" OnSelectedIndexChanged="radcmbFilterEvents_SelectedIndexChanged"
                    AutoPostBack="True">
                    <Items>
                        <telerik:RadComboBoxItem runat="server" ImageUrl="~/App_Themes/CommonResources/RadGrid/Refresh.gif"
                            Text="All" Value="All" />
                        <telerik:RadComboBoxItem runat="server" ImageUrl="~/App_Themes/CommonResources/Log/Log-Info.png"
                            Text="Message" Value="Message" />
                        <telerik:RadComboBoxItem runat="server" ImageUrl="~/App_Themes/CommonResources/Log/Log-Success.png"
                            Text="Success" Value="Success" />
                        <telerik:RadComboBoxItem runat="server" ImageUrl="~/App_Themes/CommonResources/Log/Log-Warning.png"
                            Text="Warning" Value="Warning" />
                        <telerik:RadComboBoxItem runat="server" ImageUrl="~/App_Themes/CommonResources/Log/Log-Error.png"
                            Text="Error" Value="Error" />
                    </Items>
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td style="padding: 0px 8px 0px 8px;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <telerik:RadGrid ID="radGridEvents" runat="server" AutoGenerateColumns="False" CellSpacing="0"
                            GridLines="None" Skin="WebBlue" AllowCustomPaging="True" AllowPaging="True">
                            <MasterTableView AllowCustomPaging="False">
                                <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                    <HeaderStyle Width="20px"></HeaderStyle>
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridTemplateColumn FilterControlAltText="Filter EventImageURL column" UniqueName="EventImageURL"
                                        HeaderText="Event Type">
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </EditItemTemplate>
                                        <ItemTemplate>
                                            <table style="width: 100%;">
                                                <tr>
                                                    <td style="width: 16px;">
                                                        <asp:Image ID="imgEventImage" runat="server" AlternateText="EventImage" ImageUrl='<%# Eval("EventImageURL") %>'
                                                            Width="16px" />
                                                    </td>
                                                    <td>
                                                        <asp:Label ID="lblEventType" runat="server" Text='<%# Eval("EventType") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </ItemTemplate>
                                    </telerik:GridTemplateColumn>
                                    <telerik:GridBoundColumn FilterControlAltText="Filter EventTime column" HeaderText="Time"
                                        UniqueName="EventTime" DataField="EventTime" AllowFiltering="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn FilterControlAltText="Filter EventMessage column" HeaderText="Message"
                                        UniqueName="EventMessage" DataField="EventMessage" AllowFiltering="False">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                            </HeaderContextMenu>
                        </telerik:RadGrid>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

                            <asp:Panel ID="Panel1" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical">
                                <div class="style1">
                                    <div style="float: right; margin-top: -10px;">
                                        <asp:ImageButton ID="btnClosePopup" runat="server"
                                            ImageUrl="~/App_Themes/NewTheme/img/close.png" />
                                    </div>
                                    <div id="org-stucture">
                                        <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                                    </div>
                                </div>
                            </asp:Panel>

                              <asp:Panel ID="Panel2" runat="server" Visible="false">
                            <table style="width: 100%;">
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadButton ID="radchkCombineToGetFullName" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Combine 'First Name' &amp; 'Last Name' to generate 'Full Name' where employee full name field is empty."
                                            ToggleType="CheckBox">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadButton ID="radchkConvertNameFieldDataToCapsEachWord" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Name fields conversion 'Capitalize Each Word'" ToggleType="CheckBox">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultGender" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Gender' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchkDefaultGender_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="radcmbDefaultGender" runat="server" Enabled="False" Skin="Windows7">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Selected="True" Text="Male" Value="Male"
                                                    Owner="radcmbDefaultGender" />
                                                <telerik:RadComboBoxItem runat="server" Text="Female" Value="Female" Owner="radcmbDefaultGender" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchDefultBCard" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'B Card Availability' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchDefultBCard_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <telerik:RadComboBox ID="radcmbDefaultBCard" runat="server" Enabled="False" Skin="Windows7">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Selected="True" Text="No" Value="No"
                                                    Owner="radcmbDefaultGender" />
                                                <telerik:RadComboBoxItem runat="server" Text="Yes" Value="Yes" Owner="radcmbDefaultBCard" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultCompany" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Company' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchkDefaultCompany_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="objDataCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetCompanyProfile" TypeName="HRM.Common.BLL.CompanyProfile">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="CompanyId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefCompany" runat="server" Enabled="False" Skin="Windows7"
                                            Width="400px" DataSourceID="objDataCompany" DataTextField="CompanyName" DataValueField="CompanyID">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultJobCategory" runat="server" ButtonType="ToggleButton"
                                            OnCheckedChanged="radchkDefaultJobCategory_CheckedChanged" Skin="Forest" Text="Default 'Job Category' where field is empty."
                                            ToggleType="CheckBox">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="objDataJobCat" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetJobCategory" TypeName="HRM.Common.BLL.Reference">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="JobCategoryId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefJobCategory" runat="server" DataSourceID="objDataJobCat"
                                            DataTextField="JobCategoryName" DataValueField="JobCategoryId" Enabled="False"
                                            Skin="Windows7" Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultDepartment" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Department' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchkDefaultDepartment_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="objDataDepartment" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetOrganizationTypes" TypeName="HRM.Common.BLL.Organization">
                                            <SelectParameters>
                                                <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                                <asp:Parameter DefaultValue="0" Name="OrganizationTypeId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefDepartment" runat="server" Enabled="False" DataSourceID="objDataDepartment"
                                            DataTextField="OrganizationTypeCode" DataValueField="OrganizationTypeID" Skin="Windows7"
                                            Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radckDefaultBranch" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Branch' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radckDefaultBranch_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="oblDataBranch" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetBranch" TypeName="HRM.Common.BLL.Reference">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="BranchId" Type="Int32" />
                                                <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefBranch" runat="server" Enabled="False" DataSourceID="oblDataBranch"
                                            DataTextField="Location" DataValueField="BranchId" Skin="Windows7"
                                            Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultDesignation" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Designation' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchkDefaultDesignation_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="objDataDesignation" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetDesignation" TypeName="HRM.Common.BLL.Designation">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="DesignationId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefJobDesignation" runat="server" Enabled="False"
                                            DataSourceID="objDataDesignation" DataTextField="DesignationName" DataValueField="DesignationID"
                                            Skin="Windows7" Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultJobGrade" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Job Grade' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchkDefaultJobGrade_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="objDataJobGrade" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetJobGrade" TypeName="HRM.Common.BLL.Reference">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="JobGradeId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefJobGrade" runat="server" Enabled="False" DataSourceID="objDataJobGrade"
                                            DataTextField="JobGrade" DataValueField="JobGradeID" Skin="Windows7" Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultEmpType" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Employemnt Type' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchkDefaultEmpType_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="objDataEmpType" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetEmploymentType" TypeName="HRM.Common.BLL.Employee">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="EmploymentTypeId" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefEmpType" runat="server" Enabled="False" DataSourceID="objDataEmpType"
                                            DataTextField="EmploymentTypeName" DataValueField="EmploymentTypeID" Skin="Windows7"
                                            Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkDefaultEmpGrade" runat="server" ButtonType="ToggleButton"
                                            Skin="Forest" Text="Default 'Employemnt Grade' where field is empty." ToggleType="CheckBox"
                                            OnCheckedChanged="radchkDefaultEmpGrade_CheckedChanged">
                                        </telerik:RadButton>
                                    </td>
                                    <td>
                                        <asp:ObjectDataSource ID="objDataEmpGrade" runat="server" OldValuesParameterFormatString="original_{0}"
                                            SelectMethod="GetEmploymentGrade" TypeName="HRM.Common.BLL.Employee">
                                            <SelectParameters>
                                                <asp:Parameter DefaultValue="0" Name="EmploymentGradeID" Type="Int32" />
                                            </SelectParameters>
                                        </asp:ObjectDataSource>
                                        <telerik:RadComboBox ID="radcmbDefEmpGrade" runat="server" Enabled="False" DataSourceID="objDataEmpGrade"
                                            DataTextField="EmploymentGradeName" DataValueField="EmploymentGradeID" Skin="Windows7"
                                            Width="400px">
                                        </telerik:RadComboBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <telerik:RadButton ID="radchkReplaceTextWith" runat="server" ButtonType="ToggleButton"
                                            OnCheckedChanged="radchkReplaceTextWith_CheckedChanged" Skin="Forest" Text="Search &amp; replace text on "
                                            ToggleType="CheckBox">
                                        </telerik:RadButton>
                                        <telerik:RadComboBox ID="radcmbRepaceMethod" Enabled="false" runat="server" Skin="Windows7">
                                            <Items>
                                                <telerik:RadComboBoxItem runat="server" Text="field data only" Value="field data only"
                                                    Owner="radcmbRepaceMethod" />
                                                <telerik:RadComboBoxItem runat="server" Text="any occurrence" Value="any occurrence"
                                                    Owner="radcmbRepaceMethod" />
                                            </Items>
                                        </telerik:RadComboBox>
                                    </td>
                                    <td>
                                        <asp:Panel ID="pnlSearchAndReplace" runat="server" Enabled="false">
                                            <telerik:RadTextBox ID="radtxtSerchFor" runat="server" EmptyMessage="Search for..."
                                                Skin="Windows7" Width="125px">
                                            </telerik:RadTextBox>&nbsp;&nbsp;&nbsp;
                                            <telerik:RadTextBox ID="radtxtReplaceWith" runat="server" EmptyMessage="Replace with..."
                                                Skin="Windows7" Width="125px">
                                            </telerik:RadTextBox>
                                        </asp:Panel>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <telerik:RadButton ID="radchkEnablePartialUploads" runat="server" ButtonType="ToggleButton"
                                            Checked="True" Skin="Forest" Text="Upload partial list when errors occur in upload process!"
                                            ToggleType="CheckBox">
                                        </telerik:RadButton>
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
  <%--                          <asp:Panel ID="Panel1" runat="server" CssClass="popstyles popstyle-2 " ScrollBars="Vertical">
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
                    </div>
                </div>
            </div>
                </div>
            </div>
   </ContentTemplate>
</asp:Content>