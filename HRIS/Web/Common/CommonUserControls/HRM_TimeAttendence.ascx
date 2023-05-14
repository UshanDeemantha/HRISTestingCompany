<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HRM_TimeAttendence.ascx.cs" Inherits="CommenUserControls_HRM_TimeAttendence" %>
<style type="text/css">

img{ border: none;}
</style>
                                    <table style="text-align:center" 
    width="100%">
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgEmployeeTimeShift" runat="server" ImageUrl="~/App_Themes/Common/Dashboard/Small/Employee-time-Shift.png"
                                                    Width="80px" OnClick="imgEmployeeTimeShift_Click" />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgLeaveTypes" runat="server" 
                                                    ImageUrl="~/App_Themes/Common/Dashboard/Small/leave.png" Width="80px" onclick="imgLeaveTypes_Click" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <asp:ImageButton ID="imgShifts" runat="server" 
                                                    ImageUrl="~/App_Themes/Common/Dashboard/Small/Shift.png" Width="80px" onclick="imgShifts_Click"  />
                                            </td>
                                            <td>
                                                <asp:ImageButton ID="imgTimeCard" runat="server" 
                                                    ImageUrl="~/App_Themes/Common/Dashboard/Small/time-card.png" Width="80px" onclick="imgTimeCard_Click" />
                                            </td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                            <td>
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                
