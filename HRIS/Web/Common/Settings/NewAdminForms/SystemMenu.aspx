<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SystemMenu.aspx.cs" Inherits="Common_Settings_NewAdminForms_SystemMenu"  MasterPageFile="~/Common/Settings/NewMenu.master"%>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <script type="" src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    
    <style>
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
    <div class="row" style="background-color: ghostwhite">
        <div class="col-md-12" style="border-top-color: black">
            <div class="border" style="border-radius: 10px">
                <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Application"></asp:Label>
                            <telerik:RadComboBox ID="radcboApplicationId" runat="server" Width="100%"
                                AutoPostBack="True" DataSourceID="objApplicationId"
                                DataTextField="ApplicationName" DataValueField="ApplicationId"
                                OnDataBound="radcboApplicationId_DataBound" Skin="Windows7">
                            </telerik:RadComboBox>
                            <asp:ObjectDataSource ID="objApplicationId" runat="server"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetApplication"
                                TypeName="HRM.Common.BLL.MksSecurity">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="ApplicationId" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                             <asp:HiddenField ID="hfApplicationId" runat="server" Visible="False" />
                             <asp:HiddenField ID="hfMenuId" runat="server" Visible="False" />
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Menu Name"></asp:Label>
                            <telerik:RadTextBox ID="radtxtMenuName" runat="server" Skin="Windows7" CssClass="form-control form-control-lg">
                            </telerik:RadTextBox>
                            <div class="col d-flex justify-content-end pr-2 mt-4" style="grid-gap: 6px">
                                <asp:Button ID="btnSaves" runat="server" OnClick="btnSaves_Click"
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
                    <div class="row">
                        <div class="col table-scroll1">
                            <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue"
                                KeyFieldName="ApplicationId,MenuId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsMenu" ForeColor="Black">
                                <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                <Columns>
                                    <dx:GridViewDataColumn FieldName="MenuId" Caption="PK" PropertiesEditType="TextBox" Width="100px" VisibleIndex="14" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                                    <dx:GridViewDataColumn FieldName="ApplicationId" Caption="PK" PropertiesEditType="TextBox" Width="100px" VisibleIndex="14" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                                    <dx:GridViewDataColumn FieldName="ApplicationName" HeaderStyle-Font-Bold="false" Caption="Application Name" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                    <dx:GridViewDataColumn FieldName="MenuName" HeaderStyle-Font-Bold="false" Caption="Menu Name" PropertiesEditType="TextBox" Width="300px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
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
                            <asp:ObjectDataSource ID="objdsMenu" runat="server"
                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetMenu"
                                TypeName="HRM.Common.BLL.MksSecurity">
                                <SelectParameters>
                                    <asp:ControlParameter ControlID="radcboApplicationId" DefaultValue=""
                                        Name="ApplicationId" PropertyName="SelectedValue" Type="String" />
                                    <asp:Parameter DefaultValue="0" Name="MenuId" Type="String" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </div>
                          <div id="toastrContainer">
                                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                                    <img style="width: 12px; cursor: pointer" src="../../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                                </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
