<%@ Page Title="HRM Common | User Payroll" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="UserPayrollPermission.aspx.cs" Inherits="Common_Settings_UserPayrollPermission" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script>
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "UserPayrollPermission.aspx/downloadExcelFromGrid",
                //data: JSON.stringify({ startDate: date._d.format('dd/MM/yyyy') }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {

                }
            });
        }

    </script>

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
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
       <ContentTemplate>
            <asp:HiddenField ID="HiddenField1" runat="server" />
            <div class="overflow-auto" style="height: calc(100vh - 110px)">
                <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
                    <div class="row">
                        <div class="col head-container">
                            <h4 class="header">User Payroll</h4>
                            <span onclick="toggleProfileForm(365);" id="profileButton" class="dot shadow"></span>
                        </div>
                    </div>
                    <div class="row" style="background-color: ghostwhite">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow">
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" CssClass="form-label" Text="User" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radcbSelectUser" CssClass="form-label"
                                                ErrorMessage="a" ValidationGroup="a" ToolTip="Required Field!" ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <dx:ASPxComboBox ID="radcbSelectUser" runat="server" DataSourceID="objdsUser" CssClass="form-control form-control-lg"
                                                AutoPostBack="True" TextField="UserName" ValueField="UserId" Skin="Windows7" NullText="Please Select User Name"
                                                OnDataBound="radcbSelectUser_DataBound" OnSelectedIndexChanged="radcbSelectUser_SelectedIndexChanged1">
                                            </dx:ASPxComboBox>
                                            <asp:ObjectDataSource ID="objdsUser" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetActiveUser" TypeName="HRM.Common.BLL.MksSecurity">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="hfCurrentUserType" Name="UserTypeId" PropertyName="Value" Type="Int32" />
                                                    <asp:Parameter DefaultValue="0" Name="UserId" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                            <asp:HiddenField ID="hfCurrentUserType" runat="server" />
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" CssClass="form-label" Text="Company" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radcbSelectCompany" CssClass="form-label"
                                                ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <dx:ASPxComboBox ID="radcbSelectCompany" runat="server" CssClass="form-control form-control-lg" Enabled="false"
                                                AutoPostBack="True" TextField="CompanyName" ValueField="CompanyId" Skin="Windows7" NullText="Please Select Company Name"
                                                OnDataBound="radcbSelectCompany_DataBound" OnSelectedIndexChanged="radcbSelectCompany_SelectedIndexChanged1">
                                            </dx:ASPxComboBox>
                                            <asp:ObjectDataSource ID="objdsCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                                                SelectMethod="GetUserCompany" TypeName="HRM.Common.BLL.MksSecurity">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="radcbSelectUser" Name="UserId" PropertyName="Value"
                                                        Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label ID="lblPaycategory" CssClass="form-label" runat="server" Text="Pay Category"></asp:Label>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="cblPayCategory" CssClass="form-label"
                                                ErrorMessage="Required Field!" ValidationGroup="a" ToolTip="Required Field!"
                                                ForeColor="Red">*</asp:RequiredFieldValidator>
                                            <telerik:RadComboBox ID="cblPayCategory" runat="server" CheckBoxes="true" CheckedItemsTexts="DisplayAllInInput" Enabled="false"
                                                OnClientLoad="OnClientLoadComboBox" Width="100%"
                                                DataTextField="PayPeriodCategory" DataValueField="PayPeriodCategoryId" OnDataBound="cblPayCategory_DataBound">
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
                    <div id="toastrContainer">
                        <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                        <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                    </div>

                    &nbsp;
                    <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
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
                        <dx:ASPxGridView ID="radgvDetails" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" ForeColor="Black">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="UserName" HeaderStyle-Font-Bold="false" Caption="User Name" PropertiesEditType="TextBox" Width="120px" VisibleIndex="0" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="CompanyName" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Caption="Company Name" PropertiesEditType="TextBox" Width="400px" VisibleIndex="1" />
                        <dx:GridViewDataColumn FieldName="PayPeriodCategory" HeaderStyle-Font-Bold="false" Caption="Pay Category" PropertiesEditType="TextBox" Width="450px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                       </Columns>

                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="230" UseFixedTableLayout="true" />

                    <SettingsPager Mode="ShowPager" PageSize="10" />
                </dx:ASPxGridView>
                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
                    </div>
                </div>
                <asp:ObjectDataSource ID="objdsUserCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                    SelectMethod="GetUserCompany" TypeName="HRM.Common.BLL.MksSecurity">
                    <%--     <SelectParameters>
                        <asp:ControlParameter ControlID="radcbUser" DefaultValue="" Name="UserId" PropertyName="SelectedValue"
                            Type="String" />
                    </SelectParameters>--%>
                </asp:ObjectDataSource>
       </div>
        </ContentTemplate>
</asp:Content>
