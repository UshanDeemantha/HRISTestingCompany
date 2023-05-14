<%@ Page Title="HRIS Common | Designation Structure" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="DesignationStructure.aspx.cs" Inherits="Designation_DesignationStructure" StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="DesignationStructureControl.ascx" TagName="DesignationStructureControl" TagPrefix="uc2" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style16 {
            width: 100%;
        }

        .pl-3, .px-3 {
            padding-left: 9rem !important;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
    </style>
    <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.3/jquery.min.js"></script>
    <script type="text/javascript" src="https://www.google.com/jsapi"></script>

    <script type="text/javascript">
        google.load("visualization", "1", { packages: ["orgchart"] });
        google.setOnLoadCallback(drawChart);
        function drawChart() {
            $.ajax({
                type: "POST",
                url: "DesignationStructure.aspx/GetChartData",
                data: '{}',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (r) {
                    var data = new google.visualization.DataTable();
                    data.addColumn('string', 'Entity');
                    data.addColumn('string', 'ParentEntity');
                    data.addColumn('string', 'ToolTip');
                    for (var i = 0; i < r.d.length; i++) {
                        var employeeId = r.d[i][0].toString();
                        var employeeName = r.d[i][1];
                        var designation = r.d[i][2];
                        var reportingManager = r.d[i][3] != null ? r.d[i][3].toString() : '';
                        var Mobile = r.d[i][4];
                        data.addRows([[{
                            v: employeeId,
                            f: '<div class="container">' + '<img class="image-style" src = "/Pictures/' + 2 + '.jpg" />' + '<div style="padding-bottom:1px"> <h3 style="padding-bottom:0px">' + employeeName + '</h3></div>' + '<div style="padding-top:0px"> <h5 class="text-muted" style="padding-top:0px"></h5></div>' + '<div> <h5  class="text-muted"></h5></div>' + '</div>'
                        }, reportingManager, designation]]);
                    }
                    var chart = new google.visualization.OrgChart($("#chart")[0]);
                    chart.draw(data, { allowHtml: true, nodeClass: "none", selectionColor: "none" });
                },
                failure: function (r) {
                    alert(r.d);
                },
                error: function (r) {
                    alert(r.d);
                }
            });
        }
    </script>
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/employeeList.css" />
    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure.css" />
    <%--<script type="text/javascript" src="/App_Themes/NewTheme/script/employeeList.js"></script>--%>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/organization-structure.js"></script>
    <link rel="stylesheet" href="/App_Themes/NewTheme/font-awesome-4.7.0/css/font-awesome.min.css" />


</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">


    <asp:UpdatePanel ID="UpdatePanel1" runat="server">

        <ContentTemplate>
            <cc1:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
            </cc1:ToolkitScriptManager>
            <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>

            <div class="overflow-auto" style="height: calc(100vh - 110px)">
                <div class="form-container  shadow" id="formContainer" runat="server" style="height: 54px;">
                    <div class="row">
                        <div class="col head-container">
                            <h4 class="header">Designation Structrue</h4>
                            <span onclick="toggleProfileForm(390);" id="profileButton" class="dot shadow"></span>
                        </div>
                    </div>
                    <div class="row" style="background-color: ghostwhite">
                        <div class="col-md-12" style="border-top-color: black">
                            <div class="border" style="border-radius: 10px">
                                <div class="card-body shadow" style="border-top-color: #097cbd; border-top-width: 5px; background-color: white; border-radius: 10px;">
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" CssClass="form-label" Text="Company" Visible="false" />
                                            <dx:ASPxTextBox ID="radcboCompany" runat="server" Visible="false" CssClass="form-control form-control-lg" Skin="Windows7"
                                                Width="250px" AutoPostBack="True" Enabled="False">
                                            </dx:ASPxTextBox>
                                            <telerik:RadTreeView ID="tvDesignation" runat="server"
                                                OnNodeClick="tvDesignation_NodeClick" OnNodeCreated="tvDesignation_NodeCreated"
                                                Skin="Office2007">
                                            </telerik:RadTreeView>
                                            <asp:Label ID="txtRootLevel" runat="server"></asp:Label>
                                           <%-- <asp:Button ID="btnOrganizationSelect" runat="server" CssClass="btn btn-light mb-3" Text="Designation"
                                                Width="503px" Height="35px" BorderColor="Silver" ForeColor="Blue" OnClientClick="addOrgStruStyle()" />
                                            <cc1:ModalPopupExtender ID="btnOrganizationSelect_ModalPopupExtender" runat="server"
                                                CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True" PopupControlID="PopupPane2"
                                                TargetControlID="btnOrganizationSelect">
                                            </cc1:ModalPopupExtender>--%>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                                <asp:Label runat="server" CssClass="form-label" Text="Designation" />
                                                <dx:ASPxComboBox ID="radcboDesignations" runat="server" CssClass="form-control form-control-lg"
                                                   OnDataBound="radcboDesignations_DataBound"
                                                    Skin="Windows7">
                                                </dx:ASPxComboBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                        <div class="col-md-6">
                                            <asp:Label runat="server" CssClass="form-label" Text="Tree Index" />
                                            <dx:ASPxComboBox ID="ddlTreeIndex" runat="server" CssClass="form-control form-control-lg" Width="100%" Skin="Windows7" MaxHeight="80px"
                                                MaxLength="10" MinFilterLength="10">
                                                <Items>
                                                    <dx:ListEditItem runat="server" Text="01" Value="01" Selected="true" />
                                                    <dx:ListEditItem runat="server" Text="02" Value="02" />
                                                    <dx:ListEditItem runat="server" Text="03" Value="03" />
                                                    <dx:ListEditItem runat="server" Text="04" Value="04" />
                                                    <dx:ListEditItem runat="server" Text="05" Value="05" />
                                                    <dx:ListEditItem runat="server" Text="06" Value="06" />
                                                    <dx:ListEditItem runat="server" Text="07" Value="07" />
                                                    <dx:ListEditItem runat="server" Text="08" Value="08" />
                                                    <dx:ListEditItem runat="server" Text="09" Value="09" />
                                                    <dx:ListEditItem runat="server" Text="10" Value="10" />
                                                    <dx:ListEditItem runat="server" Text="11" Value="11" />
                                                    <dx:ListEditItem runat="server" Text="12" Value="12" />
                                                    <dx:ListEditItem runat="server" Text="13" Value="13" />
                                                    <dx:ListEditItem runat="server" Text="14" Value="14" />
                                                    <dx:ListEditItem runat="server" Text="15" Value="15" />
                                                    <dx:ListEditItem runat="server" Text="16" Value="16" />
                                                    <dx:ListEditItem runat="server" Text="17" Value="17" />
                                                    <dx:ListEditItem runat="server" Text="18" Value="18" />
                                                    <dx:ListEditItem runat="server" Text="19" Value="19" />
                                                    <dx:ListEditItem runat="server" Text="20" Value="20" />
                                                </Items>
                                            </dx:ASPxComboBox>
                                        </div>
                                    </div>
                                    <div class="row mt-3">
                                    </div>
                                    <div class="row mt-3">
                                    </div>
                                    <div class="row mt-3">
                                    </div>
                                    <div class="d-flex justify-content-end mr-lg-5 mt-4" style="grid-gap: 15px">
                                        <div>
                                            <telerik:RadButton ID="btnSave" runat="server" OnClick="btnSave_Click" Text="Save"
                                                Skin="WebBlue" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnUpdate" runat="server" Enabled="False" OnClick="btnUpdate_Click"
                                                Text="Update" Skin="WebBlue" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnDelete" runat="server" Text="Delete" Visible="False"
                                                Skin="WebBlue" OnClick="btnDelete_Click" />
                                        </div>
                                        <div>
                                            <telerik:RadButton ID="btnCancel" runat="server" Text="Cancel" Skin="WebBlue"
                                                OnClick="btnCancel_Click1" />
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>


                    <div id="toastrContainer">
                        <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                        <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                    </div>
                    <td style="text-align: right">
                        <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                            <ProgressTemplate>
                                <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 25px; height: 25px" />
                            </ProgressTemplate>
                        </asp:UpdateProgress>
                    </td>

                </div>

                <div class="container">
                    <div class="row">
                        <div class="col table-scroll">
                            <div id="chart">
                            </div>
                        </div>
                    </div>
                </div>
                </td>
                </tr>
                <tr>
                    <td>&nbsp;
                    </td>
                    <td>
                        <asp:HiddenField ID="hfParentId" runat="server" />
                        <asp:ObjectDataSource ID="dsDesignationStructure" runat="server" OldValuesParameterFormatString="original_{0}"
                            SelectMethod="GetDesignationStructure" TypeName="HRM.Common.BLL.Designation">
                            <SelectParameters>
                                <asp:Parameter DefaultValue="0" Name="DesignationStructureId" Type="Int32" />
                            </SelectParameters>
                        </asp:ObjectDataSource>
                    </td>
                </tr>
                </table>
                     <div>
          <%--               <div id="PopupPane2" class="shadow-sm" style="background-color: white; border-radius: 6px">
                             <asp:Panel ID="Panel2" runat="server" CssClass="popstyle">
                                 <div>
                                     <div class="row">
                                         <div class="col-12 d-flex justify-content-end">
                                             <asp:ImageButton ID="btnClosePopup" CssClass="btn-close" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                                         </div>
                                     </div>
                                     <div class="row">
                                         <div class="row">
                                             <uc2:DesignationStructureControl ID="DesignationStructureControl" runat="server" />
                                         </div>
                                     </div>
                                 </div>
                             </asp:Panel>
                         </div>--%>
                     </div>

                <%--<div id="FormContentArea">
                <div class="ContentBlock">
                    <div class="ContentGridArea">
                        <div id="PopupPanel">
                            <asp:Panel ID="Panel1" runat="server" CssClass="popstyle" ScrollBars="Vertical" Height="550px">
                                <table>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                        <td style="text-align: right">
                                            <asp:ImageButton ID="btnClosePopup" runat="server" ImageUrl="~/App_Themes/CommonResources/ClosePopup.png" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td>
                                            <uc2:DesignationStructureControl ID="DesignationStructureControl" runat="server" />
                                        </td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>&nbsp;
                                        </td>
                                        <td></td>
                                        <td>&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </div>
                    </div>
                </div>
            </div>--%>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
