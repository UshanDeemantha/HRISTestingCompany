<%@ Page Title="HRM | Please Register Your Application" Language="C#" MasterPageFile="~/EmptyTemplateMasterPage.master"
    AutoEventWireup="true" CodeFile="EnterRegisrationInformation.aspx.cs" Inherits="Common_Settings_EnterRegisrationInformation" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td style="width: 64px;">
                <img alt="" src="../../App_Themes/Common/RegisterHRM.png" />
            </td>
            <td colspan="2" valign="top">
                <div style="vertical-align: top;">
                </div>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <telerik:RadTextBox ID="radtxtSerialP1" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="100px" MaxLength="5" OnTextChanged="radtxtSerialP1_TextChanged"
                    AutoCompleteType="Disabled">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
                &nbsp;-
                <telerik:RadTextBox ID="radtxtSerialP2" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="100px" MaxLength="5" AutoCompleteType="Disabled">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
                &nbsp;-
                <telerik:RadTextBox ID="radtxtSerialP3" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="100px" MaxLength="5" AutoCompleteType="Disabled">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
                &nbsp;-
                <telerik:RadTextBox ID="radtxtSerialP4" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="100px" MaxLength="5" AutoCompleteType="Disabled">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
                &nbsp;-
                <telerik:RadTextBox ID="radtxtSerialP5" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="100px" MaxLength="5" AutoCompleteType="Disabled">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
                &nbsp;-
                <telerik:RadTextBox ID="radtxtSerialP6" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="100px" MaxLength="5" AutoCompleteType="Disabled">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
                &nbsp;-
                <telerik:RadTextBox ID="radtxtSerialP7" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="100px" MaxLength="5" AutoCompleteType="Disabled">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="3" align="center">
                <div style="width: 800px;">
                    <table style="width: 100%;">
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblMessage" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <telerik:RadButton ID="radtxtValidateAndSave" runat="server" Skin="Windows7" Text="Register Now"
                                    OnClick="radtxtValidateAndSave_Click">
                                </telerik:RadButton>
                                &nbsp;
                                <telerik:RadButton ID="radtxtCancel" runat="server" Skin="Windows7" Text="Go to Login"
                                    OnClick="radtxtCancel_Click">
                                </telerik:RadButton>
                            </td>
                        </tr>
                    </table>
                </div>
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
