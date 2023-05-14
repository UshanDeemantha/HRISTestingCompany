<%@ Page Title="HRIS Employee | Employee Bulk Upload" Language="C#" AutoEventWireup="true" CodeFile="EmployeeExcelDataUpload.aspx.cs" Inherits="HR_Employee_EmployeeExcelDataUpload" MasterPageFile="~/HR/HR_MenuMasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        a:hover {
    background-color: transparent;
    opacity: 1;
    color: #070000;
}
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
        li.mrleft {
    margin-left: 39px;
}
        .style1{
            margin-bottom:114px;
        }
        .content{
            content: url(../../../NewTheme/img/warning.png);
        }
        .btn-success {
    color: gray;
    background-color: transparent;
    border-color: gray;
}
        .btn-warning {
    color: white;
    background-color: indianred;
    border-color: indianred;
    margin-left: 101px;
}
    </style>
    <script type = "text/javascript">
        function DisableButton() {
            document.getElementById("<%=Button1.ClientID %>").disabled = true;
            }
            window.onbeforeunload = DisableButton;
        </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/dataUpload.css" />
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
        <div class="row">
            <div class="col head-container">
                <h4 class="header">Employee Bulk Details Upload</h4>

            </div>
        </div>
    </div>
          
    <div style="display: grid; grid-gap: 10px; grid-template-columns: 1fr 1fr; place-content: center; margin-top: 35px;">
        <div style="font-size: 16px;">
            <ul class="infoInnerMargin">
                <li><b style="color:#0c65a5">*</b> Uploading Excel file must be in <b style="color:#0c65a5">Microsoft Office 97-2003 Workbook or Excel Workbook</b> format.</li>
                <li><b style="color:#0c65a5">*</b> Excel file <b style="color:#0c65a5">Sheet Name</b> containing employee master data should be named as <b style="color:#0c65a5">'EmployeeMaster'</b>.</li>
                <li><b style="color:#0c65a5">*</b> Only one excel file can be uploaded at once; no limit of records in file.</li>
                <li><b style="color:#0c65a5">*</b> Following list of fields cannot be empty in uploading excel file.
                            <ul>
                                <li><b style="color:#0c65a5">'EmployeeCode'</b> column data must be Unique & Compulsory</li>
                                <li><b style="color:#0c65a5">'NIC'</b> data column must be Unique & Compulsory (can be passport number, or any other
                                     unique field)</li>
                                <li><b style="color:#0c65a5">'Recruitment date'</b> column data must be specified</li>
                                <li><b style="color:#0c65a5">'Designation1SecondLevel/ EmployeeDesignation'</b> column data must be specified</li>
                                <li><b style="color:#0c65a5">'BranchCategory/ Department'</b> column data must Be specified</li>
                                <li><b style="color:#0c65a5">'CompanyName'</b> column data must be specified</li>
                                <li>If employee status <b style="color:#0c65a5">"Non Active" 'ResignDate'</b> must be specified</li>
                                <li><b style="color:#0c65a5">'FirstName' & 'LastName'</b> columns are required</li>
                            </ul>
                </li>
                <li>&nbsp;</li>
                <hr style="border-top-color: #f7971f; border-top-width: 2px; margin-bottom: 2rem;" />
                <li style="margin-top: 35px;margin-right: 407px;">
                    <asp:ImageButton ID="btnDownloadTemplates" OnClick="btnDownloadTemplates_Click" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/dwnloadtemplate.png" ValidationGroup="a"></asp:ImageButton>
                    <%-- <asp:HyperLink ID="hlEmployeePatrollTemplate" NavigateUrl="~/Common/Settings/UploadFormats.aspx?TempateName=EmpMaster&PagePath=HR/Employee/EmployeeExcelDataUpload.aspx"
                                runat="server"></asp:HyperLink>--%>

    
                </li>
            </ul>
        </div>
        <div class="shadow p-4">
            <div style="border: 4px solid black; border-style: dotted; height: 100%" class="p-3">
                <div style="display: grid; place-content: center; height: 100%">

                    <div style="display: grid; place-content: center;">
                        <h1>Upload File</h1>
                    </div>
                    <div style="display: grid; place-content: center">
                        <div style="display: grid; place-content: center">
                            <img src="../../App_Themes/NewTheme/images/upload.png" />
                        </div>
                        <telerik:RadUpload ID="radUploadEmpExcel" runat="server" InputSize="50" MaxFileInputsCount="1"
                            OverwriteExistingFiles="True" Skin="WebBlue" MaxFileSize="5242880" TargetFolder="~/Payroll/RawDataUploads/"
                            OnFileExists="radUploadEmpExcel_FileExists" ControlObjectsVisibility="None" AllowedFileExtensions=".xls,.xlsx">
                        </telerik:RadUpload>
                    </div>
                    <div style="display: grid; place-content: center;">
                        <asp:Button ID="radbtnUploadFile" runat="server" Text="Upload File" CssClass="btn btn-outline-danger check-aspbtn1" 
                            OnClick="radbtnUploadFile_Click"></asp:Button>
                    </div>
                </div>
                <div>
                    <dx:ASPxPopupControl ID="confirmpopup" runat="server" HeaderText="Confirm Message" Height="80px" PopupHorizontalAlign="WindowCenter" Width="380px" PopupVerticalAlign="WindowCenter" Modal="True">
                        <HeaderTemplate>
                            Confirm Message
                        </HeaderTemplate>
                        <ContentCollection>
                            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                                <table style="width: 100%;">
                                    <tr>
                                        <td class="Content" colspan="3">&nbsp;</td>
                                    </tr>
                                   <tr>
                                        <td> <image src="../../App_Themes/NewTheme/images/warning.png" /></td>
                                        <td class="Content" colspan="3" style="font-size: small; font-weight: bold; text-align: center; color: darkgray;font-family: Arial, Helvetica, sans-serif; font-style: normal;">Are you sure to upload the selected file? </td>
                                    </tr>
                                    <tr>
                                        <td class="auto-style10">&nbsp;</td>
                                        <td></td>
                                        <td>&nbsp;</td>
                                    </tr>
                                </table>

                                <table style="width: 100%; ">
                                    <tr>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td></td>
                                        <td>
                                            <asp:Button ID="Button3" runat="server" Height="25px" Width="80px" Text="CANCEL"  OnClick="btnNo_Click" CssClass="btn btn-block btn-warning btn-large" />

                                        </td>
                                        <td>
                                             <asp:Button ID="Button1" runat="server" Height="25px" Width="80px" Text="OK" OnClick="btnYes_Click" CssClass="btn btn-block btn-success btn-large" />

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
                <div style="display:none">

                    <div>

                        <%--<cc1:ConfirmButtonExtender ID="radbtnUploadFile_ConfirmButtonExtender" runat="server"
                                ConfirmText="Are you sure to import the selected file's employee data to master data? (If any errors on uploading file, please make sure to set relevent 'Error Correction Options'!)"
                                Enabled="True" TargetControlID="radbtnUploadFile">
                            </cc1:ConfirmButtonExtender>--%>


                    </div>
                    <div >
                        <asp:Label ID="Label2" runat="server" Text="Error Log:" />&nbsp;&nbsp;&nbsp;
                <telerik:RadButton ID="radbtnClearLog" runat="server" Text="Clear Log" OnClick="radbtnClearLog_Click"
                    Skin="WebBlue">
                </telerik:RadButton>
                        <cc1:ConfirmButtonExtender ID="radbtnClearLog_ConfirmButtonExtender" runat="server"
                            ConfirmText="Are you sure to clear all events in Event Log?" Enabled="True" TargetControlID="radbtnClearLog">
                        </cc1:ConfirmButtonExtender>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label3" runat="server" Text="Filter events by" />
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
                    </div>
                    <div>
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
                    </div>
                    <div>
                        <telerik:RadButton ID="radbtnUploadOptions" runat="server" Skin="Black" Text="Uploaded file error correction options..."
                            AutoPostBack="False" OnClick="radbtnUploadOptions_Click">
                        </telerik:RadButton>
                    </div>
                    <div>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <asp:Panel ID="Panel1" runat="server">
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
                                                    OnCheckedChanged="radchkReplaceTextWith_CheckedChanged" Skin="Forest" Text="Search &amp; replace text on"
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
                                <cc1:CollapsiblePanelExtender ID="Panel1_CollapsiblePanelExtender0" runat="server"
                                    Enabled="True" TargetControlID="Panel1" Collapsed="true" CollapsedText="Show Options..."
                                    ExpandedText="Hide Options" ExpandControlID="radbtnUploadOptions" CollapseControlID="radbtnUploadOptions">
                                </cc1:CollapsiblePanelExtender>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </div>
                </div>
            </div>

        </div>
    </div>

    <dx:ASPxPopupControl ID="TemPopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <h6 style="font-family: Source Sans; font-size: 16px; font-weight: 600; color: #007792">Download Template</h6>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">


                                <table class="style1">
                                    <tr>
                                        <td>
                                            <h4>Data Uplad Formats</h4>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <div class="infoBox">
                                                <ul class="infoInnerMargin">
                                                    <li>All Uplad Templates Excel file are in Microsoft Office 2010-2016 format.</li>
                                                    <li>All Tempates are with saple data so plese make shure you remve them befor use the tempate</li>
                                                    <li>Relevent Template is mark in green color tik</li>
                                                </ul>
                                            </div>
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label1" runat="server" Text="Employee Master Data :" />&nbsp;&nbsp;&nbsp;


                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink1" runat="server"
                                            NavigateUrl="~/Common/FileUploadFormats/EmployeeMaster.xls">Employee Master</asp:HyperLink>

                                        <asp:Image ID="imgEmpMaster" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label4" runat="server" Text="Employee Additional Details:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink11" runat="server"
                                            NavigateUrl="~/Common/FileUploadFormats/EmployeeAdditionalDetails.xls">Employee Additional Details</asp:HyperLink>

                                        <asp:Image ID="ImgEmpAddData" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label5" runat="server" Text="Employee Payroll Data:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink2" runat="server"
                                            NavigateUrl="~/Common/FileUploadFormats/EmployeePayroll.xls">Employee Payroll</asp:HyperLink>

                                        <asp:Image ID="imgEmpPayroll" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label6" runat="server" Text="Employee Bank Data:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink12" runat="server"
                                            NavigateUrl="~/Common/FileUploadFormats/BankDetails.xls">Employee Bank Details</asp:HyperLink>

                                        <asp:Image ID="imgEmpBank" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label7" runat="server" Text="Employee Fix Data:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink7" runat="server"
                                            NavigateUrl="~/Common/FileUploadFormats/EmployeeFixData.xls">Employee Pay</asp:HyperLink>

                                        <asp:Image ID="imgEmpPay" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label8" runat="server" Text="Employee Mobile Bill Data:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink9" runat="server"
                                            NavigateUrl="~/Common/FileUploadFormats/EmployeeMobileBillData.xls">Employee Mobile Bill</asp:HyperLink>

                                        <asp:Image ID="ImgMobBill" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png" Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label9" runat="server" Text="Employee Time:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl="~/Common/FileUploadFormats/DailyLog.xls">Employee Time</asp:HyperLink>

                                        <asp:Image ID="imgEmpTime" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png"
                                            Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label10" runat="server" Text="Leave Entitlement:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink5" runat="server" NavigateUrl="~/Common/FileUploadFormats/LeaveEntitlement.xls">Leave Entitlement</asp:HyperLink>

                                        <asp:Image ID="ImgLeaveEtitl" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png"
                                            Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label11" runat="server" Text="Employee Variable Data:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink6" runat="server" NavigateUrl="~/Common/FileUploadFormats/VariableData.xls">Employee Transaction</asp:HyperLink>

                                        <asp:Image ID="ImgTransaction" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png"
                                            Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label12" runat="server" Text="Employee Advances:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl="~/Common/FileUploadFormats/EmployeeAdvances.xls">Employee Advances</asp:HyperLink>

                                        <asp:Image ID="ImgAdvances" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png"
                                            Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label13" runat="server" Text="Employee Increment:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink8" runat="server" NavigateUrl="~/Common/FileUploadFormats/EmployeeIncrement.xls">Employee Increment</asp:HyperLink>

                                        <asp:Image ID="ImgIncrement" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png"
                                            Visible="False" />
                                    </div>
                                </div>
                                <div class="row">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label14" runat="server" Text="Employee Loans:" />&nbsp;&nbsp;&nbsp;

                                    </div>
                                    <div class="col-md-6">
                                        <asp:HyperLink ID="HyperLink10" runat="server" NavigateUrl="~/Common/FileUploadFormats/EmployeeLoan.xls">Employee Loans</asp:HyperLink>

                                        <asp:Image ID="ImageLoan" runat="server"
                                            ImageUrl="~/App_Themes/Common/UpladSelect.png"
                                            Visible="False" />
                                    </div>
                                </div>
                                <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                    <asp:Button ID="RadButton1" runat="server" OnClick="RadButton1_Click" CssClass="btn btn-outline-warning check-aspbtn"
                                        Skin="WebBlue" Text="Back"></asp:Button>
                                </div>

                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
</asp:Content>
