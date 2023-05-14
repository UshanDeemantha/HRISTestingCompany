<%@ Page Language="C#" Title="HRM Common | User Permissions" AutoEventWireup="true"
    CodeFile="UserPermissionView.aspx.cs" MasterPageFile="~/Common/Common_MenuMasterPage.master" Inherits="Common_Settings_UserPermissionView" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        /*.percentage {
            height: 30px;
            border-style: solid;
            border-color: Highlight;
            margin-bottom: 2px;
            padding-top: 6px;
        }*/

        .mt-3, .my-3 {
            margin-top: 0.5rem !important;
            margin-right: -129px;
        }

        .pl-4, .px-4 {
            padding-left: 6.5rem !important;
        }

        .pl-5, .px-5 {
            padding-left: 38rem !important;
        }
        .ml-5, .mx-5 {
    margin-left: 6rem!important;
}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
   
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css">
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>

      <script type="text/javascript">
        var $T = Telerik.Web.UI;
        function OnLoad(sender, args) {
            // #region Add ComboBox reference to TextBox
            function getComboBoxReference() {
                var comboBox = $telerik.$(sender.get_element()).closest(".RadComboBox")[0].control;
                //add a reference of the containing combo box to the RadTextBox instance
                sender.__containingComboBox = comboBox;

                //add a reference of the RadTextBox instance to the containing combo box
                comboBox.__filterTextBox = sender;

                Sys.Application.remove_load(getComboBoxReference);
            }
            Sys.Application.add_load(getComboBoxReference);
            // #endregion

            $telerik.$(sender.get_element()).on("keyup", function (e) {
                sender.__containingComboBox.highlightAllMatches(sender.get_textBoxValue());
            })
        }

        function OnClientLoadComboBox(sender, args) {
            var originalFunction = $T.RadComboBox.prototype.highlightAllMatches;
            // override the highlightAllMatches only on this RadComboBox instance
            sender.highlightAllMatches = function (text) {
                this.set_filter($T.RadComboBoxFilter.Contains);
                originalFunction.call(this, text);
                this.set_filter($T.RadComboBoxFilter.None);
            };
        }
         
        function OnClientDropDownClosing(sender, args) {
            sender.get_items().forEach(function (item) {
                item.clearEmTags();
            });

            sender.setAllItemsVisible(true);
            sender.__filterTextBox.clear();
        }
    </script>
       <ContentTemplate>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">User Permissions</h4>
                    <span onclick="toggleProfileForm(265);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-2">
                                    <asp:Label ID="Label2" runat="server" CssClass="form-label" Text="User Name"></asp:Label>
                                    <dx:ASPxComboBox ID="txtUserRole" ValueField="UserId" TextField="UserName" Width="250px"
                                        CssClass="form-control form-control-lg" runat="server" DropDownStyle="DropDownList" DataSourceID="objdsUser"
                                        ValueType="System.String" TextFormatString="{0}" NullText="Please Select User Name" MaxLength="50" AutoPostBack="true"
                                        OnSelectedIndexChanged="txtUserRole_SelectedIndexChanged">
                                    </dx:ASPxComboBox>
                                </div>
                                  <div class="col-md-3 ml-5">
                                      <asp:Label ID="Label4" runat="server" Text="Application" CssClass="form-label"></asp:Label>
                                      <telerik:RadComboBox ID="RadComboBox2" runat="server" CheckBoxes="true" Visible="true" CheckedItemsTexts="DisplayAllInInput" OnClientLoad="OnClientLoadComboBox"
                                DataTextField="ApplicationName" DataValueField="ApplicationId" Width="100%">
                                <%--<HeaderTemplate>
                                    <telerik:RadTextBox runat="server" ID="RadTextBox1" Width="100%" ClientEvents-OnLoad="OnLoad">
                                    </telerik:RadTextBox>
                                </HeaderTemplate>--%>
                            </telerik:RadComboBox>
                                      </div>
                                 <div class="col-md-1" style="margin-top:27px">
                                       <div>

                                        <telerik:RadButton ID="btnAspx" runat="server" Text="Search" OnClick="btnAspx_Click">
                                        </telerik:RadButton>
                                    </div>
                                     </div>
                                 <div class="col-md-1">
                                     </div>
                            </div>
                             <div class="row mt-3">
                                <div class="col-md-6">
                                   
                                   <%-- <dx:ASPxCheckBoxList ID="chApplication" runat="server" CssClass="form-control form-control-lg"
                                        ValueType="System.String" RepeatColumns="3" RepeatDirection="Horizontal" 
                                        Theme="MetropolisBlue" OnSelectedIndexChanged="chApplication_SelectedIndexChanged">
                                    </dx:ASPxCheckBoxList>--%>
                               <%--        <telerik:RadComboBox ID="chApplication" Visible="true" runat="server" CheckBoxes="true" CheckedItemsTexts="DisplayAllInInput"
                                        OnClientLoad="OnClientLoadComboBox" DataTextField="ApplicationName" DataValueField="ApplicationId"
                                       OnSelectedIndexChanged="chApplication_SelectedIndexChanged"   EmptyMessage="">
                                        <HeaderTemplate>
                                            <telerik:RadTextBox runat="server" ID="RadTextBox1" ClientEvents-OnLoad="OnLoad">
                                            </telerik:RadTextBox>
                                        </HeaderTemplate>
                                    </telerik:RadComboBox>--%>
                           

                                </div>
                            </div>
                           
                            <div class="row mt-2">
                                

                                    <div>
                                        <dx:ASPxCheckBox ID="cbNone" AutoPostBack="true" Text="All None" Width="150px" runat="server" OnCheckedChanged="cbNone_CheckedChanged"></dx:ASPxCheckBox>
                                    </div>

                                    <div>
                                        <dx:ASPxCheckBox ID="cbView" runat="server" Width="150px" AutoPostBack="true" Text="All View" OnCheckedChanged="cbView_CheckedChanged"></dx:ASPxCheckBox>
                                    </div>

                                    <div>
                                        <dx:ASPxCheckBox ID="cbAdd" runat="server" Width="150px" AutoPostBack="true" Text="All Add" OnCheckedChanged="cbAdd_CheckedChanged"></dx:ASPxCheckBox>
                                    </div>

                                    <div>
                                        <dx:ASPxCheckBox ID="cbEdit" runat="server" Width="150px" AutoPostBack="true" Text="All Edit" OnCheckedChanged="cbEdit_CheckedChanged"></dx:ASPxCheckBox>
                                    </div>

                                    <div>
                                        <dx:ASPxCheckBox ID="cbDelete" runat="server" Width="150px" AutoPostBack="true" Text="All Delete" OnCheckedChanged="cbDelete_CheckedChanged"></dx:ASPxCheckBox>
                                    </div>

                                    <div>
                                        <dx:ASPxCheckBox ID="cbFull" runat="server" Width="500px" AutoPostBack="true" Text="Full Controll" OnCheckedChanged="cbFull_CheckedChanged"></dx:ASPxCheckBox>
                                    </div>


                               
                            </div>
                              <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">
                <div>
                    <telerik:RadButton ID="radbtnSave" runat="server" Text="Apply" OnClick="btnSave_Click"></telerik:RadButton>
                </div>
                <div>
                    <telerik:RadButton ID="ASPxButton1" runat="server" Width="150px" Text="Copy Permission" OnClick="ASPxButton1_Click"></telerik:RadButton>
                </div>
            </div>
                        </div>
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col-md-8">
                    <div class="row">
                        <div class="col-md-6">
                        </div>

                    </div>
                    <div class="row mt-2">

                        <div class="col-md-6">

                            <div class="row mt-3 mr-5">


                                <%-- <dx:ASPxButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click">
                                        </dx:ASPxButton>--%>
                            </div>


                        </div>
                        <div class="col-md-6">
                        </div>

                    </div>

                </div>
            </div>


            <asp:ObjectDataSource ID="objdsUser" runat="server" OldValuesParameterFormatString="original_{0}"
                SelectMethod="GetUser" TypeName="HRM.Common.BLL.MksSecurity">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfCurrentUserType" Name="UserTypeId" PropertyName="Value" Type="Int32" />
                    <asp:Parameter DefaultValue="0" Name="UserId" Type="String" />
                </SelectParameters>
            </asp:ObjectDataSource>
            <asp:HiddenField ID="hfCurrentUserType" runat="server" />

          
            <div id="toastrContainer">
                <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
            </div>
        </div>
       
        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
              
            <div class="col table-scroll">
                <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" class="gridstyle" runat="server" CssClass="tableStyle font-weight:fa-500px" Theme="MetropolisBlue"
                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID"  ForeColor="Black">
<%--    <dx:ASPxGridView ID="RadGrid1" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue"
                KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" Width="100%" Style="margin-top: 0px">--%>
                <SettingsSearchPanel Visible="true" HighlightResults="true" />

                <Columns>
                     <dx:GridViewDataColumn FieldName="UserName" HeaderStyle-Font-Bold="false" Caption="User" Width="180px" PropertiesEditType="TextBox" VisibleIndex="0" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                     <dx:GridViewDataColumn FieldName="ApplicationId" HeaderStyle-Font-Bold="false" Visible="false" Caption="User" Width="180px" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                    <dx:GridViewDataColumn FieldName="ApplicationName" HeaderStyle-Font-Bold="false"  Caption="Application" Width="180px" PropertiesEditType="TextBox" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"/>
                    <dx:GridViewDataColumn FieldName="MenuName" HeaderStyle-Font-Bold="false"  Caption="Menu" Width="180px" PropertiesEditType="TextBox" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"/>
                    <dx:GridViewDataColumn FieldName="FormName" HeaderStyle-Font-Bold="false"  Caption="Form" Width="180px" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"/>
                    <dx:GridViewDataColumn FieldName="FormId" HeaderStyle-Font-Bold="false"  Caption="Form" Width="180px" Visible="false" PropertiesEditType="TextBox" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle"/>

                    <dx:GridViewDataCheckColumn Caption="None" HeaderStyle-Font-Bold="false"  VisibleIndex="4" Width="80px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="cbNone" Checked='<%# Eval("None")%>' runat="server" Width="80px"></dx:ASPxCheckBox>
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataCheckColumn Caption="View" HeaderStyle-Font-Bold="false"  VisibleIndex="5" Width="80px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="cbAView" Checked='<%# Eval("ViewOnly")%>' runat="server" Width="80px"></dx:ASPxCheckBox>
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataCheckColumn Caption="Add" HeaderStyle-Font-Bold="false"  VisibleIndex="6" Width="80px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="cbAdd" Checked='<%# Eval("AddOnly")%>' runat="server" Width="80px"></dx:ASPxCheckBox>
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataCheckColumn Caption="Edit" HeaderStyle-Font-Bold="false"  VisibleIndex="7" Width="80px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="cbEdit" Checked='<%# Eval("EditOnly")%>' runat="server" Width="80px"></dx:ASPxCheckBox>
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>

                    <dx:GridViewDataCheckColumn Caption="Delete" HeaderStyle-Font-Bold="false"  VisibleIndex="8" Width="80px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="cbDelete" Checked='<%# Eval("DeleteOnly")%>' runat="server" Width="80px"></dx:ASPxCheckBox>
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>


                    <dx:GridViewDataCheckColumn Caption="Full Controll" HeaderStyle-Font-Bold="false"  VisibleIndex="9" Width="80px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                        <DataItemTemplate>
                            <dx:ASPxCheckBox ID="cbFull" Checked='<%# Eval("FullControl")%>' runat="server" Width="80px"></dx:ASPxCheckBox>
                        </DataItemTemplate>
                    </dx:GridViewDataCheckColumn>
                    <dx:GridViewDataColumn FieldName="FormDescription" HeaderStyle-Font-Bold="false"  Caption="Form" Width="180px" PropertiesEditType="TextBox" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                </Columns>

                 <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                     <%--<Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="230" UseFixedTableLayout="true" />--%>
                <Settings VerticalScrollBarMode="Visible" ShowPreview="true" VerticalScrollableHeight="300" ShowFooter="True" />
                     
                <SettingsPager Mode="ShowAllRecords" />
            </dx:ASPxGridView>

                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="RadGrid1">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>

            </div>
        </div>
        <dx:ASPxPopupControl ID="changeAttPopUp" runat="server" Width="500px" Height="100px"
            ClientInstanceName="popup" PopupElementID="popupArea" ShowOnPageLoad="True" Theme="Office2010Blue"
            PopupVerticalAlign="WindowCenter" PopupHorizontalAlign="WindowCenter"
            AllowDragging="True" AllowResize="false" CloseAction="CloseButton"
            ScrollBars="None" HeaderText="Map" ShowFooter="true" FooterText="" PopupAnimationType="Fade">
            <HeaderTemplate>
                Coppy user permission
            </HeaderTemplate>
            <ContentCollection>
                <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">




                    <table style="width: 90%;" class="newFont">

                        <tr>
                            <td class="auto-style1">From User:</td>
                            <td>
                                <dx:ASPxComboBox ID="cbFromUser" Height="30px" ValueField="UserId" TextField="UserName" BackColor="#F5F5F5" Width="150px" runat="server" DropDownStyle="DropDownList" DataSourceID="objdsUser"
                                    ValueType="System.String" TextFormatString="{0}" NullText=" << Select User >> " MaxLength="50" AutoPostBack="true" OnSelectedIndexChanged="txtUserRole_SelectedIndexChanged">
                                    <InvalidStyle BackColor="LightPink" />
                                </dx:ASPxComboBox>

                            </td>

                        </tr>
                        <tr>
                            <td class="auto-style1">To User:</td>
                            <td>
                                <dx:ASPxComboBox ID="cbToUser" Height="30px" ValueField="UserId" TextField="UserName" BackColor="#F5F5F5" Width="150px" runat="server" DropDownStyle="DropDownList" DataSourceID="objdsUser"
                                    ValueType="System.String" TextFormatString="{0}" NullText=" << Select User >> " MaxLength="50" AutoPostBack="true" OnSelectedIndexChanged="txtUserRole_SelectedIndexChanged">
                                    <InvalidStyle BackColor="LightPink" />
                                </dx:ASPxComboBox>

                            </td>

                        </tr>
                        <tr>
                            <td></td>
                            <td>
                                <asp:Button ID="btnApprove" runat="server" CssClass="btn btn-info" Width="70px" Text="Apply" OnClick="btnApprove_Click" />
                                <asp:Button ID="Button1" Width="70px" runat="server" CssClass="btn btn-info" Text="Cancel" OnClick="Button1_Click" />
                            </td>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red"></asp:Label>
                        </tr>


                    </table>
                </dx:PopupControlContentControl>
            </ContentCollection>
        </dx:ASPxPopupControl>
    </div>

</ContentTemplate>
</asp:Content>
