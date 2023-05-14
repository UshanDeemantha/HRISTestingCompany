<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmoployeeMembershipControl.ascx.cs" Inherits="Employee_UserControls_EmoployeeMembershipControl" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
    .auto-style2 {
        margin-left: 20px;
    }
    input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmoployeeMembershipControl1_cbIsToDate {
    margin-left: 94%;
    margin-top: 2px;
}
    thead {
    background-color: #8cbcee54;
    font-family: "Hiragino Maru Gothic ProN";
    font-size: 12px;
    letter-spacing: 0.52px;
    line-height: 21px;
    background-color: white;
}
    td.wizard-hed {
    padding-top: 20px;
}
    table#ContentPlaceHolder1_EmployeeAditionalInfoWizard_SideBarContainer_SideBarList {
    margin-left: 20px;
}
    .card-body.shadow {
    width: 97%;
}
</style> 
<link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
        <div class="row">
            <div class="col-md-12" style="border-top-color: black; margin-left:20px">
                <div class="border">
                    <div class="card-body shadow" style="border-radius: 10px">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:HiddenField ID="hfEmployeeMembershipId" runat="server" />
                                <asp:Label ID="Label1" runat="server" Text="Employee" CssClass="form-label"></asp:Label>

                                <telerik:RadComboBox ID="ddlEmployee" runat="server" AutoPostBack="True"
                                    Enabled="False" Width="100%" Skin="Windows7">
                                </telerik:RadComboBox>

                            </div>
                        </div>
                        <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label49" runat="server" Text="Membership" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlMembership" runat="server" Skin="Windows7" Width="100%"
                                                            OnDataBound="ddlMembership_DataBound">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label50" runat="server" Text="From Date" CssClass="form-label"></asp:Label>

                                                        <telerik:RadDatePicker ID="txtFromDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar8" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput8" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label51" runat="server" Text="To Date" CssClass="form-label"></asp:Label>
                                                        <telerik:RadDatePicker ID="txtToDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label" Enabled="true"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar9" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput9" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                        <%--<dx:ASPxDateEdit ID="txtToDate" runat="server" Width="250px" CssClass="form-control form-control-lg" Enabled="false"></dx:ASPxDateEdit>--%>
                                                        <asp:CheckBox ID="cbIsToDate" runat="server" AutoPostBack="True" Text="Yes" CssClass="checkalign" Visible="false"
                                                            OnCheckedChanged="CheckBox1_CheckedChanged" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label52" runat="server" Text="Grade" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtGrade" runat="server" Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label53" runat="server" Text="Remark" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtRemark" runat="server"
                                                            TextMode="MultiLine" Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                        <div class="d-flex justify-content-end mr-lg-5 mt-5 mb-2" style="grid-gap: 15px;">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btnSave_Click" ToolTip="Add Information"
                                ValidationGroup="a" />
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnUpdate_Click" ToolTip="Press when need to save modify record"
                                ValidationGroup="a" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btnDelete_Click" ToolTip="Delete the Record"
                                OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                ValidationGroup="a" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btnCancel_Click" ToolTip="Clear Controls"
                                ValidationGroup="a" />
                        </div>
                        <div class="row">
                            <div class="col table-scroll1">
                                <dx:ASPxGridView ID="RadGrid1" CssClass="tableStyle" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsEmployeeMembership">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EmployeeMembershipID"  HeaderStyle-Font-Bold="false" Caption="EmployeeMembershipID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false"/>
                                        <dx:GridViewDataColumn FieldName="EmployeeID" HeaderStyle-Font-Bold="false" Visible="false" Caption="EmployeeID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="FullName" HeaderStyle-Font-Bold="false" Caption="Full Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="MembershipID" Visible="false" HeaderStyle-Font-Bold="false" Caption="Member" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="FromDate" Visible="true" HeaderStyle-Font-Bold="false" Caption="FromDate" PropertiesEditType="TextBox" Width="100px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="ToDate" Visible="true" HeaderStyle-Font-Bold="false" Caption="To Date" PropertiesEditType="TextBox" Width="100px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="Grade" Visible="true" HeaderStyle-Font-Bold="false" Caption="CourseName" PropertiesEditType="TextBox" Width="100px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="Remark" Visible="true" HeaderStyle-Font-Bold="false" Caption="Remark" PropertiesEditType="TextBox" Width="100px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />

                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="0" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" ForeColor="Blue"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" style="margin-top:-5px"/></asp:LinkButton>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                        <AlternatingRow Enabled="true" />
                                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true" Wrap="True"></Header>
                                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                    </Styles>
                                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />

                                    <SettingsPager Mode="ShowPager" PageSize="10" />
                                </dx:ASPxGridView>

                                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div id="toastrContainer">
            <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
            <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
        </div>
        <table class="Contact" style="width: 75%">
            <tr>
                <td>
                    <table>
             
                        <tr>
                            <td class="style4">&nbsp;</td>
                            <td>
                              
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
     
                </td>
            </tr>
        </table>
        <table class="Contact" style="width: 75%; margin-left: 20px">
            <tr>
                <td style="width: 143px;"></td>
                <td>
                  
                </td>
            </tr>
        </table>
       
        <table>
            <tr>
                <td>
                    <asp:ObjectDataSource ID="objdsEmployeeMembership" runat="server"
                        SelectMethod="GetEmployeeMembershipByEmployeeId"
                        TypeName="HRM.HR.BLL.Membership" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfEmployeeId" DefaultValue="0"
                                Name="EmployeeId" PropertyName="Value" Type="Int64" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfEmployeeId" runat="server" />
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
        <br />
    </ContentTemplate>
</asp:UpdatePanel>


