<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemMenu.ascx.cs" Inherits="Common_Settings_UserControls_SystemMenu" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<table>
    <tr>
        <td>Application
        </td>
        <td>
            <telerik:RadComboBox ID="radcboApplicationId" Runat="server" 
                AutoPostBack="True" DataSourceID="objApplicationId" 
                DataTextField="ApplicationName" DataValueField="ApplicationId" 
                ondatabound="radcboApplicationId_DataBound" Skin="Windows7">
            </telerik:RadComboBox>
            <asp:ObjectDataSource ID="objApplicationId" runat="server" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetApplication" 
                TypeName="HRM.Common.BLL.MksSecurity">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="ApplicationId" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td>Menu Name</td>
        <td>
            <telerik:RadTextBox ID="radtxtMenuName" Runat="server" Skin="Windows7">
            </telerik:RadTextBox>
        </td>
    </tr>
    <tr>
        <td>&nbsp;</td>
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
        <td class="style16">
            <asp:HiddenField ID="hfMenuId" runat="server" />
        </td>
        <td class="style16">
            &nbsp;</td>
    </tr>
    <tr>
        <td class="style16" colspan="2">
            <telerik:RadGrid ID="radgvDetails" runat="server" Skin="Windows7" 
                AllowPaging="True" AutoGenerateColumns="False" CellSpacing="0" 
                GridLines="None" DataSourceID="objdsMenu" 
                onitemcommand="radgvDetails_ItemCommand" AllowSorting="True">
<MasterTableView DataKeyNames="ApplicationId,MenuId" DataSourceID="objdsMenu">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridButtonColumn ButtonType="PushButton" 
            CommandArgument="ApplicationId, MenuId" CommandName="Modify" 
            FilterControlAltText="Filter column2 column" Text="Modify" UniqueName="column2">
        </telerik:GridButtonColumn>
        <telerik:GridBoundColumn DataField="ApplicationName" 
            FilterControlAltText="Filter column column" HeaderText="Application Name" 
            UniqueName="column">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="MenuName" 
            FilterControlAltText="Filter column1 column" HeaderText="Menu Name" 
            UniqueName="column1">
        </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Windows7"></HeaderContextMenu>
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="objdsMenu" runat="server" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetMenu" 
                TypeName="HRM.Common.BLL.MksSecurity">
                <SelectParameters>
                    <asp:ControlParameter ControlID="radcboApplicationId" DefaultValue="" 
                        Name="ApplicationId" PropertyName="SelectedValue" Type="String" />
                    <asp:Parameter DefaultValue="0" Name="MenuId" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td class="style16">&nbsp;</td>
        <td class="style16">&nbsp;</td>
    </tr>
    </table>