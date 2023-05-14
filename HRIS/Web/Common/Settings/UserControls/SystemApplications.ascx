<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SystemApplications.ascx.cs" Inherits="Common_Settings_UserControls_SystemApplications" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<style type="text/css">
        .style16
        {
            height: 18px;
        }
    </style>
      <table>
    <tr>
        <td>Application Code</td>
        <td><telerik:RadTextBox ID="radtxtApplicationCode" Runat="server" MaxLength="10" 
                Skin="Windows7">
            </telerik:RadTextBox>
</td>
    </tr>
    <tr>
        <td>Application Name</td>
        <td>
            <telerik:RadTextBox ID="radtxtApplicationName" Runat="server" Skin="Windows7">
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
           &nbsp;
        </td>
        <td>
        <telerik:RadButton ID="radbtnSave" runat="server" Skin="WebBlue" Text="Save" 
                onclick="radbtnSave_Click">
            </telerik:RadButton>&nbsp;&nbsp;
        <telerik:RadButton ID="radbtnUpdate" runat="server" Skin="WebBlue" Text="Update" 
                onclick="radbtnUpdate_Click">
            </telerik:RadButton>&nbsp;&nbsp;
        <telerik:RadButton ID="radbtnDelete" runat="server" Skin="WebBlue" 
                Text="Delete" onclick="radbtnDelete_Click">
            </telerik:RadButton>&nbsp;&nbsp;
        <telerik:RadButton ID="radbtnCancel" runat="server" Skin="WebBlue" Text="Cancel" 
                onclick="radbtnCancel_Click">
            </telerik:RadButton>
        </td>
    </tr>
    <tr>
        <td class="style16"></td>
        <td class="style16">
            <asp:HiddenField ID="hfApplicationId" runat="server" Visible="False" />
        </td>
    </tr>
    <tr>
        <td class="style16" colspan="2">
            <telerik:RadGrid ID="radgvDetails" runat="server" AutoGenerateColumns="False" 
                CellSpacing="0" DataSourceID="objdsApplication" GridLines="None" 
                Skin="Windows7" AllowPaging="True" 
                onitemcommand="radgvDetails_ItemCommand" Height="71px" Width="1289px">
<MasterTableView DataSourceID="objdsApplication" DataKeyNames="ApplicationId">
<CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>

<RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
<HeaderStyle Width="20px"></HeaderStyle>
</RowIndicatorColumn>

<ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
<HeaderStyle Width="20px"></HeaderStyle>
</ExpandCollapseColumn>

    <Columns>
        <telerik:GridButtonColumn ButtonType="PushButton" 
            CommandArgument="ApplicationId" 
            FilterControlAltText="Filter column2 column" Text="Modify" 
            UniqueName="column2" CommandName="Modify">
        </telerik:GridButtonColumn>
        <telerik:GridBoundColumn DataField="ApplicationCode" 
            FilterControlAltText="Filter column1 column" HeaderText="Application Code" 
            UniqueName="column1">
        </telerik:GridBoundColumn>
        <telerik:GridBoundColumn DataField="ApplicationName" 
            FilterControlAltText="Filter column column" HeaderText="Application Name" 
            UniqueName="column">
        </telerik:GridBoundColumn>
    </Columns>

<EditFormSettings>
<EditColumn FilterControlAltText="Filter EditCommandColumn column"></EditColumn>
</EditFormSettings>
</MasterTableView>

<FilterMenu EnableImageSprites="False"></FilterMenu>

<HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default"></HeaderContextMenu>
            </telerik:RadGrid>
            <asp:ObjectDataSource ID="objdsApplication" runat="server" 
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetApplication" 
                TypeName="HRM.Common.BLL.MksSecurity">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="ApplicationId" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            </td>
        </tr>
    <tr>
        <td>&nbsp;</td>
        <td>&nbsp;</td>
    </tr>
   </table>