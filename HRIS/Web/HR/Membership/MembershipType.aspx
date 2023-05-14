<%@ Page Title="HR | Membership Types" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="MembershipType.aspx.cs" Inherits="Membership_MembershipType"  StylesheetTheme="Common" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
             <asp:HiddenField ID="hfMembershipTypeId" runat="server" />
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label3" runat="server" SkinID="FormHeader" 
                            Text="Membership Type "></asp:Label>
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
                        <asp:Label ID="Label1" runat="server" Text="Membership Type Code"></asp:Label>
                    </td>
                    <td>
                      
                        <telerik:RadTextBox ID="txtMembershipTypeCode" runat="server" MaxLength="50" Width="150px" 
                            Skin="Windows7"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtMembershipTypeCode" ErrorMessage="Required Field!" 
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Membership Type Name"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtMembershipTypeName" runat="server" Width="250px" 
                            MaxLength="50" Skin="Windows7"></telerik:RadTextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtMembershipTypeName" ErrorMessage="Required Field!" 
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <td>
                        <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right" >
                        &nbsp;</td>
                    <td>
                        <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" 
                            ValidationGroup="a" Skin="WebBlue" />
                        &nbsp;<telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" 
                            Text="Update" ValidationGroup="a" Skin="WebBlue" />
                        &nbsp;<telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                            OnClientClick="return confirm('Are you sure you want to Delete this Record?');" 
                            Text="Delete" Skin="WebBlue" />
                        &nbsp;<telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                            Text="Cancel" Skin="WebBlue" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: left">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" 
                            AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" 
                            AllowSorting="True" ataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
                            CellSpacing="0" DataSourceID="objdsMembershipType" GridLines="None" 
                            onitemcommand="RadGrid1_ItemCommand" onitemcreated="RadGrid1_ItemCreated" 
                            PageSize="7" ShowFooter="True" ShowStatusBar="True" Skin="Windows7">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="MembershipTypeID" 
                                DataSourceID="objdsMembershipType" Width="100%">
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
                                    <telerik:GridBoundColumn DataField="MembershipTypeID" 
                                        FilterControlAltText="Filter EmployeeMembershipID column" 
                                        HeaderText="MembershipTypeID" UniqueName="MembershipTypeID" 
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MembershipTypeCode" DataType="System.Int64" 
                                        FilterControlAltText="Filter EmployeeID column" HeaderText="MembershipType Code" 
                                        ReadOnly="True" SortExpression="MembershipTypeCode" 
                                        UniqueName="MembershipTypeCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MembershipTypeName" 
                                        FilterControlAltText="Filter DefaultShiftId column" HeaderText="Membership Type Name" 
                                        SortExpression="MembershipTypeName" UniqueName="MembershipTypeName" 
                                        DataType="System.Int32">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <CommandItemTemplate>
                                    <div style="padding: 5px 5px;">
                                        <b>Membership Types : </b>&nbsp;
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
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Membership Type list</asp:LinkButton>
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
                    <td colspan="2" style="text-align: right">
                        <asp:ObjectDataSource ID="objdsMembershipType" runat="server" 
                            SelectMethod="GetMembershipType" TypeName="HRM.HR.BLL.Membership">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="MembershipTypeId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
