<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="webUCDate.ascx.cs" Inherits="misSystem.UserControl.utils.webUCDate" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<style>
    .TextboxWatermark {
        font-style: italic;
        color: #ACA899;
    }
 </style>

<asp:TextBox ID="txtDate" CssClass="uc_tb_css_w50x" runat="server" Width="75px" MaxLength="10" onclick="this.focus();this.select()"
    OnTextChanged="txtDate_TextChanged"></asp:TextBox>
 <cc1:TextBoxWatermarkExtender ID="TextBoxWatermarkExtender1" runat="server" 
                               WatermarkText="1991/01/01"    
                               TargetControlID="txtDate"                            
                               WatermarkCssClass="TextboxWatermark" >
</cc1:TextBoxWatermarkExtender>
<asp:ImageButton ID="butDate" runat="server" AlternateText="開啟日曆，點選日期" Height="16px"
    ImageUrl="~/img/calendar/Calendar_scheduleHS.png" Width="16px" />
<cc1:CalendarExtender ID="CalendarExtender1" runat="server" Enabled="True" PopupButtonID="butDate"
    TargetControlID="txtDate" Format="yyyy/MM/dd" CssClass="APCalendar">
</cc1:CalendarExtender>

