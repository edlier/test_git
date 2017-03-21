<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webUCDateBetween.ascx.cs" Inherits="misSystem.UserControl.utils.webUCDateBetween" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

    <asp:TextBox ID="txtDate1" runat="server" CssClass="uc_tb_css_w50x" Width="75px" MaxLength="10" OnTextChanged="txtDate1_TextChanged" onclick="this.focus();this.select()"></asp:TextBox>
    <asp:ImageButton ID="butDate1" runat="server" AlternateText="Open Calendar，Pickup Date" Height="16px"
        ImageUrl="~/img/calendar/Calendar_scheduleHS.png" Width="16px" />
    ～<asp:TextBox ID="txtDate2" CssClass="uc_tb_css_w50x" runat="server" Width="75px" MaxLength="10" OnTextChanged="txtDate2_TextChanged" onclick="this.focus();this.select()"></asp:TextBox>
    <asp:ImageButton ID="butDate2" runat="server" AlternateText="Open Calendar，Pickup Date" Height="16px"
        ImageUrl="~/img/calendar/Calendar_scheduleHS.png" Width="16px" />    

<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="butDate1"
    TargetControlID="txtDate1" Format="yyyy/MM/dd" CssClass="APCalendar">
</cc1:CalendarExtender>
<cc1:CalendarExtender ID="CalendarExtender2" runat="server" Enabled="True" PopupButtonID="butDate2"
    TargetControlID="txtDate2" Format="yyyy/MM/dd" CssClass="APCalendar">
</cc1:CalendarExtender>

