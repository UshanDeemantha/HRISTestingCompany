<%@ Page Title="HRIS Common | Company Branches" Language="C#" AutoEventWireup="true" CodeFile="CompanyBranch.aspx.cs" Inherits="Common_CompanyProfile_CompanyBranch"
    MasterPageFile="~/Common/Common_MenuMasterPage.master" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register TagPrefix="dx" Namespace="DevExpress.Web" Assembly="DevExpress.Web.v14.2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .rounded-pill {
            border-radius: 0rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
    </asp:ScriptManager>
<script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Company Branches</h4>
                    <span onclick="toggleProfileForm(408);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>

            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" runat="server" CssClass="form-label" Text="Branch Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBarnchCode" CssClass="form-label"
                                        ErrorMessage="a" ValidationGroup="a" ToolTip="Required Field!" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtBarnchCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="150px"
                                        Skin="Windows7" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Contact No "></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="txtContactNo" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtContactNo" runat="server" MaxLength="10" Width="250px" CssClass="form-control form-control-lg"
                                        Skin="Windows7" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Branch Email"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtEmail" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail" CssClass="form-label"
                                        ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="Red">Invalid Email</asp:RegularExpressionValidator>
                                    <telerik:RadTextBox ID="txtEmail" runat="server" MaxLength="50" Width="250px" Skin="Windows7" CssClass="form-control form-control-lg" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label4" runat="server" CssClass="form-label" Text="Contact Person"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtContactPerson" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*
                                    </asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtContactPerson" runat="server" MaxLength="100" Width="250px" CssClass="form-control form-control-lg"
                                        Skin="Windows7" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Location"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtLocation" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtLocation" runat="server" MaxLength="50" Width="250px" CssClass="form-control form-control-lg"
                                        Skin="Windows7" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Address"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtAddress" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtAddress" runat="server" MaxLength="70" Width="250px" TextMode="MultiLine" CssClass="form-control form-control-lg"
                                        Height="60px" Skin="Windows7" />
                                </div>
                            </div>                 
                             <div class="row mt-3">
                                 </div>
                             <div class="row mt-3">
                                 </div> 
                             <div class="row mt-3">
                                 </div>
                        <div class="d-flex justify-content-end mr-5  mt-4" style="grid-gap: 15px">
                            <div>
                                <telerik:RadButton ID="radbtnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                                    ValidationGroup="a" Skin="WebBlue" />
                            </div>
                            <div>
                                <telerik:RadButton ID="radbtnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update"
                                    ValidationGroup="a" />
                            </div>
                            <div>
                                <telerik:RadButton ID="radbtnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                    Text="Delete" />
                            </div>
                            <div>
                                <telerik:RadButton ID="radbtnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel" />
                            </div>
                        </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="col-md-12">
                <div class="row mt-3">
                    <div class="col-md-4">
                        <asp:HiddenField ID="hfCompanyBranchId" runat="server" />
                        <asp:HiddenField ID="hfCompanyId" runat="server" />
                        <asp:Label runat="server" CssClass="form-label" Text="Company" Visible="false" />
                        <dx:ASPxTextBox ID="radcboCompany" runat="server" Skin="Windows7" Width="250px" CssClass="form-control form-control-lg" Visible="false"
                            Enabled="False">
                        </dx:ASPxTextBox>
                        <%--     <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Company"></asp:Label>
                                <asp:HiddenField ID="hfCompanyBranchId" runat="server" />

                                <telerik:RadComboBox ID="radcboCompany" runat="server" DataSourceID="objdsCompany"
                                    DataTextField="CompanyName" DataValueField="CompanyId" Skin="Windows7" Width="250px"
                                    Enabled="False">
                                </telerik:RadComboBox>--%>
                    </div>
                    <%-- <asp:ObjectDataSource ID="objdsCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                                SelectMethod="GetCompanyProfile" TypeName="HRM.Common.BLL.CompanyProfile">
                                <SelectParameters>
                                    <asp:Parameter DefaultValue="0" Name="companyId" Type="Int32" />
                                </SelectParameters>
                            </asp:ObjectDataSource>--%>

                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row mt-3">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>

                <div id="toastrContainer">
                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                    <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                </div>
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
                <div class="row">
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                    <div class="col-md-4">
                    </div>
                </div>
            </div>

            <tr>
                <td align="right">
                    <asp:UpdateProgress ID="UpdateProgress" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px"
                                id="UpdateProgress" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
                <%-- <td>
                        <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                    </td>--%>
            </tr>
       </div>

        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">

                <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" runat="server" ForeColor="Black" order-bordercolor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsCompanybranch" CssClass="tableStyle">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />

                    <Columns>
                        <dx:GridViewDataColumn FieldName="BranchCode" Caption="Code" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="Location" Caption="Location" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="160px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="Address" Caption="Address" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="450px" VisibleIndex="4" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="Email" Caption="Email" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="230px" VisibleIndex="3" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="CountactPerson" HeaderStyle-Font-Bold="false" Caption="Contact Person" PropertiesEditType="TextBox" Width="200px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="ContactNo" HeaderStyle-Font-Bold="false" Caption="Contact No" PropertiesEditType="TextBox" Width="120px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                        <dx:GridViewDataColumn FieldName="BranchId" HeaderStyle-Font-Bold="false" Caption="Contact Nox" PropertiesEditType="TextBox" Width="50px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyId" HeaderStyle-Font-Bold="false" Caption="Fax" PropertiesEditType="TextBox" Width="50px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" HeaderStyle-Font-Bold="false" Width="30px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" onclientclicked="showForm" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />

                    <SettingsPager Mode="ShowPager" PageSize="10" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails" FileName="Company_Branch">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>
        <%--          <asp:ObjectDataSource ID="obsOrganizationTypes" runat="server" SelectMethod="GetOrganizationTypes"
                        TypeName="HRM.Common.BLL.Organization" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="radcboCompany" Name="CompanyId" PropertyName="Value"
                                Type="Int32" />
                            <asp:Parameter DefaultValue="0" Name="OrganizationTypeId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>--%>

        <asp:ObjectDataSource ID="objdsCompanybranch" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetCompanyBranch" TypeName="HRM.Common.BLL.CompanyProfile">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfCompanyId" Name="CompanyId" PropertyName="Value"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="BranchId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
    </div>

</asp:Content>
