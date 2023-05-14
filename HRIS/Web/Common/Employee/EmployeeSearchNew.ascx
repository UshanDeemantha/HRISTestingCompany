<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSearchNew.ascx.cs" Inherits="Common_Employee_EmployeeSearchNew" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>

<style type="text/css">
/*    .style1 {
        width: 800px;
    }
*/
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

    .auto-style2 {
        width: 700px;
        text-align: center;
    }
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>

        <headertemplate>
            <h6 style="font-family: Source Sans; font-size: 16px; font-weight: 600; color: white; background-color: #097cbd; height: 31px; margin-top: -19px; margin-left: -42px; margin-right: -48px; padding-left: 23px; padding-top: 3px">Search Employee</h6>
        </headertemplate>

               <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border mt-2" style="border-radius: 10px">
                                <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                                 <div class="row">

                         <dx:ASPxGridView ID="gvSearch"  runat ="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceSearchGrid"
                             ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="CustomerId" Theme="MetropolisBlue" Width="688px">
                        <SettingsPager PageSize="16">
                        </SettingsPager>
                             <SettingsSearchPanel Visible="true" HighlightResults="true" />
                        <Columns>
                            <dx:GridViewDataTextColumn FieldName="EmployeeID" Caption="EmployeeID" VisibleIndex="1" Visible="false" Width="100">
                                <CellStyle HorizontalAlign="Center" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee Code" VisibleIndex="1" Width="100">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FirstName" Caption="First Name" VisibleIndex="2" Width="150">
                                 <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="LastName" Caption="Last Name" VisibleIndex="3" Width="150">
                                  <CellStyle HorizontalAlign="Left" />
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No" VisibleIndex="4" Width="100">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" VisibleIndex="5" Width="120" Visible="false">
                            </dx:GridViewDataTextColumn>
                               <dx:GridViewDataTextColumn FieldName="NIC" Caption="NIC"  VisibleIndex="6" Width="120">
                            </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn Caption="Select" VisibleIndex="0" Width="80" >
                                <CellStyle HorizontalAlign="Center" />
                                <DataItemTemplate>
                                    <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                                </DataItemTemplate>
                            </dx:GridViewDataTextColumn>
                         

                        </Columns>
                             <SettingsPager Mode="ShowPager" PageSize="10"/>
                            <Styles>
                               <Header BackColor="WindowFrame" Font-Bold="true" HorizontalAlign="Center">
                               </Header>
                                  <AlternatingRow BackColor="#F5F5F5">
                                  </AlternatingRow>
                            </Styles>
                    </dx:ASPxGridView>
                       
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
                                    </div>
                                </div>
                            </div>
                   </div>
       
        <table>
       <%--     <tr style="background-color:red">
                <td>&nbsp;</td>
                <td>Search Employee</td>
                <td>&nbsp;</td>
            </tr>--%>
            <tr>
                <td>&nbsp;</td>
                <td class="auto-style2">

                    <table class="style4">
                        <tr>
                            <td>
                                <telerik:RadTextBox ID="tbSearchSupplier" runat="server" CssClass="textBox"
                                    EmptyMessage="Type Search Criteria..." Skin="Windows7" Width="125px" Visible="false" />
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlSearchCollom" runat="server" ZIndex="1000000"
                                    Skin="Windows7" Visible="false">
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                <telerik:RadComboBox ID="ddlContentLocation" runat="server" ZIndex="1000000"
                                    Skin="Windows7" Visible="false">
                                    <Items>
                                        <telerik:RadComboBoxItem runat="server" Text="Start with" Value="1" />
                                        <telerik:RadComboBoxItem runat="server" Text="Contain" Value="2" />
                                        <telerik:RadComboBoxItem runat="server" Text="End with" Value="3" />
                                    </Items>
                                </telerik:RadComboBox>
                            </td>
                            <td>
                                    <asp:ImageButton ID="btnSearchSupplier" runat="server" Visible="false"
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
