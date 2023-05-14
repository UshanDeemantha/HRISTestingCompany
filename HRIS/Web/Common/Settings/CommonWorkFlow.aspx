<%@ Page Title="HRIS Common |  Approval WorkFlows" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="CommonWorkFlow.aspx.cs" Inherits="Common_Settings_CommonWorkFlow" %>

<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }

        .auto-style2 {
            width: 266px;
        }

        .auto-style4 {
            height: 47px;
            width: 1231px;
        }

        .auto-style5 {
            width: 1657px;
            height: 47px;
        }

        .auto-style6 {
            width: 400px;
        }

        .auto-style7 {
            width: 1657px;
        }

        .auto-style8 {
            width: 500px;
        }

        .auto-style9 {
            width: 500px;
        }        
        .form-control:disabled {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            background-color:#f9fafb!important;
        }
        .ml-4, .mx-4 {
    margin-left: 2.5rem!important;
}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <div id="FormContentArea">
        <script>
            function downloadExcel() {
                $.ajax({
                    type: "POST",
                    url: "CommonWorkFlow.aspx/downloadExcelFromGrid",
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
                        <h4 class="header">Approval WorkFlows</h4>
                        <span onclick="toggleProfileForm(505);" id="profileButton" class="dot shadow"></span>
                    </div>
                </div>
                <div class="row" style="background-color: ghostwhite">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border" style="border-radius: 10px;">
                            <div class="card-body shadow">
                                <div class="row mt-3">
                                    <div class="col-md-3">
                                        <asp:Label ID="lblApprove" CssClass="form-label" runat="server" Text="Approval Type"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="radcbApprovalType" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        <dx:ASPxComboBox ID="radcbApprovalType" runat="server" CssClass="form-control form-control-lg" DataSourceID="SqlDsApprovalType" BackColor="Transparent"
                                            TextField="ApprovalTypeName" ValueField="ApprovalTypeID" Skin="Windows7" NullText="Please Select Approval Type Name">
                                        </dx:ASPxComboBox>
                                    </div>
                                    <div class="col-md-2">
                                        <asp:Label ID="Label6" CssClass="form-label" runat="server" Text="Code"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtGroupCode" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        <telerik:RadTextBox ID="txtGroupCode" CssClass="form-control form-control-lg" runat="server" MaxLength="50" Skin="Windows7"
                                            Width="220px" />
                                    </div>
                                    <div class="col-md-1 ml-4">
                                        <asp:Label ID="Label4" CssClass="form-label" runat="server" Text="Active"></asp:Label><br />
                                        <asp:CheckBox ID="cbActive" runat="server" Checked="true" Skin="Windows7" CssClass="commongroupcheck"/>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label9" CssClass="form-label" runat="server" Text="Name"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtGroupName" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        <telerik:RadTextBox ID="txtGroupName" CssClass="form-control form-control-lg" runat="server" MaxLength="100" Skin="Windows7"
                                            Width="220px" />
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Approval Person (1st)"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" ControlToValidate="radcbFirstApprovePerson" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        <dx:ASPxComboBox ID="radcbFirstApprovePerson" runat="server" CssClass="form-control form-control-lg"
                                            DataSourceID="sqldsFirstApprovePerson" BackColor="Transparent" OnSelectedIndexChanged="radcbFirstApprovePerson_SelectedIndexChanged"
                                            TextField="FullName" ValueField="EmployeeID" Skin="Windows7" NullText="Please Select 1st Approval Person" AutoPostBack="True">
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label11" runat="server" CssClass="form-label" Text="Approval Person (2nd)"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" ControlToValidate="radcbSecondApprovePerson" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        <dx:ASPxComboBox ID="radcbSecondApprovePerson" runat="server" CssClass="form-control form-control-lg"
                                            DataSourceID="sqldsSecondApprovePerson" BackColor="Transparent" OnSelectedIndexChanged="radcbSecondApprovePerson_SelectedIndexChanged"
                                            TextField="FullName" ValueField="EmployeeID" Skin="Windows7" NullText="Please Select 2nd Approval Person" AutoPostBack="True" >
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label3" runat="server" CssClass="form-label" Text="Approval Person (3rd)"></asp:Label>
                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="radcbThirdApprovePerson" CssClass="form-label"
                                            ErrorMessage="Required Field!" ForeColor="Red" ValidationGroup="a">*</asp:RequiredFieldValidator>
                                        <dx:ASPxComboBox ID="radcbThirdApprovePerson" CssClass="form-control form-control-lg" runat="server"
                                            DataSourceID="sqldsThirdApprovePerson" BackColor="Transparent" OnSelectedIndexChanged="radcbThirdApprovePerson_SelectedIndexChanged"
                                            TextField="FullName" ValueField="EmployeeID" Skin="Windows7" NullText="Please Select 3rd Approval Person" AutoPostBack="True" >
                                        </dx:ASPxComboBox>
                                    </div>
                                </div>
                                <div class="row mt-3">
                                </div>
                                <div class="row mt-3">
                                </div>
                                <div class="row mt-3">
                                </div>
                                <div class="d-flex justify-content-end pr-2 mt-3" style="grid-gap: 15px">
                                    <div>
                                        <telerik:RadButton ID="radbtnSave" runat="server" OnClick="radbtnSave_Click" Text="Save"
                                            ValidationGroup="a" Skin="WebBlue" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="radbtnUpdate" runat="server" OnClick="radbtnUpdate_Click"
                                            Text="Update" ValidationGroup="a" Skin="WebBlue" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="radbtnDelete" runat="server" OnClick="radbtnReset_Click"
                                            OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                            Text="Reset" Skin="WebBlue" />
                                    </div>
                                    <div>
                                        <telerik:RadButton ID="radbtnCancel" runat="server" OnClick="radbtnCancel_Click"
                                            Text="Cancel" Skin="WebBlue" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>

                <asp:SqlDataSource ID="SqlDsApprovalType" runat="server"
                    ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT [ApprovalTypeID], [ApprovalTypeName] FROM [ApprovalType]"></asp:SqlDataSource>

                <asp:Label ID="Label7" runat="server" ForeColor="Red"></asp:Label>
             </div>
            <div class="ContentBlock">
                <asp:Label ID="Label5" runat="server" Height="10" Text=""></asp:Label>
                <div class="ContentBlockDetail">
                </div>
            </div>
            <div class="ContentBlock">
                <div class="ContentBlockHeader">
                    <div class="ContentProgressIcon">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </div>
                </div>
                <div id="toastrContainer">
                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                    <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                </div>
            </div>
            <div class="ContentBlock">
                <div class="ContentBlockHeader">
                </div>

                <div class="row" style="position: relative">
                    <telerik:RadButton ID="RadButton1" runat="server" CssClass="download-excel" OnClick="RadButton1_Click">
                        <ContentTemplate>
                            <img src="../../../App_Themes/NewTheme/img/exceldwn.png" />
                        </ContentTemplate>
                    </telerik:RadButton>
                    <div class="col table-scroll">
                        <dx:ASPxGridView ID="grEmployeeLeaveSetup" CssClass="tableStyle" ClientInstanceName="grid" runat="server" ForeColor="Black" Theme="MetropolisBlue"
                            KeyFieldName="ApprovalWorkFlowId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="dsEmploueeLeaveSetup">
                            <SettingsDataSecurity AllowDelete="False" AllowInsert="False" />
                            <SettingsSearchPanel Visible="true" HighlightResults="true" />
                            <Columns>
                                <%--             <dx:GridViewDataColumn FieldName="ApprovalTypeName" Caption="Approval Type Name" GroupIndex="0" PropertiesEditType="TextBox" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" >
                                    <HeaderStyle BackColor="#3399FF" HorizontalAlign="Center" />
                                </dx:GridViewDataColumn>--%>
                                <dx:GridViewDataColumn FieldName="ApprovalWorkFlowId" Caption="Id" PropertiesEditType="TextBox" Width="100" Visible="false" />
                                <%--  <dx:GridViewDataColumn FieldName="ApprovalTypeName" Caption="Approval Type Name" PropertiesEditType="TextBox" Width="100" VisibleIndex="1" />--%>
                                <dx:GridViewDataColumn FieldName="IsActive" CellStyle-HorizontalAlign="Left" HeaderStyle-Font-Bold="false" Caption="Active" Width="70px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovalGroupCode" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Caption="Code" PropertiesEditType="TextBox" Width="200px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovalGroupName" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Caption="Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovePerson1Name" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Caption="1st Approve Person" PropertiesEditType="TextBox" Width="200px" VisibleIndex="3" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovePerson1Code" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Visible="false" Caption="1st Code" PropertiesEditType="TextBox" Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovePerson2Name" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Caption="2nd Approve Person" PropertiesEditType="TextBox" Width="200px" VisibleIndex="4" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovePerson2Code" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Visible="false" Caption="2nd Code" PropertiesEditType="TextBox" Width="100px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovePerson3Name" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Caption="3rd Approve Person" PropertiesEditType="TextBox" Width="200px" VisibleIndex="5" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovePerson3Code" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Visible="false" PropertiesEditType="TextBox" Width="100px" VisibleIndex="6" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                <dx:GridViewDataColumn FieldName="ApprovalTypeID" HeaderStyle-Font-Bold="false" CellStyle-HorizontalAlign="Left" Caption="ApprovalTypeID" PropertiesEditType="TextBox" Visible="false" />
                                <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" VisibleIndex="0">
                                    <DataItemTemplate>
                                        <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" /></asp:LinkButton>
                                    </DataItemTemplate>
                                    <CellStyle HorizontalAlign="Center" />
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
                        <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="grEmployeeLeaveSetup" FileName="Approval WorkFlows">
                            <Styles>
                                <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                </Header>
                            </Styles>
                        </dx:ASPxGridViewExporter>
                    </div>
                </div>

                <%-- Data Source --%>
                <asp:SqlDataSource ID="dsEmploueeLeaveSetup" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand=" Select  Lf.ApprovalWorkFlowId,Lf.ApprovalTypeID,AT.ApprovalTypeName,Lf.ApprovalGroupName,Lf.ApprovalGroupCode,Lf.IsActive,
                                                EP1.EmployeeCode as EmployeeCode1,EP2.EmployeeCode as EmployeeCode2,EP3.EmployeeCode as EmployeeCode3,
												LF.ApprovePerson1 ApprovePerson1Code,CASE
												WHEN CompanyProfile.EmployeeNameStatus=1 THEN EP1.NameWithInitial+' '+EP1.LastName
												ELSE EP1.FirstName+' '+EP1.LastName
												END AS ApprovePerson1Name, 
                                                LF.ApprovePerson2 ApprovePerson2Code,CASE
												WHEN CompanyProfile.EmployeeNameStatus=1 THEN EP2.NameWithInitial+' '+EP2.LastName
												ELSE EP2.FirstName+' '+EP2.LastName
												END AS ApprovePerson2Name,
												LF.ApprovePerson3 ApprovePerson3Code,CASE
												WHEN CompanyProfile.EmployeeNameStatus=1 THEN EP3.NameWithInitial+' '+EP3.LastName
												ELSE EP3.FirstName+' '+EP3.LastName
												END AS ApprovePerson3Name,
												LF.ApprovalTypeID
                                                from Com_CommonApprovalWorkFlow LF LEFT JOIN  
												Employee EP1 ON EP1.EmployeeId=LF.ApprovePerson1 LEFT JOIN  
												Employee EP2 ON EP2.EmployeeId=LF.ApprovePerson2 LEFT JOIN  
												Employee EP3 ON EP3.EmployeeId=LF.ApprovePerson3 LEFT JOIN  
                                                ApprovalType AT ON AT.ApprovalTypeID = LF.ApprovalTypeID INNER JOIN
												CompanyProfile ON CompanyProfile.CompanyID= Lf.CompanyID
                                                Where Lf.CompanyId=@CompanyId">
                    <SelectParameters>
                        <asp:SessionParameter Name="CompanyId" SessionField="CompanyId" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>

                &nbsp;<cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
                </cc1:ToolkitScriptManager>
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentBlockHeader">
                <asp:SqlDataSource ID="sqldsEmployee" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="SELECT EmployeeId, EmployeeCode, CASE
		                            WHEN CompanyProfile.EmployeeReportViewName=1 THEN Employee_1.EmployeeCode+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=2 THEN Employee_1.EmployeeCode+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=3 THEN Employee_1.EmployeeCode+' | '+Employee_1.CallName
		                            WHEN CompanyProfile.EmployeeReportViewName=4 THEN Employee_1.EmployeeCode+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=5 THEN Employee_1.EPFNo+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=6 THEN Employee_1.EPFNo+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=7 THEN Employee_1.EPFNo+' | '+Employee_1.CallName
		                            ELSE Employee_1.EPFNo+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            END AS FullName, OrgStructureID 
                                    FROM Employee AS Employee_1 INNER JOIN
		                            CompanyProfile ON CompanyProfile.CompanyID=Employee_1.CompanyID WHERE EmployeeId NOT IN (@SecondApprovePerson,@ThirdApprovePerson) Order By Convert(int,EmployeeCode)"></asp:SqlDataSource>
                <asp:SqlDataSource ID="sqldsFirstApprovePerson" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand=" SELECT EmployeeId, EmployeeCode, CASE
		                            WHEN CompanyProfile.EmployeeReportViewName=1 THEN Employee_1.EmployeeCode+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=2 THEN Employee_1.EmployeeCode+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=3 THEN Employee_1.EmployeeCode+' | '+Employee_1.CallName
		                            WHEN CompanyProfile.EmployeeReportViewName=4 THEN Employee_1.EmployeeCode+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=5 THEN Employee_1.EPFNo+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=6 THEN Employee_1.EPFNo+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=7 THEN Employee_1.EPFNo+' | '+Employee_1.CallName
		                            ELSE Employee_1.EPFNo+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            END AS FullName, OrgStructureID 
                                    FROM Employee AS Employee_1 INNER JOIN
		                            CompanyProfile ON CompanyProfile.CompanyID=Employee_1.CompanyID WHERE EmployeeId NOT IN (@SecondApprovePerson,@ThirdApprovePerson) Order By Convert(int,EmployeeCode)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="radcbSecondApprovePerson" Name="SecondApprovePerson" PropertyName="Value" DefaultValue="0" />
                        <asp:ControlParameter ControlID="radcbThirdApprovePerson" Name="ThirdApprovePerson" PropertyName="Value" DefaultValue="0" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqldsSecondApprovePerson" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand=" SELECT EmployeeId, EmployeeCode, CASE
		                            WHEN CompanyProfile.EmployeeReportViewName=1 THEN Employee_1.EmployeeCode+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=2 THEN Employee_1.EmployeeCode+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=3 THEN Employee_1.EmployeeCode+' | '+Employee_1.CallName
		                            WHEN CompanyProfile.EmployeeReportViewName=4 THEN Employee_1.EmployeeCode+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=5 THEN Employee_1.EPFNo+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=6 THEN Employee_1.EPFNo+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=7 THEN Employee_1.EPFNo+' | '+Employee_1.CallName
		                            ELSE Employee_1.EPFNo+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            END AS FullName, OrgStructureID 
                                    FROM Employee AS Employee_1 INNER JOIN
		                            CompanyProfile ON CompanyProfile.CompanyID=Employee_1.CompanyID WHERE EmployeeId NOT IN (@FirstApprovePerson,@ThirdApprovePerson) Order By Convert(int,EmployeeCode)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfFirstApprovePerson" Name="FirstApprovePerson" PropertyName="Value" />
                        <asp:ControlParameter ControlID="radcbThirdApprovePerson" Name="ThirdApprovePerson" PropertyName="Value" DefaultValue="0" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:SqlDataSource ID="sqldsThirdApprovePerson" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand=" SELECT EmployeeId, EmployeeCode, CASE
		                            WHEN CompanyProfile.EmployeeReportViewName=1 THEN Employee_1.EmployeeCode+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=2 THEN Employee_1.EmployeeCode+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=3 THEN Employee_1.EmployeeCode+' | '+Employee_1.CallName
		                            WHEN CompanyProfile.EmployeeReportViewName=4 THEN Employee_1.EmployeeCode+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=5 THEN Employee_1.EPFNo+' | '+Employee_1.FirstName
		                            WHEN CompanyProfile.EmployeeReportViewName=6 THEN Employee_1.EPFNo+' | '+Employee_1.LastName
		                            WHEN CompanyProfile.EmployeeReportViewName=7 THEN Employee_1.EPFNo+' | '+Employee_1.CallName
		                            ELSE Employee_1.EPFNo+' | '+Employee_1.NameWithInitial+' '+Employee_1.LastName
		                            END AS FullName, OrgStructureID 
                                    FROM Employee AS Employee_1 INNER JOIN
		                            CompanyProfile ON CompanyProfile.CompanyID=Employee_1.CompanyID WHERE EmployeeId NOT IN(@FirstApprovePerson,@SecondApprovePerson) Order By Convert(int,EmployeeCode)">
                    <SelectParameters>
                        <asp:ControlParameter ControlID="hfFirstApprovePerson" Name="FirstApprovePerson" PropertyName="Value" DefaultValue="0" />
                        <asp:ControlParameter ControlID="hfSecondApprovePerson" Name="SecondApprovePerson" PropertyName="Value" DefaultValue="0" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <br />

                <asp:HiddenField ID="hfEmployeeId" runat="server" />
                <asp:HiddenField ID="hfFirstApprovePerson" runat="server" />
                <asp:HiddenField ID="hfSecondApprovePerson" runat="server" />
                <asp:HiddenField ID="hfThirdApprovePerson" runat="server" />
            </div>
            <div class="ContentBlockDetail">
            </div>
        </div>
        <div class="ContentBlock">
            <div class="ContentGridArea">
                <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                    SelectCommand="Lev_GetLeaveEntitlements" SelectCommandType="StoredProcedure">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="0" Name="CompanyId" Type="Int32" />
                    </SelectParameters>
                </asp:SqlDataSource>
                <asp:HiddenField ID="hfCompanyId" runat="server" />
            </div>
        </div>
        <asp:HiddenField ID="hfApproveId" runat="server" />
    </div>
    </div>
</asp:Content>
