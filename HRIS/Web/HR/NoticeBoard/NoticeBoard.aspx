<%@ Page Title="HR | Notice Board" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="NoticeBoard.aspx.cs" Inherits="NoticeBoard_NoticeBoard" StylesheetTheme="Common" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            height: 16px;
        }
                thead {
    background-color: #8cbcee54;
    font-family: "Hiragino Maru Gothic ProN";
    font-size: 12px;
    letter-spacing: 0.52px;
    line-height: 21px;
    background-color: white;
}
 .mt-2, .my-2 {
    margin-top: -2rem!important;
}
 .mt-1, .my-1 {
    margin-top: -0.5rem!important;
}
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
  <ContentTemplate>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
             <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
            <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script> 
                <div class="overflow-auto" style="height: calc(100vh - 110px)">
                    <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
                        <div class="row rm-margin">
                            <div class="col head-container">
                                <h4 class="header">Notice Board</h4>
                                <span onclick="toggleProfileForm(365);" id="profileButton" class="dot shadow"></span>
                            </div>
                        </div>
                        <div class="row">
                            <div class="col-md-12" style="border-top-color: black">
                                <div class="border" style="border-radius: 10px">
                                    <div class="card-body shadow" style="border-radius: 10px">
                                        <div class="row">
                                            <div class="col-md-6">
                                                <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Code"></asp:Label>
                                                <telerik:RadTextBox ID="txtNoticeBoardCode" runat="server" MaxLength="50"
                                                    Skin="Windows7" Width="100%" CssClass="form-control form-control-lg">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                    ControlToValidate="txtNoticeBoardCode" ErrorMessage="Required Field!"
                                                    ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="NoticeBoard Title"></asp:Label>
                                                <telerik:RadTextBox ID="txtNoticeBoardName" runat="server" MaxLength="50"
                                                    Skin="Windows7" Width="100%" CssClass="form-control form-control-lg">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                    ControlToValidate="txtNoticeBoardName" ErrorMessage="Required Field!"
                                                    ValidationGroup="a">*</asp:RequiredFieldValidator>

                                                <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="Active"></asp:Label>
                                                <asp:CheckBox ID="cbActive" runat="server" Checked="True" />
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-md-6 mt-2">
                                                <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Description"></asp:Label>
                                                <telerik:RadTextBox ID="txtDescription" runat="server" Height="63px"
                                                    MaxLength="250" Skin="Windows7" TextMode="MultiLine" Width="100%" CssClass="form-control form-control-lg">
                                                </telerik:RadTextBox>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                                    ControlToValidate="txtDescription" ErrorMessage="Required Field!"
                                                    ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6">
                                            </div>
                                        </div>
                                        <div class="row mt-1">
                                            <div class="col-md-6">
                                                <asp:Label ID="Label5" runat="server" Text="From Date" CssClass="form-label"></asp:Label>
                                                <telerik:RadDatePicker ID="raddpFromDate" runat="server" Culture="en-US" Width="100%"
                                                    FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                                    Skin="Windows7">
                                                    <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                        runat="server" Skin="Windows7" CssClass="calender shadow">
                                                    </Calendar>
                                                    <DateInput ID="DateInput2" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                        runat="server">
                                                    </DateInput>
                                                    <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                </telerik:RadDatePicker>
                                                <%--         <telerik:RadDatePicker ID="txtFromDate" runat="server" Skin="WebBlue" Width="250px">
                            <Calendar Skin="WebBlue" UseColumnHeadersAsSelectors="False"
                                UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                            </DateInput>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>--%>
                                                <asp:RangeValidator ID="RangeValidator2" runat="server"
                                                    ControlToValidate="raddpFromDate" ErrorMessage="RangeValidator"
                                                    MaximumValue="01/01/2050" MinimumValue="01/01/1900" SetFocusOnError="True"
                                                    Type="Date" ValidationGroup="a">Invalid Date</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                    ControlToValidate="raddpFromDate" ErrorMessage="Required Field!"
                                                    ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:Label ID="Label6" runat="server" Text="To Date" CssClass="form-label"></asp:Label>
                                                <telerik:RadDatePicker ID="raddpToDate" runat="server" Culture="en-US" Width="100%"
                                                    FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="2010-01-01" CssClass="form-label"
                                                    Skin="Windows7">
                                                    <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                        runat="server" Skin="Windows7" CssClass="calender shadow">
                                                    </Calendar>
                                                    <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                        runat="server">
                                                    </DateInput>
                                                    <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                </telerik:RadDatePicker>

                                                <%--         <telerik:RadDatePicker ID="txtToDate" runat="server" Skin="WebBlue" Width="250px">
                            <Calendar Skin="WebBlue" UseColumnHeadersAsSelectors="False"
                                UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                            </Calendar>
                            <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                            </DateInput>
                            <DatePopupButton HoverImageUrl="" ImageUrl="" />
                        </telerik:RadDatePicker>--%>
                                                <asp:RangeValidator ID="RangeValidator3" runat="server"
                                                    ControlToValidate="raddpToDate" ErrorMessage="RangeValidator"
                                                    MaximumValue="01/01/2050" MinimumValue="01/01/1900" SetFocusOnError="True"
                                                    Type="Date" ValidationGroup="a">Invalid Date</asp:RangeValidator>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                                    ControlToValidate="raddpToDate" ErrorMessage="Required Field!"
                                                    ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            </div>
                                        </div>
                                        <div class="row">
                                        </div>
                                        <div class="col d-flex justify-content-end pr-2" style="grid-gap: 6px">
                                            <asp:Button ID="btnSaves" runat="server" OnClick="btnSave_Click"
                                                Text="Save" ValidationGroup="a" CssClass="btn btn-outline-primary check-aspbtn" />
                                            <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btn btn-outline-success check-aspbtn"
                                                Text="Update" ValidationGroup="a" />
                                            <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="btn btn-outline-danger check-aspbtn"
                                                OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                                Text="Delete" />
                                            <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-outline-secondary check-aspbtn"
                                                Text="Cancel" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                   
                        <div class="row" style="position: relative">
                            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                                <ContentTemplate>
                                    <img alt="" src="../../App_Themes/NewTheme/img/exceldwn.png" />
                                </ContentTemplate>
                            </telerik:RadButton>
                            <div class="col table-scroll1">
                                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" CssClass="tableStyle" Theme="MetropolisBlue" DataSourceID="objdsNoticeBoard"
                                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="NoticeBoardID" Caption="PK" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="100px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                                        <dx:GridViewDataColumn FieldName="NoticeBoardCode" Caption="Notice Board Code" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="150px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                        <dx:GridViewDataColumn FieldName="NoticeBoardTitle" Caption="Notice Board Title" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="200px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                        <dx:GridViewDataColumn FieldName="NoticeBoardDescription" HeaderStyle-CssClass="headerStyle" HeaderStyle-Font-Bold="false" CellStyle-CssClass="cellStyle" Caption="Notice Board Description" PropertiesEditType="TextBox" Width="400px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" />
                                        <dx:GridViewDataColumn FieldName="NoticeBoardCreateDate" Caption="Notice Board C Date" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="200px" VisibleIndex="4" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                                        <dx:GridViewDataColumn FieldName="NoticeBoardFromDate" Caption="Notice Board F Date" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="200px" VisibleIndex="5" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                        <dx:GridViewDataColumn FieldName="NoticeBoardToDate" Caption="Notice Board To Date" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="200px" VisibleIndex="6" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                        <dx:GridViewDataColumn FieldName="Active" Caption="Active" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false" Width="200px" VisibleIndex="7" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                        <dx:GridViewDataColumn Caption="" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false" Width="100px" VisibleIndex="0">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" onclientclicked="showForm" OnClick="lkSelect_Click" ForeColor="Blue" Cell-HorizontalAlign="Left"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                                </asp:LinkButton>
                                            </DataItemTemplate>
                                        </dx:GridViewDataColumn>
                                    </Columns>
                                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                    </Styles>
                                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                    <SettingsPager Mode="ShowPager" PageSize="4" />
                                </dx:ASPxGridView>  
                                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="RadGrid1">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                <div id="toastrContainer">
                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                    <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                </div>
            <table style="width: 50%;">

                <tr>
                    <td>
                        
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

                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>
                       
                    </td>
                </tr>
            </table>
              <tr>
                    <td>
                        <asp:HiddenField ID="hfNoticeBoardId" runat="server" />
                        <asp:ObjectDataSource ID="objdsNoticeBoard" runat="server"
                            SelectMethod="GetNoticeBoard" TypeName="HRM.HR.BLL.NoticeBoard">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="NoticeBoardId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                    <td>&nbsp;</td>
                </tr>
            </table>


                    </div>
        </ContentTemplate>
                                    
 </asp:Content>
