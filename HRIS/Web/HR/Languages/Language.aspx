<%@ Page Title="HR | Language" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="Language.aspx.cs" Inherits="Languages_Language" StylesheetTheme="Common" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 265px;
        }
        .style2
        {
        }
        .style3
        {
            width: 104px;
            height: 26px;
        }
        .style4
        {
            height: 26px;
        }
        .style5
        {
            width: 245px;
            height: 26px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
            <table>
                <tr>
                    <th colspan="3" align="left">
                        Languages
                    </th>
                </tr>
                <tr>
                    <td class="style2">
                    </td>
                    <td>
                    </td>
                    <td align="left" class="style1">
                        <asp:HiddenField ID="hfLanguageId" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td class="style3">
                        <asp:Label ID="Label1" runat="server" Text="Language Name"></asp:Label>
                    </td>
                    <td class="style4">
                    </td>
                    <td class="style5">
                        <telerik:RadTextBox ID="txtLanguageName" runat="server" MaxLength="50" 
                            Width="250px" Skin="Windows7"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtLanguageName"
                            ErrorMessage="Required Field!" ValidationGroup="a" CssClass="validator">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td class="style2" align="right" colspan="2" rowspan="3" valign="top">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 40px; height: 40px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <td class="style1">
                        <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="style1">
                        <telerik:RadButton ID="btnSave" runat="server" Text="Save" 
                            OnClick="btnSave_Click" ValidationGroup="a"
                            ToolTip="Add Information" Skin="WebBlue" />&nbsp;<telerik:RadButton ID="btnUpdate" runat="server" Text="Update" ToolTip="Press when need to save modify record"
                            OnClick="btnUpdate_Click" ValidationGroup="a" Skin="WebBlue" />&nbsp;<telerik:RadButton ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
                            ToolTip="Delete the Record" 
                            OnClientClick="return confirm('Are you sure you want to Delete this Record?');" 
                            Skin="WebBlue" />
                        &nbsp;<telerik:RadButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click"
                            ToolTip="Clear Controls" Skin="WebBlue" />
                        &nbsp;</td>
                </tr>
                <tr>
                    <td class="style1">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" class="style2" colspan="3">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" 
                            AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" 
                            AllowSorting="True" ataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
                            CellSpacing="0" DataSourceID="objdsLanguage" GridLines="None" 
                            onitemcommand="RadGrid1_ItemCommand" onitemcreated="RadGrid1_ItemCreated" 
                            PageSize="7" ShowFooter="True" ShowStatusBar="True" Skin="Windows7">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="LanguageID" 
                                DataSourceID="objdsLanguage" Width="100%">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td>
                                                Currency Code</td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="RadMaskedTextBox1" Runat="server" 
                                                    Text='<%# Bind("CurrencyCode") %>' Width="125px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Country</td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBox1" Runat="server" 
                                                    DataSourceID="XmlDataSource1" DataTextField="code" DataValueField="iso" 
                                                    Height="22px" SelectedValue='<%# Bind("Country") %>' Skin="Windows7" 
                                                    Width="133px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                Currency Name</td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="RadMaskedTextBox2" Runat="server" 
                                                    Text='<%# Bind("CurrencyName") %>' Width="125px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="LanguageID" 
                                        FilterControlAltText="Filter EmployeeMembershipID column" 
                                        HeaderText="LanguageID" UniqueName="LanguageID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="LanguageName" DataType="System.Int64" 
                                        FilterControlAltText="Filter EmployeeID column" HeaderText="Language Name" 
                                        ReadOnly="True" SortExpression="LanguageName" UniqueName="LanguageName">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <CommandItemTemplate>
                                    <div style="padding: 5px 5px;">
                                        <b>Languages : </b>&nbsp;
                                        <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="Select" 
                                            Visible="<%# RadGrid1.EditIndexes.Count == 0 %>"><img 
                    style="border:0px;vertical-align:middle;" alt="" 
                    src="../../App_Themes/CommonResources/RadGrid/Edit.gif" />Modify</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="btnCancel0" runat="server" CommandName="CancelAll" 
                                            Visible="<%# RadGrid1.EditIndexes.Count > 0 || RadGrid1.MasterTableView.IsItemInserted %>"><img style="border:0px;vertical-align:middle;" 
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Cancel.gif" />Cancel editing</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <%--                    <asp:LinkButton ID="LinkButton1" OnClientClick="javascript:return confirm('Delete all selected employee pay ?')"
runat="server" CommandName="DeleteSelected"><img style="border:0px;vertical-align:middle;" alt="" src="../App_Themes/Images/RadGrid/Delete.gif" />Delete selected employee pay</asp:LinkButton>
                &nbsp;&nbsp;--%>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid"><img style="border:0px;vertical-align:middle;" 
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Language list</asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Windows7">
                            </HeaderContextMenu>
                        </telerik:RadGrid>
                    </td>
                </tr>
                <tr>
                    <td align="center" class="style2" colspan="3">
                        <asp:ObjectDataSource ID="objdsLanguage" runat="server" 
                            SelectMethod="GetLanguage" TypeName="HRM.HR.BLL.Languages">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="LanguageId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td class="style2">
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td class="style1">
                        &nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
