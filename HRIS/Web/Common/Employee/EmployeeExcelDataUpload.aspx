<%@ Page Title="HRIS Common | Employee Excel Upload" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="EmployeeExcelDataUpload.aspx.cs" Inherits="Common_Employee_EmployeeExcelDataUpload" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .infoBox
        {
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
        
        .infoInnerMargin
        {
            margin-top: 10px;
            margin-bottom: 10px;
            margin-right: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td>
                <h4>
                    Employee Data Upload</h4>
            </td>
        </tr>
        <tr>
            <td>
                <div class="infoBox">
                    <ul class="infoInnerMargin">
                        <li>Uploading Excel file must be in Microsoft Office 2003/2010 format.</li>
                        <li>Excel file sheet name containing employee master data should be named as 'EmployeeMaster'
                            (File can contain any number of sheets).</li>
                        <li>Only one excel file can be uploaded at once; no limit of records in file.</li>
                        <li>Following list of fields cannot be empty in uploading excel file.
                            <ul>
                                <li>'EmployeeCode' column data must be unique & compulsory</li>
                                <li>'NIC' data column must be unique & compulsory (can be passport number, or any other
                                    unique field)</li>
                                <li>'Join date' column data must be specified</li>
                                <li>'Designation' column data must be specified</li>
                                <li>'Department_Or_Section' column data must Be specified</li>
                                <li>'CompanyName' column data must be specified</li>
                                <li>If employee status "Non Active" 'ResignDate' must be specified</li>
                                <li>'FirstName' & 'LastName' columns are required</li>
                            </ul>
                        </li>
                        <li>
                            <asp:HyperLink ID="hlEmployeePatrollTemplate" NavigateUrl="~/Common/Settings/UploadFormats.aspx?TempateName=EmpMaster&PagePath=Common/Employee/EmployeeExcelDataUpload.aspx"
                                runat="server">Download Template</asp:HyperLink></li>
                    </ul>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <table style="width: 100%;">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label runat="server" Text="Upload file" />
                        </td>
                        <td>
                            <telerik:RadUpload ID="radUploadEmpExcel" runat="server" InputSize="50" MaxFileInputsCount="1"
                                OverwriteExistingFiles="True" Skin="WebBlue" MaxFileSize="5242880" TargetFolder="~/Payroll/RawDataUploads/"
                                OnFileExists="radUploadEmpExcel_FileExists" ControlObjectsVisibility="None" AllowedFileExtensions=".xls,.xlsx">
                            </telerik:RadUpload>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            <telerik:RadButton ID="radbtnUploadFile" runat="server" Skin="WebBlue" Text="Upload File"
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
                                                    <td class="auto-style10" colspan="3" style="font-size:small; font-weight:normal; font-family: Arial, Helvetica, sans-serif; font-style: normal;">Are you sure to import the selected file's employee data to master data? 
                                                        (If any errors on uploading file, please make sure to set relevent 'Error Correction Options'!)</td>
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
                                                        <asp:Button ID="Button1" runat="server" Height="20px" Width="80px" Text="OK" OnClick="btnYes_Click" CssClass="btn btn-block btn-success btn-large" />
                                                    </td>
                                                    <td>
                                                        <asp:Button ID="Button3" runat="server" Height="20px" Width="80px" Text="Cancel" OnClick="btnNo_Click" CssClass="btn btn-block btn-warning btn-large" />
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
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Error Log:" />&nbsp;&nbsp;&nbsp;
                <telerik:RadButton ID="radbtnClearLog" runat="server" Text="Clear Log" OnClick="radbtnClearLog_Click"
                    Skin="WebBlue">
                </telerik:RadButton>
                <cc1:ConfirmButtonExtender ID="radbtnClearLog_ConfirmButtonExtender" runat="server"
                    ConfirmText="Are you sure to clear all events in Event Log?" Enabled="True" TargetControlID="radbtnClearLog">
                </cc1:ConfirmButtonExtender>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="Filter events by" />
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
        <tr>
            <td>
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <telerik:RadButton ID="radbtnUploadOptions" runat="server" Skin="Black" Text="Uploaded file error correction options..."
                    AutoPostBack="False" OnClick="radbtnUploadOptions_Click">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
            <td>
                <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                    <ContentTemplate>
                        <asp:Panel ID="Panel1" runat="server">
                            <table style="width: 100%;">
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
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
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
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
                                    <td colspan="2">
                                        &nbsp;
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
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td>
                                        &nbsp;
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender0" runat="server"
                            Enabled="True" TargetControlID="Panel1" Collapsed="true" CollapsedText="Show Options..."
                            ExpandedText="Hide Options" ExpandControlID="radbtnUploadOptions" CollapseControlID="radbtnUploadOptions">
                        </cc1:CollapsiblePanelExtender>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
    </table>
    <br />
</asp:Content>
