<%@ Page Title="HR | Certifications" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="Certification.aspx.cs" Inherits="Certification_Certification" StyleSheetTheme="Common" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
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
                         <asp:Label runat="server" SkinID="FormHeader" Text="Certification"></asp:Label>
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
                         <asp:Label runat="server" Text="Certification Code"></asp:Label>
                     </td>
                     <td>
                         <telerik:RadTextBox ID="txtCertificationCode" runat="server" MaxLength="50" Width="150px" 
                             Skin="Windows7" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                             ControlToValidate="txtCertificationCode" ErrorMessage="Required Field!" 
                             ValidationGroup="a">*</asp:RequiredFieldValidator>
                     </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:Label ID="Label7" runat="server" Text="Institute"></asp:Label>
                     </td>
                     <td>
                         <telerik:RadComboBox ID="ddlInstitue" runat="server" 
                             ondatabound="ddlInstitue_DataBound" Width="250px" Skin="Windows7">
                         </telerik:RadComboBox>
                     </td>
                 </tr>
                 <tr>
                     <td>
                         <asp:Label ID="Label8" runat="server" Text="Certification Name"></asp:Label>
                     </td>
                     <td>
                         <telerik:RadTextBox ID="txtCertificationName" runat="server" MaxLength="50" 
                             Width="250px" Skin="Windows7" />
                         <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                             ControlToValidate="txtCertificationName" ErrorMessage="Required Field!" 
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
                         <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" 
                             Skin="WebBlue" Text="Save" ToolTip="Add Information" ValidationGroup="a" />
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
                             CellSpacing="0" GridLines="None" OnItemDeleted="RadGrid1_ItemDeleted" 
                             PageSize="7" ShowFooter="True" ShowStatusBar="True" Skin="Windows7" 
                             DataSourceID="objdsCertification" onitemcommand="RadGrid1_ItemCommand" 
                             onitemcreated="RadGrid1_ItemCreated">
                             <PagerStyle Mode="NextPrevAndNumeric" />
                             <MasterTableView CommandItemDisplay="Top" DataKeyNames="CertificationID" 
                                 Width="100%" DataSourceID="objdsCertification">
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
                                     <telerik:GridBoundColumn DataField="CertificationID" 
                                         FilterControlAltText="Filter EmployeeMembershipID column" 
                                         HeaderText="CertificationID" UniqueName="CertificationID" Visible="False">
                                     </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="CertificationCode" DataType="System.Int64" 
                                         FilterControlAltText="Filter EmployeeID column" HeaderText="Certification Code" 
                                         ReadOnly="True" SortExpression="CertificationCode" 
                                         UniqueName="CertificationCode">
                                     </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="CertificationName" 
                                         FilterControlAltText="Filter EmployeeCode column" 
                                         HeaderText="Certification Name" SortExpression="CertificationName" 
                                         UniqueName="CertificationName">
                                     </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="InstitueID" 
                                         FilterControlAltText="Filter FullName column" HeaderText="InstitueID" 
                                         SortExpression="InstitueID" UniqueName="InstitueID" Visible="False">
                                     </telerik:GridBoundColumn>
                                     <telerik:GridBoundColumn DataField="InstituteName" DataType="System.Int32" 
                                         FilterControlAltText="Filter DefaultShiftId column" HeaderText="Institute Name" 
                                         SortExpression="InstituteName" UniqueName="InstituteName">
                                     </telerik:GridBoundColumn>
                                 </Columns>
                                 <EditFormSettings>
                                     <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                     </EditColumn>
                                 </EditFormSettings>
                                 <CommandItemTemplate>
                                     <div style="padding: 5px 5px;">
                                         <b>Certifications : </b>&nbsp;
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
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Certification list</asp:LinkButton>
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
                         <asp:ObjectDataSource ID="objdsCertification" runat="server" 
                             SelectMethod="GetCertification" TypeName="HRM.HR.BLL.Certification">
                             <SelectParameters>
                                 <asp:Parameter DefaultValue="0" Name="CertificationId" Type="Int32" />
                             </SelectParameters>
                         </asp:ObjectDataSource>
                         <asp:HiddenField ID="hfCertificationId" runat="server" Value="0" />
                     </td>
                     <td>
                         &nbsp;</td>
                 </tr>
            </table>
            <br />
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

