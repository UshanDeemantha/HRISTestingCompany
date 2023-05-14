<%@ Page Title="HRM Common | User Companies" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="UserCompany.aspx.cs" Inherits="Common_Settings_UserCompany" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
   <style type="text/css">
        .gridstyle {
            font-weight: 500;
        }

        .style {
            font-weight: bold;
            text-align: center;
        }

        .checkboxlistformat label {
            margin-left: 20px;
        }

        .form-control-lg {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 250px;
        }

        .mb-5, .my-5 {
            margin-bottom: 15rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: -1.5rem !important;
        }

        .mt-2, .my-2 {
            margin-top: -0.5rem !important;
        }

        .pl-2, .px-2 {
            padding-left: 14.5rem !important;
        }

        .mb-4, .my-4 {
            margin-bottom: 0.5rem !important;
        }

        .mt-4, .my-4 {
            margin-top: -1.5rem !important;
        }

        .pr-2, .px-2 {
            padding-right: 2.5rem !important;
        }

        .col-md-4 {
            -ms-flex: 0 0 33.333333%;
            flex: 0 0 33.333333%;
            max-width: 26.333333%;
        }

        .mt-2, .my-2 {
            margin-top: 0.5rem !important;
        }

        .style2 {
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

        .style3 {
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

        .ml-2, .mx-2 {
            margin-left: -2.5rem !important;
        }

        .pt-2, .py-2 {
            padding-top: 2.5rem !important;
        }

        .ml-3, .mx-3 {
            margin-left: -5rem !important;
        }

        .mt-4, .my-4 {
            margin-top: 1.5rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: -3.5rem !important;
        }

        hr {
            box-sizing: content-box;
            height: 4px;
            overflow: visible;
            background-color: #097cbd;
            border-radius: 10PX;
        }

        .form-control-lg {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
        }

        .form-control:disabled {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            background-color:#f9fafb!important;
        }

        .style4 {
            margin-right: 10px;
        }
    </style>
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript">
        var $T = Telerik.Web.UI;
        function OnLoad(sender, args) {
            // #region Add ComboBox reference to TextBox
            function getComboBoxReference() {
                var comboBox = $telerik.$(sender.get_element()).closest(".RadComboBox")[0].control;
                //add a reference of the containing combo box to the RadTextBox instance
                sender.__containingComboBox = comboBox;

                //add a reference of the RadTextBox instance to the containing combo box
                comboBox.__filterTextBox = sender;

                Sys.Application.remove_load(getComboBoxReference);
            }
            Sys.Application.add_load(getComboBoxReference);
            // #endregion

            $telerik.$(sender.get_element()).on("keyup", function (e) {
                sender.__containingComboBox.highlightAllMatches(sender.get_textBoxValue());
            })
        }

        function OnClientLoadComboBox(sender, args) {
            var originalFunction = $T.RadComboBox.prototype.highlightAllMatches;
            // override the highlightAllMatches only on this RadComboBox instance
            sender.highlightAllMatches = function (text) {
                this.set_filter($T.RadComboBoxFilter.Contains);
                originalFunction.call(this, text);
                this.set_filter($T.RadComboBoxFilter.None);
            };
        }

        function OnClientDropDownClosing(sender, args) {
            sender.get_items().forEach(function (item) {
                item.clearEmTags();
            });

            sender.setAllItemsVisible(true);
            sender.__filterTextBox.clear();
        }
    </script>
    <script>
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "UserCompany.aspx/downloadExcelFromGrid",
                //data: JSON.stringify({ startDate: date._d.format('dd/MM/yyyy') }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {

                }
            });
        }

    </script>

        <ContentTemplate>
            <asp:HiddenField ID="hfUserId" runat="server" />
             <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
            <div class="overflow-auto" style="height: calc(100vh - 110px)">
                <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
                    <div class="row">
                        <div class="col head-container">
                            <h4 class="header">User Companies</h4>
                            <span onclick="toggleProfileForm(300);" id="profileButton" class="dot shadow"></span>
                        </div>
                    </div>
                    <div class="row" style="background-color: ghostwhite">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow">
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" CssClass="form-label" Text="User" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radcbUser" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <dx:ASPxComboBox ID="radcbUser" runat="server" CssClass="form-control form-control-lg" AutoPostBack="True" DataSourceID="objdsUser"
                                                TextField="UserName" ValueField="UserId" NullText="Please Select User Name"
                                                Skin="Windows7" OnSelectedIndexChanged="radcbUser_SelectedIndexChanged1">
                                            </dx:ASPxComboBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" CssClass="form-label" Text="Company" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="cblCompany" CssClass="form-label"
                                        ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                        ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <telerik:RadComboBox ID="cblCompany" Enabled="false" runat="server" CheckBoxes="true" OnClientLoad="OnClientLoadComboBox" Width="100%"  CheckedItemsTexts="DisplayAllInInput"
                                                DataTextField="CompanyName" DataValueField="CompanyID" OnDataBound="cblCompany_DataBound1" DataSourceID="objdsCompany" EmptyMessage="Please Select Company Name">                                
                                            </telerik:RadComboBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                    </div>
                                    <div class="row mt-3">
                                    </div>
                                    <div class="row mt-3">
                                    </div>
                                    <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                        <div>
                                            <telerik:RadButton ID="radbtnSave" runat="server" OnClick="radbtnSave_Click" Skin="WebBlue"
                                                Text="Save" ValidationGroup="a">
                                            </telerik:RadButton>
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="radbtnCancel" runat="server" OnClick="radbtnCancel_Click"
                                                Skin="WebBlue" Text="Cancel">
                                            </telerik:RadButton>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="row">
                        <div class="col-md-8">
                            <div class="row">
                                <div class="col-md-6">
                                </div>
                            </div>
                            <div class="row mt-2">

                                <div class="col-md-6">

                                    <%--  <asp:CheckBoxList ID="cblCompany" runat="server" AutoPostBack="True" DataSourceID="objdsCompany"
                    DataTextField="CompanyName" DataValueField="CompanyID" OnDataBound="cblCompany_DataBound">
                </asp:CheckBoxList>--%>
                                </div>
                            </div>
                        </div>

                    </div>
                    <asp:HiddenField ID="ssss" runat="server" />
                    <table style="width: 50%;">
                        <tr>
                            <td>

                                <asp:ObjectDataSource ID="objdsUser" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetActiveUser" TypeName="HRM.Common.BLL.MksSecurity">
                                    <SelectParameters>
                                        <asp:ControlParameter ControlID="hfCurrentUserType" Name="UserTypeId" PropertyName="Value" Type="Int32" />
                                        <asp:Parameter DefaultValue="0" Name="UserId" Type="String" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:HiddenField ID="hfCurrentUserType" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">

                                <asp:ObjectDataSource ID="objdsCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                                    SelectMethod="GetCompanyProfile" TypeName="HRM.Common.BLL.CompanyProfile">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="companyId" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" valign="top">&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>


                        <tr>
                            <td align="left" valign="top" colspan="2">&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td></td>

                        </tr>
                        <div id="toastrContainer">
                            <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                            <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                        </div>



                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </div>

                <div class="row" style="position: relative">
                    <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                        <ContentTemplate>
                            <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                        </ContentTemplate>
                    </telerik:RadButton>
                    <div class="col table-scroll">
                    <dx:ASPxGridView ID="radgvUserCompanies" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsUserCompany" ForeColor="Black">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="UserId" Visible="false" Caption="UserId" PropertiesEditType="TextBox" Width="100px" VisibleIndex="14" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CompanyId" Visible="false" HeaderStyle-Font-Bold="false" Caption="CompanyId" PropertiesEditType="TextBox" Width="100px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="PushButton" Visible="false" HeaderStyle-Font-Bold="false" Caption="Modify" PropertiesEditType="TextBox" Width="300px"  HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="UserName" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Caption="User Name" PropertiesEditType="TextBox" Width="120px" VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="CompanyName" HeaderStyle-Font-Bold="false" Caption="Company Name" PropertiesEditType="TextBox" Width="700px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Width="35px" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" onclientclicked="showForm" OnClick="lkSelect_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                </asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>

                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="230" UseFixedTableLayout="true" />

                    <SettingsPager Mode="ShowPager" PageSize="10" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvUserCompanies">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
                    </div>
                </div>
                <asp:ObjectDataSource ID="objdsUserCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetAllUsersCompany" TypeName="HRM.Common.BLL.MksSecurity">
                
                </asp:ObjectDataSource>
                </tr>
    </table>
            </div>
        </ContentTemplate>

</asp:Content>
