<%@ Page Title="HRIS Common | Job Categories" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="JobCategory.aspx.cs" Inherits="Reference_JobCategory" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-control-lg {
            /* height: calc(1.5em + 1rem + 2px); */
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
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
                url: "CompanyBranch.aspx/downloadExcelFromGrid",
                //data: JSON.stringify({ startDate: date._d.format('dd/MM/yyyy') }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {

                }
            });
        }

    </script>
 
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/3.6.0/jquery.min.js"></script>
    <script type="text/javascript" src="../../../App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Job Categories</h4>
                    <span onclick="toggleProfileForm(333);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:HiddenField ID="hfJobCategoryId" runat="server" />
                                    <asp:Label ID="Label5" runat="server" CssClass="form-label" Text="Code"></asp:Label>
                                     <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtJobCategoryCode" CssClass="form-label"
                                               ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtJobCategoryCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50"
                                        Skin="Windows7" Width="150px"/>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label6" runat="server" CssClass="form-label" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtJobCategoryName" ErrorMessage="Required Field!" CssClass="form-label"
                                        ForeColor="Red" ToolTip="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtJobCategoryName" CssClass="form-control form-control-lg" runat="server" MaxLength="50"
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
                                    <telerik:RadButton ID="btnSave" runat="server" Text="Save" ValidationGroup="a"
                                        Skin="WebBlue" OnClick="btnSave_Click" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" Text="Update" Enabled="False"
                                        OnClick="btnUpdate_Click" Skin="WebBlue" ValidationGroup="a">
                                    </telerik:RadButton>
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" Text="Delete" Enabled="False"
                                        Skin="WebBlue" OnClick="btnDelete_Click">
                                    </telerik:RadButton>
                                </div>
                                <div>
                                    <telerik:RadButton ID="radbtnCancel" runat="server" Text="Cancel" Skin="WebBlue" OnClick="btnCancel_Click" />
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

            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>

        </div>

        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">
                <dx:ASPxGridView ID="radgvDetails" CssClass="tableStyle" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsJobCategory">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="JobCategoryID" HeaderStyle-Font-Bold="false" Caption="JobCategoryID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="JobCategoryCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="JobCategoryName" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="800px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="30px" VisibleIndex="0"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <AlternatingRow Enabled="true" />
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />

                    <SettingsPager Mode="ShowPager" PageSize="10" />
                </dx:ASPxGridView>

                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails" FileName="Job_Category">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>

        <asp:ObjectDataSource ID="objdsJobCategory" runat="server"
            SelectMethod="GetJobCategoryCompanyWise" TypeName="HRM.Common.BLL.Reference">
            <SelectParameters>
                <asp:SessionParameter DefaultValue="" Name="CompanyID" SessionField="CompanyId"
                    Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </tr>
                <tr>
                    <td colspan="2">&nbsp;</td>
                </tr>
        <tr>
            <td colspan="2">&nbsp;</td>
        </tr>
    </div>

</asp:Content>

