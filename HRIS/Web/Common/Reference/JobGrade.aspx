<%@ Page Title="HRIS Common | Job Grades" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="JobGrade.aspx.cs" Inherits="Reference_JobGrade"
    StylesheetTheme="Common" %>

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
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
            <script type="text/javascript">
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
             <div class="overflow-auto" style="height:calc(100vh - 110px)">
            <div class="form-container shadow" ID="formContainer"  runat="server" style="height: 54px;">
                <div class="row">
                    <div class="col head-container">
                        <h4 class="header">Job Grades</h4>
                        <span onclick="toggleProfileForm(335);" id="profileButton" class="dot shadow"></span>
                    </div>
                </div>
                <div class="row" style="background-color: ghostwhite">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border" style="border-radius: 10px">
                            <div class="card-body shadow">
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtJobGradeCode" CssClass="form-label"
                                            ErrorMessage="*" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <telerik:RadTextBox ID="txtJobGradeCode" Width="250px" CssClass="form-control form-control-lg" runat="server" Skin="Windows7" />
                                    </div>
                                </div>
                                <div class="row mt-3"">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Grade"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtJobGrade" CssClass="form-label"
                                            ErrorMessage="*" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                        <telerik:RadTextBox ID="txtJobGrade" Width="250px" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                        Skin="Windows7" />
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
                                        <telerik:RadButton ID="btnSave" runat="server"  OnClick="btnSave_Click" Text="Save"
                                            ValidationGroup="a" Skin="WebBlue" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update" ValidationGroup="a"
                                            Skin="WebBlue" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                            Text="Delete" Skin="WebBlue" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" Text="Cancel"
                                            Skin="WebBlue" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
           
                  <asp:HiddenField ID="hfJobGradeId" runat="server" />

                <div id="toastrContainer">
                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                    <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                </div>
                
                    <td align="right">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                &nbsp;
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                 
               
           
                </div>
         
              <div class="row"  style="position: relative">
              <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                            <ContentTemplate>
                                <img src="../../App_Themes/NewTheme/img/exceldwn.png"
                            </ContentTemplate>
                        </telerik:RadButton>
                    <dx:ASPxGridView ID="radgvDetails" CssClass="tableStyle" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                        KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsJobGrade">
                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                        <Columns>
                            <dx:GridViewDataColumn FieldName="JobGradeID" HeaderStyle-Font-Bold="false" Caption="JobGradeID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"/>
                            <dx:GridViewDataColumn FieldName="JobGradeCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"/>
                            <dx:GridViewDataColumn FieldName="JobGrade" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="800px" VisibleIndex="2" CellStyle-HorizontalAlign="Left"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"/>
                            <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="30px"  CellStyle-HorizontalAlign="Center" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click1" ForeColor="Blue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                        </Columns>
                         <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                              <AlternatingRow Enabled="true" />
                            </Styles>
                            <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />

                            <SettingsPager Mode="ShowPager" PageSize="11" />
                    </dx:ASPxGridView>
                  <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails" FileName="Job_Grade">
                            <Styles>
                                <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                </Header>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                    </div>
                 </div>
                    <asp:ObjectDataSource ID="objdsJobGrade" runat="server" SelectMethod="GetJobGrade"
                        TypeName="HRM.Common.BLL.Reference" OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="JobGradeId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </div>
              
</asp:Content>
