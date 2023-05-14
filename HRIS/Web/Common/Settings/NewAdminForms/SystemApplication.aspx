<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemApplication.aspx.cs" Inherits="Common_Settings_NewAdminForms_SystemApplication" MasterPageFile="~/Common/Settings/NewMenu.master"%>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <style type="text/css">
        .gridstyle {
            font-weight: 500;
        }

        .style {
            font-weight: bold;
            text-align: center;
        }

        .checkboxlistformat label {
            margin-left: 20px;
        }

        .form-control-lg {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 250px;
        }

        .mb-5, .my-5 {
            margin-bottom: 15rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: -1.5rem !important;
        }

        .mt-2, .my-2 {
            margin-top: -0.5rem !important;
        }

        .pl-2, .px-2 {
            padding-left: 14.5rem !important;
        }

        .mb-4, .my-4 {
            margin-bottom: 0.5rem !important;
        }

        .mt-4, .my-4 {
            margin-top: -1.5rem !important;
        }

        .pr-2, .px-2 {
            padding-right: 2.5rem !important;
        }

        .col-md-4 {
            -ms-flex: 0 0 33.333333%;
            flex: 0 0 33.333333%;
            max-width: 26.333333%;
        }

        .mt-2, .my-2 {
            margin-top: 0.5rem !important;
        }

        .style2 {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            display: block;
            font-weight: 400;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .style3 {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            display: block;
            font-weight: 400;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
        }

        .ml-2, .mx-2 {
            margin-left: -2.5rem !important;
        }

        .pt-2, .py-2 {
            padding-top: 2.5rem !important;
        }

        .ml-3, .mx-3 {
            margin-left: -5rem !important;
        }

        .mt-4, .my-4 {
            margin-top: 1.5rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: -3.5rem !important;
        }

        hr {
            box-sizing: content-box;
            height: 4px;
            overflow: visible;
            background-color: #097cbd;
            border-radius: 10PX;
        }

        .form-control-lg {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
        }

        .form-control:disabled {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            background-color:#f9fafb!important;
        }

        .style4 {
            margin-right: 10px;
        }   
        .form-containers {
            border-radius: 0px;
            border-top-left-radius: 0px;
            border-top-right-radius: 0px;
            border-bottom-right-radius: 0px;
            border-bottom-left-radius: 0px;
            overflow: hidden;
            transition: height 1s ease-in;
            padding-bottom: 34px;
            background-color: ghostwhite;
        }
    </style>
</asp:Content>

    <asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
         <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>

    <div class="overflow-auto">
         <div class="form-containers shadow" id="formContainers" runat="server" style="height: 0px;">
            <div class="row rm-margin">
                <div class="col head-container" >
                  
<%--                    <span onclick="toggleProfileForm(585);" id="profileButton" class="dot shadow"></span>--%>
                </div>
            </div>
            </div>
        </div>
        <div>
            <div class="row mt-5" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                            <div class="row">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Application Code"></asp:Label>
                                    <telerik:RadTextBox ID="radtxtApplicationCode" runat="server" CssClass="form-control form-control-lg"
                                        Skin="Windows7">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Application Name"></asp:Label>
                                    <telerik:RadTextBox ID="radtxtApplicationName" runat="server" Skin="Windows7" CssClass="form-control form-control-lg">
                                    </telerik:RadTextBox>
                                </div>
                               
                                    <div class="col d-flex justify-content-end pr-2 mt-4" style="grid-gap: 6px">
                                        <asp:Button ID="btnSaves" runat="server" OnClick="btnSaves_Click"
                                            Text="Save" ValidationGroup="a" CssClass="btn btn-outline-primary check-aspbtn" />
                                        <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btn btn-outline-success check-aspbtn"
                                            Text="Update" ValidationGroup="a" />
                                        <asp:Button ID="btnDelete" runat="server" OnClick="btnDelete_Click" CssClass="btn btn-outline-danger check-aspbtn"
                                            OnClientClick="btnDelete_Click"
                                            Text="Delete" />
                                        <asp:Button ID="btnCancel" runat="server" OnClick="btnCancel_Click" CssClass="btn btn-outline-secondary check-aspbtn"
                                            Text="Cancel" />
                                    </div>
                                <div id="toastrContainer">
                                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                                    <img style="width: 12px; cursor: pointer" src="../../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                                </div>
                                <asp:HiddenField ID="hfApplicationId" runat="server" Visible="False" />
                            </div>
                            <div class="row">
                                <div class="col table-scroll1">
                                    <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue"
                                        KeyFieldName="ApplicationId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsApplication" ForeColor="Black">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataColumn FieldName="ApplicationId" Caption="PK" PropertiesEditType="TextBox" Width="100px" VisibleIndex="14" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                                            <dx:GridViewDataColumn FieldName="ApplicationCode" HeaderStyle-Font-Bold="false" Caption="Application Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                            <dx:GridViewDataColumn FieldName="ApplicationName" HeaderStyle-Font-Bold="false" Caption="Application Name" PropertiesEditType="TextBox" Width="300px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                            <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Width="35px" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelect" runat="server" onclientclicked="showForm" OnClick="lkSelect_Click" ForeColor="cornflowerblue"><img src="../../../App_Themes/NewTheme/img/edit.png"  />
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
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
                                    <asp:ObjectDataSource ID="objdsApplication" runat="server"
                                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetApplication"
                                        TypeName="HRM.Common.BLL.MksSecurity">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="ApplicationId" Type="String" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
  </asp:Content>


