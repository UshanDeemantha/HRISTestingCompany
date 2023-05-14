<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSearch.ascx.cs" Inherits="Employee_EmployeeSearch" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%--<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>--%>

<style type="text/css">
    .style1 {
        width: 800px;
    }

    .style2 {
        width: 96px;
    }

    .style5 {
        width: 100%;
    }

    .style3 {
        width: 627px;
        text-align: center;
    }

    .style4 {
        width: 661px;
    }

    tr.AlternatingRowStyle {
        text-align: center;
        background-color: #7fefae;
    }

    .FooterStyle {
        background-color: #938ede;
        height: 25px;
    }

    .HeaderStyle {
        border: 1px, solid, #ddd;
        background-color: #839192;
    }



        .HeaderStyle th {
            padding: 5px 0px 5px 0px;
            color: #333;
            text-align: center;
        }

    .altrowstyle {
        background-color: #edf5ff;
    }

        /*New grid css*/

        .rowstyle .sortaltrow, .altrowstyle .sortaltrow {
            background-color: #edf5ff;
        }

    .mGrid {
        /*width: 100%;*/
        background-color: #fff;
        margin: 5px 0 10px 0;
        border: solid 1px #e7eef6;
        border-collapse: collapse;
    }

        .mGrid td {
            padding: 2px;
            /*border: solid 1px #c1c1c1;*/
            /*color: #717171;*/
        }

        .mGrid th {
            padding: 4px 2px;
            color: #212121;
            background: #839192 url(grd_head.png) repeat-x top;
            border-left: solid 1px #E5E7E9;
            font-size: 0.9em;
        }

        .mGrid .alt {
            background: #fcfcfc url(grd_alt.png) repeat-x top;
        }

        .mGrid .pgr {
            background: #424242 url(grd_pgr.png) repeat-x top;
        }

            .mGrid .pgr table {
                margin: 5px 0;
            }

            .mGrid .pgr td {
                border-width: 0;
                padding: 0 6px;
                border-left: solid 1px #212121;
                font-weight: bold;
                color: #212121;
                line-height: 12px;
            }

            .mGrid .pgr a {
                color: #666;
                text-decoration: none;
            }

                .mGrid .pgr a:hover {
                    color: #000;
                    text-decoration: none;
                }

    .grdDiv tr:hover {
        background-color: #E5E7E9;
        text-decoration: none;
        cursor:pointer;
    }
    .auto-style1 {
        width: 580px;
    }
    .auto-style2 {
        width: 700px;
        text-align: center;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <table>
            <tr>
                <td>&nbsp;</td>
                <td>Search Employee</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">

                    <table class="style4">
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="tbSearchSupplier" runat="server" CssClass="textBox"
                                    EmptyMessage="Type Search Criteria..." Skin="Windows7" Width="125px" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlSearchCollom" runat="server" ZIndex="1000000"
                                    Skin="Windows7">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlContentLocation" runat="server" ZIndex="1000000"
                                    Skin="Windows7">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Start with" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Contain" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="End with" Value="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                    <asp:ImageButton ID="btnSearchSupplier" runat="server"
                                    ImageUrl="~/App_Themes/CommonResources/search.png" OnClick="btnSearchSupplier_Click"
                                    Style="width: 10px; height: 10px;" />
                            </td>
                        </tr>
                    </table>


                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <div style="width:700px; height: 480px" class="grdDiv">
                        <asp:GridView ID="gvSearch" runat="server" AllowPaging="True"
                            AutoGenerateColumns="False" CssClass="mGrid" EnableTheming="true"
                            PageSize="15" DataSourceID="ObjectDataSourceSearchGrid"
                            OnRowCommand="gvSearch_RowCommand" 
                            OnSelectedIndexChanged="gvSearch_SelectedIndexChanged"
                            OnPageIndexChanged="gvSearch_PageIndexChanged"
                            OnPageIndexChanging="gvSearch_PageIndexChanging">


                            <Columns>
                                <asp:TemplateField ShowHeader="False">
                                    <ItemTemplate>
                                        <telerik:RadButton ID="Button1" runat="server"
                                            CommandName="Select" CommandArgument='<%# Eval("EmployeeID") %>'
                                            Text="Select" Skin="WebBlue" />
                                    </ItemTemplate>
                                    <ControlStyle CssClass="GridBtn" />
                                </asp:TemplateField>
                                <asp:BoundField DataField="EmployeeID" HeaderText="Employee ID"
                                    SortExpression="EmployeeID" Visible="False" />
                                <asp:BoundField DataField="EmployeeCode" HeaderText="Employee Code"
                                    SortExpression="EmployeeCode" />
                                <asp:BoundField DataField="FirstName" HeaderText="First Name"
                                    SortExpression="FirstName" ItemStyle-Width="150px" />
                                <asp:BoundField DataField="LastName" HeaderText="Last Name" ItemStyle-Width="150px"
                                    SortExpression="LastName" />
                                <asp:BoundField DataField="HomeContactNo" HeaderText="Home No"
                                    SortExpression="HomeContactNo" Visible="false" />
                                <asp:BoundField DataField="MobileNo" HeaderText="Mobile No"
                                    SortExpression="MobileNo" />
                                <%--        <asp:BoundField DataField="Address" HeaderText="Address"
                                    SortExpression="Address" />--%>
                                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                                <asp:BoundField DataField="NIC" HeaderText="NIC" SortExpression="NIC" />
                                <%--  <asp:BoundField DataField="RecruitementDate" HeaderText="RecruitementDate"
                                    SortExpression="RecruitementDate" />--%>
                            </Columns>
                            <%--   <HeaderStyle CssClass="GridViewHeader" />--%>
                            <AlternatingRowStyle CssClass="altrowstyle" />
                        </asp:GridView>
                    </div>
                    <asp:ObjectDataSource ID="ObjectDataSourceSearchGrid" runat="server"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
                        TypeName="CommenDataSetTableAdapters.GetEmployeesForSearchTableAdapter">
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
                            <asp:Parameter DefaultValue="Common" Name="ApplicationCode" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">
                    <asp:HiddenField ID="hfSupplierId" runat="server" />
                    <asp:HiddenField ID="hfSupplireCode" runat="server" />
                    <table class="style5">
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td style="text-align: right">&nbsp;</td>
                        </tr>
                    </table>
                </td>
                <td>&nbsp;</td>
            </tr>
        </table>
    </ContentTemplate>
    <Triggers>
        <asp:PostBackTrigger ControlID="gvSearch" />
    </Triggers>
</asp:UpdatePanel>
