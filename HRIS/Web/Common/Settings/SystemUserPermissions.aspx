<%@ Page Title="HRM Common | System User Permissions" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="SystemUserPermissions.aspx.cs" Inherits="Settings_SystemUserForms" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table>
                <tr>
                    <td>
                        User
                    </td>
                    <td>
                        <dx:ASPxComboBox ID="txtUserRole" Height="30px" ValueField="UserId" TextField="UserName" BackColor="#F5F5F5" Width="259px" runat="server"  DropDownStyle="DropDownList"  DataSourceID="objdsUser"
                                    ValueType="System.String" TextFormatString="{0}" NullText=" << Select UserType >> " MaxLength="50" AutoPostBack="true" OnSelectedIndexChanged="txtUserRole_SelectedIndexChanged" >
                                           <ValidationSettings Display="Dynamic" ErrorDisplayMode="ImageWithTooltip" ErrorTextPosition="Right">
                                                        <RequiredField IsRequired="true" ErrorText="User Type is required" />
                                                    </ValidationSettings>
                                                    <InvalidStyle BackColor="LightPink"/>
                                    </dx:ASPxComboBox>


                        <asp:ObjectDataSource ID="objdsUser" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetUser" TypeName="HRM.Common.BLL.MksSecurity">
                            <SelectParameters>
                                <asp:ControlParameter ControlID="hfCurrentUserType"  Name="UserTypeId" PropertyName="Value" Type="Int32" />
                                <asp:Parameter DefaultValue="0" Name="UserId" Type="String" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                          <asp:HiddenField ID="hfCurrentUserType" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Permission Type
                    </td>
                    <td>
               <div class="text"> <dx:ASPxRadioButtonList ID="RbListPermission" AutoPostBack="true" runat="server" ValueType="System.String" RepeatColumns="6" RepeatDirection="Horizontal" Theme="Office2003Blue" OnSelectedIndexChanged="RbListPermission_SelectedIndexChanged" ></dx:ASPxRadioButtonList> 
     
                          <asp:HiddenField ID="HiddenField1" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Application
                    </td>
                    <td>
                     <div class="text"> <dx:ASPxRadioButtonList ID="chApplication" AutoPostBack="true" runat="server" ValueType="System.String" RepeatColumns="6" RepeatDirection="Horizontal" Theme="Office2003Blue" OnSelectedIndexChanged="chApplication_SelectedIndexChanged" >
                       
                     </dx:ASPxRadioButtonList>
                    </td>
                </tr>
                <tr>
                              <td>
                        Menu
                    </td>
                    <td>
 <div class="text"> <dx:ASPxRadioButtonList ID="chMenu" runat="server"  AutoPostBack="true" ValueType="System.String" RepeatColumns="6" RepeatDirection="Horizontal" Theme="Office2003Blue" OnSelectedIndexChanged="chMenu_SelectedIndexChanged">
      <Items>
                            <dx:ListEditItem Value="0" Text="Selet" />
                        </Items>
                    </dx:ASPxRadioButtonList> 
                               
                    </td>
                       </tr>
                <tr>
                              <td>
                        Forms
                    </td>
                    <td>
 <div class="text"> <dx:ASPxCheckBoxList ID="rdForms" runat="server"  AutoPostBack="true" ValueType="System.String" RepeatColumns="6" RepeatDirection="Horizontal" Theme="Office2003Blue" >
          <Items>
                            <dx:ListEditItem Value="0" Text="Selet" />
                        </Items>

                    </dx:ASPxCheckBoxList> 
                               
                    </td>
                       </tr>

                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <telerik:RadButton ID="radbtnSave" runat="server" OnClick="radbtnSave_Click" Skin="WebBlue"
                            Text="Save">
                        </telerik:RadButton>
                        &nbsp;&nbsp;
                        <telerik:RadButton ID="radbtnCancel" runat="server" OnClick="radbtnCancel_Click"
                            Skin="WebBlue" Text="Cancel">
                        </telerik:RadButton>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                    </td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
