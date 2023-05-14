<%@ Page Title="HRIS Common |  Workflow Assigning" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="AssignCommonGroups.aspx.cs" Inherits="Common_Settings_AssignCommonGroups" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="CC2" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%--<%@ Register Src="~/TimeAttendance/Master/EmployeeSearch.ascx" TagName="EmployeeSearch" TagPrefix="uc1" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .search {
            float: left;
            padding: 6px;
            margin-top: 8px;
            margin-right: 16px;
            border: none;
            font-size: 14px;
        }

        .chkBoxList {
            padding: 5px 5px 5px 10px;
            border-radius: 4px;
            position: relative;
            cursor: pointer;
            font-size: 14px;
            color: #575757;
        }

            .chkBoxList tr {
                height: 25px;
            }

            .chkBoxList td {
                width: 450px;
            }

            .chkBoxList tr {
                height: 24px;
            }

            .chkBoxList td {
                width: 250px; /* or percent value: 25% */
            }
            .rcbScroll.rcbWidth {
                z-index:1;
                overflow:scroll;
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


        .auto-style2 {
            width: 17px;
        }

        .auto-style4 {
            width: 1200px;
        }

        .auto-style5 {
            width: 663px;
        }

        .auto-style6 {
            width: 750px;
            height: 5px;
        }

        .auto-style7 {
            width: 663px;
            height: 5px;
        }

        .auto-style8 {
            width: 1251px;
        }

        input#ContentPlaceHolder1_txtRootLevel {
            background-color: #f7971f;
            opacity: 1;
            color: white;
            font-size: 15px;
        }
    </style>

            <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />
            <link rel="stylesheet" href="/App_Themes/NewTheme/font-awesome-4.7.0/css/font-awesome.min.css" />



    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js"></script>
    <script type="text/javascript">

        function SearchEmployees(txtSearch, cblEmployees) {
            if ($(txtSearch).val() != "") {
                var count = 0;
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    var match = false;
                    $(this).children('td').children('label').each(function () {
                        if ($(this).text().toUpperCase().indexOf($(txtSearch).val().toUpperCase()) > -1)
                            match = true;
                    });
                    if (match) {
                        $(this).show();
                        count++;
                    }
                    else { $(this).hide(); }
                });
                $('#spnCount').html((count) + ' match');
            }
            else {
                $(cblEmployees).children('tbody').children('tr').each(function () {
                    $(this).show();
                });
                $('#spnCount').html('');
            }
        }



    </script>
    <script type="text/javascript">
        $(document).ready(function () {
            $('#list1Allcheckd').click(function () {
                if ($(this).is(":checked")) {
                    $('[id *= cblEmployees]').find('input[type="list1Allcheckd"]').each(function () {
                        $(this).prop("checked", true);
                    });
                }
                else {
                    $('[id *= cblEmployees]').find('input[type="list1Allcheckd"]').each(function () {
                        $(this).prop("checked", false);
                    });
                }
            });

        });



    </script>


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
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


    <telerik:RadCodeBlock ID="RadCodeBlock1" runat="server">
        <script type="text/javascript">
            function RowDblClick(sender, eventArgs) {
                sender.get_masterTableView().editItem(eventArgs.get_itemIndexHierarchical());
            }
        </script>
    </telerik:RadCodeBlock>
    <script>
        function downloadExcel() {
            $.ajax({
                type: "POST",
                url: "AssignCommonGroups.aspx/downloadExcelFromGrid",
                //data: JSON.stringify({ startDate: date._d.format('dd/MM/yyyy') }),
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: false,
                success: function (data) {

                }
            });
        }

    </script>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>


    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Workflow Assigning</h4>
                    <span onclick="toggleProfileForm(445);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <asp:DataList ID="DataList1" runat="server" RepeatColumns="3" RepeatDirection="Horizontal"
                            OnItemDataBound="DataList1_ItemDataBound" Width="800px" OnSelectedIndexChanged="DataList1_SelectedIndexChanged">
                            <ItemTemplate>
                                <table class="emptable" width="100%">
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td>
                                                        <table>
                                                            <tr>
                                                                <td>&nbsp;
                                                                </td>
                                                                <td class="empinnerStyle">&nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="emphederStyle">
                                                                    <asp:Label ID="Label1" runat="server" Text="Employee Code"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblEmployeeCode" runat="server" Text='<%# Eval("EmployeeCode") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="emphederStyle">
                                                                    <asp:Label ID="Label2" runat="server" Text="Employee Name"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblEmployeeName" runat="server" Text='<%# Eval("FirstName")+" "+Eval("LastName") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="emphederStyle">
                                                                    <asp:Label ID="Label3" runat="server" Text="Date Of Birth"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDateOFBirth" runat="server" Text='<%# Eval("DateOfBirth") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="emphederStyle">
                                                                    <asp:Label ID="Label4" runat="server" Text="NIC No"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblNIC" runat="server" Text='<%# Eval("NIC") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="emphederStyle">
                                                                    <asp:Label ID="Label5" runat="server" Text="Contact No"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblContactNo" runat="server" Text='<%# Eval("MobileNo") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="emphederStyle">
                                                                    <asp:Label ID="Label6" runat="server" Text="Designation"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblDesignation" runat="server" Text='<%# Eval("DesignationName") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td class="emphederStyle">
                                                                    <asp:Label ID="Label7" runat="server" Text="Organization"></asp:Label>
                                                                </td>
                                                                <td>
                                                                    <asp:Label ID="lblOrganization" runat="server" Text='<%# Eval("OrganizationTypeName") %>'></asp:Label>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:HiddenField ID="hfStatus" runat="server" Value='<%# Eval("Status") %>' />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                        <td>
                                            <asp:Image ID="imgEmployee" runat="server" ImageUrl='<%# "~/Common/EmployeeImages/"+Eval("Image") %>'
                                                Width="120px" ImageAlign="Right" />
                                        </td>
                                    </tr>
                                </table>
                            </ItemTemplate>
                        </asp:DataList>
                    </td>
                </tr>
            </table>
            <tr>
                <td class="auto-style8">&nbsp;
                </td>
            </tr>

            <tr>
                <td>
                    <asp:Label ID="lblEmpCode" runat="server" ForeColor="Black" />
                </td>
                <td>
                    <asp:Label ID="lblEmpName" runat="server" ForeColor="Black" />
                </td>
            </tr>
            <tr>
                <td></td>
                <td>
                    <telerik:RadTextBox ID="radtxtCardNo" runat="server" Visible="false">
                    </telerik:RadTextBox>
                </td>
            </tr>

            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px">
                        <div class="card-body shadow" >
                            <div class="row mt-3">
                                <div class="col-md-6">
                                   <asp:TextBox ID="txtRootLevel" runat="server" MaxLength="50" Width="100%" CssClass="form-control form-control-lg"
                                        ReadOnly="True" AutoPostBack="True" OnPreRender="txtRootLevel_PreRender" />
                                    <cc1:TextBoxWatermarkExtender ID="txtRootLevel_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txtRootLevel" WatermarkText="Click To Select Department">
                                    </cc1:TextBoxWatermarkExtender>
                                   <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                                                CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPane2"
                                                TargetControlID="txtRootLevel">
                                            </cc1:ModalPopupExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="txtRootLevel"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a" Visible="False"></asp:RequiredFieldValidator>
                                        <%--    <asp:Button ID="btnOrganizationSelect" runat="server" CssClass="Buttons2" Text="Select Department"
                                                BackColor="WhiteSmoke" BorderColor="WindowFrame" ForeColor="Black" Font-Size="14px" />
                                            <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                                                CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPanel"
                                                TargetControlID="btnOrganizationSelect">
                                            </cc1:ModalPopupExtender>--%>
                                           <%-- &nbsp;&nbsp;<asp:Label ID="lblOrganization" runat="server"></asp:Label>--%>
                                            <asp:HiddenField ID="hfOrganizationStructure" runat="server" />
                                            <asp:HiddenField ID="hfPreviousOrgId" runat="server" />
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="lblApprove" runat="server" Text=" Approval Type" CssClass="form-label" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="radcbApprovalType" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="radcbApprovalType" runat="server" AutoPostBack="True" CssClass="form-control form-control-lg"
                                        BackColor="Transparent" DataSourceID="SqlDsApprovalType" Enabled="false" NullText="&lt;&lt; Select Approval Type &gt;&gt;"
                                        OnSelectedIndexChanged="radcbApprovalType_SelectedIndexChanged" Skin="Windows7" TextField="ApprovalTypeName" ValueField="ApprovalTypeID">
                                    </dx:ASPxComboBox>
                                    <asp:SqlDataSource ID="SqlDsApprovalType" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="SELECT Distinct(ApprovalType.ApprovalTypeName),Com_CommonApprovalWorkFlow.ApprovalTypeID
                                                        FROM   Com_CommonApprovalWorkFlow INNER JOIN 
                                                        ApprovalType ON ApprovalType.ApprovalTypeID = Com_CommonApprovalWorkFlow.ApprovalTypeID And Com_CommonApprovalWorkFlow.CompanyId=@CompanyId">
                                        <SelectParameters>
                                            <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label9" runat="server" Text="Group Name" CssClass="form-label" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="ddlLeaveGroups" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <dx:ASPxComboBox ID="ddlLeaveGroups" runat="server" BackColor="Transparent" Enabled="false" CssClass="form-control form-control-lg"
                                        NullText="&lt;&lt; Select Group Name &gt;&gt;" OnSelectedIndexChanged="ddlLeaveGroups_SelectedIndexChanged" Skin="Windows7"
                                        TextField="ApprovalGroupName" ValueField="ApprovalWorkFlowId">
                                    </dx:ASPxComboBox>
                                    <asp:HiddenField ID="HiddenField1" runat="server" />
                                    <asp:SqlDataSource ID="dsLeaveGroups" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand="select  ApprovalWorkFlowId,ApprovalGroupName,ApprovalTypeID
                                                        from Com_CommonApprovalWorkFlow 
                                                        Where IsActive=1 AND ApprovalTypeID=@ApprovalTypeID">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="radcbApprovalType" Name="ApprovalTypeID" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label10" runat="server" Text="Employee" CssClass="form-label" />
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="RadComboBox2" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                    <telerik:RadComboBox ID="RadComboBox2" runat="server" CheckBoxes="true" CssClass="telerik-radcombo" CheckedItemsTexts="DisplayAllInInput"
                                        EnableCheckAllItemsCheckBox="true" DataTextField="FullName"
                                        DataValueField="EmployeeId" Enabled="false" OnClientLoad="OnClientLoadComboBox" OnTextChanged="RadComboBox2_TextChanged" MaxHeight="100px">
                                        <HeaderTemplate>
                                            <telerik:RadTextBox ID="RadTextBox1" runat="server" ClientEvents-OnLoad="OnLoad" CssClass="form-control form-control-sm">
                                            </telerik:RadTextBox>
                                        </HeaderTemplate>
                                    </telerik:RadComboBox>
                                    <asp:SqlDataSource ID="getEmployeescommon" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand=" WITH OrgStruc AS(  select OrgStructureID, ParentID,LevelIndex,OrganizationTypeID,CompanyId from OrganisationStructure WHERE OrganisationStructure.CompanyId=@CompanyId AND (OrgStructureID=@OrgStructureID OR @OrgStructureID=0)
                                                                       UNION ALL  select OG.OrgStructureID,OG.ParentID,OG.LevelIndex,OG.OrganizationTypeID,OG.CompanyId from OrganisationStructure OG 
                                                                       INNER JOIN OrgStruc  OS ON OG.ParentID=OS.OrgStructureID )  
                                                                        SELECT EmployeeId, EmployeeCode, CASE
		                                                                WHEN CompanyProfile.EmployeeReportViewName=1 THEN Employee.EmployeeCode+' | '+Employee.FirstName
		                                                                WHEN CompanyProfile.EmployeeReportViewName=2 THEN Employee.EmployeeCode+' | '+Employee.LastName
		                                                                WHEN CompanyProfile.EmployeeReportViewName=3 THEN Employee.EmployeeCode+' | '+Employee.CallName
		                                                                WHEN CompanyProfile.EmployeeReportViewName=4 THEN Employee.EmployeeCode+' | '+Employee.NameWithInitial+' '+Employee.LastName
		                                                                WHEN CompanyProfile.EmployeeReportViewName=5 THEN Employee.EPFNo+' | '+Employee.FirstName
		                                                                WHEN CompanyProfile.EmployeeReportViewName=6 THEN Employee.EPFNo+' | '+Employee.LastName
		                                                                WHEN CompanyProfile.EmployeeReportViewName=7 THEN Employee.EPFNo+' | '+Employee.CallName
		                                                                ELSE Employee.EPFNo+' | '+Employee.NameWithInitial+' '+Employee.LastName
		                                                                END AS FullName, OrgStructureID 
                                                                        FROM Employee AS Employee INNER JOIN
		                                                                CompanyProfile ON CompanyProfile.CompanyID=Employee.CompanyID
                                                                        WHERE  OrgStructureID IN(SELECT OrgStructureID FROM OrgStruc )  and  Employee.CompanyId=@CompanyId AND Employee.EmployeeId
                                                                       NOT IN ( SELECT EmployeeId FROM Com_CommonAssignData) AND Employee.Active=1 AND Employee.InactiveStatus=1 Order By Convert(int,EmployeeCode)">
                                        <SelectParameters>
                                            <asp:ControlParameter ControlID="hfOrganizationStructure" DefaultValue="0" Name="OrgStructureID" PropertyName="Value" Type="Int32" />
                                            <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:SqlDataSource>
                                </div>
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="row mt-3">
                            </div>
                            <div class="d-flex justify-content-end mr-lg-5" style="grid-gap: 15px">
                                <div>
                                    <telerik:RadButton ID="btnSave" runat="server" Enabled="false" OnClick="radbtnSave_Click" Text="Save"  ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate"  Enabled="false" runat="server" OnClick="radbtnUpdate_Click" Text="Update"
                                        ValidationGroup="a" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="radbtnCancel_Click" Text="Cancel" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
            <td>

                <asp:Label ID="Label7" runat="server" ForeColor="Red"></asp:Label>
            </td>

            <asp:HiddenField ID="hfEmployeeId" runat="server" />
            <div id="toastrContainer">
                <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
            </div>

            <asp:SqlDataSource ID="SqlDataSource5" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand=" WITH OrgStruc AS( SELECT OrgStructureID, ParentID,LevelIndex,OrganizationTypeID,CompanyId 
					                    FROM OrganisationStructure 
					                    WHERE OrganisationStructure.CompanyId=@CompanyId AND OrgStructureID=@OrgStructureID
                                        UNION ALL  select OG.OrgStructureID,OG.ParentID,OG.LevelIndex,OG.OrganizationTypeID,OG.CompanyId from OrganisationStructure OG 
                                        INNER JOIN OrgStruc  OS ON OG.ParentID=OS.OrgStructureID )  
                                        SELECT     dbo.Employee.EmployeeCode+'/'+ dbo.Employee.FirstName AS FirstName,Employee.EmployeeId
                                        FROM            dbo.Employee 
                                        WHERE  OrgStructureID IN(SELECT OrgStructureID FROM OrgStruc )  and  Employee.CompanyId=@CompanyId">
                <SelectParameters>
                    <asp:ControlParameter ControlID="hfOrganizationStructure" Name="OrgStructureID" PropertyName="Value" Type="Int32" />
                    <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
            <asp:SqlDataSource ID="dsEmployeeTypes" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>" SelectCommand=" select EmploymentTypeID,EmploymentTypeName  +' - '+ EmploymentTypeCode AS EmploymentTypeCode  from EmploymentType "></asp:SqlDataSource>
        </div>
        <asp:HiddenField ID="hfCommonAssignDataId" runat="server" />
        <div class="row" style="position: relative">
            <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                <ContentTemplate>
                    <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                </ContentTemplate>
            </telerik:RadButton>
            <div class="col table-scroll">
                <dx:ASPxGridView ID="grEmployeeLeaveSetup" ClientInstanceName="grid" runat="server" ForeColor="Black" Theme="MetropolisBlue"
                    KeyFieldName="CommonAssignDataId" AutoGenerateColumns="False" CssClass="tableStyle" ClientIDMode="AutoID" DataSourceID="dsEmploueeLeaveSetup">
                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                    <Columns>
                        <dx:GridViewDataColumn FieldName="CommonAssignDataId" Caption="Id" PropertiesEditType="TextBox" Width="90" Visible="false" />
                        <dx:GridViewDataColumn FieldName="EmployeeId" HeaderStyle-Font-Bold="false"
                            Caption="EmployeeId" PropertiesEditType="TextBox" Width="90" Visible="false" />
                        <dx:GridViewDataColumn FieldName="EmployeeCode" HeaderStyle-Font-Bold="false"
                            Caption="Employee Code" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="NameWithInitials" HeaderStyle-Font-Bold="false"
                            Caption="Employee Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="ApprovalTypeName" HeaderStyle-Font-Bold="false"
                            Caption="Approval Type" PropertiesEditType="TextBox" Width="200px" VisibleIndex="3" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="ApprovalGroupName" HeaderStyle-Font-Bold="false" 
                            Caption="Group Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="4" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                        <dx:GridViewDataColumn FieldName="IsActive" HeaderStyle-Font-Bold="false"
                            Caption="Active" Width="60" VisibleIndex="5" Visible="false" />
                        <dx:GridViewDataColumn FieldName="ApprovalTypeID" HeaderStyle-Font-Bold="false"
                            Caption="ApprovalTypeID" Width="70" VisibleIndex="5" Visible="false" />

                        <dx:GridViewDataColumn FieldName="ApprovalWorkFlowId" Caption="ApprovalWorkFlowId" Width="70" Visible="false" />

                        <dx:GridViewDataTextColumn Caption="" Width="30px"  CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" VisibleIndex="0">
                            <DataItemTemplate>
                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" ForeColor="Blue"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                            </DataItemTemplate>
                        </dx:GridViewDataTextColumn>
                    </Columns>
                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                        <AlternatingRow Enabled="true" />
                    </Styles>
                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />

                    <SettingsPager Mode="ShowPager" PageSize="11" />
                </dx:ASPxGridView>

                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="grEmployeeLeaveSetup" FileName="Workflow Assigning">
                    <Styles>
                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                        </Header>
                    </Styles>
                </dx:ASPxGridViewExporter>
            </div>
        </div>

        <tr>
            <td colspan="2">
                <asp:SqlDataSource ID="dsEmploueeLeaveSetup" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="Select CommonAssignDataId,Com_CommonAssignData.ApprovalTypeID,Com_CommonApprovalWorkFlow.ApprovalGroupCode,Employee.EmployeeCode,Com_CommonApprovalWorkFlow.ApprovalWorkFlowId,
                                            Com_CommonApprovalWorkFlow.ApprovalGroupName,Com_CommonAssignData.EmployeeId,CASE
					                        WHEN CompanyProfile.EmployeeNameStatus=1 THEN Employee.NameWithInitial+' '+Employee.LastName
					                        ELSE Employee.FirstName+' '+Employee.LastName
				                            END AS NameWithInitials,
				                            Com_CommonApprovalWorkFlow.IsActive,ApprovalType.ApprovalTypeName 
                                            from Com_CommonAssignData INNER JOIN 
                                            Com_CommonApprovalWorkFlow ON Com_CommonApprovalWorkFlow.ApprovalWorkFlowId = Com_CommonAssignData.ApprovalWorkFlowId INNER JOIN 
                                            ApprovalType ON Com_CommonApprovalWorkFlow.ApprovalTypeID = ApprovalType.ApprovalTypeID  INNER JOIN 
                                            Employee ON Employee.EmployeeId =Com_CommonAssignData.EmployeeId INNER JOIN
									        CompanyProfile ON CompanyProfile.CompanyID= Employee.CompanyID
                                            WHERE  (Com_CommonApprovalWorkFlow.ApprovalTypeID=@ApprovalTypeID Or @ApprovalTypeID = 0) and Employee.CompanyId = @CompanyId And Employee.Active = 1
                                            Order By ApprovalTypeID,ApprovalWorkFlowId,Convert(int, EmployeeCode)">

                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfEmployeeId" Name="EmployeeId" Type="Int32" DefaultValue="0" />
                        <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                        <asp:ControlParameter ControlID="radcbApprovalType" Name="ApprovalTypeID" Type="Int32" DefaultValue="0" />
                    </SelectParameters>
                </asp:SqlDataSource>
            </td>
        </tr>
        <asp:HiddenField ID="hfAassignId" runat="server" />
        <%--    <tr id="Tr2" runat="server">
                <td colspan="2"></td>
            </tr>--%>

        <div>
            <asp:Panel ID="PopupPane2" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical">
                <div class="style1">
                    <div style="float: right">
                        <asp:ImageButton ID="btnClosePopup" runat="server"
                            ImageUrl="~/App_Themes/NewTheme/img/close.png" />
                    </div>
                    <div id="org-stucture">
                    </div>
                </div>
            </asp:Panel>

<%--            <div id="PopupPane2" class="shadow-sm" style="background-color: white; border-radius: 6px">
                <asp:Panel ID="Panel2" runat="server" CssClass="popstyle">
                    <div>
                        <div class="row">
                            <div class="col-12 d-flex justify-content-end">
                                <asp:ImageButton ID="btnClosePopup" CssClass="btn-close" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                            </div>
                        </div>
                        <div class="row">
                            <div class="row">
                                <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                            </div>
                        </div>
                    </div>
                </asp:Panel>
            </div>--%>
        </div>
    </div>
    </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>

