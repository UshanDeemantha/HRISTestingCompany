<%@ Page Title="HRM Common | Nationality" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" StylesheetTheme="Common"
    CodeFile="Nationality.aspx.cs" Inherits="Nationality_Nationality"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register assembly="Telerik.Web.UI" namespace="Telerik.Web.UI" tagprefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>
            <table style="width: 100%;">
                <tr>
                    <td colspan="2">
                        <h4>Nationality<asp:HiddenField ID="hfNationalityId" runat="server" />
                        </h4>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        &nbsp;
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Nationality Name" />
                    </td>
                    <td>
                        <telerik:RadTextBox ID="txtNationalityName" runat="server" MaxLength="50" 
                            Skin="Windows7" Width="200px" />
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="txtNationalityName" ErrorMessage="*" ForeColor="Red">*</asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label runat="server" Text="Country"></asp:Label>
                    </td>
                    <td>
                        &nbsp;
                        <asp:XmlDataSource ID="xmlDataCountries" runat="server" 
                            DataFile="~/Common/Countries.xml"></asp:XmlDataSource>
                        <telerik:RadComboBox ID="ddlCountry" runat="server" 
                            DataSourceID="xmlDataCountries" DataTextField="country" 
                            DataValueField="country" Skin="Windows7" />
                    </td>
                </tr>
                <tr>
                    <td align="right" valign="top">
                        <asp:UpdateProgress ID="UpdateProgress2" runat="server">
                            <ProgressTemplate>
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 40px; height: 40px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>
                    <td>
                        <asp:Label ID="lblResult" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" 
                            Skin="WebBlue" Text="Save" ToolTip="Add Information" ValidationGroup="a" />&nbsp;
                        <telerik:RadButton ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" 
                            Skin="WebBlue" Text="Update" ToolTip="Press when need to save modify record" 
                            ValidationGroup="a" />&nbsp;
                        <telerik:RadButton ID="btnDelete" runat="server" OnClick="btnDelete_Click" 
                            OnClientClick="return confirm('Are you sure you want to Delete this Record?');" 
                            Skin="WebBlue" Text="Delete" ToolTip="Delete the Record" />&nbsp;
                        <telerik:RadButton ID="btnCancel" runat="server" OnClick="btnCancel_Click" 
                            Skin="WebBlue" Text="Cancel" ToolTip="Clear Controls" />
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
                <tr>
                    <td colspan="2" style="padding: 0px 8px 0px 8px;">
                        <telerik:RadGrid ID="radgvDetails" runat="server" AllowPaging="True" 
                            AutoGenerateColumns="False" CellSpacing="0" DataSourceID="objdsNationality" 
                            GridLines="None" onitemcommand="radgvDetails_ItemCommand" 
                            onitemcreated="radgvDetails_ItemCreated" Skin="Windows7">
                            <ClientSettings>
                                <Selecting AllowRowSelect="True" />
                            </ClientSettings>
                            <MasterTableView CommandItemDisplay="Top" DataKeyNames="NationalityID" 
                                DataSourceID="objdsNationality">
                                <CommandItemSettings ExportToPdfText="Export to PDF" />
                                <RowIndicatorColumn FilterControlAltText="Filter RowIndicator column">
                                </RowIndicatorColumn>
                                <ExpandCollapseColumn FilterControlAltText="Filter ExpandColumn column">
                                </ExpandCollapseColumn>
                                <Columns>
                                    <telerik:GridBoundColumn DataField="NationalityID" Display="False" 
                                        FilterControlAltText="Filter column column" HeaderText="NationalityID" 
                                        UniqueName="column" Visible="False">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="NationalityName" 
                                        FilterControlAltText="Filter column1 column" HeaderText="Nationality" 
                                        UniqueName="column1">
                                    </telerik:GridBoundColumn>
                                    <telerik:GridBoundColumn DataField="Country" 
                                        FilterControlAltText="Filter column2 column" HeaderText="Country" 
                                        UniqueName="column2">
                                    </telerik:GridBoundColumn>
                                </Columns>
                                <EditFormSettings>
                                    <EditColumn FilterControlAltText="Filter EditCommandColumn column">
                                    </EditColumn>
                                </EditFormSettings>
                                <CommandItemTemplate>
                                    <div style="padding: 5px 5px;">
                                        <b>Nationality List : </b>&nbsp;
                                        <asp:LinkButton ID="radgrdbtnEditSelected" runat="server" CommandName="Select" 
                                            Visible="<%# radgvDetails.EditIndexes.Count == 0 %>"><img 
                    style="border:0px;vertical-align:middle;" alt="" 
                    src="../../App_Themes/CommonResources/RadGrid/Edit.gif" />Modify</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="radgrdbtnCancel" runat="server" CommandName="CancelAll" 
                                            Visible="<%# radgvDetails.EditIndexes.Count > 0 || radgvDetails.MasterTableView.IsItemInserted %>"><img style="border:0px;vertical-align:middle;" 
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Cancel.gif" />Cancel editing</asp:LinkButton>
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="radgrdbtnRefresh" runat="server" CommandName="RebindGrid"><img style="border:0px;vertical-align:middle;" 
                    alt="" src="../../App_Themes/CommonResources/RadGrid/Refresh.gif" />Refresh Nationality list</asp:LinkButton>
                                    </div>
                                </CommandItemTemplate>
                            </MasterTableView>
                            <FilterMenu EnableImageSprites="False">
                            </FilterMenu>
                            <HeaderContextMenu CssClass="GridContextMenu GridContextMenu_Default">
                            </HeaderContextMenu>
                        </telerik:RadGrid>
                        <asp:ObjectDataSource ID="objdsNationality" runat="server" 
                            OldValuesParameterFormatString="original_{0}" SelectMethod="GetNationality" 
                            TypeName="HRM.Common.BLL.Reference">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="NationalityId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;</td>
                    <td>
                        &nbsp;</td>
                </tr>
            </table>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
