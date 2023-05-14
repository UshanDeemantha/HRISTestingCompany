<%@ Page Title="HRIS Common | Designation Types" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="Designation.aspx.cs" Inherits="Designation_Designation"
    StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style2 {
            text-align: left;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
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
                url: "Designation.aspx/downloadExcelFromGrid",
                //data: JSON.stringify({ startDate: date._d.format('dd/MM/yyyy') }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {

                }
            });
        }

    </script>
    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Designation Types</h4>
                    <span onclick="toggleProfileForm(333);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDesignationCode" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a"  ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtDesignationCode" runat="server" MaxLength="50" Skin="Windows7"
                                        Style="margin-left: 0px" Width="250px" CssClass="form-control form-control-lg" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtDesignationName" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtDesignationName" runat="server" Skin="Windows7"
                                        Width="250px" CssClass="form-control form-control-lg" />
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
                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Skin="WebBlue"
                                        Text="Save" ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                        Skin="WebBlue" Text="Update" ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                        OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                        Skin="WebBlue" Text="Delete" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                        Skin="WebBlue" Text="Cancel" />
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
                    <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                </div>
            </tr>


        </div>
     
        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>

            <div class="col table-scroll">
                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsDesignation" CssClass="tableStyle">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="DesignationID" HeaderStyle-Font-Bold="false" Caption="DesignationID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="DesignationCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="DesignationName" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="800px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" HeaderStyle-Font-Bold="false" Width="30px"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
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
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="RadGrid1" FileName="Designation Types">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>
        <asp:ObjectDataSource ID="objdsCompanyProfile" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="" TypeName="HRM.Common.BLL.CompanyProfile">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="companyId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </td>
        </tr>
            </table>
            <asp:ObjectDataSource ID="objdsDesignation" runat="server" SelectMethod="GetDesignation"
                TypeName="HRM.Common.BLL.Designation">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="DesignationId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>
        <asp:HiddenField ID="hfDesignationId" runat="server" />
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
