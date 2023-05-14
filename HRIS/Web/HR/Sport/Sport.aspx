<%@ Page Title="HR | Sport" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master"  AutoEventWireup="true" CodeFile="Sport.aspx.cs" Inherits="Sport_Sport"  StylesheetTheme="Common" %>


<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1
        {
            width: 100%;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
              <table class="style1">
                  <tr>
                      <td colspan="2">
                          <asp:Label ID="Label3" runat="server" SkinID="FormHeader" Text="Sport"></asp:Label>
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
                          <asp:Label ID="Label1" runat="server" Text="Sport Code"></asp:Label>
                      </td>
                      <td>
                          <telerik:RadTextBox ID="txtSportCode" runat="server" MaxLength="50" Width="150px" 
                              Skin="Windows7" Style="margin-left: 0px">
                          </telerik:RadTextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                              ControlToValidate="txtSportCode" CssClass="validator" 
                              ErrorMessage="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                      </td>
                  </tr>
                  <tr>
                      <td>
                          <asp:Label ID="Label2" runat="server" Text="Sport Name"></asp:Label>
                      </td>
                      <td>
                          <telerik:RadTextBox ID="txtSportName" runat="server" MaxLength="50" Width="250px" 
                              Skin="Windows7">
                          </telerik:RadTextBox>
                          <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
                              ControlToValidate="txtSportName" CssClass="validator" 
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
                      <td>
                          &nbsp;</td>
                      <td>
                          <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" 
                              Skin="WebBlue" Text="Save" ValidationGroup="a" />
                          &nbsp;<telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" 
                              Skin="WebBlue" Text="Update" ValidationGroup="a" />
                          &nbsp;<telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                              OnClientClick="return confirm('Are you sure you want to Delete this Record?');" 
                              Skin="WebBlue" Text="Delete" />
                          &nbsp;<telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
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
                              CellSpacing="0" DataSourceID="objdsSport" GridLines="None" 
                              onitemcommand="RadGrid1_ItemCommand" onitemcreated="RadGrid1_ItemCreated" 
                              PageSize="7" ShowFooter="True" ShowStatusBar="True" Skin="Windows7">
                              <PagerStyle Mode="NextPrevAndNumeric" />
                              <MasterTableView CommandItemDisplay="Top" DataKeyNames="SportID" 
                                  DataSourceID="objdsSport" Width="100%">
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
                                      <telerik:GridBoundColumn DataField="SportID" 
                                          FilterControlAltText="Filter EmployeeMembershipID column" HeaderText="SportID" 
                                          UniqueName="SportID" Visible="False">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="SportCode" 
                                          FilterControlAltText="Filter EmployeeID column" HeaderText="Sport Code" 
                                          ReadOnly="True" SortExpression="SportCode" UniqueName="SportCode">
                                      </telerik:GridBoundColumn>
                                      <telerik:GridBoundColumn DataField="SportName" 
                                          FilterControlAltText="Filter DefaultShiftId column" HeaderText="Sport Name" 
                                          SortExpression="SportName" UniqueName="SportName">
                                      </telerik:GridBoundColumn>
                                  </Columns>
                                  <EditFormSettings>
                                      <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                      </EditColumn>
                                  </EditFormSettings>
                                  <CommandItemTemplate>
                                      <div style="padding: 5px 5px;">
                                          <b>Fluancy : </b>&nbsp;
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
                      <td>
                          &nbsp;</td>
                      <td>
                          &nbsp;</td>
                  </tr>
                  <tr>
                      <td>
                          <asp:HiddenField ID="hfSportId" runat="server" />
                          <asp:ObjectDataSource ID="objdsSport" runat="server" SelectMethod="GetSport" 
                              TypeName="HRM.HR.BLL.Sport">
                              <SelectParameters>
                                  <asp:Parameter DefaultValue="0" Name="SportId" Type="Int32" />
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

