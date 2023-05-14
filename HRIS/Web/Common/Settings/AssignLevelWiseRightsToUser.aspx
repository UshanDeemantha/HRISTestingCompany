<%@ Page Title="HRM Common | Level Wise User Rights" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="AssignLevelWiseRightsToUser.aspx.cs" Inherits="UserRights_AssignLevelWiseRightsToUser" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .infoBox
        {
            font-size:13px;
            font-style:italic;
            border-radius: 15px; 
            -moz-border-radius: 15px;
            margin: 20px 0;
            border-width: 2px;
            border-color: Green;
            border-style: solid;
            width:100%;
        }
        
        .infoInnerMargin
        {
            margin-top: 10px;
            margin-bottom: 10px;
            margin-right: 20px;
        }
        
        .hierarchyAlignment
        {
            margin-left: 75px;
        }
        .style1
        {
            height: 18px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
    <telerik:RadAjaxManager ID="RadAjaxManager1" runat="server">
        <AjaxSettings>
            <telerik:AjaxSetting AjaxControlID="radcmbCompanySelect">
                <UpdatedControls>
                    <telerik:AjaxUpdatedControl ControlID="radtreeOrgLevel" />
                    <telerik:AjaxUpdatedControl ControlID="radtreeDesignationLevel" />
                </UpdatedControls>
            </telerik:AjaxSetting>
        </AjaxSettings>
    </telerik:RadAjaxManager>
    <table>
        <tr>
            <td colspan="2">
                <div class="infoBox">
                <ul class="infoInnerMargin">                    
                    <li>Currently logged in user cannot change user rights for the account.</li>
                    <li>All relevant\required levels of organizations & designations must be selected; selecting top level of hierarchy will not assign rights to levels within said hierarchy!</li>
                    <li>Users may have to refresh pages to get the newly assigned rights once saved.</li>
                    <li>Newly assigned rights does not apply to already saved data automatically!</li>
                </ul>
                </div>
                </td>
        </tr>
        <tr>
            <td >
                <asp:Label runat="server" Text="Select Company" />
            </td>
            <td>
                <asp:ObjectDataSource ID="objDataCompanySelect" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetCompanyProfile" 
                    TypeName="HRM.Common.BLL.CompanyProfile">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="CompanyId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <telerik:RadComboBox ID="radcmbCompanySelect" Width="250px" Runat="server" 
                    DataSourceID="objDataCompanySelect" Skin="Windows7" 
                    DataTextField="CompanyName" DataValueField="CompanyID" 
                    ondatabound="radcmbCompanySelect_DataBound" 
                    onselectedindexchanged="radcmbCompanySelect_SelectedIndexChanged" 
                    AutoPostBack="True">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td >
                &nbsp;</td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td >
                <asp:Label runat="server" Text="Select Application" />
            </td>
            <td>
                <asp:ObjectDataSource ID="objSelectAppName" runat="server" 
                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetApplication" 
                    TypeName="HRM.Common.BLL.MksSecurity">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="ApplicationId" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <telerik:RadComboBox ID="radcmbApplications" Runat="server" Skin="Windows7" 
                    Width="300px" DataSourceID="objSelectAppName" 
                    DataTextField="ApplicationName" DataValueField="ApplicationId" 
                    AutoPostBack="true" 
                    onselectedindexchanged="radcmbApplications_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td >
                <asp:Label runat="server" Text="Select User" />
            </td>
            <td>
                <asp:ObjectDataSource ID="objSelectUser" runat="server" 
                    OldValuesParameterFormatString="original_{0}" 
                    SelectMethod="SelectUsersToAssignRights" TypeName="HRM.Common.BLL.MksSecurity">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfCurrentUser" DefaultValue="" 
                            Name="currentUserName" PropertyName="Value" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <telerik:RadComboBox ID="radcmbUsers" Runat="server" Skin="Windows7" 
                    Width="300px" DataSourceID="objSelectUser" DataTextField="FullUserName" 
                    DataValueField="UserId" AutoPostBack="true" 
                    onselectedindexchanged="radcmbUsers_SelectedIndexChanged">
                </telerik:RadComboBox>
            </td>
        </tr>
        <tr>
            <td class="style1">
                </td>
            <td class="style1">
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
            &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="Select organization level user rights...." Font-Size="15px" ForeColor="Green" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="hierarchyAlignment">
                    <telerik:RadTreeView ID="radtreeOrgLevel" Runat="server" CheckBoxes="True" 
                    CheckChildNodes="True" onnodecreated="radtreeOrgLevel_NodeCreated" 
                    Skin="Office2007">
                </telerik:RadTreeView>
                </div>
                </td>
        </tr>
        <tr>
            <td>
                </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label runat="server" Text="Select designation level user rights...." Font-Size="15px" ForeColor="Green" />
                </td>
        </tr>
        <tr>
            <td colspan="2">
                <div class="hierarchyAlignment">
                    <telerik:RadTreeView ID="radtreeDesignationLevel" Runat="server" 
                    CheckBoxes="True" CheckChildNodes="True" 
                    onnodecreated="radtreeDesignationLevel_NodeCreated" Skin="Office2007">
                </telerik:RadTreeView>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;</td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <asp:Label ID="lblError" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;</td>
            <td>
                <telerik:RadButton ID="radbtnSave" runat="server" Text="Save" Skin="WebBlue" onclick="radbtnSave_Click">
                </telerik:RadButton>&nbsp;&nbsp;
                <telerik:RadButton ID="radbtnUpdate" runat="server" Text="Update" Skin="WebBlue" onclick="radbtnUpdate_Click">
                </telerik:RadButton>
                <cc1:ConfirmButtonExtender ID="radbtnUpdate_ConfirmUpdateExtender" runat="server" ConfirmText="Are you sure to update selected user access levels to selected configuration?" Enabled="True" TargetControlID="radbtnUpdate">
                </cc1:ConfirmButtonExtender>
                &nbsp;&nbsp;
                <telerik:RadButton ID="radbtnCancel" runat="server" Text="Cancel" 
                    Skin="WebBlue" onclick="radbtnCancel_Click">
                </telerik:RadButton>
                <cc1:ConfirmButtonExtender ID="radbtnCancel_ConfirmCancelExtender" 
                    runat="server" ConfirmText="Are you sure to reset all fields to default? (Selected user assigned rights will be displayed as default!)" Enabled="True" TargetControlID="radbtnCancel">
                </cc1:ConfirmButtonExtender>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:HiddenField ID="hfCurrentUser" runat="server" />
            </td>
        </tr>
    </table>
</asp:Content>

