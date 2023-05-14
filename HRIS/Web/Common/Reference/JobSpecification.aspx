<%@ Page Title="HRIS Common | Job Specifications" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="JobSpecification.aspx.cs" Inherits="Reference_JobSpecification" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        element.style {
            width: 248px;
            overflow: hidden auto;
            padding-right: 0px;
            height: 149.93px;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
        
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
                url: "JobSpecification.aspx/downloadExcelFromGrid",
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
                    <h4 class="header">Job Specifications</h4>
                    <span onclick="toggleProfileForm(555);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label runat="server" CssClass="form-label" Text="Designation" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlDesignation" CssClass="form-label"
                                        ForeColor="Red" ToolTip="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="ddlDesignation" runat="server" CssClass="style1" Width="100%" NullText="Please Select Designation Name"></dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:Label runat="server" Text="Responsibilities" CssClass="form-label" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtResponsibilities" CssClass="form-label"
                                        ForeColor="Red" ToolTip="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtResponsibilities" runat="server" Height="60px" CssClass="form-control form-control-lg"
                                        Skin="Windows7" TextMode="MultiLine" Width="450px" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <asp:Label runat="server" Text="Benifits" CssClass="form-label" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtBenifites" CssClass="form-label"
                                        ForeColor="Red" ToolTip="Required Field!" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtBenifites" runat="server" Height="60px" CssClass="form-control form-control-lg"
                                        Skin="Windows7" TextMode="MultiLine" Width="450px" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-12">

                                    <asp:Label runat="server" Text="Remarks" CssClass="form-label" />
                                    <telerik:RadTextBox ID="txtRemarks" runat="server" Height="60px" CssClass="form-control form-control-lg"
                                        Skin="Windows7" TextMode="MultiLine" Width="450px" />

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
                                    <telerik:RadButton ID="radbtnSave" runat="server" OnClick="btnSave_Click"
                                        Text="Save" ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="radbtnUpdate" runat="server" OnClick="btnUpdate_Click"
                                        Text="Update" ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="radbtnDelete" runat="server" OnClick="btnDelete_Click"
                                        OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                        Skin="WebBlue" Text="Delete" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="radbtnCancel" runat="server" OnClick="btnCancel_Click"
                                        Skin="WebBlue" Text="Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>

            <div class="row">
                <asp:HiddenField ID="hfJobSpecificationId" runat="server" />


            </div>


            <div id="toastrContainer">
                <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
            </div>

            <tr>
                <td colspan="2">&nbsp;</td>
            </tr>

        </div>


        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click1">
                <ContentTemplate>
                    <img src="../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>

            <div class="col table-scroll">
                <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="objdsJobSpecification">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="JobSpecificationId" Caption="JobSpecificationId" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="DesignationName" HeaderStyle-Font-Bold="false" Caption="Designation Name" PropertiesEditType="TextBox" Width="150px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Responsibilities" HeaderStyle-Font-Bold="false" Caption="Responsibilities" PropertiesEditType="TextBox" Width="250px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Benifits" HeaderStyle-Font-Bold="false" Caption="Benifits" PropertiesEditType="TextBox" Width="250px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Remarks" HeaderStyle-Font-Bold="false" Caption="Remarks" PropertiesEditType="TextBox" Width="250px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="DesignationId" HeaderStyle-Font-Bold="false" Caption="Remarks" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="30px"  CellStyle-HorizontalAlign="Center" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
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
                    <SettingsPager Mode="ShowPager" PageSize="10"/>
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails" FileName="Job-Specification">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>
        <asp:ObjectDataSource ID="objdsJobSpecification" runat="server"
            SelectMethod="GetJobSpecifiction" TypeName="HRM.Common.BLL.Reference">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="JobSpecifictionId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>

    </div>
    <tr>
        <td style="text-align: right;">
            <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                <ProgressTemplate>
                    <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px;" />
                </ProgressTemplate>
            </asp:UpdateProgress>
        </td>

    </tr>
</asp:Content>
