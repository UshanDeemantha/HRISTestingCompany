<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeContactDetails.ascx.cs" Inherits="HR_Employee_UserControls_EmployeeContactDetails" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="CollectionPager" Namespace="SiteUtils" TagPrefix="cc2" %>

<style type="text/css">
    .form-control1 {
        display: block;
        width: 100%;
        height: calc(1.5em + .75rem + 7px);
        padding: .375rem .75rem;
        font-size: 1.25rem;
        font-weight: 400;
        line-height: 1.5;
        color: #495057;
        background-color: #fff;
        background-clip: padding-box;
        border: 1px solid #ced4da;
        border-radius: .25rem;
        transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
    }

    .form-control2 {
        display: block;
        width: 250px;
        height: calc(1.5em + .75rem + 7px);
        padding: .375rem .75rem;
        font-size: 1rem;
        font-weight: 400;
        line-height: 1.5;
        color: #495057;
        background-color: #fff;
        background-clip: padding-box;
        border: 1px solid #ced4da;
        border-radius: .25rem;
        transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
    }

    .hoverclass {
        margin-top: 31px;
    }

    .emptable {
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

        .emptable img {
            border: #666666 solid 2px;
            margin: 0 20px 0 0;
        }



    /*        .auto-style1 {
            width: 403px;
        }*/

    .auto-style2 {
        margin-left: 20px;
    }

    .auto-style3 {
        width: 250px;
    }

    .Contact {
        border-radius: 5px;
        -moz-border-radius: 5px;
        font-family: Arial, Helvetica, sans-serif;
        font-size: 12px;
        color: #333333;
    }

    span.checkalign {
        margin-left: 46%;
    }

    table#ContentPlaceHolder1_EmployeeAditionalInfoWizard_rbEmpCard {
        margin-top: 3px;
    }

    input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_StepNavigationTemplateContainerID_StepPreviousButton {
        /*margin-right: 35px;*/
    }

    input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_StepNavigationTemplateContainerID_StepNextButton {
        margin-right: 19px;
    }

    .mt-3, .my-3 {
        margin-top: 0rem !important;
    }

    .mt-4, .my-4 {
        margin-top: 1rem !important;
    }

    .mt-x, .my-x {
        margin-top: 1.35rem !important;
    }

    .mt-3, .my-3 {
        margin-top: -0.5rem !important;
    }

    .form-containers {
        margin: 15px 5px 15px 20px;
        border-radius: 8px;
        overflow: hidden;
        transition: height 1s ease-in;
        padding-bottom: 34px;
    }

    .mt-1, .my-1 {
        margin-top: -0.5rem !important;
    }

    .mb-2, .my-2 {
        margin-bottom: -1rem !important;
    }

    .mr-lg-6, .mx-lg-6 {
        margin-right: 23rem !important;
    }

    .form-label {
        font-size: 14px;
        font-family: 'Source Sans Pro',sans-serif;
    }

    label.choose:before {
        background: none repeat scroll 0 0 #f7971f;
        border: 0 none;
        color: #FFFFFF;
        cursor: pointer;
        font-family: 'Altis_Book';
        font-size: 15px;
        padding: 3px 15px;
        width: 98px;
    }

    label.choose:before {
        content: 'Choose file';
        padding: 3px 6px;
        position: absolute;
    }

    .cus-radio > tbody > tr > td {
        display: flex;
        grid-gap: 6px;
        justify-content: center;
        align-items: center;
    }

    .cus-radio > tbody > tr {
        display: flex;
        height: 24px !important;
    }

        .cus-radio > tbody > tr > td > label {
            margin-bottom: 0px
        }

    .daytypecheck {
        margin-top: 10px !important;
    }

    input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_checkepf {
        margin-top: 30px;
    }

    input#ContentPlaceHolder1_EmployeeAditionalInfoWizard_cbBankTranferRequired {
        margin-top: 30px;
    }

    label {
        margin-right: 15px;
    }

    .col-md-12 {
        margin: 15px 5px 15px 20px;
        border-radius: 8px;
        overflow: hidden;
        transition: height 1s ease-in;
        padding-bottom: 34px;
    }

    td.wizard-linkback {
        margin-left: 40px;
    }

    label {
        margin-top: 3px;
        margin-left: 15px;
        font-size: 14px;
    }
</style>
        

<link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/datePicker.css" />
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
        <div class="row">
                <div class="col-md-12" style="border-top-color: black;">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow" style="border-radius: 10px;">
                                    <div class="row">
                                        <div class="col-md-6">
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label12" runat="server" Text="E-Mail" CssClass="form-label"></asp:Label>
                                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtEmail"
                                                        CssClass="validator" ErrorMessage="*" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                                        ValidationGroup="a"> * Invalid Email</asp:RegularExpressionValidator>
                                                    <telerik:RadTextBox ID="txtEmail" runat="server" MaxLength="50" CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label16" runat="server" Text="Contact No (Mobile 1)" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator11" runat="server" ControlToValidate="txtMobileNo"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtMobileNo" runat="server" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label34" runat="server" Text="Contact No (Mobile 2)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtMobileNo2" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label2" runat="server" Text="Contact No (Home 1)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtHomeNo" runat="server" CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label89" runat="server" Text="Contact No (Home 2)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtHomeNo2" runat="server" MaxLength="10" CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label4" runat="server" Text="Office No (Mobile)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtOfficeNo" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label14" runat="server" Text="NIC" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" ControlToValidate="txtNIC"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtNIC" runat="server" CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label15" runat="server" Text="Passport No" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtPassport" runat="server" MaxLength="50" CssClass="form-control form-control-lg" AutoPostBack="true" Skin="Windows7" OnTextChanged="txtPassport_TextChanged" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label17" runat="server" Text="Expiry Date" CssClass="form-label"></asp:Label>
                                                    <telerik:RadDatePicker ID="raddpPasportExpiyDate" runat="server" Culture="en-US" Width="100%" Enabled="false"
                                                        FocusedDate="2011-06-10" MaxDate="2030-12-31" MinDate="1900-01-01" CssClass="form-label"
                                                        Skin="Windows7">
                                                        <Calendar ID="Calendar6" UseRowHeadersAsSelectors="False" UseColumnHeadersAsSelectors="False" ViewSelectorText="x"
                                                            runat="server" Skin="Windows7" CssClass="calender shadow">
                                                        </Calendar>
                                                        <DateInput ID="DateInput6" DisplayDateFormat="dd/MM/yyyy" DateFormat="dd/MM/yyyy" CssClass="form-control form-control-lg"
                                                            runat="server">
                                                        </DateInput>
                                                        <DatePopupButton ImageUrl="/App_Themes/NewTheme/img/calender.png" HoverImageUrl="/App_Themes/NewTheme/img/calender-hover.png"></DatePopupButton>
                                                    </telerik:RadDatePicker>
                                                    <%-- <dx:ASPxDateEdit ID="raddpPasportExpiyDate" runat="server" CssClass="form-control form-control-lg" Enabled="false"></dx:ASPxDateEdit>--%>
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label13" runat="server" Text="Postal Code" CssClass="form-label"></asp:Label>

                                                    <telerik:RadTextBox ID="txtPostalCode" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label19" runat="server" Text="Emergency Contact Person" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator9" runat="server" ControlToValidate="txtEmergencycontactPerson"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencycontactPerson" runat="server" MaxLength="50"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label18" runat="server" Text="Relationship" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator13" runat="server" ControlToValidate="txtRelationship"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtRelationship" runat="server" MaxLength="50" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label20" runat="server" Text="Emergency Mobile No" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator14" runat="server" ControlToValidate="txtEmergencyContactNo"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencyContactNo" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                                <div class="col">
                                                    <asp:Label ID="Label90" runat="server" Text="Emergency Home No" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtEmergencyContactNoHome" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label5" runat="server" Text="Remarks" CssClass="form-label"></asp:Label>

                                                    <telerik:RadTextBox ID="txtRemark" runat="server" MaxLength="500" 
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>
                                        </div>
                                        <div class="col-md-6">
                                            <div class="row" style="margin-top: 11px">
                                                <div class="col">
                                                    <asp:Label ID="Label21" runat="server" Text="Address Line 1" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" ControlToValidate="txtAddress"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtAddress" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label88" runat="server" Text="Address Line 2" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator17" runat="server" ControlToValidate="txtAddress2"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtAddress2" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label79" runat="server" Text="City" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" ControlToValidate="txtAddresscity"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtAddresscity" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4" style="margin-top: 11px">
                                                <div class="col">
                                                    <asp:Label ID="Label81" runat="server" Text="Temporary Address Line 1" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtemporary1" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4" style="margin-top: 11px">
                                                <div class="col">
                                                    <asp:Label ID="Label82" runat="server" Text="Temporary Address Line 2" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtemporary2" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label83" runat="server" Text="Temporary City" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtemporarycity" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />
                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label1" runat="server" Text="Emergency Address Line1" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator22" runat="server" ControlToValidate="txtEmergencyAddressline1"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencyAddressline1" runat="server" Height="35px" MaxLength="50" TextMode="MultiLine"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label48" runat="server" Text="Emergency Address Line2" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator23" runat="server" ControlToValidate="txtEmergencyAddressline2"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencyAddressline2" runat="server"
                                                        CssClass="form-control form-control-lg" Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row mt-4">
                                                <div class="col">
                                                    <asp:Label ID="Label77" runat="server" Text="Emergency City" CssClass="form-label"></asp:Label>
                                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" ControlToValidate="txtEmergencyAddressCity"
                                                        ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a"> *</asp:RequiredFieldValidator>
                                                    <telerik:RadTextBox ID="txtEmergencyAddressCity" runat="server" MaxLength="10" CssClass="form-control form-control-lg"
                                                        Skin="Windows7" />

                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col">
                                                    <asp:Label ID="Label87" Visible="false" runat="server" Text="Contact No (Mobile 3)" CssClass="form-label"></asp:Label>
                                                    <telerik:RadTextBox ID="txtMobileNo3" Visible="false" CssClass="form-control form-control-lg" runat="server" MaxLength="10" Width="200px"
                                                        Skin="Windows7" />
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                     <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">
                                          <asp:Panel ID="btnPanelUpdate" runat="server" Width="233px">
                                    <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnUpdate_Click"
                                        ValidationGroup="Add"  />
                                    <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btnDelete_Click"
                                        OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                       />
                                    <cc1:ConfirmButtonExtender ID="btnDelete_ConfirmButtonExtender" runat="server"
                                        ConfirmText="Are you sure you want to delete ?" Enabled="True"
                                        TargetControlID="btnDelete">
                                    </cc1:ConfirmButtonExtender>
                                    <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btnCancel_Click"
                                        />
                                </asp:Panel>
                                         </div>
                                    </div>
                                </div>
                            </div>
                     </div>
                 <div id="toastrContainer">
                     <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                     <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                 </div>
        <table class="Contact" style="width: 100%">
         
                    <table>
                        
                               
                  
                        <tr>
                            <td>
                                <asp:Panel ID="PanelVisa" runat="server" Visible="False">
                                    <table>
                                        <tr>
                                            <td>Visa No :</td>
                                            <td>
                                                <telerik:RadTextBox ID="txtVisa" runat="server" MaxLength="50"
                                                    Skin="Windows7" Width="150px" EmptyMessage="Visa Number" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>Expiry Date :</td>
                                            <td>
                                                <telerik:RadDatePicker ID="raddpVisaExpiyDate" runat="server" Skin="WebBlue" Width="130px">
                                                    <Calendar Skin="WebBlue" UseColumnHeadersAsSelectors="False"
                                                        UseRowHeadersAsSelectors="False" ViewSelectorText="x">
                                                    </Calendar>
                                                    <DateInput DateFormat="dd/MM/yyyy" DisplayDateFormat="dd/MM/yyyy">
                                                    </DateInput>
                                                    <DatePopupButton HoverImageUrl="" ImageUrl="" />
                                                </telerik:RadDatePicker>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
               
                        <tr>
                            <td>
                              
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>&nbsp;
                                
                                <telerik:RadNotification ID="RadNotification1" runat="server" Animation="Fade" AutoCloseDelay="6000"
                                    Position="Center" Skin="Web20" Width="500px">
                                    <NotificationMenu ID="TitleMenu">
                                    </NotificationMenu>
                                </telerik:RadNotification>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                                    <ProgressTemplate>
                                        <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px;" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;</td>
                            <td>
                          
                                <asp:Panel ID="btnPanel" runat="server" Width="174px" Visible="False">
                                    <telerik:RadButton ID="btnApprove" runat="server" OnClientClick="return confirm('Are you sure you want to Approve this Record?');"
                                        Text="Approve" Skin="WebBlue" OnClick="btnApprove_Click" />
                                    <cc1:ConfirmButtonExtender ID="btnApprove_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to Approve this Record ?"
                                        Enabled="True" TargetControlID="btnApprove">
                                    </cc1:ConfirmButtonExtender>
                                    &nbsp;&nbsp;<telerik:RadButton ID="btnReject" runat="server" OnClientClick="return confirm('Are you sure you want to Reject this Record?');"
                                        Text="Reject" Skin="WebBlue" OnClick="btnReject_Click" />
                                    <cc1:ConfirmButtonExtender ID="btnReject_ConfirmButtonExtender" runat="server" ConfirmText="Are you sure you want to Reject this Record ?"
                                        Enabled="True" TargetControlID="btnReject">
                                    </cc1:ConfirmButtonExtender>
                                </asp:Panel>
                                <br />
                                <asp:LinkButton ID="lbtnModify" runat="server"
                                    OnClick="btnUpdatePersonalInfoEditRequest_Click"
                                    PostBackUrl='<%# "../Employee/EditEmployee.aspx?EmployeeId="+Eval("EmployeeID") %>'
                                    Visible="False">Personal Info Change Request is Pending.</asp:LinkButton>
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="hfCompanyId" runat="server" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

