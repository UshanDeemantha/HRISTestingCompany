<%@ Page Title="HRIS Common | Courses" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="Course.aspx.cs" Inherits="Course_Course" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
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
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "Course.aspx/downloadExcelFromGrid",
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
                    <h4 class="header">Courses</h4>
                    <span onclick="toggleProfileForm(400);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px; height: 182px;">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                                        ControlToValidate="txtCourseCode" ErrorMessage="Required Field!" ForeColor="Red"
                                        ValidationGroup="a" CssClass="form-label">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtCourseCode" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                        Skin="Windows7" Width="150px" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Course Type"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server"
                                        ControlToValidate="ddlCourseType" ErrorMessage="Required Field!" ForeColor="Red"
                                        ValidationGroup="a" CssClass="form-label">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="ddlCourseType" runat="server"  CssClass="style1" NullText="Please Select Course Type"
                                        Skin="Windows7" Width="100%" OnSelectedIndexChanged="ddlCourseType_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label3" CssClass="form-label" runat="server" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtCourseName" ErrorMessage="Required Field!" ForeColor="Red"
                                        ValidationGroup="a" CssClass="form-label">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtCourseName" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                        Skin="Windows7" Width="250px" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-4">
                                    <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Institute Type"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="ddlInstitute" ErrorMessage="Required Field!" ForeColor="Red"
                                        ValidationGroup="a" CssClass="form-label">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="ddlInstitute" runat="server"  CssClass="style1" NullText="Please Select Institute Type"
                                        Skin="Windows7" Width="100%" OnSelectedIndexChanged="ddlInstitute_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                </div>
                                <div class="col-md-2">
                                    <asp:Label ID="Label5" CssClass="form-label" runat="server" Text="Course Medium"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                                        ControlToValidate="txtCourseStreem" ErrorMessage="Required Field!" ForeColor="Red"
                                        ValidationGroup="a" CssClass="form-label">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtCourseStreem" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
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
                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click"
                                        Skin="WebBlue" Text="Save" ToolTip="Add Information" ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                        Skin="WebBlue" Text="Update" ToolTip="Press when need to save modify record"
                                        ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                        OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                        Skin="WebBlue" Text="Delete" ToolTip="Delete the Record" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                        Skin="WebBlue" Text="Cancel" ToolTip="Clear Controls" />
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
        </div>

        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">
                <dx:ASPxGridView ID="gvDetail" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="objdsCourse">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="CourseID" HeaderStyle-Font-Bold="false" Caption="CourseID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="CourseCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CourseTypeID" HeaderStyle-Font-Bold="false" Caption="CourseTypeID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CourseTypeName" HeaderStyle-Font-Bold="false" Caption="Course Type" PropertiesEditType="TextBox" Width="200px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CourseName" HeaderStyle-Font-Bold="false" Caption="Course Name" PropertiesEditType="TextBox" Width="250px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="InstituteID" HeaderStyle-Font-Bold="false" Caption="InstituteID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="InstituteName" HeaderStyle-Font-Bold="false" Caption="Institute Type" PropertiesEditType="TextBox" Width="250px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CourseStreem" HeaderStyle-Font-Bold="false" Caption="Course Medium" PropertiesEditType="TextBox" Width="100px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
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
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="gvDetail">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>
        <asp:HiddenField ID="hfCourseId" runat="server" Value="0" />
        <asp:ObjectDataSource ID="objdsCourse" runat="server"
            SelectMethod="GetCourseDetails" TypeName="HRM.HR.BLL.Course">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="CourseId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
        </tr>
    </div>

</asp:Content>

