<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrganizationStucture.ascx.cs"
    Inherits="Organization_OrganizationStuctureCom" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadTreeView ID="tvOrganizationStucture" runat="server" 
    Skin="Office2007" onnodeclick="tvOrganizationStucture_NodeClick" 
    onnodecreated="tvOrganizationStucture_NodeCreated">
</telerik:RadTreeView>
<asp:HiddenField ID="hfOrganizationStucture" runat="server" />
<asp:HiddenField ID="hfOrganizationTypeName" runat="server" />
<asp:HiddenField ID="hfCompanyIdNew"  runat="server" />
