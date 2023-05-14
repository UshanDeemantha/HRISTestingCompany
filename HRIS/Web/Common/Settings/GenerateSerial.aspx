<%@ Page Title="
    
    
    
    HRM | Generate Serial" Language="C#" MasterPageFile="~/EmptyTemplateMasterPage.master"
    AutoEventWireup="true" CodeFile="GenerateSerial.aspx.cs" Inherits="Common_Settings_GenerateSerial" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%;">
        <tr>
            <td colspan="2">
                <h4>
                    Generate serial for application</h4>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Username" />
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtUsername" Width="200px" runat="server" Skin="Metro"
                    AutoCompleteType="Disabled" EnableViewState="False">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radtxtUsername"
                    Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="g">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Password" />
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtPassword" Width="200px" runat="server" Skin="Metro"
                    TextMode="Password" EnableViewState="False">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="radtxtPassword"
                    Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="g">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label runat="server" Text="Verification Code" />
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtVerificationCode" Width="200px" runat="server" Skin="Metro"
                    AutoCompleteType="Disabled" EnableViewState="False">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="radtxtVerificationCode"
                    Display="Dynamic" ErrorMessage="*" ForeColor="Red" ValidationGroup="g">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <asp:Label ID="lblMessage" runat="server" />
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td>
                <telerik:RadButton ID="radbtnGenerateSerial" runat="server" Skin="Metro" Text="Generate Serial Number"
                    OnClick="radbtnGenerateSerial_Click" ValidationGroup="g">
                </telerik:RadButton>
                &nbsp;
                <telerik:RadButton ID="radbtnCancel" runat="server" Skin="Metro" Text="Cancel"
                    OnClick="radbtnCancel_Click">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td colspan="2" align="center">
                <telerik:RadTextBox ID="radtxtSerial" runat="server" EmptyMessage="XXXXX" Font-Size="25px"
                    Skin="Metro" Width="800px" MaxLength="5" ReadOnly="True">
                    <EnabledStyle HorizontalAlign="Center" />
                </telerik:RadTextBox>
            </td>
        </tr>
        <tr>
            <td>
                &nbsp;
            </td>
            <td align="right" style="padding: 5px 15px 0px 0px;">
                <telerik:RadButton ID="radbtnRegister" runat="server" Skin="Metro" Text="Register Now!"
                    OnClick="radbtnRegister_Click" Visible="False">
                </telerik:RadButton>
            </td>
        </tr>
        <tr>
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
        </tr>
        <tr>
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
        </tr>
        <tr>
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
