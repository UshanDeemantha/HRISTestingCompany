<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DesignationStructureControl.ascx.cs" Inherits="HR_Designation_DesignationStructureControl" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<telerik:RadTreeView ID="tvDesignationStuctureUserControl" runat="server" onnodeclick="tvDesignationStuctureUserControl_NodeClick" onnodecreated="tvDesignationStuctureUserControl_NodeCreated" Skin="Office2007" />

<asp:HiddenField ID="hfDesignationId" runat="server" />
<asp:HiddenField ID="hfDesignationName" runat="server" />
