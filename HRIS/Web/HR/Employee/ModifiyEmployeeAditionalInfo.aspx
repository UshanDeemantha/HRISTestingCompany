<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModifiyEmployeeAditionalInfo.aspx.cs" MasterPageFile="~/HR/HR_MenuMasterPage.master" Inherits="HR_Employee_ModifiyEmployeeAditionalInfo" %>

<%@ Register Src="UserControls/EmployeeSummaryList.ascx" TagName="EmployeeSummaryList" TagPrefix="uc2" %>
<%@ Register Src="UserControls/EmployeeAditionalInfoContol.ascx" TagName="EmployeeAditionalInfoContol" TagPrefix="uc1" %>
<%@ Register Src="UserControls/EmployeeCertificationControl.ascx" TagName="EmployeeCertificationControl" TagPrefix="uc4" %>
<%@ Register Src="UserControls/EmployeeChildControl.ascx" TagName="EmployeeChildControl" TagPrefix="uc5" %>
<%@ Register Src="UserControls/EmoployeeMembershipControl.ascx" TagName="EmoployeeMembershipControl" TagPrefix="uc6" %>
<%@ Register Src="UserControls/EmployeeQulificationControl.ascx" TagName="EmployeeQulificationControl" TagPrefix="uc7" %>
<%@ Register Src="UserControls/EmployeeFluencyrControl.ascx" TagName="EmployeeFluencyrControl" TagPrefix="uc8" %>
<%@ Register Src="UserControls/EmployeeSpouseControl.ascx" TagName="EmployeeSpouseControl" TagPrefix="uc9" %>
<%@ Register Src="UserControls/EmoloyeeSportsControl.ascx" TagName="EmoloyeeSportsControl" TagPrefix="uc10" %>
<%@ Register Src="UserControls/EmployeeSkillsControl.ascx" TagName="EmployeeSkillsControl" TagPrefix="uc11" %>
<%@ Register Src="UserControls/EmployeeSearchNew.ascx" TagName="EmployeeSearchNew" TagPrefix="uc12" %>
<%@ Register Src="~/HR/Employee/UserControls/NewEmployeeAdd.ascx" TagName="NewEmployeeAdd" TagPrefix="uc13" %>
<%@ Register Src="~/HR/Employee/UserControls/EmployeeContactDetails.ascx" TagName="EmployeeContactDetails" TagPrefix="uc14" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .emptable {
            border: #333333 1px solid;
            border-radius: 5px;
            -moz-border-radius: 5px;
            -moz-box-shadow: 2px 2px 2px #888;
            -webkit-box-shadow: 2px 2px 2px #888;
            box-shadow: 2px 2px 2px #888;
            font-family: 'Century Gothic', Helvetica, sans-serif;
            font-size: 11px;
            color: #333333;
            width: 101%;
        }

            .emptable img {
                border: #2683b5 solid 1px;
                margin: 0 20px 0 0;
            }




        .Contact {
            border-radius: 5px;
            -moz-border-radius: 5px;
            font-family: Arial, Helvetica, sans-serif;
            font-size: 12px;
            color: #333333;
        }
        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_FinishNavigationTemplateContainerID_StepFinishButton {
            position: fixed;
            right: 40px;
            bottom: 10%;
        }
        input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_FinishNavigationTemplateContainerID_StepPreviousButton {
            position: fixed;
            right: 160px;
            bottom: 10%;
        }
    </style>
<%--    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-2.css" />--%>
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/wizard.css" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    &nbsp;&nbsp;
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <table style="width: 100%">
        <tr>
            <td>
                <table style="margin-left: 30px">
                    <tr>
                        <td>
                            <dx:ASPxComboBox ID="CmbCustomers" runat="server" style="border-radius: 10px; margin-left:5px; width:700px" DropDownWidth="550"
                                DropDownStyle="DropDownList" ValueField="EmployeeID" DataSourceID="ObjectDataSourceSearchGrid" NullText="  Advance Search"
                                ValueType="System.String" TextFormatString="First name :{0}/Code : {1} /{2}/ {3}" EnableCallbackMode="true" OnSelectedIndexChanged="CmbCustomers_SelectedIndexChanged" AutoPostBack="true" 
                                IncrementalFilteringMode="Contains" OnTextChanged="CmbCustomers_TextChanged"
                                CallbackPageSize="30">
                                <ClientSideEvents SelectedIndexChanged="" />
                                <Columns>
                                    <dx:ListBoxColumn FieldName="EmployeeID" Width="130px" Visible="false" />
                                    <dx:ListBoxColumn FieldName="FirstName" Width="130px" />
                                    <dx:ListBoxColumn FieldName="EmployeeCode" Width="130px" />
                                    <dx:ListBoxColumn FieldName="LastName" Width="70px" />
                                    <dx:ListBoxColumn FieldName="MobileNo" Width="100px" />
                                    <dx:ListBoxColumn FieldName="NIC" Width="100px" />
                                </Columns>
                            </dx:ASPxComboBox>
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
<tr>
    <td style="display: grid; grid-template-columns: 50px 1fr 50px;">
        <asp:Wizard ID="EmployeeAditionalInfoWizard" runat="server" ActiveStepIndex="1"
            Width="100%" HeaderText=" "
            OnActiveStepChanged="EmployeeAditionalInfoWizard_ActiveStepChanged"
            CssClass="wizard-main"
            StepNextButtonImageUrl="~/App_Themes/HR/wizard-next.jpg"
            StepNextButtonType="Image"
            StepPreviousButtonImageUrl="~/App_Themes/HR/wizard-previous.jpg"
            StepPreviousButtonType="Image"
            OnFinishButtonClick="EmployeeAditionalInfoWizard_FinishButtonClick">
            <FinishNavigationTemplate>
                <asp:ImageButton ID="StepPreviousButton" runat="server"
                    AlternateText="Previous" CausesValidation="False" CommandName="MovePrevious"
                    ImageUrl="~/App_Themes/HR/wizard-previous.png" />
                <asp:ImageButton ID="StepFinishButton" runat="server" AlternateText="Finish"
                    CommandName="MoveComplete" ImageUrl="~/App_Themes/HR/wizard-finish.png" />
            </FinishNavigationTemplate>
            <HeaderStyle CssClass="wizard-hed" />
            <SideBarStyle CssClass="wizard-linkback" HorizontalAlign="Left"
                VerticalAlign="Top" />
            <SideBarTemplate>
                <asp:DataList ID="SideBarList" runat="server"
                    OnItemDataBound="SideBarList_ItemDataBound">
                    <ItemTemplate>
                        <asp:LinkButton ID="SideBarButton" runat="server" OnClick="SideBarButton_Click"
                            CssClass="wizard-linkback-A"></asp:LinkButton>
                    </ItemTemplate>
                    <SelectedItemStyle Font-Bold="True" />
                </asp:DataList>
            </SideBarTemplate>
            <StartNavigationTemplate>
                <table class="style18">
                    <tr>
                        <td>&nbsp;</td>
                        <td>
                            <asp:Label ID="lblSelectEmp" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;</td>
                        <td style="position: fixed; right: 60px; bottom: 10%; text-align: right">
                            <asp:ImageButton ID="StartNextButton" runat="server" CommandName="MoveNext"
                                ImageUrl="~/App_Themes/HR/wizard-start.png" />
                        </td>
                    </tr>
                </table>
            </StartNavigationTemplate>
            <StepNavigationTemplate>
                <div style="position: fixed; right: 40px; bottom: 10%;">

                    <asp:ImageButton ID="StepPreviousButton" runat="server"
                        AlternateText="Previous" CausesValidation="False" CommandName="MovePrevious"
                        ImageUrl="~/App_Themes/NewTheme/img/back-button.png" />
                    <asp:ImageButton ID="StepNextButton" runat="server" AlternateText="Next"
                        CommandName="MoveNext" ImageUrl="~/App_Themes/NewTheme/img/next-button.png" />
                </div>

            </StepNavigationTemplate>

            <WizardSteps>
<%--                <asp:WizardStep ID="WizardStep1" runat="server" Title="Employee Profile" >
                    <table class="Contact" width="100%">
                        <tr>
                            <td>&nbsp;</td>
                            <td>&nbsp;</td>
                            <td>
                                <asp:Image ID="Image1" runat="server"
                                    ImageUrl="~/App_Themes/HR/search_Employee.png" Height="150px" Width="300px" />
                            </td>
                        </tr>
                    </table>

                </asp:WizardStep>--%>
                <asp:WizardStep runat="server" Title="Employee's Details" ID="WizardStep9">
                    <uc13:NewEmployeeAdd ID="NewEmployeeAddContol1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Employee's Contact Details" ID="WizardStep10">
                    <uc14:EmployeeContactDetails ID="EmployeeContactDetailsContol1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Additional Information" ID="Step0">
                    <uc1:EmployeeAditionalInfoContol ID="EmployeeAditionalInfoContol1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep2" runat="server" Title="Qualification Details">
                    <uc7:EmployeeQulificationControl ID="EmployeeQulificationControl1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep3" runat="server" Title="Certification Details">
                    <uc4:EmployeeCertificationControl ID="EmployeeCertificationControl1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep4" runat="server" Title="Membership Details">
                    <uc6:EmoployeeMembershipControl ID="EmoployeeMembershipControl1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep5" runat="server" Title="Language Ability Details">
                    <uc8:EmployeeFluencyrControl ID="EmployeeFluencyrControl1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep6" runat="server" Title="Spouce's Details">
                    <uc9:EmployeeSpouseControl ID="EmployeeSpouseControl1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep runat="server" Title="Children's Details" ID="DDD">
                    <uc5:EmployeeChildControl ID="EmployeeChildControl1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep7" runat="server" Title="Extra Skills Details">
                    <uc11:EmployeeSkillsControl ID="EmployeeSkillsControl1" runat="server" />
                </asp:WizardStep>
                <asp:WizardStep ID="WizardStep8" runat="server" Title="Sports Skill Details">
                    <uc10:EmoloyeeSportsControl ID="EmoloyeeSportsControl1" runat="server" />
                </asp:WizardStep>

            </WizardSteps>

        </asp:Wizard>
    </td>

        <div class="row ml-3">
       <dx:ASPxGridView ID="gvSearch" runat="server" AutoGenerateColumns="False" DataSourceID="ObjectDataSourceSearchGrid" Visible="false"
            ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="CustomerId" Theme="MetropolisBlue" Width="100%">
            <SettingsPager PageSize="15">
            </SettingsPager>
            <SettingsSearchPanel Visible="true" HighlightResults="true" />
            <Columns>
                <dx:GridViewDataTextColumn FieldName="EmployeeID" Caption="EmployeeID" VisibleIndex="2" Visible="false" Width="100">
                    <CellStyle HorizontalAlign="Center" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="EmployeeCode" Caption="Employee Code" VisibleIndex="2" Width="100">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="FirstName" Caption="First Name" VisibleIndex="3" Width="150">
                    <CellStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="LastName" Caption="Last Name" VisibleIndex="4" Width="150">
                    <CellStyle HorizontalAlign="Left" />
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="MobileNo" Caption="Mobile No" VisibleIndex="5" Width="100">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="Email" Caption="Email" VisibleIndex="6" Width="120" Visible="false">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn FieldName="NIC" Caption="NIC" VisibleIndex="7" Width="120">
                </dx:GridViewDataTextColumn>
                <dx:GridViewDataTextColumn Caption="Select" VisibleIndex="1" Width="80">
                    <CellStyle HorizontalAlign="Center" />
                    <DataItemTemplate>
                        <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click">Select</asp:LinkButton>
                    </DataItemTemplate>
                </dx:GridViewDataTextColumn>


            </Columns>
            <Styles>
                <Header BackColor="WindowFrame" Font-Bold="true" HorizontalAlign="Center">
                </Header>
                <AlternatingRow BackColor="#F5F5F5">
                </AlternatingRow>
            </Styles>
        </dx:ASPxGridView>

    </div>
    
        <asp:Label ID="Label2" runat="server" Font-Names="Tahoma" Font-Bold="true" ForeColor="#336699" Font-Size="Small"></asp:Label>
        <telerik:RadTextBox ID="tbSearchSupplier" runat="server" AutoPostBack="True" OnTextChanged="tbSearchSupplier_TextChanged"
            Skin="Windows7" Visible="false" />

    <asp:ObjectDataSource ID="ObjectDataSourceSearchGrid" runat="server"
        OldValuesParameterFormatString="original_{0}" SelectMethod="GetData"
        TypeName="CommenDataSetTableAdapters.GetAdvanceEmployeesForSearchTableAdapter">
        <SelectParameters>
            <asp:ControlParameter ControlID="ddlSearchCollom" Name="ContainCollom"
                PropertyName="SelectedValue" Type="String" />
            <asp:ControlParameter ControlID="tbSearchSupplier" Name="ContainText"
                PropertyName="Text" Type="String" />
            <asp:ControlParameter ControlID="ddlContentLocation" Name="ContainPossition"
                PropertyName="SelectedValue" Type="Int32" />
            <asp:SessionParameter DefaultValue="" Name="UserName" SessionField="UserName"
                Type="String" />
            <asp:SessionParameter DefaultValue="" Name="CompanyID" SessionField="CompanyId"
                Type="Int32" />
            <asp:Parameter DefaultValue="HR" Name="ApplicationCode" Type="String" />
        </SelectParameters>
    </asp:ObjectDataSource>

    <asp:HiddenField ID="hfSupplierId" runat="server" />
    <asp:HiddenField ID="hfSupplireCode" runat="server" />
        <div class="row">
        <div class="col-md-6">

            <telerik:RadComboBox ID="ddlSearchCollom" runat="server" ZIndex="1000000"
                Skin="Windows7" Visible="false">
            </telerik:RadComboBox>
        </div>
        <div class="col-md-6">
        </div>
    </div>
        <div class="col-md-6">
        <telerik:RadComboBox ID="ddlContentLocation" runat="server" ZIndex="1000000"
            Skin="Windows7" Visible="false">
            <Items>
                <telerik:RadComboBoxItem runat="server" Text="Start with" Value="1" />
                <telerik:RadComboBoxItem runat="server" Text="Contain" Value="2" />
                <telerik:RadComboBoxItem runat="server" Text="End with" Value="3" />
            </Items>
        </telerik:RadComboBox>
    </div>
        </tr>
    </table>
    <br />
</asp:Content>




