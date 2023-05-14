<%@ Page Title="HRIS Common | Block Users" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="BlockUser.aspx.cs" Inherits="Settings_BlockUser" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }

        .RadInput_Default {
            font: 12px "segoe ui",arial,sans-serif;
        }

        .RadInput {
            vertical-align: middle;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Block Users</h4>
                    <span onclick="toggleProfileForm(265);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label14" CssClass="form-label" runat="server" Text="User Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Enter the DayType Name" CssClass="form-label"
                                        ControlToValidate="txtUserName" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtUserName" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="250px"
                                        Skin="Windows7" />
                                </div>                                
                                <div class="col-md-3">
                                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Is Blocked" ></asp:Label><br />
                                     <asp:CheckBox ID="cbActive" runat="server" Checked="true" Skin="Windows7" />
                                </div>
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">
                                 <telerik:RadButton ID="radbtnUpdate" runat="server" OnClick="radbtnUpdate_Click" Text="Update"
                                        ValidationGroup="a" Skin="WebBlue" />
                                <div>
                                    <telerik:RadButton ID="radbtnCancel" runat="server" OnClick="radbtnCancel_Click" Text="Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                        <div class="col-md-6">
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-md-6">
                        </div>
                    </div>
                </div>
            </div>

        </div>
        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">

                <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="AllUsers">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                       <dx:GridViewDataColumn FieldName="UserName" Caption="User Name"
                            HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Email"
                            HeaderStyle-Font-Bold="false" Caption="Email" PropertiesEditType="TextBox" Width="350px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="FirstName"
                            HeaderStyle-Font-Bold="false" Caption="First Name" PropertiesEditType="TextBox" Width="150px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="LastName"
                            HeaderStyle-Font-Bold="false" Caption="Last Name" PropertiesEditType="TextBox" Width="150px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="UserTypeName"
                            HeaderStyle-Font-Bold="false" Caption="User Type" PropertiesEditType="TextBox" Width="150px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="UserTypeId"
                            HeaderStyle-Font-Bold="false" Caption="UserId" PropertiesEditType="TextBox" Width="200px" CellStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption=""
                            HeaderStyle-Font-Bold="false" Width="30px" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" ForeColor="Blue"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" />

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
        <asp:ObjectDataSource ID="AllUsers" runat="server" SelectMethod="GetAllUser"
            TypeName="HRM.Common.DAL.MksSecurityDAL">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfCurrentUserType" Name="UserTypeId" PropertyName="Value" Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="UserId" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="objSelectUser" runat="server"
            OldValuesParameterFormatString="original_{0}"
            SelectMethod="SelectUsersToAssignRights" TypeName="HRM.Common.BLL.MksSecurity">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfCurrentUser" DefaultValue=""
                    Name="currentUserName" PropertyName="Value" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfCurrentUser" runat="server" />
        <asp:HiddenField ID="hfCurrentUserType" runat="server" />
        <asp:HiddenField ID="hfUserId" runat="server" />



        <div id="toastrContainer">
            <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
            <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
        </div>
    </div>
</asp:Content>
