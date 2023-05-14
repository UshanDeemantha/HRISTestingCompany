<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSummaryList.ascx.cs"
    Inherits="Employee_EmployeeSummaryList" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc2" %>
<%@ Register Src="~/HR/Employee/UserControls/EmployeeSearchNew.ascx" TagName="EmployeeSearch"
    TagPrefix="uc1" %>
<%@ Register Src="~/Common/Organization/OrganizationStucture.ascx" TagName="OrganizationStucture"
    TagPrefix="uc2" %>
<%@ Register Src="~/HR/Organization/OrganizationStucture.ascx" TagName="OrganizationStucture" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<style type="text/css">
    /*style attribute {
    height: 0px;
    width: 0px;
    cursor: default;*/
    /* z-index: 11822; */
    /*visibility: visible;
    display: table;
    position: absolute;
    left: 181px;
    top: -31px;
}*/
    .emptable {
        border: #333333 1px solid;
        border-radius: 5px;
        -moz-border-radius: 5px;
        -moz-box-shadow: 2px 2px 2px #888;
        -webkit-box-shadow: 2px 2px 2px #888;
        box-shadow: 2px 2px 2px #888;
        font-family: Arial, Helvetica, sans-serif;
        font-size: 11px;
        color: #333333;
        margin: 5px 10px;
    }

        .emptable img {
            border: #CCCCCC 2px solid;
        }

    a:link {
        text-decoration: none !important;
        color: green;
    }

    .emphederStyle {
        font-size: 11px;
        font-family: Arial, Helvetica, sans-serif;
        color: #000000;
        font-weight: bold;
        width: 90px;
    }

    .empinnerStyle {
        font-size: 11px;
        width: 160px;
    }

    .pagecount {
    }

        .pagecount a {
            font-size: 11px;
            font-family: Arial, Helvetica, sans-serif;
            border: #CCCCCC;
            padding: 5px;
            color: #000000;
            background: #efefef;
            text-decoration: none;
        }

            .pagecount a:hover {
                font-size: 11px;
                font-family: Arial, Helvetica, sans-serif;
                border: #CCCCCC;
                padding: 5px;
                color: #000000;
                background: #FFCC33;
            }

    .style9 {
        width: 100%;
    }

    .style1 {
        width: 300px;
    }

    .style2 {
        width: 700px;
    }

    .style4 {
        width: 160px;
    }

    .style6 {
        width: 500px;
    }

    .style17 {
        width: 600px;
    }

    .style19 {
        height: 97px;
    }

    input#ContentPlaceHolder1_EmployeeSummaryList1_ddlEmployeeStatus_I {
    text-align: Center;
    color: #ffffff;
    font-weight: 900;
}

    table#ContentPlaceHolder1_EmployeeSummaryList1_ddlEmployeeStatus {
        margin-top: 0px !important;
        border-radius: 4px;
        margin-top: -6px;
        background-color: #f79819;
        border-color: #f79819;
    }

    input#ContentPlaceHolder1_EmployeeSummaryList1_ddlEmployeeStatus_I {
        background-color: #f79819;
    }

    button#ContentPlaceHolder1_EmployeeSummaryList1_Button1 {
        border-left: n;
        border-right-color: white;
    }

    .form-outline {
        padding-right: 7px;
    }

    .searchright {
        margin-right: 43%;
    }

    a#ctl00_ContentPlaceHolder1_EmployeeSummaryList1_btnReset {
        width: 70px;
        height: 28px;
        padding-top: 7px;
        margin-top: 0px;
    }

    a#ctl00_ContentPlaceHolder1_EmployeeSummaryList1_btnSave {
        width: 70px;
        height: 28px;
        padding-top: 7px;
        margin-top: 0px;
    }

    a#ctl00_ContentPlaceHolder1_EmployeeSummaryList1_RadButton1 {
        width: 70px;
        height: 28px;
        padding-top: 7px;
        margin-top: 0px;
    }

    a#ctl00_ContentPlaceHolder1_EmployeeSummaryList1_RadButton1 {
        width: 70px;
        height: 28px;
        padding-top: 7px;
        margin-top: 0px;
    }

    a#ctl00_ContentPlaceHolder1_EmployeeSummaryList1_btnOrganizationSelect {
        width: 70px;
        height: 28px;
        padding-top: 7px;
        margin-top: 0px;
    }
    /*a:hover {
    opacity: 0;
    color: white;
}*/
    .EmpSize {
        margin-top: -35px;
    }

    .m-4 {
        margin: 2.5rem !important;
    }

    }

    .dxpcModalBackLite, .dxdpModalBackLite {
        background-color: cadetblue;
        opacity: 0.7;
        filter: progid:DXImageTransform.Microsoft.Alpha(Style=0, Opacity=70);
        position: fixed;
        left: 0;
        top: 0;
        visibility: hidden;
    }
    h5.m-0 {
    font-size: 1.5rem;
}
    .rcbSlide {
        z-index: 100000 !important;
        visibility: visible !important;
        display: block !important;
        overflow: visible !important;
        margin-left: 0px !important;
        position: absolute !important;
        top: 263.5px !important;
        left: 237.75px !important;
        height: 373px !important;
        width: 300px !important;
}

        #ctl00_ContentPlaceHolder1_EmployeeSummaryList1_RadListView1_RadDataPager1_ctl02_NextButton {
    }
    a#ctl00_ContentPlaceHolder1_EmployeeSummaryList1_AddEmpbtn {
    margin-left: 25px;
}
</style>
<link rel="stylesheet" type="text/css" href="../../../App_Themes/NewTheme/css/Common/organization-structure-4.css" />
<link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/employeeList.css" />

<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
	<ContentTemplate>--%>
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

<div class="d-flex p-3 pt-4 shadow-sm justify-content-between" style="grid-gap: 20px;">
    <div>
        <div class="d-flex" style="grid-gap: 12px; align-items: center;">
            <div class="searchright">
                <div class="input-group pl-5">
                    <div class="form-outline">
                        <dx:ASPxComboBox ID="CmbCustomers" runat="server" Style="width: 700px; border-radius: 10px;" DropDownWidth="550"
                            DropDownStyle="DropDownList" ValueField="EmployeeID" DataSourceID="ObjectDataSourceSearchGrid" NullText="Advance Search"
                            ValueType="System.String" TextFormatString="First name :{0}/Code : {1} /{2}/ {3}" EnableCallbackMode="true" OnSelectedIndexChanged="CmbCustomers_SelectedIndexChanged" 
                            AutoPostBack="true" IncrementalFilteringMode="Contains"
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
                    </div>
                 </div>
            </div>
            <asp:SqlDataSource ID="SqlDataSource1" runat="server"
                ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="HR_InactiveStatusGetcombo" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
        </div>
    </div>
    <div class="d-flex pr-5" style="grid-gap: 10px;">
        <div>
            <dx:ASPxComboBox ID="ddlEmployeeStatus" runat="server" Font-Names="Tahoma" AutoPostBack="true" DataSourceID="sqldsEmployeeStatus"
                TextField="StatusName" ValueField="StatusId" Skin="Windows7" SelectedIndex="0" Width="100px" Height="32px" HorizontalAlign="Center">
            </dx:ASPxComboBox>
            <asp:SqlDataSource ID="sqldsEmployeeStatus" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                SelectCommand="Select StatusId,StatusName From InactiveSttatus"></asp:SqlDataSource>
        </div>
        <div>
            <asp:ImageButton ID="btnReset" runat="Server" Visible="true" OnClick="btnReset_Click" ImageUrl="~/App_Themes/NewTheme/img/reset.png" ValidationGroup="a"></asp:ImageButton>
        </div>
        <div>
            <asp:ImageButton ID="btnSave" runat="Server" Visible="true" OnClick="btnSave_Click" ImageUrl="~/App_Themes/NewTheme/img/import.png" ValidationGroup="a"></asp:ImageButton>
            
        </div>
        <div>
             <asp:ImageButton ID="RadButton1" runat="Server" Visible="true" OnClick="RadButton1_Click" ImageUrl="~/App_Themes/NewTheme/img/export.png" ValidationGroup="a"></asp:ImageButton>          
        </div>
        <div>
             <asp:ImageButton ID="btnOrganizationSelect" runat="Server" Visible="true" OnClick="RadButton1_Click" ImageUrl="~/App_Themes/NewTheme/img/department.png" ValidationGroup="a"></asp:ImageButton>            
            <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPane2"
                TargetControlID="btnOrganizationSelect">
            </cc1:ModalPopupExtender>

            <asp:HiddenField ID="hfOrganizationStructure" runat="server" />
             <asp:ImageButton ID="ImageBAddEmpbtnutton1" runat="Server" Visible="true" OnClick="AddEmpbtn_Click" ImageUrl="~/App_Themes/NewTheme/img/addnew.png" ValidationGroup="a"></asp:ImageButton>        
            
        </div>
    </div>

</div>
<div class="p-3 d-flex" style="grid-gap: 15px; display: none!important">
    <asp:Button ID="Button1" runat="server" CssClass="btn btn-light" Text="Filter By Department" OnClientClick="addOrgStruStyle()" />
    <asp:HiddenField ID="HiddenField1" runat="server" />
    <asp:Label ID="Label2" runat="server" Text="Advanced Search" Font-Names="Tahoma" Font-Bold="true" ForeColor="#336699" Font-Size="Small"></asp:Label>
    <telerik:RadTextBox ID="tbSearchSupplier" runat="server" AutoPostBack="True" OnTextChanged="tbSearchSupplier_TextChanged"
        Skin="Windows7" Visible="false" />
</div>
<div class="m-5">
</div>
<div>
    <cc2:CollectionPager ID="CollectionPager1" runat="server" PageSize="3" ControlCssClass="pagecount">
    </cc2:CollectionPager>
</div>
<div class="EmpSize">

    <telerik:RadListView ID="RadListView1" AllowPaging="True" runat="server" AllowSorting="true" DataSourceID="objdsEmployee"
         ItemPlaceholderID="ListViewContainer" DataKeyNames="EmployeeID"
        Skin="Black" CssClass="test">
         <LayoutTemplate>
            <div class="pt-3 d-flex justify-content-start pr-5">
                <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView1" CssClass="employee-list-paginator"
                    PageSize="20" Skin="Windows7">
                    <Fields>
                        <telerik:RadDataPagerTemplatePageField>
                            <PagerTemplate>

                                <div>

                                    <%--    <cc2:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                            CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPanel"
                                            TargetControlID="btnSearchSupplier" BackgroundCssClass="popbackground" PopupDragHandleControlID="Panel1">
                                        </cc2:ModalPopupExtender>--%>
                                </div>

                                <%--  <div>
                                        <telerik:RadTextBox ID="txtCompanyCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50" />
                                    </div>--%>
                                <div class="text-muted item-count">
                                    <b style="font-size:12px">
                                        <asp:Label runat="server" ID="CurrentPageLabel" Text="<%# Container.Owner.StartRowIndex+1%>" />
                                        to
                                                            <asp:Label runat="server" ID="TotalPagesLabel" Text="<%# Container.Owner.TotalRowCount > (Container.Owner.StartRowIndex+Container.Owner.PageSize) ? Container.Owner.StartRowIndex+Container.Owner.PageSize : Container.Owner.TotalRowCount %>" />
                                        of
                                                            <asp:Label runat="server" ID="TotalItemsLabel" Text="<%# Container.Owner.TotalRowCount%>" />
                                        <br />
                                    </b>
                                </div>
                            </PagerTemplate>
                        </telerik:RadDataPagerTemplatePageField>

                        <telerik:RadDataPagerButtonField FieldType="FirstPrev" />
                        <telerik:RadDataPagerButtonField FieldType="NextLast" />

                        <telerik:RadDataPagerPageSizeField PageSizeText="Page size: " />
                        <%-- <telerik:RadDataPagerGoToPageField CurrentPageText="Page: "  TotalPageText="of" SubmitButtonText="Go"
                                TextBoxWidth="35" />--%>
                    </Fields>
                </telerik:RadDataPager>

            </div>
            <div class="employee-list-container grid-container">
                <asp:PlaceHolder runat="server" ID="ListViewContainer" />
            </div>

        </LayoutTemplate>

        <ItemTemplate>
            <div class="employee-container employee-container-large  shadow">
                <div style="position: relative">
                    <div class="employee-code btn btn-warning">
                        <label class="m-0"><%#Eval("EmployeeCode")%></label>
                    </div>
                    <%--<div class="view-more more" onclick="viewOrHideDetail(this)">
                            <label class="fa fa-arrow-right text-muted" style="cursor: pointer"></label>
                        </div>--%>
                    <div class="grid-center" style="margin-top: 30px;">
                        <asp:Image ID="imgEmployee" runat="server" ImageUrl='<%# "~/HR/EmployeeImages/"+Eval("Image") %>'
                            Width="90px" Height="110px" />
                    </div>

                </div>
                <div class="d-flex flex-column justify-content-center" style="position: relative">
                    <div>
                        <h5 class="m-0"><%# Eval("NameWithInitial") %> <%# Eval("LastName") %></h5>
                    </div>
                    <div>
                        <label  class="d-flex mt-2" style="font-size: 15px;"><%#Eval("DesignationName")%></label>
                    </div>
                    <div>
                        <label class="m-0 text-muted" style="text-transform: uppercase; font-size: 12px"><%#Eval("OrganizationTypeName")%></label>
                    </div>
                    <div>
                        <button class="btn modify-button btn-outline-success">
                            <asp:LinkButton ID="lbtnModify" runat="server" PostBackUrl='<%# "../ModifiyEmployeeAditionalInfo.aspx?EmployeeId="+Eval("EmployeeID") %>'>Modify</asp:LinkButton>
                        </button>
                    </div>
                    <div class="d-flex mt-2" style="font-size: 14px; grid-gap: 20px">
                        <label>EPF</label>
                        <label><%# Eval("EPFNo")%></label>
                    </div>
                    <div class="d-flex" style="font-size: 14px; grid-gap: 14px">
                        <label>DOB</label>
                        <label><%#Eval("DateOfBirth","{0:d}")%></label>
                    </div>
                    <div class="d-flex" style="font-size: 14px; grid-gap: 20px">
                        <label>NIC</label>
                        <label><%#Eval("NIC")%></label>
                    </div>
                </div>
            </div>
        </ItemTemplate>

    </telerik:RadListView>

</div>
<div>
   <asp:ObjectDataSource ID="objdsEmployee" runat="server" SelectMethod="GetEmployeeOrg"
        TypeName="HRM.Common.BLL.Employee" OldValuesParameterFormatString="original_{0}">
        <SelectParameters>

            <asp:ControlParameter ControlID="hfOrganizationStructure" DefaultValue="0"
                Name="OrgStructureID" PropertyName="Value" Type="Int32" />
            <asp:ControlParameter ControlID="hfEmloyeeId" DefaultValue="0"
                Name="EmployeeId" PropertyName="Value" Type="Int64" />
            <asp:SessionParameter DefaultValue="" Name="UserName" SessionField="UserName"
                Type="String" />
            <asp:SessionParameter Name="CompanyID" SessionField="CompanyId" Type="Int32" />
            <asp:Parameter DefaultValue="HR" Name="ApplicationCode" Type="String" />
            <asp:ControlParameter ControlID="ddlEmployeeStatus" DefaultValue="0"
                Name="StatusId" PropertyName="Value" Type="Int32" />
        </SelectParameters>
    </asp:ObjectDataSource>

</div>
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
<div>
    <asp:Panel ID="PopupPane2" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical" >
        <div class="style1">
            <div style="float: right;margin-top: -10px;">
                <asp:ImageButton ID="btnClosePopup" runat="server"
                    ImageUrl="~/App_Themes/NewTheme/img/close.png" />
            </div>
            <div id="org-stucture">
                <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
            </div>
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hfEmloyeeId" runat="server" />
</div>



