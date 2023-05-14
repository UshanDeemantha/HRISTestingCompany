<%@ Page Title="HRIS Common | Institutes" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="Institutes.aspx.cs" Inherits="Institutes_Institutes" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .ml-3, .mx-3 {
            margin-left: 6rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </cc1:ToolkitScriptManager>

    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Institutes</h4>
                    <span onclick="toggleProfileForm(416);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px; height: 192px">
                        <div class="card-body shadow">
                            <div class="row">
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtInstituteCode" ErrorMessage="Required Field!" ForeColor="Red"  CssClass="form-label"
                                        ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtInstituteCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50"
                                        Skin="Windows7" Width="10px">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Institute Type"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="ddlInsitituteType" ErrorMessage="Required Field!" ForeColor="Red"  CssClass="form-label"
                                        ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="ddlInsitituteType" runat="server" CssClass="form-control form-control-lg" NullText="Please Select Insititute Type Name"
                                        OnDataBound="ddlInsitituteType_DataBound" Skin="Windows7">
                                    </dx:ASPxComboBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label8" runat="server" CssClass="form-label" Text="Contact No"></asp:Label>
                                    <telerik:RadTextBox ID="txtTel" CssClass="form-control form-control-lg" runat="server" MaxLength="10" Skin="Windows7" Width="100px">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtInstituteName" ErrorMessage="Required Field!" ForeColor="Red"  CssClass="form-label"
                                        ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtInstituteName" CssClass="form-control form-control-lg" runat="server" MaxLength="50"
                                        Skin="Windows7" Width="250px">
                                    </telerik:RadTextBox>
                                </div>
                                <div class="col-md-6">
                                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Fax"></asp:Label>
                                    <telerik:RadTextBox ID="txtFax" CssClass="form-control form-control-lg" runat="server" MaxLength="10" Skin="Windows7" Width="250px">
                                    </telerik:RadTextBox>
                                </div>
                            </div>
                            <div class="row mt-3">                                
                                <div class="col-md-6">
                                    <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Address"></asp:Label>

                                    <telerik:RadTextBox ID="txtAddress" CssClass="form-control form-control-lg" runat="server" Height="60px"
                                        MaxLength="200" Skin="Windows7" TextMode="MultiLine" Width="250px">
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

                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click"
                                        Skin="WebBlue" Text="Save" ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                        Skin="WebBlue" Text="Update" ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                        Skin="WebBlue" Text="Delete" ValidationGroup="a" />
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



            <div id="toastrContainer">
                <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                    <img style="width: 12px; cursor: pointer" src="../../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
            </div>

            <td style="text-align: right">
                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                    <ProgressTemplate>
                        <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </td>
      </div>
         <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">
                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" ForeColor="Black" order-bordercolor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="objdsInstitute">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="InstituteID" Caption="InstituteID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="InstituteCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="InstituteTypeID" HeaderStyle-Font-Bold="false" Caption="InstituteTypeID" PropertiesEditType="TextBox" Width="100px"  CellStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="InstituteTypeName" HeaderStyle-Font-Bold="false" Caption="Institute Type" PropertiesEditType="TextBox" Width="275px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="InstituteName" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="275px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Tel" HeaderStyle-Font-Bold="false" Caption="Contact No" PropertiesEditType="TextBox" Width="100px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Fax" HeaderStyle-Font-Bold="false" Caption="Fax" PropertiesEditType="TextBox" Width="120px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Address" HeaderStyle-Font-Bold="false" Caption="Address" PropertiesEditType="TextBox" Width="300px" VisibleIndex="6" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="30px" CellStyle-HorizontalAlign="Center" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
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
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="RadGrid1" FileName="Institutes">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>
        <asp:HiddenField ID="hfInstitutesId" runat="server" />
        <asp:ObjectDataSource ID="objdsInstitute" runat="server"
            SelectMethod="GetInstituteDetails" TypeName="HRM.HR.BLL.Institute">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="InstituteId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </tr>
    </div>

</asp:Content>

