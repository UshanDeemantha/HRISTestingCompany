<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EmoloyeeSportsControl.ascx.cs" Inherits="Employee_UserControls_EmoloyeeSportsControl" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<style type="text/css">
    #FormContentArea {
        float: left;
        height: auto;
        width: 820px;
        padding: 0px 0px 0px 10px;
    }

    .ContentTitle {
        float: left;
        height: auto;
        width: 810px;
    }

    .formHeadingLeable {
        width: 400px;
        height: 20px;
        float: left;
        font-family: Tahoma, Arial, Helvetica, sans-serif;
        font-size: 12 px;
        font-weight: bold;
        color: #000080;
    }

    .ContentBlock {
        float: left;
        height: auto;
        width: 810px;
    }

    .ContentBlockHeader {
        height: auto;
        float: left;
        width: 250px;
        padding: 5px 0px 0px 5px;
    }

    .dropdown {
        border: 1px solid #C0C0C0;
        font-family: Arial, Helvetica, sans-serif;
        font-size: 10px;
        font-weight: 100;
        vertical-align: middle;
        text-align: left;
        width: 180px;
        color: #000000;
        height: 18px;
    }

    .ContentBlockDetail {
        height: auto;
        float: left;
        width: 540px;
        padding: 5px 0px 0px 5px;
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

    .ContentGridArea {
        float: left;
        height: auto;
        width: 810px;
        padding: 10px 0px 0px 5px;
    }

    .GridView {
        border-width: 1px;
        border-color: #33CCFF;
        font-family: Arial, Helvetica, sans-serif;
        font-size: 10px;
    }

    .GridViewHeader {
        height: 24px;
        color: White;
        font-weight: bold;
        background-color: #0099CC;
    }

    .GridViewRowStyle {
        height: 20px;
        background-color: #FFFFFF;
        color: #000000;
    }

    .GridBtn {
        font-family: Arial, Helvetica, sans-serif;
        font-size: 10px;
        font-weight: bold;
        color: #FFFFFF;
        background-color: #0099CC;
        border: 1px solid #006699;
    }

    .GridViewAlternetRow {
        height: 20px;
        background-color: #99CCFF;
        color: #000000;
    }

    .auto-style1 {
        /*height: 39px;*/
    }

    .auto-style2 {
        margin-left: 20px;
    }
    td.wizard-hed {
    padding-top: 20px;
}
    td.wizard-linkback {
    margin-left: 40px;
}
    .card-body.shadow {
    width: 97%;
}
</style>
<asp:UpdatePanel ID="UpdatePanel1" runat="server">
    <ContentTemplate>
        <div class="row">
            <div class="col-md-12" style="border-top-color: black; margin-left:20px">
                 <div class="border ">
                    <div class="card-body shadow" style="border-radius: 10px">
                        <div class="row">
                            <div class="col-md-6">
                                <asp:Label ID="Label5" runat="server" Text="Employee" CssClass="form-label"></asp:Label>
                                <telerik:RadComboBox ID="ddlEmployee" runat="server" AutoPostBack="True"
                                    Enabled="False" Width="100%" Skin="Windows7">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="row mt-3">
                            <div class="col-md-6">
                                <asp:Label ID="Label80" runat="server" Text="Employee Sport" CssClass="form-label"></asp:Label>
                                <telerik:RadComboBox ID="ddlSport" runat="server" Width="100%"
                                    DataSourceID="oobjsport" DataTextField="SportName"
                                    DataValueField="SportID" OnDataBound="RadComboBox1_DataBound" Skin="Windows7">
                                </telerik:RadComboBox>
                            </div>
                        </div>
                        <div class="col d-flex justify-content-end pr-2" style="grid-gap: 6px">
                            <asp:Button ID="btnSave" runat="server" CssClass="btn btn-outline-primary check-aspbtn" Text="Add" OnClick="btnSave_Click" ToolTip="Add Information"
                                ValidationGroup="a" />
                            <asp:Button ID="btnUpdate" runat="server" CssClass="btn btn-outline-success check-aspbtn" Text="Update" OnClick="btnUpdate_Click" ToolTip="Press when need to save modify record"
                                ValidationGroup="a" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-outline-danger check-aspbtn" Text="Delete" OnClick="btnDelete_Click" ToolTip="Delete the Record"
                                OnClientClick="return confirm('Are you sure you want to Delete this Record?');"
                                ValidationGroup="a" />
                            <asp:Button ID="btnCancel" runat="server" CssClass="btn btn-outline-secondary check-aspbtn" Text="Cancel" OnClick="btnCancel_Click" ToolTip="Clear Controls"
                                ValidationGroup="a" />
                        </div>
                        <div class="row">
                            <div class="col table-scroll1">
                                <dx:ASPxGridView ID="RadGrid1" CssClass="tableStyle" ClientInstanceName="grid" runat="server" ForeColor="Black" order-BorderColor="Black" Theme="MetropolisBlue" Width="100%"
                                    KeyFieldName="EmployeeId" AutoGenerateColumns="False" ClientIDMode="AutoID" DataSourceID="objdsEmployeeSport">
                                    <SettingsSearchPanel Visible="true" HighlightResults="true" />
                                    <Columns>
                                        <dx:GridViewDataColumn FieldName="EmployeeSportActivityID" HeaderStyle-Font-Bold="false" Visible="false" Caption="EmployeeSportActivityID" PropertiesEditType="TextBox" Width="100px" VisibleIndex="1" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="EmployeeID" HeaderStyle-Font-Bold="false" Visible="false" Caption="EmployeeID" PropertiesEditType="TextBox" Width="100px" VisibleIndex="2" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="FullName" HeaderStyle-Font-Bold="false" Visible="true" Caption="Full Name" PropertiesEditType="TextBox" Width="200px" VisibleIndex="3" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="SportID" Visible="false" HeaderStyle-Font-Bold="false" Caption="SportID" PropertiesEditType="TextBox" Width="100px" VisibleIndex="4" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataColumn FieldName="SportName" Visible="true" HeaderStyle-Font-Bold="false" Caption="Sport Name" PropertiesEditType="TextBox" Width="100px" VisibleIndex="5" CellStyle-HorizontalAlign="Left" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle" />
                                        <dx:GridViewDataTextColumn Caption="" HeaderStyle-Font-Bold="false" Width="50px" VisibleIndex="15" CellStyle-HorizontalAlign="Center" HeaderStyle-CssClass="headerStyle" CellStyle-CssClass="cellStyle">
                                            <DataItemTemplate>
                                                <asp:LinkButton ID="lkSelect" runat="server" OnClick="lkSelect_Click" ForeColor="Blue"><img src="../../../App_Themes/NewTheme/img/edit.png" alt="delete group" style="margin-top:-5px"/></asp:LinkButton>
                                            </DataItemTemplate>
                                        </dx:GridViewDataTextColumn>
                                    </Columns>
                                    <Styles Header-BackColor="Menu" Header-Font-Bold="false" Header-HorizontalAlign="Left">
                                        <AlternatingRow Enabled="true" />
                                        <Header HorizontalAlign="Left" BackColor="Menu" Font-Bold="true" Wrap="True"></Header>
                                        <SearchPanel CssClass="searchBarStyle"></SearchPanel>
                                    </Styles>
                                    <Settings ShowPreview="true" ShowFooter="True" ColumnMinWidth="200" UseFixedTableLayout="true" />
                                    <SettingsPager Mode="ShowPager" PageSize="10" />
                                </dx:ASPxGridView>
                                <dx:ASPxGridViewExporter ID="GridExporter" runat="server" GridViewID="radgvDetails">
                                    <Styles>
                                        <Header BackColor="#3366CC" Font-Bold="True" ForeColor="White" HorizontalAlign="Center" Wrap="True">
                                        </Header>
                                    </Styles>
                                </dx:ASPxGridViewExporter>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <table class="Contact" style="width: 75%">
       <tr>
                <td>
                    <asp:ObjectDataSource ID="oobjsport" runat="server"
                        OldValuesParameterFormatString="original_{0}" SelectMethod="GetSport"
                        TypeName="HRM.HR.BLL.Sport">
                        <SelectParameters>
                            <asp:Parameter DefaultValue="0" Name="SportId" Type="Int32" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                </td>
                <td style="text-align: right">
                    <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </td>
            </tr>
        
            <tr>
                <td>
                    <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                </td></tr>
        </table>
      
        <table>
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:HiddenField ID="hfEmployeeSportId" runat="server" />
                    <asp:ObjectDataSource ID="objdsEmployeeSport" runat="server"
                        SelectMethod="GetEmployeeSportByEmployeeId" TypeName="HRM.HR.BLL.Sport"
                        OldValuesParameterFormatString="original_{0}">
                        <SelectParameters>
                            <asp:ControlParameter ControlID="hfEmployeeId" DefaultValue="0"
                                Name="EmployeeId" PropertyName="Value" Type="Int64" />
                        </SelectParameters>
                    </asp:ObjectDataSource>
                    <asp:HiddenField ID="hfEmployeeId" runat="server" />
                </td>
            </tr>
        </table>
    </ContentTemplate>
</asp:UpdatePanel>

