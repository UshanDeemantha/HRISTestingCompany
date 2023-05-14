<%@ Page Title="HR | Employee Membership" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeMembership.aspx.cs" Inherits="Membership_EmployeeMembership"  StylesheetTheme="Common"%>

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
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
      <table class="style1">
          <tr>
              <td colspan="2">
            <asp:Label ID="Label7" runat="server" SkinID="FormHeader" 
                Text="Employee Membership"></asp:Label>
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
    <asp:Label ID="Label1" runat="server" Text="Employee"></asp:Label>
              </td>
              <td>
    <telerik:RadComboBox ID="ddlEmployee" Width="300px" runat="server" Skin="Windows7" 
                    ondatabound="ddlEmployee_DataBound">
    </telerik:RadComboBox>
              </td>
          </tr>
          <tr>
              <td>
    <asp:Label ID="Label2" runat="server" Text="Membership"></asp:Label>
              </td>
              <td>
    <telerik:RadComboBox ID="ddlMembership" Width="300px" runat="server" Skin="Windows7" 
                    ondatabound="ddlMembership_DataBound">
    </telerik:RadComboBox>
              </td>
          </tr>
          <tr>
              <td>
    <asp:Label ID="Label3" runat="server" Text="From Date"></asp:Label>
              </td>
              <td>
                <telerik:RadDatePicker ID="txtFromDate" runat="server" Skin="WebBlue">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" 
                        Skin="WebBlue"></Calendar>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy"></DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                </telerik:RadDatePicker>
    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
        ControlToValidate="txtFromDate" ErrorMessage="RequiredFieldValidator" 
        SetFocusOnError="True" ToolTip="Required!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator1" runat="server" 
        ControlToValidate="txtFromDate" ErrorMessage="RangeValidator" 
        MaximumValue="01/01/2050" MinimumValue="01/01/1900" SetFocusOnError="True" 
        Type="Date" ValidationGroup="a">Invalid Date</asp:RangeValidator>
              </td>
          </tr>
          <tr>
              <td>
    <asp:Label ID="Label4" runat="server" Text="To Date"></asp:Label>
              </td>
              <td>
                <telerik:RadDatePicker ID="txtToDate" runat="server" Skin="WebBlue">
<Calendar UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x" 
                        Skin="WebBlue"></Calendar>

<DateInput DisplayDateFormat="M/d/yyyy" DateFormat="M/d/yyyy"></DateInput>

<DatePopupButton ImageUrl="" HoverImageUrl=""></DatePopupButton>
                </telerik:RadDatePicker>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" 
        ControlToValidate="txtToDate" ErrorMessage="RequiredFieldValidator" 
        SetFocusOnError="True" ToolTip="Required!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                <asp:RangeValidator ID="RangeValidator2" runat="server" 
        ControlToValidate="txtToDate" ErrorMessage="RangeValidator" 
        MaximumValue="01/01/2050" MinimumValue="01/01/1900" SetFocusOnError="True" 
        Type="Date" ValidationGroup="a">Invalid Date</asp:RangeValidator>
              </td>
          </tr>
          <tr>
              <td>
    <asp:Label ID="Label5" runat="server" Text="Grade"></asp:Label>
              </td>
              <td>
                <telerik:RadTextBox ID="txtGrade" runat="server" Skin="Windows7" />
              </td>
          </tr>
          <tr>
              <td>
    <asp:Label ID="Label6" runat="server" Text="Remark"></asp:Label>
              </td>
              <td>
                <telerik:RadTextBox ID="txtRemark" runat="server" Height="50px" Width="200px" 
                    TextMode="MultiLine" Skin="Windows7" />
              </td>
          </tr>
          <tr>
              <td style="text-align: right">
                            <asp:UpdateProgress ID="UpdateProgress1" runat="server">
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
    <telerik:RadButton ID="btnSave" runat="server" Text="Add" onclick="btnSave_Click" 
        ValidationGroup="a" ToolTip="Add Information" Skin="WebBlue" />
    &nbsp;<telerik:RadButton ID="btnUpdate" runat="server" onclick="btnUpdate_Click" 
        Text="Update" ToolTip="Press when need to save modify record" 
        ValidationGroup="a" Skin="WebBlue" />
    &nbsp;<telerik:RadButton ID="btnDelete" runat="server" Text="Delete" 
        onclick="btnDelete_Click" ToolTip="Delete the Record" 
        onclientclick="return confirm('Are you sure you want to Delete this Record?');" 
                    Skin="WebBlue" />
    &nbsp;<telerik:RadButton ID="btnCancel" runat="server" Text="Cancel" 
        onclick="btnCancel_Click" ToolTip="Clear Controls" Skin="WebBlue" />
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
<telerik:RadGrid ID="RadGrid1" 
        ShowStatusBar="True" 
        AllowAutomaticDeletes="True"
        AllowAutomaticInserts="True"
        AllowAutomaticUpdates="True" 
        AllowSorting="True" AutoGenerateColumns="False"
        AllowPaging="True" GridLines="None" runat="server" ShowFooter="True" PageSize="7" 
        OnItemDeleted="RadGrid1_ItemDeleted" Skin="Windows7" CellSpacing="0" 
        ataSourceID="SqlDataSource1" DataSourceID="objdsEmployeeMembership">
    <PagerStyle Mode="NextPrevAndNumeric" />
    <MasterTableView Width="100%" CommandItemDisplay="Top"
                DataKeyNames="EmployeeMembershipID" 
        DataSourceID="objdsEmployeeMembership">
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
        <CommandItemSettings ExportToPdfText="Export to PDF">
        </CommandItemSettings>
        <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
        </RowIndicatorColumn>
        <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
        </ExpandCollapseColumn>
        <Columns>
            <telerik:GridBoundColumn DataField="EmployeeMembershipID" 
                FilterControlAltText="Filter EmployeeMembershipID column" 
                HeaderText="EmployeeMembershipID" UniqueName="EmployeeMembershipID">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn FilterControlAltText="Filter EmployeeID column" 
                HeaderText="EmployeeID" UniqueName="EmployeeID" DataField="EmployeeID" 
                DataType="System.Int64" ReadOnly="True" SortExpression="EmployeeID" 
                Visible="False">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="FullName" 
                FilterControlAltText="Filter EmployeeCode column" HeaderText="Full Name" 
                SortExpression="FullName" UniqueName="FullName">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="MembershipID" 
                FilterControlAltText="Filter FullName column" HeaderText="MembershipID" 
                SortExpression="MembershipID" UniqueName="MembershipID" Visible="False">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="MembershipName" 
                FilterControlAltText="Filter DefaultShiftId column" HeaderText="Membership Name" 
                UniqueName="MembershipName" DataType="System.Int32" 
                SortExpression="MembershipName">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="FromDate" 
                FilterControlAltText="Filter RosterGroupId column" HeaderText="From Date" 
                UniqueName="FromDate" DataType="System.Int32" 
                SortExpression="From Date">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="ToDate" 
                FilterControlAltText="Filter ToDate column" HeaderText="To Date" 
                SortExpression="ToDate" UniqueName="ToDate">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Grade" 
                FilterControlAltText="Filter Grade column" HeaderText="Grade" 
                SortExpression="Grade" UniqueName="Grade">
            </telerik:GridBoundColumn>
            <telerik:GridBoundColumn DataField="Remark" 
                FilterControlAltText="Filter Remark column" HeaderText="Remark" 
                UniqueName="Remark">
            </telerik:GridBoundColumn>
        </Columns>
        <EditFormSettings>
            <EditColumn FilterControlAltText="Filter EditCommandColumn column">
            </EditColumn>
        </EditFormSettings>
        <CommandItemTemplate>
            <div style="padding: 5px 5px;">
                <b>Employee Membership : </b>&nbsp;
                    <asp:LinkButton ID="btnEditSelected" runat="server" CommandName="Select" Visible='<%# RadGrid1.EditIndexes.Count == 0 %>'><img style="border:0px;vertical-align:middle;" alt="" src="../../App_Themes/CommonResources/RadGrid/Edit.gif" />Modify</asp:LinkButton>
                &nbsp;&nbsp;
                    <asp:LinkButton ID="btnCancel0" runat="server" CommandName="CancelAll" 
                    Visible='<%# RadGrid1.EditIndexes.Count > 0 || RadGrid1.MasterTableView.IsItemInserted %>'><img style="border:0px;vertical-align:middle;" alt="" src="../../App_Themes/CommonResources/RadGrid/Cancel.gif" />Cancel editing</asp:LinkButton>
                &nbsp;&nbsp;
<%--                    <asp:LinkButton ID="LinkButton1" OnClientClick="javascript:return confirm('Delete all selected employee pay ?')"
runat="server" CommandName="DeleteSelected"><img style="border:0px;vertical-align:middle;" alt="" src="../App_Themes/Images/RadGrid/Delete.gif" />Delete selected employee pay</asp:LinkButton>
                &nbsp;&nbsp;--%>
                    <asp:LinkButton ID="LinkButton4" runat="server" CommandName="RebindGrid"><img style="border:0px;vertical-align:middle;" alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Membership list</asp:LinkButton>
            </div>
        </CommandItemTemplate>
    </MasterTableView>
    <ClientSettings>
        <Selecting AllowRowSelect="True" EnableDragToSelectRows="True" />
        <Selecting AllowRowSelect="True">
        </Selecting>
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
              <td colspan="2">
    <asp:GridView ID="gvDetails" runat="server" AllowPaging="True" 
        AutoGenerateColumns="False" DataSourceID="objdsEmployeeMembership" 
        onrowcommand="gvDetails_RowCommand" Width="696px">
        <Columns>
            <asp:TemplateField ShowHeader="False">
                <ItemTemplate>
                    <telerik:RadButton ID="Button1" runat="server" CausesValidation="false" CommandName="Modify" 
                        Text="Modify" CommandArgument='<% # Eval("EmployeeMembershipID") %>' 
                        Skin="WebBlue" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="EmployeeMembershipID" 
                HeaderText="Employee Membership ID" Visible="False" />
            <asp:BoundField DataField="EmployeeID" HeaderText="EmployeeID" 
                Visible="False" />
            <asp:BoundField DataField="FullName" HeaderText="Employee">
            <ItemStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="MembershipID" HeaderText="Membership ID" 
                Visible="False" />
            <asp:BoundField DataField="MembershipName" HeaderText="Membership ">
            <ItemStyle Width="200px" />
            </asp:BoundField>
            <asp:BoundField DataField="FromDate" HeaderText="FromDate" 
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="ToDate" HeaderText="ToDate" 
                DataFormatString="{0:d}" />
            <asp:BoundField DataField="Grade" HeaderText="Grade" />
            <asp:BoundField DataField="Remark" HeaderText="Remark">
            <ItemStyle Width="400px" />
            </asp:BoundField>
        </Columns>
    </asp:GridView>
              </td>
          </tr>
    </table>
    <br />
      <div id="FormContentArea">
        <div class="ContentTitle">
    <asp:HiddenField ID="hfEmployeeMembershipId" runat="server" />
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">
            </div>
        </div>
         <div class="ContentBlock">
                    <div class="ContentBlockHeader">
                        <div class="ContentProgressIcon">
                        </div>
                    </div>
                    <div class="ContentBlockDetail">
                    </div>
                </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
            </div>
            <div class="ContentBlockDetail">
    &nbsp;&nbsp; 
                &nbsp;
    &nbsp;
            </div>
        </div>
        <div class="ContentBlock">
           <div class="ContentGridArea">
    <asp:ObjectDataSource ID="objdsEmployeeMembership" runat="server" 
        SelectMethod="GetEmployeeMembership" TypeName="HRM.HR.BLL.Membership">
        <SelectParameters>
            <asp:Parameter DefaultValue="0" Name="EmployeeMembershipId" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>
           </div>
        </div>
     
    </div>
</asp:Content>

