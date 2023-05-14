<%@ Page Title="HRIS Common | Department Types" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="OrganizationType.aspx.cs" Inherits="Organization_OrganizationType"
    StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .form-control {
            width: 82%;
        }

        .form-control {
            width: 100%;
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
            margin-top: 0px;
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
                url: "CompanyProfile.aspx/downloadExcelFromGrid",
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
                    <h4 class="header">Department Types</h4>
                    <span onclick="toggleProfileForm(425);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Level Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ddlOrganizationLevel" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="ddlOrganizationLevel" CssClass="style1" runat="server" AppendDataBoundItems="True" OnSelectedIndexChanged="ddlOrganizationLevel_SelectedIndexChanged1"
                                        DataSourceID="odsOrganizationLevels" TextField="OrganizationalLevel" ValueField="OrganizationalLevelID"
                                        Width="100%" Skin="Windows7" AutoPostBack="True" OnDataBound="ddlOrganizationLevel_DataBound" NullText="Please select Level Name">
                                    </dx:ASPxComboBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtOrganizationTypeCode" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtOrganizationTypeCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="100%"
                                        Skin="Windows7" />
                                </div>
                                <div class="col-md-1">
                                    <asp:Label ID="Label9" runat="server" CssClass="form-label" Text=" Index"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="ddlIndex" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="ddlIndex" runat="server" CssClass="style1" Skin="Windows7" Width="100%"
                                        MaxLength="10" Enabled="false">
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
                                <div class="col-md-3">
                                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Contact No"></asp:Label>
                                    <telerik:RadTextBox ID="txtContactNo" CssClass="form-control form-control-lg" runat="server" MaxLength="10" Width="250px"
                                        Skin="Windows7" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Fax"></asp:Label>
                                    <telerik:RadTextBox ID="radtxtFax" CssClass="form-control form-control-lg" runat="server" MaxLength="10" Width="250px" Skin="Windows7" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtOrganizationName" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ForeColor="Red">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtOrganizationName" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="100%"
                                        Skin="Windows7" />
                                    <asp:Label ID="Label7" runat="server" CssClass="form-label" Text="Company" Visible="false"></asp:Label>
                                    <telerik:RadComboBox ID="radcboCompany" runat="server" DataSourceID="objdsCompany" Visible="false"
                                        DataTextField="CompanyName" DataValueField="CompanyId" Skin="Windows7" Width="250px"
                                        AutoPostBack="True" OnSelectedIndexChanged="radcboCompany_SelectedIndexChanged"
                                        Enabled="False">
                                    </telerik:RadComboBox>
                                    <asp:ObjectDataSource ID="objdsCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetCompanyProfile" TypeName="HRM.Common.BLL.CompanyProfile">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="companyId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                    <asp:HiddenField ID="hfOrgTypeID" runat="server" />
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="E-mail"></asp:Label>
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtEmail" CssClass="form-label"
                                        ErrorMessage="RegularExpressionValidator" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                        ForeColor="Red">Invalid 
                                    Email</asp:RegularExpressionValidator>
                                    <telerik:RadTextBox ID="txtEmail" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Width="250px" Skin="Windows7" />
                                </div>
                                </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Address"></asp:Label>
                                    <telerik:RadTextBox ID="txtAddress" CssClass="form-control form-control-lg" runat="server" MaxLength="100" Width="250px" Height="60px"
                                        TextMode="MultiLine" Skin="Windows7" />
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
            </div>



            <div class="row">

                <div class="col-md-4">
                    <asp:Label ID="Label10" CssClass="form-label" runat="server" Text="Fax" Visible="false"></asp:Label>

                </div>
            </div>
            <div id="toastrContainer">
                <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
            </div>

            <td>
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 40px; height: 40px; float: right;" />
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
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="obsOrganizationTypes" CssClass="tableStyle">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="OrganizationTypeID" Caption="PK" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="OrganizationalLevelID" HeaderStyle-Font-Bold="false" Caption="OrganizationalLevelID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="OrganizationTypeCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="OrganizationTypeName" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="350px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="OrganizationalLevel" HeaderStyle-Font-Bold="false" Caption="Level Name" PropertiesEditType="TextBox" Width="120px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="OrganizationalIndex" HeaderStyle-Font-Bold="false" Caption="Index" PropertiesEditType="TextBox" Width="50px" VisibleIndex="3" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Address" HeaderStyle-Font-Bold="false" Caption="Address" PropertiesEditType="TextBox" Width="400px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="ContactNo" HeaderStyle-Font-Bold="false" Caption="Contact No" PropertiesEditType="TextBox" Width="100px" VisibleIndex="6" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Fax" HeaderStyle-Font-Bold="false" Caption="Fax No" PropertiesEditType="TextBox" Width="100px" VisibleIndex="7" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Email" HeaderStyle-Font-Bold="false" Caption="Email" PropertiesEditType="TextBox" Width="200px" VisibleIndex="8" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="30px" VisibleIndex="0"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
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

                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails" FileName="Organization_Type">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>
        <asp:ObjectDataSource ID="obsOrganizationTypes" runat="server" SelectMethod="GetOrganizationTypes"
            TypeName="HRM.Common.BLL.Organization" OldValuesParameterFormatString="original_{0}">
            <SelectParameters>
                <asp:ControlParameter ControlID="radcboCompany" Name="CompanyId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="OrganizationTypeId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        <asp:ObjectDataSource ID="odsOrganizationLevels" runat="server" OldValuesParameterFormatString="original_{0}"
            SelectMethod="GetOrganizationLevel" TypeName="HRM.Common.BLL.Organization">
            <SelectParameters>
                <asp:ControlParameter ControlID="radcboCompany" Name="CompanyId" PropertyName="SelectedValue"
                    Type="Int32" />
                <asp:Parameter DefaultValue="0" Name="OrganizationLevelId" Type="Int32" />
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
