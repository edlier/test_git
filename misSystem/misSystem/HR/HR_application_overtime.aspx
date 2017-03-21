﻿<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_application_overtime.aspx.cs" Inherits="misSystem.HR.HR_application_overtime" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Overtime application</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet"/>
    <script src="myJs/jquery.timepicker.js"></script>
    <link href="myCss/jquery.timepicker.css" rel="stylesheet" />
    <link href="myCss/HR_css.css" rel="stylesheet" />
    <script src="myJs/HR_js.js"></script>
        
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">    
    <asp:Panel ID="out_panel" CssClass="out_panel" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

        <asp:Panel ID="form_panel" runat="server">

        <h3>加班申請單</h3>
        申請人: <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
        <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label><br />
        日期: <asp:TextBox ID="start_date_text" runat="server" CssClass="datepicker" TextMode="DateTime"></asp:TextBox><br />
        時間: <asp:TextBox ID="start_time_text" runat="server" CssClass="timepicker" AutoPostBack="True" OnTextChanged="start_time_text_TextChanged" ></asp:TextBox> To 
        <asp:TextBox ID="end_time_text" runat="server" CssClass="timepicker" AutoPostBack="True" OnTextChanged="start_time_text_TextChanged"></asp:TextBox>
            <asp:Label ID="hours_lab" Text="" runat="server" CssClass="label-2"></asp:Label><br />
            <asp:Label ID="datetime_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label>
            <table style="margin: 0; padding: 0; height: auto;">
                <tr>
                    <td style="vertical-align: top;">工作內容:</td>
                    <td style="vertical-align: top; padding: 0;">
                        <asp:TextBox ID="content_text" runat="server" MaxLength="100" TextMode="MultiLine" Style="width: 15em; height: 100%;" AutoPostBack="True" OnTextChanged="content_text_TextChanged"></asp:TextBox></td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Label ID="content_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></td>
                </tr>
                <tr>
                    
                </tr>

            </table>
            地點:
            <asp:TextBox ID="location_text" runat="server" MaxLength="20" Width="12em" AutoPostBack="True" OnTextChanged="location_text_TextChanged"></asp:TextBox>
            <asp:Label ID="location_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label><br />
            <br />
            <asp:CheckBox runat="server" ID="cb_foodAllowance"/><asp:Label ID="Label1" Text="誤餐費" runat="server" Visible="true"></asp:Label>
            <table>
                <tr>
                    <br />
                    <br />
            <td>予以:</td>
            <td><asp:RadioButtonList ID="pay_rad" runat="server" >
                    <asp:ListItem Text="加班費" Value="extraPay"></asp:ListItem>
                    <asp:ListItem Text="補休" Value="extraDayoff" Selected="True"></asp:ListItem>
                </asp:RadioButtonList></td>
        </tr></table>

        </asp:Panel>

        <asp:Button ID="send_btn" runat="server" Text="Send" Font-Size="16pt" style="margin-left:10em" OnClick="send_btn_Click" />
        <asp:Button ID="pass_btn" runat="server" Text="Pass" Font-Size="16pt" style="margin-left:10em" OnClick="pass_btn_Click" Visible="False" />
        <asp:Button ID="fail_btn" runat="server" Text="Fail" Font-Size="16pt" style="margin-left:1em" OnClick="fail_btn_Click" Visible="False" />
        <asp:Button ID="edit_btn" runat="server" Text="Edit" Font-Size="16pt" style="margin-left:10em" OnClick="edit_btn_Click" Visible="False" />
        <asp:Button ID="delete_btn" runat="server" Text="Delete" Font-Size="16pt" style="margin-left:1em" OnClick="delete_btn_Click" Visible="False" />
    
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker("option", "minDate", -5);
            $('.datepicker').datepicker("option", "maxDate", +20);

            //配合update panel不會重新執行ready事件
            //為update panel註冊事件，在update完成後執行initi_up();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_pageLoaded(initi_up);
            function initi_up() {
                date_initi();
                $('.datepicker').datepicker("option", "minDate", -5);
                $('.datepicker').datepicker("option", "maxDate", +20);
            }
        });
    </script>
</asp:Content>
