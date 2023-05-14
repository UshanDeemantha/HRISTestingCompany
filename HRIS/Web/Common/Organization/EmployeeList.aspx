<%@ Page Title="HRM Common | Employee List" Language="C#" MasterPageFile="~/Common/Common_MenuMasterPage.master" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="Employee_EmployeeList" %>

<%@ Register src="EmployeeSummaryList.ascx" tagname="EmployeeSummaryList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <uc1:EmployeeSummaryList ID="EmployeeSummaryList1" runat="server" />
</asp:Content>

