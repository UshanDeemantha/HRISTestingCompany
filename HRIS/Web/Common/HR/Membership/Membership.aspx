<%@ Page Title="HRIS Common | Memberships" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="Membership.aspx.cs" Inherits="Membership_Membership" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            height: 22px;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

        <ContentTemplate>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
            <asp:HiddenField ID="hfMembershipId" runat="server" />
            <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
            <div class="overflow-auto" style="height: calc(100vh - 110px)">
                <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
                    <div class="row">
                        <div class="col head-container">
                            <h4 class="header">Memberships</h4>
                            <span onclick="toggleProfileForm(335);" id="profileButton" class="dot shadow"></span>
                        </div>
                    </div>
                    <div class="row" style="background-color: ghostwhite">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow">
                                    <div class="row mt-3">
                                        <div class="col-md-3">
                                            <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                                ControlToValidate="txtMembershipCode" ErrorMessage="Required Field!" ForeColor="Red" CssClass="form-label"
                                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            <telerik:RadTextBox ID="txtMembershipCode" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                Skin="Windows7" Width="150px" />
                                        </div>
                                        <div class="col-md-3">
                                            <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Membership Type"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                                ControlToValidate="ddlMembershipType" ErrorMessage="Required Field!" ForeColor="Red" CssClass="form-label"
                                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            <dx:ASPxComboBox ID="ddlMembershipType" runat="server" AutoPostBack="True" NullText="Please Select Membership Type"
                                                DataSourceID="odsMembershipTypes" TextField="MembershipTypeName" CssClass="form-control form-control-lg"
                                                ValueField="MembershipTypeID" OnDataBound="ddlMembershipType_DataBound" OnSelectedIndexChanged="ddlMembershipType_SelectedIndexChanged"
                                                Skin="Windows7" />
                                            <asp:ObjectDataSource ID="odsMembershipTypes" runat="server"
                                                OldValuesParameterFormatString="original_{0}" SelectMethod="GetMembershipType"
                                                TypeName="HRM.HR.BLL.Membership">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="0" Name="MembershipTypeId" Type="Int32" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Name"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                                ControlToValidate="txtMembershipName" ErrorMessage="Required Field!" ForeColor="Red" CssClass="form-label"
                                                ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            <telerik:RadTextBox ID="txtMembershipName" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                Skin="Windows7" Width="250px" />
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
                                                ToolTip="Add Information" ValidationGroup="a" Skin="WebBlue" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                                Text="Update" ValidationGroup="a" Skin="WebBlue" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                                OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                                Text="Delete" ToolTip="Delete the Record" Skin="WebBlue" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                                Text="Cancel" ToolTip="Clear Controls" Skin="WebBlue" />
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
                            <img alt="" src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                        </ContentTemplate>
                    </telerik:RadButton>
                    <div class="col table-scroll">
                        <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                            KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="objdsMembership">
                            <SettingsSearchPanel Visible="true" HighlightResults="true" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="MembershipID" HeaderStyle-Font-Bold="false" Caption="MembershipID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                                <dx:GridViewDataColumn FieldName="MembershipCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="FluencyTypeID" HeaderStyle-Font-Bold="false" Caption="FluencyTypeID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                                <dx:GridViewDataColumn FieldName="MembershipTypeID" Caption="MembershipTypeID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" Visible="false" />
                                <dx:GridViewDataColumn FieldName="MembershipTypeName" HeaderStyle-Font-Bold="false" Caption="Membership Type" PropertiesEditType="TextBox" Width="400px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="MembershipName" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="400px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataTextColumn Caption="" Width="30px" VisibleIndex="0" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
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
                            <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                            <SettingsPager Mode="ShowPager" PageSize="4" />
                        </dx:ASPxGridView>
                        <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="RadGrid1" FileName="Membership">
                            <Styles>
                                <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                </Header>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                    </div>
                </div>
                <asp:ObjectDataSource ID="objdsMembership" runat="server"
                    SelectMethod="GetMembership" TypeName="HRM.HR.BLL.Membership">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="MembershipId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                </tr>

            </div>
        </ContentTemplate>
  
</asp:Content>
