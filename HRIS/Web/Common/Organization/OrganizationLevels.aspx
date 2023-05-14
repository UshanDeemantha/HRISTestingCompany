<%@ Page Title="HRIS Common | Department Levels" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="OrganizationLevels.aspx.cs" Inherits="Organization_OrganizationLevels" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
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
            margin-top: 0px;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }

    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script>
        //$(document).ready(function () {
        //    alert("1111");
        //});
        //$("#downloadExcel").click(function() {
        //    alert("Handler for .click() called.");
        //});
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "OrganizationLevels.aspx/downloadExcelFromGrid",
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
                    <h4 class="header">Department Levels</h4>
                    <span onclick="toggleProfileForm(335);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label runat="server" CssClass="form-label" Text="Company" Visible="false" />
                                    <dx:ASPxComboBox ID="radcboCompany" runat="server" Skin="Windows7" Visible="false" Width="250px" CssClass="form-control form-control-lg" ValueField="CompanyId"
                                        Enabled="False">
                                    </dx:ASPxComboBox>
                                    <asp:Label runat="server" Text="Index" CssClass="form-label"></asp:Label>                                    
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radcboIndex" CssClass="form-label"
                                        ErrorMessage="a" ValidationGroup="a" ToolTip="Required Field!" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="radcboIndex" CssClass="style1" runat="server" Skin="Windows7" Width="100%" NullText="Please Select Index">
                                        <Items>
                                            <dx:ListEditItem runat="server" Text="01" Value="1" />
                                            <dx:ListEditItem runat="server" Text="02" Value="2" />
                                            <dx:ListEditItem runat="server" Text="03" Value="3" />
                                            <dx:ListEditItem runat="server" Text="04" Value="4" />
                                            <dx:ListEditItem runat="server" Text="05" Value="5" />
                                            <dx:ListEditItem runat="server" Text="06" Value="6" />
                                            <dx:ListEditItem runat="server" Text="07" Value="7" />
                                            <dx:ListEditItem runat="server" Text="08" Value="8" />
                                            <dx:ListEditItem runat="server" Text="09" Value="9" />
                                            <dx:ListEditItem runat="server" Text="10" Value="10" />
                                        </Items>
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label runat="server" CssClass="form-label" Text="Level Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radtxtOrganizationLevel" CssClass="form-label"
                                        ErrorMessage="a" ValidationGroup="a" ToolTip="Required Field!" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="radtxtOrganizationLevel" runat="server" MaxLength="50" CssClass="form-control form-control-lg" Skin="Windows7" Width="250px" />
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
                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="radbtnSave_Click" Text="Save"
                                        ValidationGroup="a" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="radbtnUpdate_Click"
                                        Text="Update" ValidationGroup="a" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="radbtnDelete_Click"
                                        Text="Delete" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="radbtnCancel_Click"
                                        Text="Cancel" Skin="WebBlue" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <asp:HiddenField ID="hfOrgLevelId" runat="server" />
            </div>
            <div id="toastrContainer">
                <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
            </div>

            <tr>
                <td align="right">
                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
        </div>

        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>

            <div class="col table-scroll">
                <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="OrganizationalLevelID" HeaderStyle-Font-Bold="false" Caption="OrganizationalLevelID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyName" Caption="Company Name" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyId" Caption="Company Name" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CompanyCode" Caption="Company Name" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="Active" Caption="Company Name" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Visible="false" />
                        <dx:GridViewDataColumn FieldName="OrganizationalLevel" HeaderStyle-Font-Bold="false" Caption="Level Name" PropertiesEditType="TextBox" Width="800px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="OrganizationalIndex" HeaderStyle-Font-Bold="false" Caption="Index" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" Width="30px" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Center" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
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
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails" FileName="Organizationsl_Level">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>

        <%--                    <asp:ObjectDataSource ID="objdsUserLevels" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetOrganizationLevel" TypeName="HRM.Common.BLL.Organization">
                    <SelectParameters>
                          <asp:ControlParameter ControlID="hfCompanyId" Name="CompanyId" PropertyName="Value"
                                Type="Int32" />
                        <asp:Parameter DefaultValue="0" Name="OrganizationLevelId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>--%>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>&nbsp;
                    </td>
                </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
    </div>

</asp:Content>
