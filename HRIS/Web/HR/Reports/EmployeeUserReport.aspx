<%@ Page Language="C#" AutoEventWireup="true" CodeFile="EmployeeUserReport.aspx.cs" Inherits="HR_Reports_EmployeeUserReport" MasterPageFile="~/HR/HR_MenuMasterPage.master" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>


<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<%--<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik1" %>--%>




<%@ Register Assembly="Telerik.ReportViewer.WebForms, Version=6.2.13.110, Culture=neutral, PublicKeyToken=a9d7983dfcc261be" Namespace="Telerik.ReportViewer.WebForms" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 160px;
        }
        .style3
        {
            width: 240px;
        }
        .style4
        {
            width: 300px;
        }
        .style5
        {
            width: 66px;
        }
        .style6
        {
            width: 66px;
            height: 26px;
        }
        .style7
        {
            height: 26px;
        }
        .auto-style1 {
            width: 119px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

     <table>
          <tr>
                    <td colspan="2">
                        <h4>
                            User Reports

                        </h4>
                    </td>
          </tr>
         <tr >
             <td></td>
            <td class="style1">
                <asp:Label ID="Label1" runat="server" Height="10" Text="DEPARTMENT" Font-Bold="true"></asp:Label>
            </td>
            <td class="style3">
                 <asp:Button ID="btnOrganizationSelect" runat="server" CssClass="Buttons" Text="SELECT" BackColor="Gray" BorderColor="Gray" OnClick="btnOrganizationSelect_Click" />
                 <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                    CancelControlID="btnClosePopup"  Enabled="True" PopupControlID="Panel1"
                    TargetControlID="btnOrganizationSelect">
                </cc1:ModalPopupExtender>
                <asp:Label ID="lblOrganization" runat="server"></asp:Label>
                <asp:HiddenField ID="hfOrganizationStructure" runat="server" />
            </td>
              <td>
                 <asp:CheckBox ID="CBEmpActive" runat="server"  Checked="true" Text="ACTIVE EMPLOYEE" Font-Bold="true"/>
             </td>

                      
        </tr>
         </table>
    <table>
        <tr>
             <td class="auto-style1">
                            <table style="background-color:whitesmoke; width: 1200px;">
                                <tr>
                                    <td class="auto-style3">
                                        DATA FIELD
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3">
                                        <dx:ASPxCheckBoxList ID="checkboxlist1"  TextField="ColumnName" ValueField="ColumnCode" runat="server" RepeatColumns="8" DataSourceID="SqlDataSource5"  RepeatLayout="Table" Width="100%" Theme="Office2010Blue">
                                                        <Rootstyle verticalalign="Top">                                                          
                                                        </Rootstyle>
                                                         <RootStyle BackColor="Salmon">
                                                        </RootStyle>
                                         </dx:ASPxCheckBoxList>
                                    
                                        <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                          
                                                       SelectCommand="SELECT ColumnCode,ColumnName FROM HR_ColumnNames">
                                                  
                                        </asp:SqlDataSource>
                                         
                                    </td>

                               </tr>
                        
                              </table>
                        </td>
        </tr>
        <tr>
             
           
            
                 <td align="right">
                            <asp:Label ID="lblResult" runat="server" ForeColor="Red" />
                        </td>
            
        </tr>
       </table>
       <table >
                    <tr>
                        <td></td>
                         <td></td>
                    </tr>
                          <tr>
                        <td style="width:1035px">
                            &nbsp;
                        </td>
                              <td></td>
                        <td style="width:300px">
                            <asp:Button ID="Button3" runat="server" CssClass="Buttons" Text="VIEW" OnClick="Button3_Click" BackColor="Gray" BorderColor="Gray" Width="80px" Enabled="true" />
                            <asp:Button ID="Button1" runat="server" CssClass="Buttons" Text="DOWNLOAD" OnClick="Button1_Click" BackColor="Gray" BorderColor="Gray" Width="80px" Enabled="true"/>
                          <asp:Button ID="Button2" runat="server" CssClass="Buttons" Text="CLEAR" OnClick="Button2_Click" BackColor="Gray" BorderColor="Gray" Width="80px" Enabled="true"/>
                          
                          
                          
                        </td>
                    </tr>
      </table>
    <table>
        <tr>
            <td style="width:1200px">
                 <dx:ASPxGridView ID="UserReport" runat="server" AutoGenerateColumns="False" Font-Size ="Small" ClientIDMode="AutoID" EnableTheming="True"  Theme="MetropolisBlue" Width="100%" OnDataBound="Grid_DataBound" Visible="false" Settings-HorizontalScrollBarMode="Auto">
                    <Settings VerticalScrollBarMode="Auto" />
                    <SettingsPager PageSize="50">
                    </SettingsPager>
                        <Columns>
                           <dx:GridViewDataTextColumn FieldName="COMPANYNAME" Caption="COMPANY NAME" Width="250px" Visible="false" VisibleIndex="1">
                               <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="ENPCODE" Caption="EMP CODE" Width="100px" Visible="false" VisibleIndex="2">
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="EPFNO" Caption="EPF NO" Width="100px" Visible="false" VisibleIndex="3">
                                   <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="FIRSTNAME" Caption="FIRST NAME" Width="150px" Visible="false" VisibleIndex="5">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="LASTNAME" Caption="LAST NAME" Width="150px" Visible="false" VisibleIndex="6">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="FULLNAME" Caption="FULL NAME" Width="600px" Visible="false" VisibleIndex="7">
                                 <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CALLNAME" Caption="CALL NAME" Width="150px" Visible="false" VisibleIndex="8">
                                 <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="BRANCH" Caption="DEPARTMENT" Width="300px" Visible="false" VisibleIndex="17">
                                 <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="ADDRESS" Caption="ADDRESS" Width="600px" Visible="false" VisibleIndex="11">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="HOMECONTACTNO" Caption="HOME" Width="100px" Visible="false" VisibleIndex="10">
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="MOBILENO" Caption="MOBILE" Width="100px" Visible="false" VisibleIndex="9">
                                 <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="EMAIL" Caption="EMAIL" Width="250px" Visible="false" VisibleIndex="12">
                                  <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="NIC" Caption="NIC" Width="120px" Visible="false" VisibleIndex="13">
                                   <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="GENDER" Caption="GENDER"  Width="80px" Visible="false" VisibleIndex="14">
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="DATEOFBIRTH" Caption="DATE OF BIRTH"  Width="120px" Visible="false" VisibleIndex="15">
                                 <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="JOBCATEGORY" Caption="JOB CATEGORY" VisibleIndex="8" Width="150px" Visible="false">
                                 <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="EMPLOYMENTTYPE" Caption="EMP TYPE" Width="200px" Visible="false" VisibleIndex="32">
                                 <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="EMPLOYEMENTGRADE" Caption="EMP GRADE" Width="100px" Visible="false" VisibleIndex="33">
                                 <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="EMERGENCYCONTACTPERSON" Caption="EMERG.CONT.PERSON" Width="200px" Visible="false" VisibleIndex="18">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="EMERGENCYCONTACTNO" Caption="EMERG.CONT.NO" Width="120px" Visible="false" VisibleIndex="20">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="POSTALCODE" Caption="POSTAL CODE" Width="80px" Visible="false" VisibleIndex="30">
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RECRUITEMENTDATE" Caption="RECRUI. DATE"  Width="120px" Visible="false" VisibleIndex="21">
                                 <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="CONFIRMATIONDATE" Caption="CONFI. DATE"  Width="120px" Visible="false" VisibleIndex="23">
                                 <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RESIGNDATE" Caption="RESIGN DATE"  Width="120px" Visible="false" VisibleIndex="24">
                                 <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="REMARK" Caption="REMARK" Width="200px" Visible="false" VisibleIndex="32">
                                 <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="PASSPORTNO" Caption="PASSPORT NO" Width="100px" Visible="false" VisibleIndex="26">
                                   <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="PASSPORTEXPIRYDATE" Caption="PASSPORT EX.DATE" Width="120px" Visible="false" VisibleIndex="27">
                                <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="VISANO" Caption="VISA NO" Width="100px" Visible="false" VisibleIndex="28">
                               <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="VISAEXPIRYDATE" Caption="VISA EX.DATE" Width="120px" Visible="false" VisibleIndex="29">
                                <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="RELATIONSHIPOFCONTACTPERSON" Caption="RELATIONSHIP"  Width="120px" Visible="false" VisibleIndex="19">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="EPFNODATE" Caption="EPF DATE" Width="120px" Visible="false" VisibleIndex="22">
                               <PropertiesTextEdit DisplayFormatString="yyyy/MM/dd"></PropertiesTextEdit>
                               <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                           <dx:GridViewDataTextColumn FieldName="OFFICECONTACTNO" Caption="OFFCICE NO"  Width="120px" Visible="false" VisibleIndex="25">
                               <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="EMPCARD" Caption="EMP CARD"  Width="100px" Visible="false" VisibleIndex="31">
                                <HeaderStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            <dx:GridViewDataTextColumn FieldName="INITIALS" Caption="INITIALS"  Width="100px" Visible="false" VisibleIndex="4">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                             <dx:GridViewDataTextColumn FieldName="DESIGNATION" Caption="DESIGNATION"  Width="300px" Visible="false" VisibleIndex="16">
                                <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Left" />
                           </dx:GridViewDataTextColumn>
                              <dx:GridViewDataTextColumn FieldName="JOBGRADE" Caption="JOB GRADE" VisibleIndex="8" Width="150px" Visible="false">
                                 <HeaderStyle HorizontalAlign="Center" /><CellStyle HorizontalAlign="Center" />
                           </dx:GridViewDataTextColumn>
                            
                        </Columns>

                         <Styles>
                             <Header BackColor="WhiteSmoke" Font-Bold="true" Font-Size="Small" Font-Names="Century Gothic">
                             </Header>
                                <Cell HorizontalAlign="Center"></Cell>
                                <AlternatingRow BackColor="#F5F5F5">
                                </AlternatingRow>
                          </Styles>

                        </dx:ASPxGridView>
                 <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="UserReport">
                     <Styles>
                         <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                         </Header>
                     </Styles>
                 </dx:ASPxGridViewExporter>
            </td>
        </tr>
      
    </table>
     
  

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
 

     <div class="ContentBlock">
            <asp:Panel ID="Panel1" runat="server" CssClass="popstyle" Height="580px" ScrollBars="Horizontal">
                <table class="style1">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                            &nbsp;
                        </td>
                        <td style="text-align: right">
                            <asp:ImageButton ID="btnClosePopup" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </asp:Panel>
        </div>

</asp:Content>
