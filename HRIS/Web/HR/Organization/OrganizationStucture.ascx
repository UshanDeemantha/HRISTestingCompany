<%@ Control Language="C#" AutoEventWireup="true" CodeFile="OrganizationStucture.ascx.cs" Inherits="HR_Organization_OrganizationStucture" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<div>

<telerik:RadTreeView ID="tvOrganizationStucture" runat="server" 
    Skin="Office2007" onnodeclick="tvOrganizationStucture_NodeClick" 
    onnodecreated="tvOrganizationStucture_NodeCreated">
</telerik:RadTreeView>
        <script type="text/javascript" src="/App_Themes/NewTheme/script/organization-structure.js">

        </script>
    <script type="text/javascript">
        setOrganizationStructure("");
    </script>

    </div>

<asp:HiddenField ID="hfOrganizationStucture" runat="server" />
<asp:HiddenField ID="hfOrganizationTypeName" runat="server" />