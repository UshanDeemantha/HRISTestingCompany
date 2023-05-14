<%@ Page Title="HRM Common | System Exception" Language="C#" MasterPageFile="~/EmptyTemplateMasterPage.master" AutoEventWireup="true" CodeFile="SystemException.aspx.cs" Inherits="SystemException" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .infoBox {
            font-size: 13px;
            font-style: italic;
            border-radius: 15px;
            -moz-border-radius: 15px;
            margin: 20px 0;
            border-width: 2px;
            border-color: Red;
            border-style: solid;
            width: 100%;
        }

        .goButtonClassHov .rbText {
            color: White !important;
        }

        .infoInnerMargin {
            margin-top: 10px;
            margin-bottom: 10px;
            margin-right: 20px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center">
                <asp:Image runat="server" ImageUrl="~/App_Themes/CommonResources/Oops.png" />
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td style="font-size: 16px; color:red; font-family:'Century Gothic'; font-size:16px;font-weight:600" align="center">There seems to be an issue with the last action you've taken. 
            </td>
        </tr>
        <tr>
            <td style="font-size: 16px; color:red; font-family:'Century Gothic'; font-size:16px;font-weight:600" align="center">Retry the steps taken & if this page continues to show please report the issue to application vendor.               
            </td>
        </tr>

        <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
         <tr>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td align="center" colspan="2">
                <dx:ASPxButton ID="radbtnGoBack" runat="server" Text="Go Back" OnClick="radbtnGoBack_Click" Width="100px">
                </dx:ASPxButton>
                <%--      <telerik:RadButton EnableImageButton="true" ID="radbtnGoBack" runat="server" 
                    Height="65" Skin="Office2010Black" Text="Go Back" HoveredCssClass="goButtonClassHov" 
                    Style="padding-left: 10px" Enabled="False" onclick="radbtnGoBack_Click">
                    <Icon PrimaryIconUrl="App_Themes/CommonResources/RibbonMenu/RibbonMenu.Back.png" PrimaryIconWidth="32px" PrimaryIconHeight="50px"
                            PrimaryIconTop="0px" />
                </telerik:RadButton>--%>

                <dx:ASPxButton ID="radbtnGoToDashboard" runat="server" Text="Go Main Menu" OnClick="radbtnGoToDashboard_Click" Width="100px">
                </dx:ASPxButton>

                <%--<telerik:RadButton EnableImageButton="true" ID="radbtnGoToDashboard" runat="server" 
                    Height="65" Skin="Office2010Black" Text="Go to Main Menu" 
                    HoveredCssClass="goButtonClassHov" Style="padding-left: 10px" 
                    onclick="radbtnGoToDashboard_Click" >
                    <Icon PrimaryIconUrl="App_Themes/CommonResources/RibbonMenu/RibbonMenu.Dashboard.png" PrimaryIconWidth="32px" PrimaryIconHeight="50px"
                            PrimaryIconTop="0px" />
                </telerik:RadButton>   --%>             
            </td>
        </tr>

    </table>
    <table style="width: 100%;">
        <tr>
            <td>
                <dx:ASPxButton ID="radbtnViewErrors" runat="server" Text="View error details..." OnClick="radbtnViewErrors_Click">
                </dx:ASPxButton>
                <%--<telerik:RadButton ID="radbtnViewErrors" runat="server" 
                    Text="View error details..." Skin="Metro" 
                    onclick="radbtnViewErrors_Click" Visible="False">
                </telerik:RadButton>--%>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Panel ID="errorPanel" runat="server" Visible="false" CssClass="infoBox">
                    <table style="width: 100%;" class="infoInnerMargin">

                        <tr>
                            <td valign="top">
                                <asp:Label runat="server" ForeColor="Red" Text="Error Message: " />
                            </td>
                            <td>
                                <asp:Label ID="lblError" ForeColor="Red" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label runat="server" ForeColor="Red" Text="Error Description: " />
                            </td>
                            <td>
                                <asp:Label ID="lblErrorDesc" ForeColor="Red" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label runat="server" ForeColor="Red" Text="Stack Trace: " />
                            </td>
                            <td>
                                <asp:Label ID="lblErrorStack" ForeColor="Red" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Label runat="server" ForeColor="Red" Text="Error Source: " />
                            </td>
                            <td>
                                <asp:Label ID="lblErrorSource" runat="server" ForeColor="Red" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
