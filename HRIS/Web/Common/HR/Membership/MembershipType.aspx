<%@ Page Title="HRIS Common | Membership Types" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="MembershipType.aspx.cs" Inherits="Membership_MembershipType" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <asp:HiddenField ID="hfMembershipTypeId" runat="server" />
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Membership Types</h4>
                    <span onclick="toggleProfileForm(335);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="form-label" ForeColor="Red"
                                        ControlToValidate="txtMembershipTypeCode" ErrorMessage="Required Field!"
                                        ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtMembershipTypeCode" runat="server" MaxLength="50" Width="150px" CssClass="form-control form-control-lg"
                                        Skin="Windows7">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtMembershipTypeName" ErrorMessage="Required Field!" CssClass="form-label" ForeColor="Red"
                                        ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtMembershipTypeName" runat="server" Width="250px" CssClass="form-control form-control-lg"
                                        MaxLength="50" Skin="Windows7">
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
                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                                        ValidationGroup="a" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                        Text="Update" ValidationGroup="a" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                        OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                        Text="Delete" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                        Text="Cancel" Skin="WebBlue" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>



            <tr>
                <td style="text-align: right">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
                <div id="toastrContainer">
                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                    <img style="width: 12px; cursor: pointer" src="../../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                </div>
            </tr>
</div>
        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">
                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="objdsMembershipType">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="MembershipTypeID" HeaderStyle-Font-Bold="false" Caption="MembershipTypeID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="MembershipTypeCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="MembershipTypeName" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="800px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="30px" VisibleIndex="0"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click1" ForeColor="Blue"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />

                    <SettingsPager Mode="ShowPager" PageSize="4" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="RadGrid1" FileName="Membership Types">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>

        <asp:ObjectDataSource ID="objdsMembershipType" runat="server"
            SelectMethod="GetMembershipType" TypeName="HRM.HR.BLL.Membership">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="MembershipTypeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
       </div>

</asp:Content>
