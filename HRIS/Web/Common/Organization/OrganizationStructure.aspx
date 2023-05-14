<%@ Page Title="HRIS Common | Department Structures" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master"
    AutoEventWireup="true" CodeFile="OrganizationStructure.aspx.cs" Inherits="Organization_OrganizationStructure"
    StylesheetTheme="Common" %>

<%@ Register Assembly="DevExpress.Web.v14.2, Version=14.2.4.0, Culture=neutral, PublicKeyToken=b88d1754d700e49a" Namespace="DevExpress.Web" TagPrefix="dx" %>

<%@ Register Src="OrganizationStucture.ascx" TagName="OrganizationStucture" TagPrefix="uc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="Telerik.Web.UI" Namespace="Telerik.Web.UI" TagPrefix="telerik" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .style1 {
            width: 100%;
        }

        .style2 {
            width: 204px;
        }

        .style3 {
            height: 26px;
        }

        body {
            font-family: Arial;
            font-size: 10pt;
        }

        #chart {
            width: 900px;
            height: 500px;
        }

            #chart div {
                width: 130px;
            }

            #chart span {
                color: red;
                font-size: 8pt;
                font-style: italic;
            }

            #chart img {
                width: 50px;
            }

        .image-style {
            border-radius: 5rem;
        }

        .container {
            box-shadow: 0 .5rem 1rem rgba(0,0,0,.15) !important;
        }

        .text-muted {
            color: #6c757d !important
        }

        .remove-padding {
            padding-top: 0px;
            padding-bottom: 0px;
        }

        .ml-2, .mx-2 {
            margin-left: 20.5rem !important;
        }
        .mr-lg-3, .mx-lg-3 {
    margin-right: 75rem!important;
}
        a:hover {
    opacity: 1;
    color: darkblue;
}
        .style1 {
            height: calc(1.5em + 1rem + 2px);
            padding: .5rem 1rem;
            font-size: 1.25rem;
            line-height: 1.5;
            border-radius: .3rem;
            width: 100%;
            display: block;
            font-weight: 400;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            transition: border-color .15s ease-in-out,box-shadow .15s ease-in-out;
            margin-top: 0px;
        }

        input#ContentPlaceHolder1_txtRootLevel {
            background-color: #f7971f;
            opacity: 1;
            color: white;
            font-size: 16px;
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
                url: "OrganizationStructure.aspx/GetChartData",
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
        <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/Common/organization-structure-4.css" />

    <link rel="stylesheet" type="text/css" href="/App_Themes/NewTheme/css/employeeList.css" />
    <%--<script type="text/javascript" src="/App_Themes/NewTheme/script/employeeList.js"></script>--%>
<%--    <script type="text/javascript" src="/App_Themes/NewTheme/script/organization-structure.js"></script>--%>
    <link rel="stylesheet" href="/App_Themes/NewTheme/font-awesome-4.7.0/css/font-awesome.min.css" />


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">

    <asp:ScriptManager ID="ScriptManager1" runat="server">
    </asp:ScriptManager>
    <%--  <asp:UpdatePanel ID="UpdatePanel1" runat="server">
        <ContentTemplate>--%>
    <script type="text/javascript" src="/App_Themes/NewTheme/script/form.js"></script>
    <div class="overflow-auto" style="height: calc(100vh - 110px)">
        <div class="form-container shadow" id="formContainer" runat="server" style="height: 54px;">
            <div class="row">
                <div class="col head-container">
                    <h4 class="header">Department Structrue</h4>
                    <span onclick="toggleProfileForm(375);" id="profileButton" class="dot shadow"></span>
                </div>
            </div>
            <%--<asp:HiddenField ID="hfOrgStructureId" runat="server" />--%>

            <div class="row" style="background-color: ghostwhite">
                <div class="col-md-12" style="border-top-color: black">
                    <div class="border" style="border-radius: 10px;">
                        <div class="card-body shadow">
                            <div class="row mt-3">
                                <div class="col-md-6">

                                    <asp:TextBox ID="txtRootLevel" runat="server" MaxLength="50" Width="100%" Height="30px" CssClass="form-control form-control-lg"
                                        ReadOnly="True" AutoPostBack="True" OnPreRender="txtRootLevel_PreRender" />
                                    <cc1:TextBoxWatermarkExtender ID="txtRootLevel_TextBoxWatermarkExtender" runat="server"
                                        Enabled="True" TargetControlID="txtRootLevel" WatermarkText="Click To Select Department">
                                    </cc1:TextBoxWatermarkExtender>
                                    <cc1:ModalPopupExtender ID="btnPrint_ModalPopupExtender" runat="server" CancelControlID="btnClosePopup"
                                        DynamicServicePath="" Enabled="True" PopupControlID="PopupPanel" TargetControlID="txtRootLevel"
                                        BackgroundCssClass="popbackground" PopupDragHandleControlID="Panel1"
                                        DynamicServiceMethod="DataBindToCombo" DynamicControlID="hfChildLevel">
                                    </cc1:ModalPopupExtender>
                                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtRootLevel"
                                        ErrorMessage="*" ForeColor="Red" ValidationGroup="a" Visible="False"></asp:RequiredFieldValidator>


                                    <asp:Label ID="Label7" CssClass="form-label" runat="server" Text="Company" Visible="false"></asp:Label>

                                    <telerik:RadComboBox ID="radcboCompany" runat="server" DataSourceID="objdsCompany" Visible="false"
                                        DataTextField="CompanyName" DataValueField="CompanyId" Skin="Windows7" Width="100%"
                                        AutoPostBack="True" Enabled="False">
                                    </telerik:RadComboBox>
                                    <asp:ObjectDataSource ID="objdsCompany" runat="server" OldValuesParameterFormatString="original_{0}"
                                        SelectMethod="GetCompanyProfile" TypeName="HRM.Common.BLL.CompanyProfile">
                                        <SelectParameters>
                                            <asp:Parameter DefaultValue="0" Name="companyId" Type="Int32" />
                                        </SelectParameters>
                                    </asp:ObjectDataSource>
                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label1" CssClass="form-label" runat="server" Text="Department Type"></asp:Label>

                                    <dx:ASPxComboBox ID="radcboOrganizationType" CssClass="style1" runat="server" Width="100%" 
                                        OnDataBound="radcboOrganizationType_DataBound1" OnSelectedIndexChanged="radcboOrganizationType_SelectedIndexChanged"
                                        Skin="Windows7" AutoPostBack="True" NullText="Please Select Department Type">
                                    </dx:ASPxComboBox>
             

                                </div>
                            </div>
                            <div class="row mt-3">
                                <div class="col-md-6">
                                    <asp:Label ID="Label2" CssClass="form-label" runat="server" Text="Department Level Index"></asp:Label>

                                    <dx:ASPxComboBox ID="ddlIndex" CssClass="style1" runat="server" Skin="Windows7" Width="100%" Enabled="false">
                                        <Items>
                                            <dx:ListEditItem runat="server" Text="01" Value="1" />
                                            <dx:ListEditItem runat="server" Text="02" Value="2" />
                                            <dx:ListEditItem runat="server" Text="03" Value="3" />
                                            <dx:ListEditItem runat="server" Text="04" Value="4" />
                                            <dx:ListEditItem runat="server" Text="05" Value="5" />
                                            <dx:ListEditItem runat="server" Text="06" Value="6" />
                                            <dx:ListEditItem runat="server" Text="07" Value="7" />
                                            <dx:ListEditItem runat="server" Text="08" Value="8" />
                                            <dx:ListEditItem runat="server" Text="09" Value="9" />
                                            <dx:ListEditItem runat="server" Text="10" Value="10" />
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
                            <div class="d-flex justify-content-end mr-lg-5 mt-5" style="grid-gap: 15px">
                                <div>
                                    <telerik:RadButton ID="btnSave" runat="server" OnClick="radbtnSave_Click" Text="Save"
                                        ValidationGroup="a" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnUpdate" runat="server" OnClick="radbtnUpdate_Click"
                                        Text="Update" ValidationGroup="a" Enabled="False" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnDelete" runat="server" OnClick="radbtnDelete_Click"
                                        Text="Delete" Enabled="False" Visible="False" Skin="WebBlue" />
                                </div>
                                <div>
                                    <telerik:RadButton ID="btnCancel" runat="server" OnClick="radbtnCancel_Click"
                                        Text="Cancel" Skin="WebBlue" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>



                    <asp:HiddenField ID="hfParentId" runat="server" />
                    <asp:HiddenField ID="hfChildLevel" runat="server" />

                    <div id="toastrContainer">
                        <asp:Label ID="lblResult" CssClass="form-label" runat="server" ForeColor="Red"></asp:Label>
                        <img style="width: 12px; cursor: pointer" src="../../App_Themes/NewTheme/img/cancel.png" onclick="closeToastr()" />
                    </div>

                    <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                        <ProgressTemplate>
                            <img alt="" src="../../App_Themes/CommonResources/progress.gif" style="width: 40px; height: 40px; float: right;" />
                        </ProgressTemplate>
                    </asp:UpdateProgress>
                </div>
<%--                <div>
                    <div id="PopupPane2" class="shadow-sm" style="background-color: white; border-radius: 6px">
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
                    </div>
                </div>--%>
            <div class="ContentBlock">
                          <asp:Panel ID="PopupPanel" runat="server" CssClass="popstyle popstyle-2 shadow-lg" ScrollBars="Vertical">
              <div class="style1">
                  <div style="float: right;margin-top: -10px;">
                      <asp:LinkButton ID="btnClosePopup" runat="server" > <img src="../../../App_Themes/NewTheme/img/close.png" /></asp:LinkButton>
                  </div>
                  <div id="org-stucture" >
                      <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                  </div>
              </div>
          </asp:Panel>
  <%--              <asp:Panel ID="PopupPanel" runat="server" CssClass="popstyle" Height="580px" ScrollBars="Horizontal">
                    <table class="style1">
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
                                <uc1:OrganizationStucture ID="OrganizationStucture1" runat="server" />
                            </td>
                            <td>&nbsp;
                            </td>
                        </tr>
                    </table>
                </asp:Panel>--%>

            </div>
                      </div>
        <div class="d-flex justify-content-end mr-lg-3 mt-5" style="grid-gap: 48px;position:fixed;margin-left:10%;">
            <asp:TreeView ID="TreeView1" ExpandDepth="1" PopulateNodesFromClient="true" ShowLines="true"
                ShowExpandCollapse="true" runat="server" OnTreeNodePopulate="TreeView1_TreeNodePopulate"
                SkinID="treeOrganizationstucture" OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
            </asp:TreeView>
        </div>
     

        </div>
    <%--   </ContentTemplate>
    </asp:UpdatePanel>--%>
</asp:Content>
