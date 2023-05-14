<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HRM_News.ascx.cs" Inherits="CommenUserControls_HRM_News" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="cc1" %>

<style type="text/css">

.eventbox{ border: #666666 solid 1px; margin:15px 0 0 0; border-radius: 5px; -moz-border-radius: 5px; -moz-box-shadow: 2px 2px 2px #888; -webkit-box-shadow: 2px 2px 2px #888; box-shadow: 2px 2px 2px #888; font-family: Arial, Helvetica, sans-serif; font-size: 11px; color: #000000;}
.eventboxHed{ background: url(../../App_Themes/Common/wizard-hed.jpg) repeat-x; height: 20px; padding: 5px 0; font-size: 12px; font-weight: bold; color: #333333}
.eventboxleft{ border-left: #dedede solid 1px; padding: 0 0 0 10px;}
.eventbox h2{ font-size: 15px; margin: 10px 0; padding: 0; color: #006699;}
.eventbox strong{ font-weight: bold;}
.eventbox img{ width: 80px; margin: 5px;}


</style>



<telerik:RadAjaxPanel ID="RadAjaxPanel1" runat="server" height="250px" 
    width="460px">
    <telerik:RadRotator ID="RadRotator1" DataSourceID="SqlDataSource1"
        runat="server" Width="440px" ItemWidth="400px" 
    ItemHeight="350px" FrameDuration="5000" ScrollDirection="Up" Height="230px">
        <ItemTemplate>
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="eventbox">
                <tr>
                    <td width="26%">
                        &nbsp;</td>
                    <td width="74%">
                        &nbsp;</td>
                </tr>
                <tr>
                    <td align="center" valign="middle">
                        <img alt="News Event Image" src="../../App_Themes/Common/sport_temp.png" />
                    </td>
                    <td align="left" valign="top">
                        <table width="100%" border="0" cellspacing="0" cellpadding="0" 
                            class="eventboxleft">
                            <tr>
                                <td width="4%" class="eventboxHed">
                                    &nbsp;</td>
                                <td width="96%" class="eventboxHed">
                                    EventType :
                                    <asp:Label ID="Label5" runat="server" Text='<%# Eval("EventTypeName") %>'></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <h2>
                                        <asp:Label ID="Label6" runat="server" Text='<%# Eval("Name") %>'></asp:Label>
                                    </h2>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <telerik:RadTicker ID="RadTicker1" runat="server" LineDuration="8000">
                                        <Items>
                                            <telerik:RadTickerItem Text='<%# Eval("Description")%>' />
                                        </Items>
                                    </telerik:RadTicker>
                                </td>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <table width="100%" border="0" cellspacing="0" 
        cellpadding="0">
                            <tr>
                                <td style="font-size:11px;">
                                    <strong>From :</strong>
                                    <br />
                                    <asp:Label ID="Label3" runat="server" Text='<%# Eval("FromDate") %>'></asp:Label>
                                </td>
                                <td>
                                    <strong>To :</strong>
                                    <br />
                                    <asp:Label ID="Label4" runat="server" Text='<%# Eval("ToDate") %>'></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
            </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    &nbsp;</td>
            </tr>
            </table>
        </ItemTemplate>
    </telerik:RadRotator>
</telerik:RadAjaxPanel>
<asp:SqlDataSource ID="SqlDataSource1" runat="server" 
    ConnectionString="<%$ ConnectionStrings:ConnectionString %>" 
    
    SelectCommand="SELECT Events.EventID, Events.EventTypeID, Events.Name, Events.Description, Events.ToDate, Events.FromDate, Events.EventDate, EventType.EventTypeID AS Expr1, EventType.EventTypeCode, EventType.EventTypeName, Events.Venue, Events.Image, Events.Active FROM Events INNER JOIN EventType ON Events.EventTypeID = EventType.EventTypeID">
</asp:SqlDataSource>





    






    