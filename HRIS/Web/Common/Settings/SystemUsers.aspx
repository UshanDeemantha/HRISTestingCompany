<%@ Page Title="HRIS Common | Create Users" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="SystemUsers.aspx.cs" Inherits="Settings_SystemUsers" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .mt-2, .my-2 {
            margin-top: -0.5rem !important;
        }

        .mt-4, .my-4 {
            margin-top: -0.5rem !important;
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
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script>
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "SystemUsers.aspx/downloadExcelFromGrid",
                //data: JSON.stringify({ startDate: date._d.format('dd/MM/yyyy') }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {

                }
            });
        }

    </script>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Create Users</h4>
                    <span onclick="toggleProfileForm(595);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="User Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvUserName" runat="server" ControlToValidate="radtxtUserName" CssClass="form-label"
                                        ErrorMessage="Enter a User Name!" ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="radtxtUserName" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                        Skin="Windows7" Width="200px">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Password"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ControlToValidate="radtxtPassword" CssClass="form-label"
                                        ErrorMessage="Enter a password!" ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="radtxtPassword" Width="200px" runat="server" TextMode="Password" CssClass="form-control form-control-lg"
                                        Skin="Windows7">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="User Type"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radcbUserType" CssClass="form-label"
                                        ErrorMessage="Enter a User Type!" ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="radcbUserType" runat="server" CssClass="form-control form-control-lg"
                                        OnDataBound="radcbUserType_DataBound" Skin="Windows7" EmptyMessage="Please Select User Type">
                                        <Items>
                                            <dx:ListEditItem Text="System User" Value="1"/>
                                            <dx:ListEditItem Text="Admin" Value="2" />
                                            <dx:ListEditItem Text="Manager" Value="3" />
                                            <dx:ListEditItem Text="Executive" Value="4" />
                                            <dx:ListEditItem Text="HR User" Value="5" />                                            
                                            <dx:ListEditItem Text="Trainee" Value="6" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="First Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvFirstName" runat="server" ControlToValidate="radtxtFirstName" CssClass="form-label"
                                        ErrorMessage="Enter a value for First Name!" ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="radtxtFirstName" runat="server" Skin="Windows7" CssClass="form-control form-control-lg">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Last Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="rfvLastName" runat="server" ControlToValidate="radtxtLastName" CssClass="form-label"
                                        ErrorMessage="Enter a value for Last Name!" ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="radtxtLastName" Width="200px" runat="server" Skin="Windows7" CssClass="form-control form-control-lg">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Email"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radtxtEmail" CssClass="form-label"
                                        ErrorMessage="Enter a value for Last Name!" ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="radtxtEmail" CssClass="form-label"
                                        ErrorMessage="Enter a valid email!" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="Red" ValidationGroup="s"></asp:RegularExpressionValidator>
                                    <telerik:RadTextBox ID="radtxtEmail" runat="server" Width="200px" Skin="Windows7" CssClass="form-control form-control-lg">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">
                                <div>
                                    <telerik:RadButton ID="radbtnSave" runat="server" Skin="WebBlue" Text="Save" ValidationGroup="s"
                                        OnClick="radbtnSave_Click">
                                    </telerik:RadButton>
                                </div>
                                <div>
                                    <telerik:RadButton ID="radbtnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                        ValidationGroup="s" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="radbtnCancel" runat="server" Skin="WebBlue" Text="Cancel"
                                        OnClick="radbtnCancel_Click">
                                    </telerik:RadButton>
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

            <td>&nbsp;
           
                &nbsp;
              
                &nbsp;
            
            </td>
            </tr>
        <tr>
            <td>&nbsp;
            </td>
            <td>&nbsp;
            </td>
        </tr>
        </div>
        <telerik:RadToolTip ID="RadToolTip1" Text="Download Excel" Font-Bold="true" CssClass="tool" TargetControlID="RadButton1" runat="server">
        </telerik:RadToolTip>
        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">

                <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="cbjdsSystemUsers">
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
        <asp:ObjectDataSource ID="cbjdsSystemUsers" runat="server" SelectMethod="GetUser"
            TypeName="HRM.Common.DAL.MksSecurityDAL">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfCurrentUserType" Name="UserTypeId" PropertyName="Value" Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="UserId" Type="String" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:HiddenField ID="hfCurrentUserType" runat="server" />
        <asp:HiddenField ID="hfUser" runat="server" />
        </tr>
    </div>
</asp:Content>
