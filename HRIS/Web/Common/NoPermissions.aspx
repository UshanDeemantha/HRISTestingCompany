<%@ Page Title="HRM Common | User Permission Denied" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="NoPermissions.aspx.cs" Inherits="NoPermissions" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
        <table style="height:400px;width:100%;">
        
        <tr>
            <td>
                <asp:Image ID="Image1" runat="server" 
                    ImageUrl="~/App_Themes/CommonResources/NoPermissions.png" />
            </td>
            <td colspan="2">
                <asp:Label ID="Label1" runat="server"  Font-Size="16px"
                    Text="Page\function you are trying to access is denied due to current logged in user not having permissions on page requested. Please contact system administrator for further details on this message!" 
                    ForeColor="Red" />
            </td>
        </tr>
        
    </table>
</asp:Content>

