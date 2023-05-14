<%@ Page Title="HRIS Common | Designation View" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true"
    CodeFile="DesignationView.aspx.cs" Inherits="Designation_DesignationView" %>

<%@ Register Src="DesignationStructureControl.ascx" TagName="DesignationStructureControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td>
                <h4>Designation View</h4>
            </td>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <uc1:DesignationStructureControl ID="DesignationStructureControl1" runat="server" />
                &nbsp;
            </td>
            <td>
                &nbsp;
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
</asp:Content>
