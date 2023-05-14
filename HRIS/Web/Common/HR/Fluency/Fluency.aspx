<%@ Page Title="HR | Fluency" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="Fluency.aspx.cs" Inherits="Fluency_Fluency"
    StylesheetTheme="Common" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%">
                <tr>
                    <td>
                        <asp:Label ID="Label17" runat="server" Height="30" Text="Fluency" Font-Names="Tahoma" Font-Bold="true" Font-Underline="true" ForeColor="#336699" Font-Size="Small"></asp:Label>
                    </td>
                    <td></td>
                </tr>
            </table>
            <table style="width: 50%;">
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Fluency Code"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtFluencyCode" runat="server" MaxLength="50" Skin="Windows7" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtFluencyCode"
                            ErrorMessage="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Fluency Type"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlFluencyType" runat="server" AutoPostBack="True" OnDataBound="ddlFluencyType_DataBound"
                            Skin="Windows7" Width="250px">
                        </telerik:RadComboBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label3" runat="server" Text="Fluency Name"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtFluencyName" runat="server" MaxLength="50" Skin="Windows7"
                            Width="250px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFluencyName"
                            ErrorMessage="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
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
                    <td>&nbsp;
                    </td>
                    <td>
                        <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Skin="WebBlue"
                            Text="Save" ToolTip="Add Information" ValidationGroup="a" />
                        &nbsp;<telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                            Skin="WebBlue" Text="Update" ToolTip="Press when need to save modify record"
                            ValidationGroup="a" />
                        &nbsp;<telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                            OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                            Skin="WebBlue" Text="Delete" ToolTip="Delete the Record" />
                        &nbsp;<telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                            Skin="WebBlue" Text="Cancel" ToolTip="Clear Controls" />
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" AllowAutomaticInserts="True"
                            AllowAutomaticUpdates="True" AllowPaging="True" AllowSorting="True" ataSourceID="SqlDataSource1"
                            AutoGenerateColumns="False" CellSpacing="0" DataSourceID="objdsFluency" GridLines="None"
                            OnItemCommand="RadGrid1_ItemCommand" OnItemCreated="RadGrid1_ItemCreated" PageSize="7"
                            ShowFooter="True" ShowStatusBar="True" Skin="Windows7">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="FluencyID" DataSourceID="objdsFluency"
                                Width="100%">
                                <EditItemTemplate>
                                    <table>
                                        <tr>
                                            <td>Currency Code
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="RadMaskedTextBox1" runat="server" Text='<%# Bind("CurrencyCode") %>'
                                                    Width="125px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Country
                                            </td>
                                            <td>
                                                <telerik:RadComboBox ID="RadComboBox1" runat="server" DataSourceID="XmlDataSource1"
                                                    DataTextField="code" DataValueField="iso" Height="22px" SelectedValue='<%# Bind("Country") %>'
                                                    Skin="Windows7" Width="133px">
                                                </telerik:RadComboBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Currency Name
                                            </td>
                                            <td>
                                                <telerik:RadMaskedTextBox ID="RadMaskedTextBox2" runat="server" Text='<%# Bind("CurrencyName") %>'
                                                    Width="125px">
                                                </telerik:RadMaskedTextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>&nbsp;
                                            </td>
                                            <td>&nbsp;
                                            </td>
                                        </tr>
                                    </table>
                                </EditItemTemplate>
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="FluencyID" FilterControlAltText="Filter EmployeeMembershipID column"
                                        HeaderText="FluencyID" UniqueName="FluencyID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyCode" DataType="System.Int64" FilterControlAltText="Filter EmployeeID column"
                                        HeaderText="Fluency Code" ReadOnly="True" SortExpression="FluencyCode" UniqueName="FluencyCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyTypeID" FilterControlAltText="Filter FullName column"
                                        HeaderText="FluencyTypeID" SortExpression="FluencyTypeID" UniqueName="FluencyTypeID"
                                        Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyTypeName" DataType="System.Int32" FilterControlAltText="Filter DefaultShiftId column"
                                        HeaderText="Fluency Type Name" SortExpression="FluencyTypeName" UniqueName="FluencyTypeName">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyName" FilterControlAltText="Filter FluencyName column"
                                        HeaderText="Fluency Name" SortExpression="FluencyName" UniqueName="FluencyName">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <CommandItemTemplate>
                                    <div style="padding: 5px 5px;">
                                        <b>Fluancy : </b>&nbsp;
                                        <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="Select" Visible="<%# RadGrid1.EditIndexes.Count == 0 %>"><img 
                    style="border:0px;vertical-align:middle;" alt="" 
                    src="../../App_Themes/CommonResources/RadGrid/Edit.gif" />Modify</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="btnCancel0" runat="server" CommandName="CancelAll" Visible="<%# RadGrid1.EditIndexes.Count > 0 || RadGrid1.MasterTableView.IsItemInserted %>"><img style="border:0px;vertical-align:middle;" 
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Cancel.gif" />Cancel editing</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <%--                    <asp:LinkButton ID="LinkButton1" OnClientClick="javascript:return confirm('Delete all selected employee pay ?')"
runat="server" CommandName="DeleteSelected"><img style="border:0px;vertical-align:middle;" alt="" src="../App_Themes/Images/RadGrid/Delete.gif" />Delete selected employee pay</asp:LinkButton>
                &nbsp;&nbsp;--%>
                                        <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid"><img style="border:0px;vertical-align:middle;" 
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Fluancy list</asp:LinkButton>
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
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:HiddenField ID="hfFluencyId" runat="server" />
                        <asp:ObjectDataSource ID="objdsFluency" runat="server" SelectMethod="GetFluency"
                            TypeName="HRM.HR.BLL.Fluency">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="FluencyId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
