<%@ Page Title="HRM Common | Change User Password" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="ChangePassword.aspx.cs" Inherits="Settings_ChangePassword" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style16 {
            width: 150px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <table style="width: 100%">
        <tr>
            <td>
                <asp:Label ID="Label17" runat="server" Height="30" Text="Change Password" Font-Names="Tahoma" Font-Bold="true" Font-Underline="true" ForeColor="#336699" Font-Size="Small"></asp:Label>
            </td>
            <td></td>
        </tr>
    </table>
    <table style="width: 40%;">
        <tr>
            <td class="style16">
                <asp:Label ID="lblUserName" runat="server" Text="User Name"></asp:Label>
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtUserName" runat="server" Skin="Windows7"
                    ReadOnly="True" Width="225px">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server"
                    ControlToValidate="radtxtUserName" ErrorMessage="RequiredFieldValidator"
                    ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Label ID="Label2" runat="server" Text="Old Password"></asp:Label>
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtPassword" runat="server" Skin="Windows7"
                    TextMode="Password" Width="225px">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server"
                    ControlToValidate="radtxtPassword" ErrorMessage="RequiredFieldValidator"
                    ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style16">
                <asp:Label ID="Label3" runat="server" Text="New Password"></asp:Label>
            </td>
            <td>
                <telerik:RadTextBox ID="radtxtNewPassword" runat="server" Skin="Windows7"
                    TextMode="Password" Width="225px">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server"
                    ControlToValidate="radtxtNewPassword" ErrorMessage="RequiredFieldValidator"
                    ForeColor="Red" ValidationGroup="s">*</asp:RequiredFieldValidator>
            </td>
        </tr>
        <tr>
            <td class="style16">Conform New Password</td>
            <td>
                <telerik:RadTextBox ID="radtxtConformNewPassword" runat="server"
                    Skin="Windows7" TextMode="Password" Width="225px">
                </telerik:RadTextBox>
                <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server"
                    ControlToValidate="radtxtConformNewPassword"
                    ErrorMessage="RequiredFieldValidator" ForeColor="Red" SetFocusOnError="True"
                    ValidationGroup="s">*</asp:RequiredFieldValidator>
                <asp:CompareValidator ID="CompareValidator1" runat="server"
                    ControlToCompare="radtxtNewPassword"
                    ControlToValidate="radtxtConformNewPassword" ErrorMessage="CompareValidator"
                    ForeColor="Red" SetFocusOnError="True" ValidationGroup="s">Password does not match!</asp:CompareValidator>
            </td>
        </tr>
        <asp:ScriptManager ID="ScriptManager1" runat="server">
        </asp:ScriptManager>
        <tr>
            <td>&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblResult" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr>
            <td class="style16">&nbsp;&nbsp;&nbsp;
            <telerik:RadButton ID="radbtnSave" runat="server" Text="Save"
                OnClick="radbtnSave_Click" Skin="WebBlue" ValidationGroup="s">
            </telerik:RadButton>
                <cc1:ConfirmButtonExtender ID="radbtnSave_ConfirmButtonExtender" runat="server"
                    ConfirmText="Change password for selected user?" Enabled="True" TargetControlID="radbtnSave">
                </cc1:ConfirmButtonExtender>
                &nbsp;
            <telerik:RadButton ID="radbtnCancel" runat="server" Text="Cancel"
                OnClick="radbtnCancel_Click" Skin="WebBlue">
            </telerik:RadButton>
            </td>
        </tr>

    </table>
</asp:Content>

