<%@ Page Title="HRM Common | System Permissions" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="SystemPermission.aspx.cs" Inherits="Settings_SystemPermission" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td>
                Permission
                <asp:HiddenField ID="hfPermissionId" runat="server" />
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtPermission" runat="server" MaxLength="50" Width="175px"
                    Skin="Windows7">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radtxtPermission"
                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Permission Order No
            </td>
            <td>
                <telerik:RadNumericTextBox ID="radntxtPermissionOrderNo" runat="server" Width="175px"
                    Skin="Windows7" MaxValue="100" MinValue="0">
                    <NumberFormat DecimalDigits="0" />
                </telerik:RadNumericTextBox>
                <asp:RequiredFieldValidator ID="rfvPermissionOrderNo" runat="server" ControlToValidate="radntxtPermissionOrderNo"
                    ErrorMessage="*" ForeColor="Red"></asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                Remarks
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtRemarks" runat="server" Skin="Windows7" TextMode="MultiLine"
                    Height="60px" Width="220px" OnTextChanged="radtxtRemarks_TextChanged">
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
                <telerik:RadButton ID="radbtnSave" runat="server" Skin="WebBlue" Text="Save" OnClick="radbtnSave_Click">
                </telerik:RadButton>
                &nbsp;&nbsp;
                <telerik:RadButton ID="radbtnUpdate" runat="server" Skin="WebBlue" Text="Update"
                    OnClick="radbtnUpdate_Click">
                </telerik:RadButton>
                &nbsp;&nbsp;
                <telerik:RadButton ID="radbtnDelete" runat="server" Skin="WebBlue" Text="Delete"
                    OnClick="radbtnDelete_Click">
                </telerik:RadButton>
                &nbsp;&nbsp;
                <telerik:RadButton ID="radbtnCancel" runat="server" Skin="WebBlue" Text="Cancel"
                    OnClick="radbtnCancel_Click">
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
            <td colspan="2">
                <asp:ObjectDataSource ID="objdsPermission" runat="server" SelectMethod="GetPermission"
                    TypeName="HRM.Common.BLL.MksSecurity" OldValuesParameterFormatString="original_{0}">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="PermissionId" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <telerik:RadGrid ID="radgvDetails" runat="server" Skin="Windows7" AllowPaging="True"
                    AutoGenerateColumns="False" CellSpacing="0" DataSourceID="objdsPermission" GridLines="None"
                    OnItemCommand="radgvDetails_ItemCommand">
                    <MasterTableView DataKeyNames="PermissionId" DataSourceID="objdsPermission">
                        <CommandItemSettings ExportToPdfText="Export to PDF"></CommandItemSettings>
                        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                        </RowIndicatorColumn>
                        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                        </ExpandCollapseColumn>
                        <Columns>
                            <telerik:GridButtonColumn ButtonType="PushButton" CommandArgument="PermissionId"
                                CommandName="Modify" FilterControlAltText="Filter column column" Text="Modify"
                                UniqueName="column">
                            </telerik:GridButtonColumn>
                            <telerik:GridBoundColumn DataField="Permission" FilterControlAltText="Filter column1 column"
                                HeaderText="Permission" UniqueName="column1">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="PermissionOrderNo" FilterControlAltText="Filter column2 column"
                                HeaderText="Permission Order No" UniqueName="column2">
                            </telerik:GridBoundColumn>
                            <telerik:GridBoundColumn DataField="Remarks" FilterControlAltText="Filter column3 column"
                                HeaderText="Remarks" UniqueName="column3">
                            </telerik:GridBoundColumn>
                        </Columns>
                        <EditFormSettings>
                            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                            </EditColumn>
                        </EditFormSettings>
                    </MasterTableView>
                    <FilterMenu EnableImageSprites="False">
                    </FilterMenu>
                    <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Windows7">
                    </HeaderContextMenu>
                </telerik:RadGrid>
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
</asp:Content>
