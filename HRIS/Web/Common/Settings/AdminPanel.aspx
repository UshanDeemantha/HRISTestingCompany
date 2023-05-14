<%@ Page Title="" Language="C#" AutoEventWireup="true" CodeFile="AdminPanel.aspx.cs" Inherits="Common_Settings_AdminPanel" MasterPageFile="~/Common/Settings/NewMenu.master"%>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register src="UserControls/SystemApplications.ascx" tagname="SystemApplications" tagprefix="uc1" %>
<%@ Register src="UserControls/SystemMenu.ascx" tagname="SystemMenu" tagprefix="uc2" %>
<%@ Register src="UserControls/SystemForms.ascx" tagname="SystemForms" tagprefix="uc3" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style>
        .loginmain
        {
            margin: auto;
            width: 463px;
        }
        .login
        {
            width: 463px;
            height: auto;
            margin: 120px auto;
            border-radius: 15px;
            -moz-border-radius: 15px;
            border-radius: 15px;
            border: #ececec 2px solid;
            -moz-box-shadow: 10px 10px 5px #888;
            -webkit-box-shadow: 10px 10px 5px #888;
            box-shadow: 10px 10px 5px #888;
            float: left;
        }
        .logcontent
        {
            width: 300px;
            float: left;
            height: auto;
            margin: 100px 0 0 10px;
        }
        .logStr
        {
            width: 400px;
            float: left;
            padding: 10px 0 10px 50px;
            color: #CCCCCC;
        }
        .logHed
        {
            width: 400px;
            float: left;
            padding: 10px 0 10px 55px;
            color: #0099CC;
            font-size: 13px;
            font-weight: bold;
        }
        .logStr .loghed
        {
            width: 80px;
            float: left;
            font-weight: bold;
            color: #000000;
            padding: 0 0 0 10px;
        }
        .logStr .loginput
        {
            width: 200px;
            float: left;
        }
        .logStr .loginput input
        {
            width: 180px;
            height: 18px;
            border: #CCCCCC solid 1px;
        }
        .goButtonClassHov .rbText
        {
            color: White !important;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
     <%-- <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>--%>
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
        <div class="overflow-auto" style="height: calc(100vh - 110px)">
       <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row rm-margin">
                <div class="col head-container">
                    <h4 class="header">Company Profile</h4>
                    <span onclick="toggleProfileForm(585);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
           
            <telerik:RadPanelBar runat="server" ID="RadPanelBar1" 
        ExpandMode="SingleExpandedItem" Width="100%" Skin="Office2007">
                <Items>
                    <telerik:RadPanelItem Expanded="True" Text="Step 1: Admin Login" 
                        runat="server" Selected="true">
                        <Items>
                            <telerik:RadPanelItem Value="AdminLogin" runat="server">
                                <ItemTemplate>
                                    <div class="loginmain">
                                        <div class="login">
                                            <div class="logHed">
                                                Admin login</div>
                                            <div class="logStr">
                                                <div class="loghed">
                                                    User Name</div>
                                                <div class="loginput">
                                                    <telerik:RadTextBox ID="radtxtUsername" runat="server" 
                                                        EmptyMessage="Enter Username to Login..." Skin="Windows7" Width="200px" />
                                                </div>
                                            </div>
                                            <div class="logStr">
                                                <div class="loghed">
                                                    Password</div>
                                                <div class="loginput">
                                                    <telerik:RadTextBox ID="radtxtPassword" runat="server" Skin="Windows7" 
                                                        TextMode="Password" Width="200px" />
                                                </div>
                                                &nbsp;</div>
                                            <div class="logStr">
                                                <div class="loghed">
                                                    &nbsp;</div>
                                                <div class="loginput">
                                                    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                                                </div>
                                                <div class="logStr">
                                                    <div class="loghed">
                                                        &nbsp;</div>
                                                    <div class="loginput">
                                                        <asp:ImageButton ID="imgbtnLogin" runat="server" BorderStyle="None" 
                                                            Height="24px" ImageUrl="~/App_Themes/Common/login.png" 
                                                            OnClick="imgbtnLogin_Click" Width="77px" />
                                                        &nbsp;&nbsp;&nbsp;
                                                        <asp:ImageButton ID="imgbtnCancel" runat="server" BorderStyle="None" 
                                                            Height="24px" ImageUrl="~/App_Themes/Common/cancel.png" 
                                                            onclick="imgbtnCancel_Click" Width="77px" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Enabled="False" Text="Step 2: System Applications" 
                        runat="server">
                        <Items>
                            <telerik:RadPanelItem Value="NewsletterOptions" runat="server">
                                    <ItemTemplate>
                                        <table class="style1">
                                            <tr>
                                                <td>
                                                    <telerik:RadButton ID="RadButton2" runat="server" ForeColor="Black" 
                                                        Height="64px" HoveredCssClass="goButtonClassHov" Text="Previous!" 
                                                        Width="64px" onclick="RadButton2_Click">
                                                        <Image ImageUrl="../../App_Themes/Common/Pervious_btn.png" 
                                                            IsBackgroundImage="true" />
                                                    </telerik:RadButton>
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    <telerik:RadButton ID="btnBgrImg2" runat="server" ForeColor="Black" 
                                                        Height="64px" HoveredCssClass="goButtonClassHov" Text="Next!" Width="64px" 
                                                        onclick="btnBgrImg2_Click">
                                                        <Image ImageUrl="../../App_Themes/Common/Next_btn.png" 
                                                            IsBackgroundImage="true" />
                                                    </telerik:RadButton>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    &nbsp;</td>
                                                <td>
                                                    <uc1:SystemApplications ID="SystemApplications1" runat="server" />
                                                </td>
                                                <td>
                                                    &nbsp;</td>
                                            </tr>
                                        </table>
                                    </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Enabled="False" Text="Step 3: System Menu" 
                        runat="server">
                        <Items>
                            <telerik:RadPanelItem Value="Register" runat="server">
                                <ItemTemplate>
                                   
                                    <table class="style1">
                                        <tr>
                                            <td>
                                             <telerik:RadButton ID="RadButton3" runat="server" ForeColor="Black" 
                                                        Height="64px" HoveredCssClass="goButtonClassHov" Text="Previous!" 
                                                    Width="64px" onclick="RadButton2_Click">
                                                        <Image ImageUrl="../../App_Themes/Common/Pervious_btn.png" 
                                                            IsBackgroundImage="true" />
                                                    </telerik:RadButton>
                                             </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <telerik:RadButton ID="btnBgrImg3" runat="server" ForeColor="Black" 
                                                        Height="64px" HoveredCssClass="goButtonClassHov" Text="Next!" 
                                                    Width="64px" onclick="btnBgrImg2_Click2">
                                                        <Image ImageUrl="../../App_Themes/Common/Next_btn.png" 
                                                            IsBackgroundImage="true" />
                                                    </telerik:RadButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <uc2:SystemMenu ID="SystemMenu1" runat="server" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                   
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                    <telerik:RadPanelItem Enabled="False" Text="Step 4: Sytem Forms" 
                        runat="server">
                        <Items>
                            <telerik:RadPanelItem Value="Register" runat="server">
                                <ItemTemplate>
                                   
                                    <table class="style1">
                                        <tr>
                                            <td>
                                             <telerik:RadButton ID="RadButton4" runat="server" ForeColor="Black" 
                                                        Height="64px" HoveredCssClass="goButtonClassHov" Text="Previous!" 
                                                    Width="64px" onclick="RadButton2_Click">
                                                        <Image ImageUrl="../../App_Themes/Common/Pervious_btn.png" 
                                                            IsBackgroundImage="true" />
                                                    </telerik:RadButton>
                                             </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                <uc3:SystemForms ID="SystemForms1" runat="server" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                   
                                </ItemTemplate>
                            </telerik:RadPanelItem>
                        </Items>
                    </telerik:RadPanelItem>
                </Items>
                <CollapseAnimation Duration="100" Type="None" />
                <ExpandAnimation Duration="100" Type="None" />
            </telerik:RadPanelBar>
    <telerik:RadButton ID="RadButton1" runat="server" Text="Back to System Login" 
        onclick="RadButton1_Click" Skin="WebBlue">
    </telerik:RadButton>
            </div>
            </div>
</asp:Content>

