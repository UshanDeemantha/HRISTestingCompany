<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeChildControl.ascx.cs" Inherits="Employee_UserControls_EmployeeChildControl" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
    .auto-style1 {
        height: 38px;
    }

    .auto-style2 {
        margin-left: 20px;
    }
    .mt-2, .my-2 {
    margin-top: -7.5rem!important;
}
    td.wizard-hed {
    padding-top: 20px;
}
    td.wizard-linkback {
    margin-left: 40px;
}
    .card-body.shadow {
    width: 97%;
}
    label {
    margin-left: 10px;
}
    input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_EmployeeChildControl1_rbGender_1 {
    margin-left: 10px;
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
                                <asp:Label ID="Label5" runat="server" Text="Employee" CssClass="form-label"></asp:Label>
                                <telerik:RadComboBox ID="ddlEmployee" runat="server" AutoPostBack="True"
                                    Enabled="False" Width="100%" Skin="Windows7">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label72" CssClass="form-label" runat="server" Text="First Name" class="auto-style2"></asp:Label>
                                                        <telerik:RadTextBox ID="txtFirstName" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label73" CssClass="form-label" runat="server" Text="Last Name" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtLastName" runat="server" MaxLength="50"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label74" CssClass="form-label" runat="server" Text="Full Name" class="auto-style2"></asp:Label>

                                                        <telerik:RadTextBox ID="txtFullName" runat="server" MaxLength="50" TextMode="MultiLine" Height="40px"
                                                            Skin="Windows7" CssClass="form-control form-control-lg" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-3">
                                                        <asp:Label ID="Label76" CssClass="form-label" runat="server" Text="Date of Birth" class="auto-style2"></asp:Label>

                                                        <telerik:RadDatePicker ID="txtDateOfBirth" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar11" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput11" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>

                                                        <%--<dx:ASPxDateEdit ID="txtChildDate" runat="server" CssClass="form-control form-control-lg"></dx:ASPxDateEdit>--%>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label69" CssClass="form-label" runat="server" Text="Gender"></asp:Label>

                                                        <asp:RadioButtonList ID="rbGender" runat="server" RepeatDirection="Horizontal" CssClass="form-label cus-radio">
                                                            <asp:ListItem Selected="True">Male</asp:ListItem>
                                                            <asp:ListItem>Female</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                        <div class="col d-flex justify-content-end pr-2 mt-5" style="grid-gap: 6px">
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
                                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsChild">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="ChildID" HeaderStyle-Font-Bold="false" Visible="false" Caption="ChildID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="EmployeeID" HeaderStyle-Font-Bold="false" Visible="false" Caption="EmployeeID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="FirstName" Visible="true" HeaderStyle-Font-Bold="false" Caption="First Name" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="Expr1" Visible="true" HeaderStyle-Font-Bold="false" Caption="FullName" PropertiesEditType="TextBox" Width="100px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="LastName" Visible="false" HeaderStyle-Font-Bold="false" Caption="LastName" PropertiesEditType="TextBox" Width="100px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="Gender" Visible="true" HeaderStyle-Font-Bold="false" Caption="Gender" PropertiesEditType="TextBox" Width="100px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="DateOfBirth" Visible="false" HeaderStyle-Font-Bold="false" Caption="DateOfBirth" PropertiesEditType="TextBox" Width="100px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="6" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
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
                        <asp:HiddenField ID="hfChildId" runat="server" />
                        <asp:ObjectDataSource ID="objdsChild" runat="server" SelectMethod="GetEmployeeChildByEmployeeId"
                            TypeName="HRM.HR.BLL.Child" OldValuesParameterFormatString="original_{0}">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfEmployeeId" DefaultValue="0"
                                    Name="EmployeeId" PropertyName="Value" Type="Int64" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                        <asp:HiddenField ID="hfEmployeeId" runat="server" />
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
                <td style="text-align: right">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>

            </tr>
            <tr>
                <td style="text-align: right">&nbsp;</td>
                <td>
                
                </td>
            </tr>
        </table>
      
    </ContentTemplate>
</asp:UpdatePanel>
