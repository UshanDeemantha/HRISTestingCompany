<%@ Page Title="HR | Membership" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="Membership.aspx.cs" Inherits="Membership_Membership" StylesheetTheme="Common" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            height: 22px;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
             <asp:HiddenField ID="hfMembershipId" runat="server" />
            <table>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label4" runat="server" SkinID="FormHeader" Text="Membership "></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <asp:ObjectDataSource ID="odsMembershipTypes" runat="server" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetMembershipType" 
                            TypeName="HRM.HR.BLL.Membership">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="MembershipTypeId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                 <tr>
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Membership Code"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtMembershipCode" runat="server" MaxLength="50" 
                            Skin="Windows7" Width="150px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                            ControlToValidate="txtMembershipCode" ErrorMessage="Required Field!" 
                            ValidationGroup="a" CssClass="validator">*</asp:RequiredFieldValidator>
                       
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label6" runat="server" Text="Membership Type"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlMembershipType" runat="server" AutoPostBack="True" 
                            DataSourceID="odsMembershipTypes" DataTextField="MembershipTypeName" 
                            DataValueField="MembershipTypeID" ondatabound="ddlMembershipType_DataBound" 
                            Skin="Windows7" Width="250px" />
                    </td>
                </tr>
               
                <tr>
                    <td class="style1">
                        <asp:Label ID="Label7" runat="server" Text="Membership Name"></asp:Label>
                    </td>
                    <td class="style1">
                        <telerik:RadTextBox ID="txtMembershipName" runat="server" MaxLength="50" 
                            Skin="Windows7" Width="250px"/>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                            ControlToValidate="txtMembershipName" ErrorMessage="Required Field!" 
                            ValidationGroup="a" CssClass="validator">*</asp:RequiredFieldValidator>
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
                    <td style="text-align: right">
                        &nbsp;</td>
                    <td>
                        <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save" 
                            ToolTip="Add Information" ValidationGroup="a" Skin="WebBlue" />
                        &nbsp;<telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" 
                            Text="Update" ValidationGroup="a" Skin="WebBlue" />
                        &nbsp;<telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                            OnClientClick="return confirm('Are you sure you want to Delete this Record?');" 
                            Text="Delete" ToolTip="Delete the Record" Skin="WebBlue" />
                        &nbsp;<telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                            Text="Cancel" ToolTip="Clear Controls" Skin="WebBlue" />
                    </td>
                </tr>
                <tr>
                    <td style="text-align: right">
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="text-align: right">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" 
                            AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" 
                            AllowSorting="True" ataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
                            CellSpacing="0" DataSourceID="objdsMembership" GridLines="None" 
                            onitemcommand="RadGrid1_ItemCommand" onitemcreated="RadGrid1_ItemCreated" 
                            PageSize="7" ShowFooter="True" ShowStatusBar="True" Skin="Windows7" 
                            style="text-align: left">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="MembershipID" 
                                DataSourceID="objdsMembership" Width="100%">
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
                                    <telerik:GridBoundColumn DataField="MembershipID" 
                                        FilterControlAltText="Filter EmployeeMembershipID column" 
                                        HeaderText="MembershipID" UniqueName="MembershipID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MembershipCode" DataType="System.Int64" 
                                        FilterControlAltText="Filter EmployeeID column" HeaderText="Membership Code" 
                                        ReadOnly="True" SortExpression="MembershipCode" 
                                        UniqueName="MembershipCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyTypeID" 
                                        FilterControlAltText="Filter FullName column" HeaderText="FluencyTypeID" 
                                        SortExpression="FluencyTypeID" UniqueName="MembershipCode" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MembershipTypeID" DataType="System.Int32" 
                                        FilterControlAltText="Filter DefaultShiftId column" 
                                        HeaderText="MembershipTypeID" SortExpression="MembershipTypeID" 
                                        UniqueName="MembershipTypeID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyName" 
                                        FilterControlAltText="Filter FluencyName column" HeaderText="Membership Type Name" 
                                        SortExpression="MembershipTypeName" UniqueName="MembershipTypeName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="MembershipName" 
                                        FilterControlAltText="Filter MembershipName column" 
                                        HeaderText="Membership Name" SortExpression="MembershipName" 
                                        UniqueName="MembershipName">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <CommandItemTemplate>
                                    <div style="padding: 5px 5px;">
                                        <b>Membership : </b>&nbsp;
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
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Membership list</asp:LinkButton>
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
                        <asp:ObjectDataSource ID="objdsMembership" runat="server" 
                            SelectMethod="GetMembership" TypeName="HRM.HR.BLL.Membership">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="MembershipId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
