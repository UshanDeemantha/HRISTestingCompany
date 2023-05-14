<%@ Page Title="HR | Fluency Types" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="FluencyType.aspx.cs" Inherits="Fluency_FluencyType" StylesheetTheme="Common" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
           
            <table >
                <tr>
                    <td colspan="2">
                        <asp:Label ID="Label4" runat="server" SkinID="FormHeader" Text="Fluency Type"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label1" runat="server" Text="Fluency Type Code"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtFluencyCode" runat="server" MaxLength="50" 
                            Skin="Windows7" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtFluencyCode" ErrorMessage="Required Field!" 
                            ValidationGroup="a">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="Label2" runat="server" Text="Fluency Type Name"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtFluencyName" runat="server" MaxLength="50" 
                            Skin="Windows7" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                            ControlToValidate="txtFluencyName" ErrorMessage="Required Field!" 
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
                    <td>
                        &nbsp;</td>
                    <td>
                        <telerik:RadButton ID="btnSave" runat="server" onclick="btnSave_Click" 
                            Skin="WebBlue" Text="Save" ToolTip="Add Information" ValidationGroup="a" />
                        &nbsp;<telerik:RadButton ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                            Skin="WebBlue" Text="Update" ToolTip="Press when need to save modify record" 
                            ValidationGroup="a" />
                        &nbsp;<telerik:RadButton ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                            onclientclick="return confirm('Are you sure you want to Delete this Record?');" 
                            Skin="WebBlue" Text="Delete" ToolTip="Delete the Record" />
                        &nbsp;<telerik:RadButton ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                            Skin="WebBlue" Text="Cancel" ToolTip="Clear Controls" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2">
                        <telerik:RadGrid ID="RadGrid1" runat="server" AllowAutomaticDeletes="True" 
                            AllowAutomaticInserts="True" AllowAutomaticUpdates="True" AllowPaging="True" 
                            AllowSorting="True" ataSourceID="SqlDataSource1" AutoGenerateColumns="False" 
                            CellSpacing="0" DataSourceID="objdsFluencyTypes" GridLines="None" 
                            onitemcommand="RadGrid1_ItemCommand" onitemcreated="RadGrid1_ItemCreated" 
                            PageSize="7" ShowFooter="True" ShowStatusBar="True" Skin="Windows7">
                            <PagerStyle Mode="NextPrevAndNumeric" />
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="FluencyTypeID" 
                                DataSourceID="objdsFluencyTypes" Width="100%">
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
                                    <telerik:GridBoundColumn DataField="FluencyTypeID" 
                                        FilterControlAltText="Filter EmployeeMembershipID column" 
                                        HeaderText="FluencyTypeID" UniqueName="FluencyTypeID" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyTypeCode" DataType="System.Int64" 
                                        FilterControlAltText="Filter EmployeeID column" HeaderText="Fluency Type Code" 
                                        ReadOnly="True" SortExpression="FluencyTypeCode" UniqueName="FluencyTypeCode">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="FluencyTypeName" 
                                        FilterControlAltText="Filter EmployeeCode column" 
                                        HeaderText="Fluency Type Name" SortExpression="FluencyTypeName" 
                                        UniqueName="FluencyTypeName">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <CommandItemTemplate>
                                    <div style="padding: 5px 5px;">
                                        <b>Fluancy Type : </b>&nbsp;
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
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Fluancy Type list</asp:LinkButton>
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
                    <td>
                        <asp:HiddenField ID="hfFluencyTypeId" runat="server" />
                        <asp:ObjectDataSource ID="objdsFluencyTypes" runat="server" 
                            SelectMethod="GetFluencyType" TypeName="HRM.HR.BLL.Fluency">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="FluencyTypeId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            <br />
         </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

