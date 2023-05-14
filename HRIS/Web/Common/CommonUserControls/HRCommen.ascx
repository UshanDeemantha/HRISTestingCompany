<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HRCommen.ascx.cs" Inherits="CommenUserControls_HRCommen" %>
<style type="text/css">
    img
    {
        border: none;
    }
</style>
<table style="text-align: center" width="100%">
    <tr>
        <td>
            <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/App_Themes/Common/Dashboard/Small/Employee.png"
                OnClick="ImageButton1_Click" />
        </td>
        <td>
            <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/App_Themes/Common/Dashboard/Small/Master-Setup.png"
                OnClick="ImageButton2_Click" />
        </td>
        <td>
            <asp:ImageButton ID="ImageButton3" runat="server" ImageUrl="~/App_Themes/Common/Dashboard/Small/Organization.png"
                OnClick="ImageButton3_Click" />
        </td>
        <td>
            <asp:ImageButton ID="ImageButton4" runat="server" ImageUrl="~/App_Themes/Common/Dashboard/Small/designation.png"
                OnClick="ImageButton4_Click" />
        </td>
    </tr>
    <tr>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
        <td>
            &nbsp;
        </td>
    </tr>
</table>
