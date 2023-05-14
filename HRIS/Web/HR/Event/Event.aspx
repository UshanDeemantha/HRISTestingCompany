<%@ Page Title="HR | Event Submissions" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true" CodeFile="Event.aspx.cs" Inherits="Event_Event" StylesheetTheme="Common" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
      
        .style1 {
            width: 100%;
        }

        .btns,
label.choose:before {
  background: none repeat scroll 0 0 #00B7CD;
  border: 0 none;
  color: #FFFFFF;
  cursor: pointer;
  font-family: 'Altis_Book';
  font-size: 15px;
  padding: 3px 15px;
}
label.choose:before {
  content: 'Choose file';
  padding: 3px 6px;
  position: absolute;
}
button, input, optgroup, select, textarea {
    margin: 0;
    font-family: inherit;
    font-size: inherit;
    line-height: inherit;
    padding-left: 9px;
    color:red;
}
.mt-2, .my-2 {
    margin-top: -0.6rem!important;
}
.mt-3, .my-3 {
    margin-top: -2.3rem!important;
}
    </style>
     <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
        <script type="text/javascript" src="../../App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
       <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
           <div class="row rm-margin">
               <div class="col head-container">
                   <h4 class="header">Event Submission</h4>
                   <span onclick="toggleProfileForm(440);" id="profileButton" class="dot shadow"></span>
               </div>
           </div>
           <div class="row">
               <div class="col-md-12" style="border-top-color: black">
                   <div class="border" style="border-radius: 10px">
                       <div class="card-body shadow" style="border-radius: 10px">
                           <div class="row">
                               <div class="col-md-6">
                                   <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Event Code"></asp:Label>

                                   <telerik:RadTextBox ID="txtEventCode" runat="server" CssClass="form-control form-control-lg"
                                       Skin="Windows7" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                       ControlToValidate="txtEventCode" ErrorMessage="Required Field!"
                                       ValidationGroup="a">*</asp:RequiredFieldValidator>
                               </div>
                               <div class="col-md-6">
                                   <asp:Label ID="Label2"  CssClass="form-label" runat="server" Text="Event Type"></asp:Label>

                                   <telerik:RadComboBox ID="ddlEventType" runat="server" AutoPostBack="True" CssClass="form-label" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged"
                                       OnDataBound="ddlEventType_DataBound" Skin="Windows7" Width="250px" />
                               </div>
                           </div>
                           <div class="row mt-2">
                               <div class="col-md-6">
                                   <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Name"></asp:Label>

                                   <telerik:RadTextBox ID="txtName" runat="server"  CssClass="form-control form-control-lg"
                                       Skin="Windows7"  />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                       ControlToValidate="txtName" ErrorMessage="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                               </div>
                               <div class="col-md-6">
                                   <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="Description"></asp:Label>

                                   <telerik:RadTextBox ID="txtDescription" runat="server"  CssClass="form-control form-control-lg"
                                       MaxLength="50" Skin="Windows7"  TextMode="MultiLine" />
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                       ControlToValidate="txtDescription" ErrorMessage="Required Field!"
                                       ValidationGroup="a">*</asp:RequiredFieldValidator>
                               </div>
                           </div>
                           <div class="row">
                               <div class="col-md-6 mt-3">
                                   <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="From Date"></asp:Label>
                                   <telerik:RadDatePicker ID="txtFromDate" runat="server" Culture="en-US" Width="100%"
                                       FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="1900-01-01" CssClass="form-label"
                                       Skin="Windows7">
                                       <Calendar ID="Calendar2" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                           runat="server" Skin="Windows7" CssClass="calender shadow">
                                       </Calendar>
                                       <DateInput ID="DateInput2" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                           runat="server">
                                       </DateInput>
                                       <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                   </telerik:RadDatePicker>

                                   <asp:RangeValidator ID="RangeValidator1" runat="server"
                                       ControlToValidate="txtFromDate" ErrorMessage="RangeValidator"
                                       MaximumValue="01/01/2050" MinimumValue="01/01/1900" SetFocusOnError="True"
                                       Type="Date" ValidationGroup="a">Invalid Date</asp:RangeValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                       ControlToValidate="txtFromDate" ErrorMessage="Required Field!"
                                       ValidationGroup="a">*</asp:RequiredFieldValidator>
                               </div>
                               <div class="col-md-6">
                                   <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="To Date"></asp:Label>
                                   <telerik:RadDatePicker ID="txtToDate" runat="server" Culture="en-US" Width="100%"
                                       FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="1900-01-01" CssClass="form-label"
                                       Skin="Windows7">
                                       <Calendar ID="Calendar1" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                           runat="server" Skin="Windows7" CssClass="calender shadow">
                                       </Calendar>
                                       <DateInput ID="DateInput1" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                           runat="server">
                                       </DateInput>
                                       <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                   </telerik:RadDatePicker>

                                   <asp:RangeValidator ID="RangeValidator2" runat="server"
                                       ControlToValidate="txtToDate" ErrorMessage="RangeValidator"
                                       MaximumValue="01/01/2050" MinimumValue="01/01/1900" SetFocusOnError="True"
                                       Type="Date" ValidationGroup="a">Invalid Date</asp:RangeValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                       ControlToValidate="txtToDate" ErrorMessage="Required Field!"
                                       ValidationGroup="a">*</asp:RequiredFieldValidator>
                               </div>
                           </div>
                           <div class="row">
                               <div class="col-md-6 mt-3">
                                   <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Event Date"></asp:Label>
                                   <telerik:RadDatePicker ID="txtEventDate" runat="server" Culture="en-US" Width="100%"
                                       FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="1900-01-01" CssClass="form-label"
                                       Skin="Windows7">
                                       <Calendar ID="Calendar3" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                           runat="server" Skin="Windows7" CssClass="calender shadow">
                                       </Calendar>
                                       <DateInput ID="DateInput3" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                           runat="server">
                                       </DateInput>
                                       <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                   </telerik:RadDatePicker>

                                   <asp:RangeValidator ID="RangeValidator3" runat="server"
                                       ControlToValidate="txtEventDate" ErrorMessage="RangeValidator"
                                       MaximumValue="01/01/2050" MinimumValue="01/01/1900" SetFocusOnError="True"
                                       Type="Date" ValidationGroup="a">Invalid Date</asp:RangeValidator>
                                   <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server"
                                       ControlToValidate="txtEventDate" ErrorMessage="Required Field!"
                                       ValidationGroup="a">*</asp:RequiredFieldValidator>
                               </div>
                               <div class="col-md-6">
                                   <asp:Label ID="Label34" CssClass="form-label" runat="server" Text="Image"></asp:Label>

                                   <%--  <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional" >
                                <ContentTemplate>--%>
                                  <%-- <asp:FileUpload ID="fuEmployeeImage" runat="server" OnDataBinding="fuEmployeeImage_DataBinding" />--%>
                                   <div id=" fileupload">
                                       <label class="choose">
                                           <input id="browse" type="file" />
                                       </label>
                                      
                                   </div>
                                   
                                   <%-- </ContentTemplate>
                                    <triggers>
                                        <asp:AsyncPostBackTrigger ControlID="btnSave" EventName="Click" />
                                    </triggers>
                                </asp:UpdatePanel>--%>
                               </div>
                           </div>
                           <div class="row">
                               <div class="col-md-6">
                                   <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Active"></asp:Label>

                                   <asp:CheckBox ID="cbActive" runat="server" />
                                   <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                       <ProgressTemplate>
                                           <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                       </ProgressTemplate>
                                   </asp:UpdateProgress>
                               </div>
                               <div class="col-md-6">
                               </div>
                           </div>
                          
                               <div id="toastrContainer">
                                   <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                                   <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                               </div>
                            <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 7px">
                               <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click"
                                    Text="Save" ToolTip="Add Information" ValidationGroup="a" />
                               <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                   Skin="WebBlue" Text="Update" ToolTip="Press when need to save modify record"
                                   ValidationGroup="a" />
                               <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                   OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                   Text="Delete" ToolTip="Delete the Record" />
                               <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                   Text="Cancel" ToolTip="Clear Controls" />
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

            <div class="col table-scroll">
                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue"
                    KeyFieldName="EventID" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsEvent" ForeColor="Black">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="EventID" Caption="PK" PropertiesEditType="TextBox" Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="EventCode" HeaderStyle-Font-Bold="false" Caption="Event Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="EventTypeID" HeaderStyle-Font-Bold="false" Caption="EventTypeID" PropertiesEditType="TextBox" Width="300px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false"/>
                        <dx:GridViewDataColumn FieldName="EventTypeName" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Caption="Event Type" PropertiesEditType="TextBox" Width="200px" VisibleIndex="2" />
                        <dx:GridViewDataColumn FieldName="Name" HeaderStyle-Font-Bold="false" Caption="Event Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="3" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Description" HeaderStyle-Font-Bold="false" Caption="Description" PropertiesEditType="TextBox" Width="400px" VisibleIndex="4" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="FromDate" HeaderStyle-Font-Bold="false" Caption="From Date" PropertiesEditType="TextBox" Width="100px" VisibleIndex="5" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="ToDate" HeaderStyle-Font-Bold="false" Caption="To Date" PropertiesEditType="TextBox" Width="100px" VisibleIndex="6" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="EventDate" HeaderStyle-Font-Bold="false" Caption="Event Date" PropertiesEditType="TextBox" Width="100px" VisibleIndex="7" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="Active" HeaderStyle-Font-Bold="false" Caption="Registration No" PropertiesEditType="TextBox" Width="150px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelectPopup" runat="server" onclientclicked="showForm" OnClick="lkSelectPopup_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                </asp:LinkButton>
                            </DataItemTemplate>
                            <HeaderStyle HorizontalAlign="Center" />
                            <CellStyle HorizontalAlign="Center" />
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="230" UseFixedTableLayout="true" />
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

        <asp:HiddenField ID="hfEventId" runat="server" Value="0" />
        <asp:ObjectDataSource ID="objdsEvent" runat="server" SelectMethod="GetEvent"
            TypeName="HRM.HR.BLL.Event">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="EventId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
                  </div>
      
</asp:Content>

     

