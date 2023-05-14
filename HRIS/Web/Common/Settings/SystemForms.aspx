<%@ Page Title="HRM Common | System Forms" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="SystemForms.aspx.cs" Inherits="Settings_SystemForms" %>

<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<table>
<tr>
<td>
    Application&nbsp;
</td>
<td>
    <asp:ObjectDataSource ID="objdsApplication" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetApplication" 
        TypeName="HRM.Common.BLL.MksSecurity">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="ApplicationId" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    <telerik:RadComboBox ID="radcboApplication" Runat="server" 
        DataSourceID="objdsApplication" DataTextField="ApplicationName" 
        DataValueField="ApplicationId" 
        AutoPostBack="True" ondatabound="radcboApplication_DataBound" 
        Skin="Windows7">
    </telerik:RadComboBox>
    </td>
</tr>
<tr>
<td>
    Menu&nbsp;
</td>
<td>
    <telerik:RadComboBox ID="radcboMenu" Runat="server" DataSourceID="objdsMenu" 
        AutoPostBack="True" 
        DataTextField="MenuName" DataValueField="MenuId" Skin="Windows7" 
        ondatabound="radcboMenu_DataBound1">
    </telerik:RadComboBox>
    <asp:ObjectDataSource ID="objdsMenu" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetMenu" 
        TypeName="HRM.Common.BLL.MksSecurity">
        <SelectParameters>
            <asp:ControlParameter ControlID="radcboApplication" DefaultValue="" 
                Name="ApplicationId" PropertyName="SelectedValue" Type="String" />
            <asp:Parameter DefaultValue="0" Name="MenuId" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </td>
</tr>
<tr>
<td>
    Form Name</td>
<td>
    <telerik:RadTextBox ID="radtxtFormName" Runat="server" Skin="Windows7">
    </telerik:RadTextBox>
</td>
</tr>
<tr>
<td>
    Form Index No</td>
<td>
    <telerik:RadNumericTextBox ID="radntxtFormIndexNo" Runat="server" 
        Skin="Windows7">
        <NumberFormat DecimalDigits="0" GroupSeparator="" />
    </telerik:RadNumericTextBox>
</td>
</tr>
<tr>
<td>
    Form Description</td>
<td>
    <telerik:RadTextBox ID="radtxtFormDescription" Runat="server" Skin="Windows7" 
        TextMode="MultiLine">
    </telerik:RadTextBox>
</td>
</tr>
<tr>
<td>
    &nbsp;</td>
<td>
    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
    </td>
</tr>
<tr>
<td>
    &nbsp;</td>
<td>
    <telerik:RadButton ID="radbtnSave" runat="server" Skin="WebBlue" Text="Save" 
        onclick="radbtnSave_Click">
    </telerik:RadButton>&nbsp;&nbsp;
    <telerik:RadButton ID="radbtnUpdate" runat="server" Skin="WebBlue" 
        Text="Update" onclick="radbtnUpdate_Click">
    </telerik:RadButton>&nbsp;&nbsp;
    <telerik:RadButton ID="radbtnDelete" runat="server" Skin="WebBlue" 
        Text="Delete" onclick="radbtnDelete_Click">
    </telerik:RadButton>&nbsp;&nbsp;
    <telerik:RadButton ID="radbtnCancel" runat="server" Skin="WebBlue" 
        Text="Cancel" onclick="radbtnCancel_Click">
    </telerik:RadButton>
</td>
</tr>
<tr>
<td>
    <asp:HiddenField ID="hfFormId" runat="server" />
    </td>
<td>
    &nbsp;</td>
</tr>
<tr>
<td colspan="2">
    <telerik:RadGrid ID="radgvDetails" runat="server" Skin="Windows7" 
        AllowPaging="True" AutoGenerateColumns="False" CellSpacing="0" 
        DataSourceID="objdsForms" GridLines="None" 
        onitemcommand="radgvDetails_ItemCommand">
<MasterTableView AllowPaging="False" DataKeyNames="MenuId,FormId" 
            DataSourceID="objdsForms">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridButtonColumn ButtonType="PushButton"
            CommandArgument="MenuId, FormId" CommandName="Modify" 
            FilterControlAltText="Filter column5 column" Text="Modify" 
            UniqueName="column5" ConfirmDialogType="RadWindow">
        </telerik:GridButtonColumn>
        <telerik:GridBoundColumn DataField="MenuName" 
            FilterControlAltText="Filter column4 column" HeaderText="Menu Name" 
            UniqueName="column4">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="FormName" 
            FilterControlAltText="Filter column1 column" HeaderText="Form Name" 
            UniqueName="column1">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="FormIndexNo" 
            FilterControlAltText="Filter column3 column" HeaderText="Form Index No" 
            UniqueName="column3">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="FormDescription" 
            FilterControlAltText="Filter column2 column" HeaderText="Form Description" 
            UniqueName="column2">
        </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Windows7"></HeaderContextMenu>
    </telerik:RadGrid>
    <asp:ObjectDataSource ID="objdsForms" runat="server" 
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetForm" 
        TypeName="HRM.Common.BLL.MksSecurity">
        <SelectParameters>
            <asp:ControlParameter ControlID="radcboMenu" Name="MenuId" 
                PropertyName="SelectedValue" Type="String" />
            <asp:Parameter Name="FormId" Type="String" DefaultValue="0" />
        </SelectParameters>
    </asp:ObjectDataSource>
    </td>
</tr>
<tr>
<td>
    &nbsp;</td>
<td>
    &nbsp;</td>
</tr>
</table>
</asp:Content>