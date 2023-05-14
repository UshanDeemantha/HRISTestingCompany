<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeSearch.ascx.cs" Inherits="Employee_EmployeeSearch" %>

 <link rel="stylesheet" type="text/css" href="../../../App_Themes/CommonResources/style.css" />
 <%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<style type="text/css">

        .style2
        {
            width: 96px;
        }
        .style5
     {
         width: 1073px;
        text-align: center;
    }
        .style3
        {
            width: 627px;
         text-align: center;
     }
        .style6
    {
        width: 795px;
    }
    .style7
    {
        width: 795px;
        text-align: center;
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
        background-color: #938ede;
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
        border: solid 1px #525252;
        border-collapse: collapse;
    }

        .mGrid td {
            padding: 2px;
            /*border: solid 1px #c1c1c1;*/
            /*color: #717171;*/
        }

        .mGrid th {
            padding: 4px 2px;
            color: #fff;
            background: #2e79a0 url(grd_head.png) repeat-x top;
            border-left: solid 1px #525252;
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
                border-left: solid 1px #666;
                font-weight: bold;
                color: #fff;
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
        background-color: #70baff;
        text-decoration: none;
        cursor: pointer;
    }
     </style>
       <asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
<table>
    <tr>
        <td class="wizard-hed">
            Search Employee</td>
    </tr>
    <tr>
        <td>
  
            <table>
                <tr>
                    <td>
                        <telerik:RadTextBox ID="tbSearchSupplier" runat="server" 
                            EmptyMessage="Search Criteria..." Skin="Windows7" Width="125px" />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlSearchCollom" runat="server" ZIndex="1000000" />
                    </td>
                    <td>
                        <telerik:RadComboBox ID="ddlContentLocation" runat="server" ZIndex="1000000">
                        <Items>
                            <telerik:RadComboBoxItem runat="server" Text="Start with" Value="1" />
                            <telerik:RadComboBoxItem runat="server" Text="Contain" Value="2" />
                            <telerik:RadComboBoxItem runat="server" Text="End with" Value="3" />
                        </Items>
                        </telerik:RadComboBox>
                    </td>
                    <td>
                        <asp:ImageButton ID="btnSearchSupplier" runat="server" 
                            ImageUrl="~/App_Themes/CommonResources/search.png" onclick="btnSearchSupplier_Click" 
                            style="width: 10px; height: 10px;" />
                    </td>
                </tr>
            </table>
           
         
        </td>
    </tr>
    <tr>
        <td class="style6" >
            <div style="overflow: scroll; width: 780px; height:450px">
                <asp:GridView ID="gvSearch" runat="server" AllowPaging="True" 
                    AutoGenerateColumns="False" CssClass="mGrid"
                    PageSize="15" DataSourceID="ObjectDataSourceSearchGrid" 
                    onrowcommand="gvSearch_RowCommand" ondatabound="gvSearch_DataBound" 
                    onselectedindexchanged="gvSearch_SelectedIndexChanged">
  <%--                  <RowStyle CssClass="GridViewRowStyle" />--%>
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
                        <asp:BoundField DataField="FirstName" ItemStyle-Width="150px" HeaderText="First Name" 
                            SortExpression="FirstName" />
                        <asp:BoundField DataField="LastName" ItemStyle-Width="150px" HeaderText="Last Name" 
                            SortExpression="LastName" />
                        <asp:BoundField DataField="HomeContactNo" HeaderText="Home No" 
                            SortExpression="HomeContactNo" />
                        <asp:BoundField DataField="MobileNo" HeaderText="Mobile No" 
                            SortExpression="MobileNo" />
                   
                        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" />
                        <asp:BoundField DataField="NIC" HeaderText="NIC" SortExpression="NIC" />
                      
                    </Columns>
              <%--      <HeaderStyle CssClass="GridViewHeader" />--%>
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
                    <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
                    <asp:SessionParameter Name="CompanyID" SessionField="CompanyId" Type="Int32" />
                    <asp:Parameter DefaultValue="Common" Name="ApplicationCode" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
        </td>
    </tr>
    <tr>
        <td class="style7">
            <asp:HiddenField ID="hfSupplierId" runat="server" />
            <asp:HiddenField ID="hfSupplireCode" runat="server" />
        </td>
    </tr>
</table>
 </ContentTemplate>
           <Triggers> 
               <asp:PostBackTrigger ControlID="gvSearch" />
            </Triggers> 
            </asp:UpdatePanel>
