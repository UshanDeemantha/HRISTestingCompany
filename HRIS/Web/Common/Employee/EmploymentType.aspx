<%@ Page Title="HRIS Common | Employment Types" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="EmploymentType.aspx.cs" Inherits="Employee_EmploymentType"
    StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .rounded-pill {
    border-radius: 0rem!important;
}
                .ml-2, .mx-2 {
    margin-left: 20.5rem!important;
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
            <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
            <div class="overflow-auto" style="height: calc(100vh - 110px)">
                <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
                    <div class="row">
                        <div class="col head-container">
                            <h4 class="header">Employment Types</h4>
                            <span onclick="toggleProfileForm(360);" id="profileButton" class="dot shadow"></span>
                        </div>
                    </div>
                    <div class="row" style="background-color: ghostwhite">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow">
                                    <div class="row mt-3">
                                        <div class="col-md-3">
                                            <asp:Label runat="server" CssClass="form-label" Text="Code" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtEmploymentTypeCode" CssClass="form-label"
                                                ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            <telerik:RadTextBox ID="txtEmploymentTypeCode" runat="server" MaxLength="50" Skin="Windows7" CssClass="form-control form-control-lg"
                                                Width="150px" />
                                        </div>

                                        <div class="col-md-3">
                                            <asp:Label runat="server" CssClass="form-label" Text="Name" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtEmploymentTypeName" CssClass="form-label"
                                                ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                            <telerik:RadTextBox ID="txtEmploymentTypeName" runat="server" MaxLength="50" CssClass="form-control form-control-lg" Skin="Windows7"
                                                Width="250px" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" CssClass="form-label" Text="Remarks" />
                                            <telerik:RadTextBox ID="txtRemark" CssClass="form-control form-control-lg" runat="server" Height="60px" MaxLength="100" Skin="Windows7"
                                                TextMode="MultiLine" Width="250px" />
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


                    <asp:HiddenField ID="hfEmploymentTypeId" runat="server" />

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
                </div>

                <div class="row"  style="position: relative">
                     <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click1">
                            <ContentTemplate>
                                <img src="../../App_Themes/NewTheme/img/exceldwn.png"
                            </ContentTemplate>
                        </telerik:RadButton>
                   <div class="col table-scroll">
                       <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                            KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsEmploymentType" CssClass="tableStyle">
                            <SettingsSearchPanel Visible="true" HighlightResults="true" />
                            <Columns>
                                <dx:GridViewDataColumn FieldName="EmploymentTypeID" Visible="false" Caption="Employment Type ID" PropertiesEditType="TextBox" HeaderStyle-Font-Bold="false"  Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left" />
                                <dx:GridViewDataColumn FieldName="EmploymentTypeCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left"/>
                                <dx:GridViewDataColumn FieldName="EmploymentTypeName" HeaderStyle-Font-Bold="false" Caption="Type Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Left"/>
                                <dx:GridViewDataColumn FieldName="Remark" Caption="Remarks" HeaderStyle-Font-Bold="false" PropertiesEditType="TextBox" Width="500" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataTextColumn Caption="" Width="30px" HeaderStyle-Font-Bold="false" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center">
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
                            <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails" FileName="Employment_Type">
                            <Styles>
                                <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                </Header>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                    </div>
                </div>
                <asp:ObjectDataSource ID="ObjectDataSource1" runat="server" SelectMethod="GetEmploymentType"
                    TypeName="HRM.Common.BLL.Employee">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="EmploymentTypeId" Type="Int32" />
                    </SelectParameters>
                </asp:ObjectDataSource>
               <table>
                <tr>
                    <td colspan="2">
                        <asp:ObjectDataSource ID="objdsEmploymentType" runat="server" SelectMethod="GetEmploymentType"
                            TypeName="HRM.Common.BLL.Employee">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="EmploymentTypeId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                </table>
            </div>
   
</asp:Content>
