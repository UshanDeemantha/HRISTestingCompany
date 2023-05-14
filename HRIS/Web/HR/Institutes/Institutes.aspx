<%@ Page Title="HR | Institutes" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="Institutes.aspx.cs" Inherits="Institutes_Institutes" StylesheetTheme="Common" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
        <table class="style1">
            <tr>
                <td colspan="2">
                    <asp:Label ID="Label7" runat="server" SkinID="FormHeader" Text="Institutes"></asp:Label>
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
                    <asp:Label ID="Label1" runat="server" Text="Institute Code"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtInstituteCode" runat="server" MaxLength="50" 
                        Skin="Windows7" Width="150px">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                        ControlToValidate="txtInstituteCode" ErrorMessage="Required Field!" 
                        ValidationGroup="a">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="Institute Type"></asp:Label>
                </td>
                <td>
                    <telerik:RadComboBox ID="ddlInsitituteType" runat="server" 
                        ondatabound="ddlInsitituteType_DataBound" Skin="Windows7"  Width="250px">
                    </telerik:RadComboBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Institute Name"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtInstituteName" runat="server" MaxLength="50" 
                        Skin="Windows7" Width="250px">
                    </telerik:RadTextBox>
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                        ControlToValidate="txtInstituteName" ErrorMessage="Required Field!" 
                        ValidationGroup="a">*</asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label8" runat="server" Text="Contact No"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtTel" runat="server" MaxLength="10" Skin="Windows7"  Width="250px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label5" runat="server" Text="Fax"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtFax" runat="server" MaxLength="30" Skin="Windows7"  Width="250px">
                    </telerik:RadTextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label6" runat="server" Text="Address"></asp:Label>
                </td>
                <td>
                    <telerik:RadTextBox ID="txtAddress" runat="server" Height="82px" 
                        MaxLength="200" Skin="Windows7" TextMode="MultiLine" Width="250px">
                    </telerik:RadTextBox>
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
                        Skin="WebBlue" Text="Save" ValidationGroup="a" />
                    &nbsp;<telerik:RadButton ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
                        Skin="WebBlue" Text="Update" ValidationGroup="a" />
                    &nbsp;<telerik:RadButton ID="btnDelete" runat="server" onclick="btnDelete_Click" 
                        Skin="WebBlue" Text="Delete" ValidationGroup="a" />
                    &nbsp;<telerik:RadButton ID="btnCancel" runat="server" onclick="btnCancel_Click" 
                        Skin="WebBlue" Text="Cancel" />
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
                        CellSpacing="0" DataSourceID="objdsInstitute" GridLines="None" 
                        onitemcommand="RadGrid1_ItemCommand" onitemcreated="RadGrid1_ItemCreated" 
                        PageSize="7" ShowFooter="True" ShowStatusBar="True" Skin="Windows7">
                        <PagerStyle Mode="NextPrevAndNumeric" />
                        <MasterTableView CommandItemDisplay="Top" DataKeyNames="InstituteID" 
                            DataSourceID="objdsInstitute" Width="100%">
                            <EditItemTemplate>
                                <%--<table>
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
                                </table>--%>
                            </EditItemTemplate>
                            <CommandItemSettings ExportToPdfText="Export to PDF" />
                            <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                            </RowIndicatorColumn>
                            <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                            </ExpandCollapseColumn>
                            <Columns>
                                <telerik:GridBoundColumn DataField="InstituteID" 
                                    FilterControlAltText="Filter EmployeeMembershipID column" 
                                    HeaderText="InstituteID" UniqueName="InstituteID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="InstituteCode" DataType="System.Int64" 
                                    FilterControlAltText="Filter EmployeeID column" HeaderText="Institute Code" 
                                    ReadOnly="True" SortExpression="InstituteCode" UniqueName="InstituteCode">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="InstituteTypeID" 
                                    FilterControlAltText="Filter FullName column" HeaderText="InstituteTypeID" 
                                    SortExpression="InstituteTypeID" UniqueName="InstituteTypeID" Visible="False">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="InstituteTypeName" 
                                    FilterControlAltText="Filter DefaultShiftId column" 
                                    HeaderText="Institute Type Name" SortExpression="InstituteTypeName" 
                                    UniqueName="InstituteTypeName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="InstituteName" 
                                    FilterControlAltText="Filter FluencyName column" HeaderText="Institute Name" 
                                    SortExpression="InstituteName" UniqueName="InstituteName">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Tel" 
                                    FilterControlAltText="Filter Tel column" HeaderText="Contact No" SortExpression="Tel" 
                                    UniqueName="Tel">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Fax" 
                                    FilterControlAltText="Filter Fax column" HeaderText="Fax" SortExpression="Fax" 
                                    UniqueName="Fax">
                                </telerik:GridBoundColumn>
                                <telerik:GridBoundColumn DataField="Address" 
                                    FilterControlAltText="Filter Address column" HeaderText="Address" 
                                    SortExpression="Address" UniqueName="Address">
                                </telerik:GridBoundColumn>
                            </Columns>
                            <EditFormSettings>
                                <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                </EditColumn>
                            </EditFormSettings>
                            <CommandItemTemplate>
                                <div style="padding: 5px 5px;">
                                    <b>Institute : </b>&nbsp;
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
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Institute list</asp:LinkButton>
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
                    <asp:HiddenField ID="hfInstitutesId" runat="server" />
                    <asp:ObjectDataSource ID="objdsInstitute" runat="server" 
                        SelectMethod="GetInstituteDetails" TypeName="HRM.HR.BLL.Institute">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="InstituteId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td>
                    &nbsp;</td>
            </tr>
        </table>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

