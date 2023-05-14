<%@ Page Title="HRIS Common | Skills" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="Skill.aspx.cs" Inherits="Skills_Skill" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <script>
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "Skill.aspx/downloadExcelFromGrid",
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
    <asp:HiddenField ID="hfSkillId" runat="server" />
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Skills</h4>
                    <span onclick="toggleProfileForm(360);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow" >
                            <div class="row mt-3">
                                <div class="col-md-3">
                                    <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                                        ControlToValidate="txtSkillCode" ErrorMessage="Required Field!" ForeColor="Red"
                                        ValidationGroup="a" CssClass="form-label">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtSkillCode" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                        Style="margin-left: 0px" Skin="Windows7" Width="150px" />
                                </div>
                                <div class="col-md-3">
                                    <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Name"></asp:Label>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                                        ControlToValidate="txtSkillName" ErrorMessage="Required Field!" ForeColor="Red"
                                        ValidationGroup="a" CssClass="form-label">*</asp:RequiredFieldValidator>
                                    <telerik:RadTextBox ID="txtSkillName" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                        Skin="Windows7" Width="250px" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label8" CssClass="form-label" runat="server" Text="Description"></asp:Label>
                                    <telerik:RadTextBox ID="txtDescription" runat="server" Height="60px" MaxLength="100" CssClass="form-control form-control-lg"
                                        TextMode="MultiLine" Skin="Windows7" Width="250px" />
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
                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                                        ValidationGroup="a" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click"
                                        Text="Update" ValidationGroup="a" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click"
                                        OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                        Text="Delete" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click"
                                        Text="Cancel" Skin="WebBlue" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
       
                <td>
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px; float: right;" />
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
                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" CssClass="tableStyle" DataSourceID="objdsSkill">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="SkillID" HeaderStyle-Font-Bold="false" Caption="SkillID" PropertiesEditType="TextBox" Width="100px" CellStyle-HorizontalAlign="Center" Visible="false" />
                        <dx:GridViewDataColumn FieldName="SkillCode" HeaderStyle-Font-Bold="false" Caption="Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="SkillName" HeaderStyle-Font-Bold="false" Caption="Name" PropertiesEditType="TextBox" Width="300px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="Description" HeaderStyle-Font-Bold="false" Caption="Description" PropertiesEditType="TextBox" Width="500px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
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
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" />

                    <SettingsPager Mode="ShowPager" PageSize="4" />
                </dx:ASPxGridView>

                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="RadGrid1" FileName="Skill">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>

            </div>
        </div>
        <asp:ObjectDataSource ID="objdsSkill" runat="server" SelectMethod="GetSkill"
            TypeName="HRM.HR.BLL.Skills">
            <SelectParameters>
                <asp:Parameter DefaultValue="0" Name="SkillId" Type="Int32" />
            </SelectParameters>
        </asp:ObjectDataSource>
       
    </div>

</asp:Content>
