<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeCertificationControl.ascx.cs" Inherits="Employee_UserControls_EmployeeCertificationControl" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
    
    .auto-style2 {
        margin-left: 20px;
    }
    .card-body.shadow {
    width: 96%;
}
    td.wizard-hed {
    padding-top: 20px;
}
    table#ContentPlaceHolder1_EmployeeAditionalInfoWizard_SideBarContainer_SideBarList {
    margin-left: 20px;
}
</style>

<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
        <script type="text/javascript" src="../../../App_Themes/NewTheme/script/form.js"></script>
         <div class="row">
                        <div class="col-md-12" style="border-top-color: black; margin-left:20px">
                            <div class="border ">
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
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label1" runat="server" Text="Institute" CssClass="form-label" />

                                                        <telerik:RadComboBox ID="ddlInstitute" runat="server" AutoPostBack="True" DataSourceID="SqlDsInstitution"
                                                            Skin="Windows7" Width="100%" DataTextField="InstituteCode" DataValueField="InstituteId" TextFormatString="{0}">
                                                        </telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="SqlDsInstitution" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="SELECT [InstituteId], [InstituteCode], [InstituteName], [Active] FROM [Institute]"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                    <div class="row mt-4">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label2" runat="server" Text="Certification" CssClass="form-label" />

                                                        <telerik:RadComboBox ID="ddlCertification" runat="server" AutoPostBack="True" Skin="Windows7" Width="100%" DataSourceID="sqldsCertificationType"
                                                            DataValueField="CertificationID" DataTextField="CertificationName" ValueType="System.String" TextFormatString="{0}">
                                                        </telerik:RadComboBox>
                                                        <asp:SqlDataSource ID="sqldsCertificationType" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="SELECT [CertificationID], [CertificationCode],[CertificationName] FROM [Certifications] WHERE ([InstituteId] = @InstituteId)">
                                                            <SelectParameters>
                                                                <asp:ControlParameter ControlID="ddlInstitute" Name="InstituteId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                     <div class="row mt-4">
                                                    <div class="col-md-4">
                                                        <asp:Label ID="Label38" runat="server" Text="Date" CssClass="form-label"></asp:Label>

                                                        <telerik:RadDatePicker ID="txtDate" runat="server" Culture="en-US" Width="100%"
                                                            FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                            Skin="Windows7">
                                                            <Calendar ID="Calendar7" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                                runat="server" Skin="Windows7" CssClass="calender shadow">
                                                            </Calendar>
                                                            <DateInput ID="DateInput7" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                                runat="server">
                                                            </DateInput>
                                                            <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                        </telerik:RadDatePicker>
                                                    </div>
                                                    <div class="col-md-2">

                                                        <asp:Label ID="Label47" runat="server" Text="Grade" CssClass="form-label" />

                                                        <telerik:RadTextBox ID="txtGrade" runat="server" CssClass="form-control form-control-lg" Width="100%"
                                                            Skin="Windows7" />

                                                    </div>
                                                </div>

                                    <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px; margin-bottom: 2%">
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
                                    <asp:HiddenField ID="hfemployeecertificationid" runat="server" />
                                    <div class="row">
                                        <div class="col table-scroll1">
                                            <dx:ASPxGridView ID="RadGrid1" CssClass="tableStyle" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                                                KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsEmployeeCertification">
                                                <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                                <Columns>
                                                    <dx:GridViewDataColumn FieldName="EmployeeCertificationID" HeaderStyle-Font-Bold="false" Caption="EmployeeCertificationID" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="FullName" HeaderStyle-Font-Bold="false" Visible="false" Caption="Full Name" PropertiesEditType="TextBox" Width="100px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="EmployeeID" HeaderStyle-Font-Bold="false" Visible="false" Caption="EmployeeID" PropertiesEditType="TextBox" Width="200px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="CertificationID" Visible="false" HeaderStyle-Font-Bold="false" Caption="CertificationID" PropertiesEditType="TextBox" Width="100px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="EmployeeCertificationID" Visible="false" HeaderStyle-Font-Bold="false" Caption="EmployeeCertificationID" PropertiesEditType="TextBox" Width="100px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="InstituteCode" HeaderStyle-Font-Bold="false" Caption="Institute Name" PropertiesEditType="TextBox" Width="100px" VisibleIndex="6" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="CertificationName" Visible="true" HeaderStyle-Font-Bold="false" Caption="Certification Name" PropertiesEditType="TextBox" Width="100px" VisibleIndex="7" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="Date" Visible="true" HeaderStyle-Font-Bold="false" Caption="Date" PropertiesEditType="TextBox" Width="100px" VisibleIndex="7" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataColumn FieldName="Grade" Visible="true" HeaderStyle-Font-Bold="false" Caption="Grade" PropertiesEditType="TextBox" Width="100px" VisibleIndex="7" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                                    <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="15" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
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
                    <asp:HiddenField ID="hfEmployeeId" runat="server" />
                    <asp:ObjectDataSource ID="objdsEmployeeCertification" runat="server"
                        SelectMethod="GetEmployeeCertificationByEmployeeID"
                        TypeName="HRM.HR.BLL.Certification"
                        OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfEmployeeId" DefaultValue="0"
                                Name="EmployeeId" PropertyName="Value" Type="Int64" />
                        </SelectParameters>
                    </asp:ObjectDataSource>

        <table class="Contact" style="width: 75%">
            <tr>
                <td>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>

        </table>
       
        <br />
        
    </ContentTemplate>
</asp:UpdatePanel>
