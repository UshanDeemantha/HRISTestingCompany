<%@ Page Title="HRIS Employee | Employee List" Language="C#" AutoEventWireup="true" CodeFile="EmployeeList.aspx.cs" Inherits="HR_Employee_EmployeeList" MasterPageFile="~/HR/HR_MenuMasterPage.master" %>

<%@ Register src="~/HR/Employee/UserControls/EmployeeSummaryList.ascx" tagname="EmployeeSummaryList" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
  <uc1:EmployeeSummaryList ID="EmployeeSummaryList1" runat="server" />
</asp:Content>
