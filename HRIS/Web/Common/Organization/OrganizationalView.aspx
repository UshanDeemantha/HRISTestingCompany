<%@ Page Title="" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="OrganizationalView.aspx.cs" Inherits="Organization_OrganizationalView" %>

<%@ Register src="OrganizationStucture.ascx" tagname="OrganizationStucture" tagprefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
<table >

<tr>
<td ><h4>Organizational View</h4> </td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
<tr>
<td >&nbsp;</td>
<td><uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
    </td>
<td>&nbsp;</td>
</tr>
<tr>
<td >&nbsp;</td>
<td>&nbsp;</td>
<td>&nbsp;</td>
</tr>
</table>
</asp:Content>

