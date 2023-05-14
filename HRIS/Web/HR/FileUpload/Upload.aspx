<%@ Page Title="HRIS Employee | Employee's Personal Files" Language="C#" AutoEventWireup="true" CodeFile="Upload.aspx.cs" Inherits="HR_FileUpload_Upload" MasterPageFile="~/HR/HR_MenuMasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v14.2.Web, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Src="~/Common/Employee/EmployeeSearchNew.ascx" TagName="EmployeeSearch" TagPrefix="uc3" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC2" %>
<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.13.110, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>




<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .infoBox {
            font-size: 13px;
            font-style: italic;
            border-radius: 15px;
            -moz-border-radius: 15px;
            margin: 20px 0;
            border-width: 2px;
            border-color: Green;
            border-style: solid;
            width: 100%;
        }

        .infoInnerMargin {
            margin-top: 10px;
            margin-bottom: 10px;
            margin-right: 20px;
        }


        .emptable {
            border: #333333 1px solid;
            border-radius: 5px;
            -moz-border-radius: 5px;
            -moz-box-shadow: 2px 2px 2px #888;
            -webkit-box-shadow: 2px 2px 2px #888;
            box-shadow: 2px 2px 2px #888;
            font-family: 'Century Gothic', Helvetica, sans-serif;
            font-size: 11px;
            color: #333333;
        }

            .emptable img {
                border: #666666 solid 2px;
                margin: 0 20px 0 0;
            }

        .style17 {
            width: 600px;
        }

        .style18 {
            width: 100%;
        }

        .style19 {
            height: 97px;
        }

        .auto-style1 {
            height: 27px;
            width: 40px
        }

        .style2 {
            width: 10px;
        }


        .auto-style4 {
            width: 120px;
        }

        .form-outline {
            padding-right: 7px;
        }

        .searchright {
            margin-right: 43%;
        }

        element.style {
        }

/*        .pl-5, .px-5 {
            padding-left: 30rem !important;
        }
*/
        .dxpc-header {
            color: white !important;
            background-color: #1a8ac0 !important;
            border-bottom: none !important;
            padding: 15px !important;
            /* margin: -15px -15px 0px -15px !important; */
            font-size: 16px !important;
            padding-left: 30px !important;
            font-family: 'Source Sans Pro',sans-serif;
            height: 38px;
            border-radius: 4px;
        }
             label.choose:before {
  background: none repeat scroll 0 0 #00B7CD;
  border: 0 none;
  color: #FFFFFF;
  cursor: pointer;
  font-family: 'Altis_Book';
  font-size: 15px;
  padding: 3px 15px;
}
label.choose:before {
  content: 'Choose file';
  padding: 3px 6px;
  position: absolute;
}
    </style>
<link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-2.css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>

    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>
    </telerik:RadCodeBlock>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Employee's Personal Files Upload</h4>
                    <span onclick="toggleProfileForm(412);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td>
                        <div style="font-size: 16px;">
                            <ul class="infoInnerMargin">
                                <li><b style="color: #0c65a5">*</b> Uploading Excel file must be in <b style="color: #0c65a5">Pdf</b> format.</li>
                                <li><b style="color: #0c65a5">*</b> Only one pdf file can be uploaded at once; no limit of records in file.</li>
                                <li>&nbsp;</li>
                                <li>&nbsp;</li>
                            </ul>
                        </div>
                    </td>
                </tr>

            </table>
            <div class="row">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow" style="border-radius: 10px">
                            <div class="row">
                                <div class="col-md-6">
                                    <div class="pull-right" style="float: left">
                                        <asp:Label ID="Label2" runat="server" Text="File Type" CssClass="form-label" />
                                        <dx:ASPxComboBox ID="radFiletype" runat="server" AutoPostBack="true" Height="25px" BackColor="#F5F5F5" NullText="<< Select File Category Type >>"
                                            OnTextChanged="radcbJobCategory_TextChanged" DataSourceID="SqlDropDownFC" Skin="Windows7" TextField="FileCategoryName" ValueField="FileTypeId" Width="210px">
                                        </dx:ASPxComboBox>
                                    </div>
                                    <div class="pull-right" style="margin-left: 3px; float: left; margin-top: 4.2%;">
                                        <asp:ImageButton ID="btnelectplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="Button1_Click"></asp:ImageButton>

                                        <%--<asp:Button ID="Button1" runat="server" CssClass="btn btn-outline-warning check-aspbtn" Text="Add" OnClick="Button1_Click" />--%>
                                    </div>
                                </div>
                                <div class="col-md-6">
                                    <div class="input-group pl-5">
                                        <div class="form-outline">
                                            <dx:ASPxComboBox ID="CmbCustomers" runat="server" Width="285px" DropDownWidth="550"
                                                DropDownStyle="DropDownList" ValueField="EmployeeID" NullText="Advance Search" DataSourceID="ObjectDataSourceSearchGrid"
                                                ValueType="System.String" TextFormatString="First name :{0}/Code : {1} /{2}/ {3}" EnableCallbackMode="true" OnSelectedIndexChanged="CmbCustomers_SelectedIndexChanged" AutoPostBack="true" IncrementalFilteringMode="Contains"
                                                CallbackPageSize="30">
                                                <ClientSideEvents SelectedIndexChanged="" />
                                                <Columns>
                                                    <dx:ListBoxColumn FieldName="EmployeeID" Width="130px" Visible="false" />
                                                    <dx:ListBoxColumn FieldName="EmployeeCode" Width="130px" />
                                                    <dx:ListBoxColumn FieldName="FirstName" Width="130px" />
                                                    <dx:ListBoxColumn FieldName="LastName" Width="70px" />
                                                    <dx:ListBoxColumn FieldName="MobileNo" Width="100px" />
                                                    <dx:ListBoxColumn FieldName="NIC" Width="100px" />
                                                </Columns>
                                            </dx:ASPxComboBox>
                                            <%--<input type="text" value="Advance Search" id="form1" class="form-control" style="width: 136px; height: 27px" />
                                                     <label class="form-label" for="form1"></label>--%>
                                        </div>
                                        <%--           <div class="" style="margin-left: -44px">
                                                     <button type="button" class="btn btn-primary" runat="server" id="btnSearchSupplier" onclick="btnSearchSupplier_Click" style="height: 27px; background-color: white; border-color: lightgray; border-left-color: white;">
                                                         <img src="../../Images/searchemp.png" style="height: 18px" />
                                                     </button>
                                                 </div>--%>
                                        <telerik:RadTextBox ID="tbSearchSupplier" runat="server" AutoPostBack="True" OnTextChanged="tbSearchSupplier_TextChanged"
                                            Skin="Windows7" Visible="false" />
                                    </div>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" runat="server" Text="Description" CssClass="form-label"></asp:Label>
                                    <telerik:RadTextBox ID="txtDescription" runat="server" MaxLength="200" Width="100%" CssClass="form-control form-control-lg" Height="50%" TextMode="MultiLine"
                                        Skin="Windows7" />
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtDescription"
                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> Required Field</asp:RequiredFieldValidator>
                                </div>
                                <div class="col-md-6">
                                    <asp:DataList ID="DataList1" runat="server" Visible="false"
                                        RepeatColumns="3" RepeatDirection="Horizontal"
                                        Width="100%" DataSourceID="objdsEmployee">
                                        <ItemTemplate>
                                            <div class="DataList" width="100%" cellpadding="2" cellspacing="0">
                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label1" runat="server" Text="Employee Code" Width="100px" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="lblEmployeeCode" runat="server" CssClass="form-control form-control-lg"
                                                            Text='<%# Eval("EmployeeCode") %>'>
                                                        </telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label3" runat="server" Text="EPF Number  " CssClass="form-label" ForeColor="Black"></asp:Label>

                                                        <telerik:RadTextBox ID="lblEpf" runat="server" Text='<%# Eval("EPFNo") %>' CssClass="form-control form-control-lg"></telerik:RadTextBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label2" runat="server" Text="Employee Name  " CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="lblEmployeeName" runat="server"
                                                            Text='<%# Eval("NameWithInitials") %>' CssClass="form-control form-control-lg">
                                                        </telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label4" runat="server" Text="NIC No  " CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="lblNIC" runat="server" Text='<%# Eval("NIC") %>' CssClass="form-control form-control-lg"></telerik:RadTextBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-3">
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label6" runat="server" Text="Designation  " CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="lblDesignation" runat="server"
                                                            Text='<%# Eval("DesignationName") %>' CssClass="form-control form-control-lg">
                                                        </telerik:RadTextBox>
                                                    </div>
                                                    <div class="col-md-6">
                                                        <asp:Label ID="Label7" runat="server" Text="Department  " CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="lblOrganization" runat="server"
                                                            Text='<%# Eval("OrganizationTypeName") %>' CssClass="form-control form-control-lg">
                                                        </telerik:RadTextBox>
                                                    </div>
                                                </div>

                                                <div class="row">
                                                    <div class="col-md-6">
                                                        <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("Status") %>' />
                                                    </div>
                                                </div>

                                            </div>
                                        </ItemTemplate>
                                    </asp:DataList>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-6">
                                    <%--<asp:Label runat="server" Text="File" CssClass="form-label"></asp:Label>
                                    <asp:FileUpload ID="FileUpLoad1" runat="server" />--%>
                                    <label class="choose">
                                        <asp:FileUpload ID="FileUpLoad1" runat="server" Style="margin-left: 9px" />
                                    </label>
                                    <asp:Label ID="lblFilUp" runat="server" Text="" ForeColor="#006600"></asp:Label>
                                    <asp:Label ID="lblFilUpNo" runat="server" Text="" ForeColor="#CC0000"></asp:Label>
                                </div>
                            </div>
                            <div class="col d-flex justify-content-end pr-2" style="grid-gap: 6px">
                                <asp:Button ID="btnUpload" runat="server" CssClass="btn btn-outline-primary check-aspbtn" OnClick="btnUpload_Click" ValidationGroup="Add" Text="Upload" />
                                <asp:Button ID="RbtnUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="RbtnUpdate_Click" />
                                <asp:Button ID="RbtnDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="RbtnDelete_Click" />
                                <asp:Button ID="RbtnCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="RbtnCancel_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <table style="width: 100%">
                <tr>
                    <td>
                        <%--                              <CC2:ModalPopupExtender ID="btnSearchSupplier_ModalPopupExtender" runat="server"
                                                 CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPanel"
                                                 TargetControlID="btnSearchSupplier" BackgroundCssClass="popbackground" PopupDragHandleControlID="Panel1">
                                             </CC2:ModalPopupExtender>--%>
                        <%--           <table>
                                     <tr>
                                         <td>
                                             <asp:Label ID="Label5" runat="server" Text="Search Employee" Font-Names="Tahoma" Font-Bold="true" ForeColor="#336699" Font-Size="Small"></asp:Label>
                                         </td>
                                         <td>
                                             <asp:ImageButton ID="btnSearchSupplier" runat="server" ImageUrl="~/App_Themes/CommonResources/search.png"
                                                 Width="16px" OnClick="btnSearchSupplier_Click" />
                                             <CC2:ModalPopupExtender ID="btnSearchSupplier_ModalPopupExtender" runat="server"
                                                 CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPanel"
                                                 TargetControlID="btnSearchSupplier" BackgroundCssClass="popbackground" PopupDragHandleControlID="Panel1">
                                             </CC2:ModalPopupExtender>
                                         </td>
                                         <td>&nbsp;
                                         </td>
                                         <td>&nbsp;
                                         </td>
                                     </tr>
                                 </table>--%>
                        <asp:HiddenField ID="hfEmloyeeId" runat="server" />

                    </td>
                </tr>
                <tr>
                    <td>

                        <asp:SqlDataSource ID="objdsEmployee" runat="server"
                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                            SelectCommand="COM_GetEmployee" SelectCommandType="StoredProcedure">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfEmloyeeId" Name="EmployeeId" PropertyName="Value" Type="Int64" />
                                <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
                                <asp:SessionParameter Name="CompanyID" SessionField="CompanyId" Type="Int32" />
                                <asp:Parameter DefaultValue="HR" Name="ApplicationCode" Type="String" />
                            </SelectParameters>
                        </asp:SqlDataSource>
                    </td>
                </tr>
            </table>
        </div>
        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img alt="" src="../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>

            <div class="col table-scroll">
                <dx:ASPxGridView ID="Grid" runat="server" AutoGenerateColumns="False" ClientIDMode="AutoID" ClientInstanceName="grid" ForeColor="Black"
                    KeyFieldName="EmployeeId" order-BorderColor="Black" Style="margin-top: 0px; margin-left: 0px;" Theme="MetropolisBlue" Width="100%" DataSourceID="SqlGetFileUplod">
                    <SettingsSearchPanel HighlightResults="true" Visible="true" />
                    <Columns>
                        <dx:GridViewDataColumn Caption="Employee Code" CellStyle-HorizontalAlign="Center" FieldName="EmployeeCode" PropertiesEditType="TextBox" VisibleIndex="2" Width="10%" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn Caption="Employee's Name" CellStyle-HorizontalAlign="Left" FieldName="Name" PropertiesEditType="TextBox" VisibleIndex="2" Width="20%" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn Caption="Id" CellStyle-HorizontalAlign="Center" FieldName="Id" PropertiesEditType="TextBox" VisibleIndex="2" Width="100px" Visible="false" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn Caption="Description" CellStyle-HorizontalAlign="Left" FieldName="DesCription" PropertiesEditType="TextBox" VisibleIndex="5" Width="30%" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn Caption="File Type" CellStyle-HorizontalAlign="Left" FieldName="FileCategoryName" PropertiesEditType="TextBox" VisibleIndex="4" Width="15%" Visible="true" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn Caption="FileName" CellStyle-HorizontalAlign="Left" FieldName="FileName" PropertiesEditType="TextBox" VisibleIndex="6" Width="20%" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataTextColumn Caption="View" CellStyle-HorizontalAlign="Center" VisibleIndex="7" Width="5%" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                            <DataItemTemplate>
                                <asp:LinkButton ID="FileName" runat="server" OnClientClick="opentab();" OnClick="lkView_Click">View</asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" Width="35px" VisibleIndex="0" CellStyle-HorizontalAlign="Center">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" onclientclicked="showForm" OnClick="lkSelect_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                </asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                        <dx:GridViewDataColumn FieldName="EmployeeId" PropertiesEditType="TextBox" Visible="false" />
                        <dx:GridViewDataColumn FieldName="FileTypeId" PropertiesEditType="TextBox" Visible="false" />
                        <dx:GridViewDataColumn FieldName="FileCategoryCode" PropertiesEditType="TextBox" Visible="false" />
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
        <dx:ASPxPopupControl ID="ViewAttendancePopup" runat="server" PopupHorizontalAlign="WindowCenter" Width="1000px" Height="400px" PopupVerticalAlign="WindowCenter" Theme="MetropolisBlue" Modal="True">
            <HeaderTemplate>
                Create Folder
            </HeaderTemplate>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                    <div class="row">
                        <div class="col-md-6">
                            <asp:Label ID="Label5" runat="server" Text="File Category Code  " CssClass="form-label"></asp:Label>
                            <dx:ASPxTextBox ID="dxTxtFileTypeCode" runat="server" CssClass="form-control form-control-lg" Enabled="true"></dx:ASPxTextBox>
                        </div>
                        <div class="col-md-6">
                            <asp:Label ID="Label7" runat="server" Text="File Category Name  " CssClass="form-label"></asp:Label>
                            <dx:ASPxTextBox ID="dxTxtFileTypeName" runat="server" CssClass="form-control form-control-lg" Enabled="true"></dx:ASPxTextBox>
                        </div>
                    </div>
                    <asp:Label runat="server" ID="lblMsg"></asp:Label>
                    <div class="col d-flex justify-content-end pr-2 mt-3" style="grid-gap: 6px">
                        <asp:Button ID="BtnAddViewPopup" runat="server" CssClass="btn btn-outline-primary check-aspbtn" OnClick="BtnAddViewPopup_Click1" Text="Add" />
                        <asp:Button ID="btnPopupClear" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Clear" OnClick="btnPopupClear_Click" />
                        <asp:Button ID="BtnCloseViewPopup" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="BtnCloseViewPopup_Click" />
                    </div>
                    <div class="row mt-3">
                        <dx:ASPxGridView ID="GvAtte" runat="server" AutoGenerateColumns="False" DataSourceID="SQLPopupGrid" Font-Size="Small"
                            ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" Width="100%">
                            <SettingsPager PageSize="10">
                            </SettingsPager>
                            <SettingsSearchPanel Visible="true" HighlightResults="true" />
                            <Columns>
                                <dx:GridViewDataTextColumn FieldName="FileTypeId" Caption="FileTypeId" VisibleIndex="1" Visible="false">
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FileCategoryCode" Caption="Code" Width="10%" VisibleIndex="1" CellStyle-HorizontalAlign="Left">
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="FileCategoryName" Caption="File Category Name" Width="70%" VisibleIndex="2" CellStyle-HorizontalAlign="Left">
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                                <dx:GridViewDataTextColumn FieldName="CreatedUser" Caption="Created User" Width="10%" VisibleIndex="3" CellStyle-HorizontalAlign="Left">
                                    <CellStyle HorizontalAlign="Left">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>

                                <dx:GridViewDataTextColumn Caption="" CellStyle-HorizontalAlign="Center" VisibleIndex="0" Width="10%">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click1"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center">
                                    </CellStyle>
                                </dx:GridViewDataTextColumn>
                            </Columns>
                            <Styles>
                                <Header BackColor="WhiteSmoke" Font-Bold="true" HorizontalAlign="Center">
                                </Header>
                                <Cell HorizontalAlign="Center"></Cell>
                                <AlternatingRow BackColor="#F5F5F5">
                                </AlternatingRow>
                            </Styles>
                        </dx:ASPxGridView>
                    </div>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>

        <asp:HiddenField ID="Id" runat="server" />
        <asp:SqlDataSource ID="SqlGetFileUplod" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="Ea_GetDataFileUpload" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="hfEmloyeeId" DefaultValue="0" Name="EmployeeId" PropertyName="Value" Type="Int64" />
            </SelectParameters>
        </asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDropDownFC" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT FileCategoryName,FileTypeId FROm HR_FileType"
            SelectCommandType="Text"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SQLPopupGrid" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="SELECT * FROm HR_FileType" SelectCommandType="Text"></asp:SqlDataSource>
        <asp:HiddenField ID="hfFileTypeId" runat="server" />
        <table>
            <tr>
            </tr>
        </table>
        <div>
            <dx:ASPxGridView ID="gvSearch" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceSearchGrid" Visible="false"
                ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="CustomerId" Theme="MetropolisBlue" Width="100%">
                <SettingsPager PageSize="15">
                </SettingsPager>
                <SettingsSearchPanel Visible="true" HighlightResults="true" />
                <Columns>
                    <dx:GridViewDataTextColumn FieldName="EmployeeID" Caption="EmployeeID" VisibleIndex="2" Visible="false" Width="100">
                        <CellStyle HorizontalAlign="Center" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee Code" VisibleIndex="2" Width="100">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="FirstName" Caption="First Name" VisibleIndex="3" Width="150">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="LastName" Caption="Last Name" VisibleIndex="4" Width="150">
                        <CellStyle HorizontalAlign="Left" />
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No" VisibleIndex="5" Width="100">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" VisibleIndex="6" Width="120" Visible="false">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn FieldName="NIC" Caption="NIC" VisibleIndex="7" Width="120">
                    </dx:GridViewDataTextColumn>
                    <dx:GridViewDataTextColumn Caption="Select" VisibleIndex="1" Width="80">
                        <CellStyle HorizontalAlign="Center" />
                        <DataItemTemplate>
                            <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click">Select</asp:LinkButton>
                        </DataItemTemplate>
                    </dx:GridViewDataTextColumn>


                </Columns>
                <Styles>
                    <Header BackColor="WindowFrame" Font-Bold="true" HorizontalAlign="Center">
                    </Header>
                    <AlternatingRow BackColor="#F5F5F5">
                    </AlternatingRow>
                </Styles>
            </dx:ASPxGridView>

            <asp:ObjectDataSource ID="ObjectDataSourceSearchGrid" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                TypeName="CommenDataSetTableAdapters.GetAdvanceEmployeesForSearchTableAdapter">
                <SelectParameters>
                    <asp:ControlParameter ControlID="ddlSearchCollom" Name="ContainCollom"
                        PropertyName="SelectedValue" Type="String" />
                    <asp:ControlParameter ControlID="tbSearchSupplier" Name="ContainText"
                        PropertyName="Text" Type="String" />
                    <asp:ControlParameter ControlID="ddlContentLocation" Name="ContainPossition"
                        PropertyName="SelectedValue" Type="Int32" />
                    <asp:SessionParameter DefaultValue="" Name="UserName" SessionField="UserName"
                        Type="String" />
                    <asp:SessionParameter DefaultValue="" Name="CompanyID" SessionField="CompanyId"
                        Type="Int32" />
                    <asp:Parameter DefaultValue="HR" Name="ApplicationCode" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>

            <asp:HiddenField ID="hfSupplierId" runat="server" />
            <asp:HiddenField ID="hfSupplireCode" runat="server" />

            <telerik:RadComboBox ID="ddlSearchCollom" runat="server" ZIndex="1000000"
                Skin="Windows7" Visible="false">
            </telerik:RadComboBox>
            <telerik:RadComboBox ID="ddlContentLocation" runat="server" ZIndex="1000000"
                Skin="Windows7" Visible="false">
                <Items>
                    <telerik:RadComboBoxItem runat="server" Text="Start with" Value="1" />
                    <telerik:RadComboBoxItem runat="server" Text="Contain" Value="2" />
                    <telerik:RadComboBoxItem runat="server" Text="End with" Value="3" />
                </Items>
            </telerik:RadComboBox>
        </div>
        <dx:ASPxPopupControl ID="confirmpopup" runat="server" HeaderText="Confirm Message" Height="80px" PopupHorizontalAlign="WindowCenter" Width="380px" PopupVerticalAlign="WindowCenter">
            <HeaderTemplate>
                Confirm Message
            </HeaderTemplate>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                    <table style="width: 100%;">
                        <tr>
                            <td class="auto-style10" colspan="3">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="auto-style10" colspan="3" style="font-size: small; font-weight: bold; text-align: center; font-family: Arial, Helvetica, sans-serif; font-style: normal;">Are you sure you want to delete this </td>
                        </tr>
                        <tr>
                            <td class="auto-style10">&nbsp;</td>
                            <td></td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>

                    <table style="width: 100%;">
                        <tr>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td></td>
                            <td>
                                <asp:Button ID="Button2" runat="server" Height="20px" Width="80px" Text="OK" OnClick="Button2_Click" CssClass="btn btn-block btn-success btn-large" />
                            </td>
                            <td>
                                <asp:Button ID="Button3" runat="server" Height="20px" Width="80px" Text="Cancel" OnClick="Button3_Click" CssClass="btn btn-block btn-warning btn-large" />
                            </td>
                        </tr>
                        <tr>
                            <td class="auto-style7">&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>
</asp:Content>
