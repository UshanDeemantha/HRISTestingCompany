<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSummaryList.ascx.cs"
    Inherits="Employee_EmployeeSummaryList" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc2" %>
<%@ Register Src="EmployeeSearch.ascx" TagName="EmployeeSearch" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<style type="text/css">
    .emptable
    {
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
    .emptable img
    {
        border: #CCCCCC 2px solid;
    }
    .emphederStyle
    {
        font-size: 11px;
        font-family: Arial, Helvetica, sans-serif;
        color: #000000;
        font-weight: bold;
        width: 90px;
    }
    
    .empinnerStyle
    {
        font-size: 11px;
        width: 160px;
    }
    .pagecount
    {
    }
    .pagecount a
    {
        font-size: 11px;
        font-family: Arial, Helvetica, sans-serif;
        border: #CCCCCC;
        padding: 5px;
        color: #000000;
        background: #efefef;
        text-decoration: none;
    }
    .pagecount a:hover
    {
        font-size: 11px;
        font-family: Arial, Helvetica, sans-serif;
        border: #CCCCCC;
        padding: 5px;
        color: #000000;
        background: #FFCC33;
    }
    
    .style1
    {
        width: 100%;
    }
      .style11
    {
        width: 1180px !important;
    }
    .style4
    {
        width: 536px;
    }
    
    .style6
    {
        width: 146px;
    }
</style>
<asp:ScriptManager ID="ScriptManager1" runat="server">
</asp:ScriptManager>
<%--<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>--%>
<table class="style11">
    <tr>
        <td>
            <table class="style4">
                <tr>
                    <td class="style6">
                        <telerik:RadTextBox ID="tbSearchSupplier" runat="server" AutoPostBack="True"
                            OnTextChanged="tbSearchSupplier_TextChanged" Skin="Windows7" />
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearchSupplier" runat="server" ImageUrl="~/App_Themes/CommonResources/search.png"
                            Width="16px" OnClick="btnSearchSupplier_Click" />
                        <cc1:ModalPopupExtender ID="btnSearchSupplier_ModalPopupExtender" runat="server"
                            CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPanel"
                            TargetControlID="btnSearchSupplier" BackgroundCssClass="popbackground" PopupDragHandleControlID="Panel1">
                        </cc1:ModalPopupExtender>
                    </td>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td>
            <asp:ObjectDataSource ID="objdsEmployee" runat="server" SelectMethod="GetEmployee"
                TypeName="HRM.Common.BLL.Employee" OldValuesParameterFormatString="original_{0}">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfEmloyeeId" DefaultValue="0" Name="EmployeeId"
                        PropertyName="Value" Type="Int64" />
                    <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
                    <asp:SessionParameter Name="CompanyID" SessionField="CompanyId" Type="Int32" />
                    <asp:Parameter DefaultValue="Common" Name="ApplicationCode" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <br />
            <telerik:RadListView ID="RadListView1" AllowPaging="True" runat="server" AllowSorting="true"
                DataSourceID="objdsEmployee" ItemPlaceholderID="ListViewContainer" DataKeyNames="EmployeeID"
                Skin="Black" OnItemCreated="RadListView1_ItemCreated">
                <LayoutTemplate>
                    <fieldset id="FieldSet1">
                        <legend>Employees</legend>
                        <table cellpadding="0" cellspacing="0" width="100%;" style="clear: both;">
                            <tr>
                                <td>
                                    <telerik:RadDataPager ID="RadDataPager1" runat="server" PagedControlID="RadListView1"
                                        PageSize="9"  Skin="Windows7">
                                        <Fields>
                                            <telerik:RadDataPagerButtonField FieldType="FirstPrev"  />
                                            <telerik:RadDataPagerButtonField FieldType="Numeric" />
                                            <telerik:RadDataPagerButtonField FieldType="NextLast" />
                                            <telerik:RadDataPagerPageSizeField PageSizeText="Page size: " />
                                            <telerik:RadDataPagerGoToPageField CurrentPageText="Page: " TotalPageText="of" SubmitButtonText="Go"
                                                TextBoxWidth="15" />
                                            <telerik:RadDataPagerTemplatePageField>
                                                <PagerTemplate>
                                                    <div style="float: right">
                                                        <b>Items
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
                                        </Fields>
                                    </telerik:RadDataPager>
                                </td>
                            </tr>
                        </table>
                        <asp:PlaceHolder runat="server" ID="ListViewContainer" />
                </LayoutTemplate>
                <ItemTemplate>
                    <fieldset style="float: left; width: 350px; height: 150px; margin: 10px 10px 0 0;">
                        <legend><b>Employee Code </b>: <strong>
                            <%#Eval("EmployeeCode")%></strong></legend>
                        <div class="details">
                            <div style="padding: 10px; width: 120px; float: left;">
                                <asp:Image ID="imgEmployee" runat="server" ImageUrl='<%# "../EmployeeImages/"+Eval("Image") %>'
                                    Width="90px" Height="110px" />
                            </div>
                            <div class="data-container">
                                <ul>
                                    <li>
                                        <label>
                                            Contact Name:
                                        </label>
                                        <%# Eval("FirstName")+" "+Eval("LastName") %>
                                    </li>
                                    <li>
                                        <label>
                                            Date Of Birth:
                                        </label>
                                        <%#Eval("DateOfBirth","{0:d}")%>
                                    </li>
                                    <li>
                                        <label>
                                            NIC:
                                        </label>
                                        <%#Eval("NIC")%>
                                    </li>
                                    <li>
                                        <label>
                                            Mobile No:
                                        </label>
                                        <%#Eval("MobileNo")%>
                                    </li>
                                    <li>
                                        <label>
                                            Designation:
                                        </label>
                                        <%#Eval("DesignationName")%>
                                    </li>
                                    <li>
                                        <label>
                                            Organization:
                                        </label>
                                        <%#Eval("OrganizationTypeName")%>
                                    </li>
                                    <li>
                                        <asp:LinkButton ID="lbtnModify" runat="server" PostBackUrl='<%# "../Employee/EditEmployee.aspx?EmployeeId="+Eval("EmployeeID") %>'
                                            OnClick="lbtnModify_Click">Modify</asp:LinkButton>
                                    </li>
                                </ul>
                            </div>
                        </div>
                    </fieldset>
                </ItemTemplate>
            </telerik:RadListView>
        </td>
    </tr>
    <tr>
        <td>
            <div id="PopupPanel">
                <asp:Panel ID="Panel1" runat="server" CssClass="popstyle" Height="550px ">
                    <table class="style1">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: right">
                                <asp:ImageButton ID="btnClosePopup" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td style="text-align: center">
                                                              <uc1:EmployeeSearch ID="EmployeeSearch1" runat="server" />

                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="hfEmloyeeId" runat="server" OnValueChanged="hfEmloyeeId_ValueChanged" />
                            </td>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
        </td>
    </tr>
</table>
<%--    </ContentTemplate>
          </asp:UpdatePanel>--%>
