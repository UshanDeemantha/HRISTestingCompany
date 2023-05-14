<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmployeeAditionalInfoContol.ascx.cs" Inherits="Employee_EmployeeAditionalInfoContol" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<style type="text/css">
    .Buttons {
        margin: 1px;
        font-family: Arial, Helvetica, sans-serif;
        font-size: 11px;
        font-weight: bold;
        color: #FFFFFF;
        background-color: #0099CC;
        border: 1px solid #006699;
        height: 20px;
    }

    .auto-style2 {
        margin-left: 20px;
    }

    .ContentBlockHeader {
        height: auto;
        float: left;
        /*width: 250px;*/
        padding: 5px 0px 0px 5px;
        text-align: right;
    }

    .ContentProgressIcon {
        height: auto;
        float: right;
        width: 40px;
        padding: 5px 5px 5px 5px;
    }

    img {
        border: none;
    }
    td.wizard-linkback {
    margin-left: 36px;
}
    .d-flex.justify-content-end.mr-lg-6 {
        margin-right: 262px;
    }
    td.wizard-hed {
    padding-top: 21px;
}
</style>
<contenttemplate>
            <script type="terxt/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="row">
                <div class="col-md-12" style="border-top-color: black; margin-left:20px">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow" style="border-radius: 10px;">
                                    <div class="row">
                                            <div class="col-md-6">
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label1" runat="server" Text="Blood Group" CssClass="form-label"></asp:Label>
                                                        <telerik:RadComboBox ID="ddlBloodGroup" runat="server" Width="100%"
                                                            Skin="Windows7">
                                                            <Items>
                                                                <telerik:RadComboBoxItem runat="server" Text="Please Select the Blood Group"
                                                                    Value="0" />
                                                                <telerik:RadComboBoxItem runat="server" Text="A+"
                                                                    Value="A+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="A-"
                                                                    Value="A-" />
                                                                <telerik:RadComboBoxItem runat="server" Text="B+"
                                                                    Value="B+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="B-"
                                                                    Value="B-" />
                                                                <telerik:RadComboBoxItem runat="server" Text="AB+"
                                                                    Value="AB+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="AB-"
                                                                    Value="AB-" />
                                                                <telerik:RadComboBoxItem runat="server" Text="O+"
                                                                    Value="O+" />
                                                                <telerik:RadComboBoxItem runat="server" Text="O-"
                                                                    Value="O-" />
                                                            </Items>
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label91" runat="server" Text="Nationality" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlNationality" runat="server"
                                                            DataSourceID="odsNationality" DataTextField="NationalityName"
                                                            DataValueField="NationalityID" Width="100%" Skin="Windows7">
                                                        </telerik:RadComboBox>

                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label3" runat="server" Text="Race" CssClass="form-label"></asp:Label>

                                                        <telerik:RadComboBox ID="ddlRace" runat="server"
                                                            DataSourceID="odsRace" DataTextField="RaceName"
                                                            DataValueField="RaceID" Width="100%" Skin="Windows7">
                                                        </telerik:RadComboBox>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label115" runat="server" Text="Transport Route" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtTransportRoute" runat="server" Skin="Windows7" Width="100%" DataSourceID="dstransport" ValueField="TrId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="transportplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="transportplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dstransport" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetTransportRtforcombo" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label118" runat="server" Text="Distance For Permanent Address" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="txtDistanceforPermanentAddress" CssClass="form-control form-control-lg" runat="server" Skin="Windows7" Width="100%" />
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label114" runat="server" Text="Vaccination" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="cmbvaccine" runat="server" Skin="Windows7" Width="100%" DataSourceID="DSVACCINE" ValueField="VcId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btnvcplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btnvcplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="DSVACCINE" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GETVCFORCOMBO" SelectCommandType="StoredProcedure">

                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-md-6">
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label4" runat="server" Text="District" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtDistrict" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsdistrict" ValueField="DstrictId" TextField="District" AutoPostBack="true" CssClass="form-control form-control-lg" />
                                                        <asp:SqlDataSource ID="dsdistrict" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetDistrictforcombo" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-x">
                                                    <div class="col-md-12">
                                                        <asp:Label ID="Label12" runat="server" Text="Province" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtProvince" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsprovince" ValueField="PrId" TextField="Province" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <asp:SqlDataSource ID="dsprovince" runat="server"
                                                        ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                        SelectCommand="Emp_GetProvince" SelectCommandType="StoredProcedure"></asp:SqlDataSource>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label116" runat="server" Text="GS Division" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtGSDivision" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsgsdivisioans" ValueField="GSId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btngsplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btngsplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dsgsdivisioans" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetAllGsDivisionsforcombo" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label128" runat="server" Text="Electoral District" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtElectoralDistrict" runat="server" Skin="Windows7" Width="100%" DataSourceID="dselect" ValueField="GSId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btnelectplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btnelectplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dselect" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emp_GetElectoralforcombo" SelectCommandType="StoredProcedure">
                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col-md-11">
                                                        <asp:Label ID="Label125" runat="server" Text="Divisional Secretariats" CssClass="form-label"></asp:Label>
                                                        <dx:ASPxComboBox ID="txtDivisionalSecretariats" runat="server" Skin="Windows7" Width="100%" DataSourceID="dsdivision" ValueField="VcId" TextField="Name" CssClass="form-control form-control-lg" />
                                                    </div>
                                                    <div class="col-md-1" style="margin-top: 29px">
                                                        <asp:ImageButton ID="btndivisionplus" runat="Server" Visible="true" ImageUrl="~/App_Themes/NewTheme/img/add-button.png" OnClick="btndivisionplus_Click" ValidationGroup="a"></asp:ImageButton>

                                                        <asp:SqlDataSource ID="dsdivision" runat="server"
                                                            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
                                                            SelectCommand="Emmp_Getdivisionfog" SelectCommandType="StoredProcedure">

                                                            <SelectParameters>
                                                                <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                                                                    Type="Int32" />
                                                            </SelectParameters>
                                                        </asp:SqlDataSource>
                                                    </div>
                                                </div>
                                                <div class="row mt-4">
                                                    <div class="col">
                                                        <asp:Label ID="Label2" Visible="false" runat="server" Text="Coller Size" CssClass="form-label"></asp:Label>

                                                        <telerik:RadTextBox ID="RadTextBox1" Visible="false" runat="server" Skin="Windows7" Width="100%" CssClass="form-control form-control-lg" />
                                                        <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server"
                                                            ControlToValidate="txtxCollorSize" ErrorMessage="*" ForeColor="Red"
                                                            ValidationExpression="[0-9]+(\.[0-9][0-9]?)?" ValidationGroup="a"></asp:RegularExpressionValidator>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="d-flex justify-content-end mr-5  mt-4" style="grid-gap: 15px">
                                                     <asp:Button ID="btnSave" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btnSave_Click" ToolTip="Add new additional information"
                                                         ValidationGroup="Add" />
                                                     <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnUpdate_Click" ToolTip="Press to update existing record."
                                                         ValidationGroup="Add" />
                                                         </div>
                                    </div>
                                </div>
                            </div>

                     </div>
                           <div id="toastrContainer">
                    <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                    <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                </div>

            <table class="Contact" style="width: 75%">

                <tr>
                    <td></td>
                </tr>
                <tr runat="server" id="trCollarSize" visible="false">
                    <td>
                        <asp:Label ID="Label5" runat="server" Text="Coller Size" class="auto-style2"></asp:Label>
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtxCollorSize" runat="server" Skin="Windows7" Width="200px" />
                        <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server"
                            ControlToValidate="txtxCollorSize" ErrorMessage="*" ForeColor="Red"
                            ValidationExpression="[0-9]+(\.[0-9][0-9]?)?" ValidationGroup="a"></asp:RegularExpressionValidator></td>
                    <td>
                        <div class="ContentBlockHeader">
                            <div class="ContentProgressIcon">
                                <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                                    <ProgressTemplate>
                                        <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                                    </ProgressTemplate>
                                </asp:UpdateProgress>
                            </div>
                        </div>
                    </td>
                </tr>

 

            </table>
                    <table>
                        <tr>
                            <td>&nbsp;</td>
                            <td style="text-align: center">
                                <asp:ObjectDataSource ID="odsRace" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetRaceList"
                                    TypeName="HRM.Common.BLL.Race">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="RaceId" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                                <asp:ObjectDataSource ID="odsNationality" runat="server"
                                    OldValuesParameterFormatString="original_{0}" SelectMethod="GetNationalityList"
                                    TypeName="HRM.Common.BLL.Reference">
                                    <SelectParameters>
                                        <asp:Parameter DefaultValue="0" Name="NationalityId" Type="Int32" />
                                    </SelectParameters>
                                </asp:ObjectDataSource>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <table>
                        <tr>
                            <td>&nbsp;&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td>&nbsp;&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;
                            </td>
                            <td>&nbsp;&nbsp;
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>

            <dx:ASPxPopupControl ID="GsDevisionPopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <h6 style="font-family: Source Sans; font-size: 16px; font-weight: 600; color: white">GS Division</h6>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl2" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="lblbank" CssClass="form-label" runat="server">GS Code</asp:Label>
                                        <telerik:RadTextBox ID="txtgscode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="lblbranch" CssClass="form-label" runat="server">Name</asp:Label>
                                        <telerik:RadTextBox ID="txtgsname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btngsAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btngsAdd_Click" />
                                    <asp:Button ID="btngsupdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btngsupdate_Click" Enabled="false"/>
                                    <asp:Button ID="btngscancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btngscancel_Click" />
                                    <asp:Button ID="btngsdelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btngsdelete_Click" Enabled="false"/>

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="gvPopup" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="GET_GsDivisions">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="GSId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopup" runat="server" onclientclicked="showForm" OnClick="lkSelectPopup_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                            <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                            <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                        </Styles>
                                        <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
            <asp:SqlDataSource ID="GET_GsDivisions" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="Emp_GetAllGsDivisions" SelectCommandType="StoredProcedure">
                <SelectParameters>
                    <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

<%--
            <asp:ObjectDataSource ID="GET_GsDivisions" runat="server"
                OldValuesParameterFormatString="original_{0}" SelectMethod="EmpGetAllGsDivisions"
                TypeName="HRM.HR.BLL.EmployeeAdditional">
                <SelectParameters>
                    <asp:Parameter DefaultValue="0" Name="CompanyId" Type="Int32" />
                </SelectParameters>
            </asp:ObjectDataSource>--%>
     <asp:HiddenField ID="hfgsId" runat="server" />


    <dx:ASPxPopupControl ID="ElectPopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <h6 style="font-family: Source Sans; font-size: 16px; font-weight: 600; color: white">Electoral District</h6>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl1" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label84" CssClass="form-label" runat="server">Elect Code</asp:Label>
                                        <telerik:RadTextBox ID="txtelcode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label85" CssClass="form-label" runat="server">Elect District</asp:Label>
                                        <telerik:RadTextBox ID="txteldistrict" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btneladd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btneladd_Click" />
                                    <asp:Button ID="btnelupdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnelupdate_Click" Enabled="false"/>
                                    <asp:Button ID="btnelcancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btnelcancel_Click" />
                                    <asp:Button ID="btneldelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btneldelete_Click" Enabled="false"/>

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="gvelectoral" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="GET_electoral">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="ELId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                            <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                            <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                        </Styles>
                                        <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
            <asp:SqlDataSource ID="GET_electoral" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="Emp_GetElectoralDsForgrid" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                    <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
     <asp:HiddenField ID="hfElectoral" runat="server" />

                           <%-- Division Popup --%>
    <dx:ASPxPopupControl ID="Divisianalpopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <h6 style="font-family: Source Sans; font-size: 16px; font-weight: 600; color: white">Divisional Secretariats</h6>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl3" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label86" CssClass="form-label" runat="server">Division Code</asp:Label>
                                        <telerik:RadTextBox ID="txtdvcode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label122" CssClass="form-label" runat="server">Division Name</asp:Label>
                                        <telerik:RadTextBox ID="txtdvname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btndvAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btndvAdd_Click" />
                                    <asp:Button ID="btndvUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btndvUpdate_Click" Enabled="false"/>
                                    <asp:Button ID="btndvCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btndvCancel_Click" />
                                    <asp:Button ID="btndvDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btndvDelete_Click" Enabled="false"/>

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="gvdivision" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="dsdivisiongrid">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="DvId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click1" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                            <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                            <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                        </Styles>
                                        <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
            <asp:SqlDataSource ID="dsdivisiongrid" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="Emp_Getdisionalsec" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                    <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>
     <asp:HiddenField ID="hfDvId" runat="server" />

    <dx:ASPxPopupControl ID="vaccinepopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <h6 style="font-family: Source Sans; font-size: 16px; font-weight: 600; color: white">Vacciene</h6>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl5" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                        <div class="border mt-2" style="border-radius: 10px">
                            <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label126" CssClass="form-label" runat="server">Vacciene Code</asp:Label>
                                        <telerik:RadTextBox ID="txtvccode" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label127" CssClass="form-label" runat="server">Vacciene Name</asp:Label>
                                        <telerik:RadTextBox ID="txtvcname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btnvcAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btnvcAdd_Click" />
                                    <asp:Button ID="btnvcUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnvcUpdate_Click" Enabled="false"/>
                                    <asp:Button ID="btnvcCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btnvcCancel_Click" />
                                    <asp:Button ID="btnvcDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btnvcDelete_Click" Enabled="false" />

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="vaccienegv" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="VcId" Theme="MetropolisBlue" DataSourceID="dsvaccienefrgrid">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="VcId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Code" Caption="Vacciene Code" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Vacciene Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click3" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                            <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                            <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                        </Styles>
                                        <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
            <asp:SqlDataSource ID="dsvaccienefrgrid" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="Emp_Gettvaccienefrvcgrid" SelectCommandType="StoredProcedure"></asp:SqlDataSource>

     <asp:HiddenField ID="hfvcId" runat="server" />

            <dx:ASPxPopupControl ID="transportpopup" runat="server" CssClass="popup-container" PopupHorizontalAlign="WindowCenter" PopupVerticalAlign="WindowCenter" Modal="True">
        <HeaderTemplate>
            <h6 style="font-family: Source Sans; font-size: 16px; font-weight: 600; color: white">Transport Route</h6>
        </HeaderTemplate>
        <ContentCollection>
            <dx:PopupControlContentControl ID="PopupControlContentControl4" runat="server">
                <div class="row" style="background-color: ghostwhite; padding-bottom: 9px">
                    <div class="col-md-12" style="border-top-color: black">
                         <div class="border scroll-form">
                            <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px">
                                <div class="row mt-3">
                                    <div class="col-md-6">
                                        <asp:Label ID="Label123" CssClass="form-label" runat="server">Rout number</asp:Label>
                                        <telerik:RadTextBox ID="txtrtnumber" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                    <div class="col-md-6">
                                        <asp:Label ID="Label124" CssClass="form-label" runat="server">Rout Name</asp:Label>
                                        <telerik:RadTextBox ID="txtrtname" runat="server" Skin="Windows7" Width="200px" CssClass="form-control form-control-lg" />
                                    </div>
                                </div>

                                <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">

                                    <asp:Button ID="btntrAdd" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btntrAdd_Click" />
                                    <asp:Button ID="btntrUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btntrUpdate_Click" Enabled="false"/>
                                    <asp:Button ID="btntrCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btntrCancel_Click" />
                                    <asp:Button ID="btntrDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btntrDelete_Click" Enabled="false" />

                                </div>
                                <div class="row mt-4">
                                    <dx:ASPxGridView ID="transportgv" runat="server" AutoGenerateColumns="False" Font-Size="Small" CssClass="tableStyle"
                                        ClientIDMode="AutoID" EnableTheming="True" KeyFieldName="ContractId" Theme="MetropolisBlue" DataSourceID="dstransportsroute">
                                        <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                        <Columns>
                                            <dx:GridViewDataTextColumn FieldName="TrId" Caption="" Visible="false" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Number" Caption="Number" Width="100px" VisibleIndex="1" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn FieldName="Name" Caption="Route Name " Width="100px" VisibleIndex="2" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" HeaderStyle-Font-Bold="false">
                                            </dx:GridViewDataTextColumn>
                                            <dx:GridViewDataTextColumn Caption="" VisibleIndex="0" Width="30px" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" CellStyle-HorizontalAlign="Center" HeaderStyle-Font-Bold="false">
                                                <DataItemTemplate>
                                                    <asp:LinkButton ID="lkSelectPopupel" runat="server" onclientclicked="showForm" OnClick="lkSelectPopupel_Click2" ForeColor="cornflowerblue"><img src="../../App_Themes/NewTheme/img/edit.png" alt="delete group" />
                                                    </asp:LinkButton>
                                                </DataItemTemplate>
                                                <HeaderStyle HorizontalAlign="Center" />
                                                <CellStyle HorizontalAlign="Center" />
                                            </dx:GridViewDataTextColumn>
                                        </Columns>
                                        <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                            <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true"></Header>
                                            <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                        </Styles>
                                        <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                        <SettingsPager Mode="ShowPager" PageSize="5" />
                                    </dx:ASPxGridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </dx:PopupControlContentControl>
        </ContentCollection>

    </dx:ASPxPopupControl>
            <asp:SqlDataSource ID="dstransportsroute" runat="server"
            ConnectionString="<%$ ConnectionStrings:ConnectionString %>"
            SelectCommand="Emp_Gettransportroutforgv" SelectCommandType="StoredProcedure">
                                <SelectParameters>
                    <asp:SessionParameter DefaultValue="" Name="CompanyId" SessionField="CompanyId"
                        Type="Int32" />
                </SelectParameters>
            </asp:SqlDataSource>

     <asp:HiddenField ID="hfTrId" runat="server" />

    </contenttemplate>

