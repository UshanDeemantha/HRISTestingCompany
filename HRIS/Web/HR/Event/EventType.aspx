<%@ Page Title="HR | Event Types" Language="C#" MasterPageFile="~/HR/HR_MenuMasterPage.master" AutoEventWireup="true"
    CodeFile="EventType.aspx.cs" Inherits="Event_EventType" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }
        
element.style {
}

.mt-2, .my-2 {
    margin-top: -0.6rem!important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

            <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
            <div class="overflow-auto" style="height: calc(100vh - 110px)">
                <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
                    <div class="row">
                        <div class="col head-container">
                            <h4 class="header">Event Type</h4>
                            <span onclick="toggleProfileForm(305);" id="profileButton" class="dot shadow"></span>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow" style="border-radius: 10px">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Event Type Code"></asp:Label>

                                            <telerik:RadTextBox ID="txtEventCode" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                Skin="Windows7" Width="150px" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="txtEventCode" ErrorMessage="Required Field!"
                                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="row mt-2">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Event Type Name"></asp:Label>

                                            <telerik:RadTextBox ID="txtEventName" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                Skin="Windows7" Width="250px" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                                ControlToValidate="txtEventName" ErrorMessage="Required Field!"
                                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">
                                        <div>
                                            <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click"
                                                Skin="WebBlue" Text="Save" ToolTip="Add Information" ValidationGroup="a" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                                Skin="WebBlue" Text="Update" ToolTip="Press when need to save modify record"
                                                ValidationGroup="a" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                                OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                                Skin="WebBlue" Text="Delete" ToolTip="Delete the Record" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                                Skin="WebBlue" Text="Cancel" ToolTip="Clear Controls" />
                                        </div>
                                    </div>

                                </div>
                            </div>
                        </div>
                    </div>

                    <td style="text-align: right">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <div id="toastrContainer">
                        <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                        <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                    </div>


                </div>
             
                <div class="row" style="position: relative">
                    <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                        <ContentTemplate>
                            <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                        </ContentTemplate>
                    </telerik:RadButton>
                    <div class="col table-scroll">
                        <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" CssClass="tableStyle" Theme="MetropolisBlue"
                            KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsEventTypes">
                            <SettingsSearchPanel Visible="true" HighlightResults="true" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="EventTypeID" HeaderStyle-Font-Bold="false" Caption="PK" PropertiesEditType="TextBox" Width="100px" VisibleIndex="14" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataColumn FieldName="EventTypeCode" HeaderStyle-Font-Bold="false" Caption="Event Type Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataColumn FieldName="EventTypeName" HeaderStyle-Font-Bold="false" Caption="Event Type Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Width="100px" VisibleIndex="0">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lkSelect" runat="server"  onclientclicked="showForm" OnClick="lkSelect_Click" ForeColor="Blue" CellStyle-HorizontalAlign="Left"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                        </asp:LinkButton>
                                    </DataItemTemplate>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                <SearchPanel CssClass="searchBarStyle"></SearchPanel>

                                <AlternatingRow Enabled="true" />
                            </Styles>
                            <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                            <SettingsPager Mode="ShowPager" PageSize="4" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails">
                            <Styles>
                                <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                </Header>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                    </div>
                </div>
                <asp:HiddenField ID="hfEventTypeId" runat="server" />
                <asp:ObjectDataSource ID="objdsEventTypes" runat="server"
                    SelectMethod="GetEventType" TypeName="HRM.HR.BLL.Event">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="EventTypeId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>


                <br />
            </div>
    
</asp:Content>
