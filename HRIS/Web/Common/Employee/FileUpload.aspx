<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUpload.aspx.cs" Inherits="Common_Employee_FileUpload" MasterPageFile="~/Common/Common_MenuMasterPage.master"  %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="DevExpress.XtraReports.v14.2.Web, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.XtraReports.Web" TagPrefix="dx" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>
<%@ Register src="EmployeeSearchNew.ascx" tagname="EmployeeSearch" tagprefix="uc3" %>

<%--<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik1" %>--%>

<%@ Register assembly="AjaxControlToolkit" namespace="AjaxControlToolkit" tagprefix="CC2" %>


<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.13.110, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <style type="text/css">

    .emptable 
  {
  	border: #333333 1px solid;
  	border-radius: 5px; 
  	-moz-border-radius: 5px; 
  	-moz-box-shadow: 2px 2px 2px #888;
  	 -webkit-box-shadow: 2px 2px 2px #888; 
  	 box-shadow: 2px 2px 2px #888; 
  	 font-family: Arial, Helvetica, sans-serif;
  	 font-size: 11px; 
  	 color: #333333;
  	}
  	.emptable img{ border: #666666 solid 2px; margin: 0 20px 0 0 }
        .style17
        {
            width: 600px;
        }
        .style18
        {
            width: 100%;
        }
        .style19
        {
            height: 97px;
        }
         .auto-style1 {
             height: 27px;
         }



         .auto-style3 {
             width: 861px;
         }



    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table>
        <tr>
            <td class="auto-style3">
           <telerik:RadTextBox ID="tbSearchSupplier" runat="server"
                                    AutoPostBack="True" 
                    ontextchanged="tbSearchSupplier_TextChanged" EmptyMessage="Employee Code" 
                    Skin="Windows7" Width="125px" />
                                <%--<CC2:TextBoxWatermarkExtender ID="tbSearchSupplier_TextBoxWatermarkExtender" 
                    runat="server" Enabled="True" TargetControlID="tbSearchSupplier" 
                    WatermarkText="Search Employee">
                </CC2:TextBoxWatermarkExtender>--%>
                                <asp:ImageButton ID="btnSearchSupplier" runat="server" 
                            ImageUrl="~/App_Themes/CommonResources/search.png" 
                            Width="16px" onclick="btnSearchSupplier_Click"/>
                          
                       
                                <cc2:modalpopupextender ID="btnSearchSupplier_ModalPopupExtender" 
                                     runat="server" 
                                CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" 
                                PopupControlID="PopupPanel" TargetControlID="btnSearchSupplier" 
                                BackgroundCssClass="popbackground" 
                    PopupDragHandleControlID="Panel1">
                                </cc2:modalpopupextender>
                            </td>
        </tr>



        <tr>
            <td class="auto-style3">
                    <asp:DataList ID="DataList1" runat="server" Visible="false" 
                        RepeatColumns="3" RepeatDirection="Horizontal"  
                        Width="800px" DataSourceID="objdsEmployee">
                        <ItemTemplate>
                            <table  class="emptable" width="100%" cellpadding="2" cellspacing="0" >
                                <tr>
                                    <td>
                                        <table>
                                            <tr>
                                                <td>
                                                    <table>
                                                        <tr>
                                                            <td>
                                                                &nbsp;</td>
                                                            <td class="empinnerStyle">
                                                                &nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td class="emphederStyle">
                                                                <asp:Label ID="Label1" runat="server" Text="Employee Code"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEmployeeCode" runat="server" 
                                                                    Text='<%# Eval("EmployeeCode") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblEmployeeName" runat="server" 
                                                                    Text='<%# Eval("FirstName")+" "+Eval("LastName") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label3" runat="server" Text="Date Of Birth"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDateOFBirth" runat="server" Text='<%# Eval("DateOfBirth","{0:d}") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label4" runat="server" Text="NIC No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblNIC" runat="server" Text='<%# Eval("NIC") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label5" runat="server" Text="Contact No"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label6" runat="server" Text="Designation"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblDesignation" runat="server" 
                                                                    Text='<%# Eval("DesignationName") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>
                                                                <asp:Label ID="Label7" runat="server" Text="Organization"></asp:Label>
                                                            </td>
                                                            <td>
                                                                <asp:Label ID="lblOrganization" runat="server" 
                                                                    Text='<%# Eval("OrganizationTypeName") %>'></asp:Label>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("Status") %>' />
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td>
                                        <asp:Image ID="imgEmployee" runat="server" 
                                            ImageUrl='<%# "~/Common/EmployeeImages/"+Eval("Image") %>' Width="120px" ImageAlign="Right" />
                                    </td>
                                </tr>
                            </table>
                        </ItemTemplate>
                    </asp:DataList>
                    <asp:ObjectDataSource ID="objdsEmployee" runat="server" 
                            SelectMethod="GetEmployee" TypeName="HRM.Common.BLL.Employee" 
                            OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfEmloyeeId" DefaultValue="0" Name="EmployeeId" PropertyName="Value" Type="Int64" />
                            <asp:SessionParameter Name="UserName" SessionField="UserName" Type="String" />
                            <asp:SessionParameter Name="CompanyID" SessionField="CompanyId" Type="Int32" />
                            <asp:Parameter DefaultValue="HR" Name="ApplicationCode" Type="String" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
        </tr>
         <tr>
            <td class="auto-style3">
              
                <table class="style17">
                    <tr>
                        <td>
                              <div id="PopupPanel">
                    <asp:Panel ID="Panel1" runat="server" CssClass="popstyle" Height="550px" Width="960px">
                        <table>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    &nbsp;</td>
                                <td style="text-align: right">
                                    <asp:ImageButton ID="btnClosePopup" runat="server" 
                                        ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                                </td>
                            </tr>
                            <tr>
                                <td class="auto-style1">
                                    </td>
                                <td style="text-align: left" class="auto-style1">
                                    
                                    <uc3:EmployeeSearch ID="EmployeeSearch1" runat="server"  />
                                    
                                </td>
                                <td class="auto-style1">
                                    </td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td>
                                    <asp:HiddenField ID="hfEmloyeeId" runat="server" />
                                </td>
                                <td>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div></td>
                    </tr>
                </table>
              
            </td>
        </tr>

   
    <dx:ASPxFileManager ID="ASPxFileManager1" runat="server"   OnFileUploading="ASPxFileManager1_FileUploading" OnItemDeleting="ASPxFileManager1_ItemDeleting" OnFolderCreating="ASPxFileManager1_FolderCreating" ClientInstanceName="ASPxFileManager1" SettingsEditing-AllowCreate="true"
        
         OnDataBinding="ASPxFileManager1_DataBinding"
        OnUnload="ASPxFileManager1_Unload" Height="400px" >

        <Settings RootFolder="D:\Sachitha\Working  Project\Elcardo\HRM\Web\Documents" ThumbnailFolder="~\Thumb\"  />
      
        <SettingsEditing AllowCopy="True"   AllowDelete="True" AllowDownload="True" AllowMove="True" AllowRename="True" />
    </dx:ASPxFileManager>
            
</asp:Content>