<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_setHolidays.aspx.cs" Inherits="misSystem.HR.HR_setHolidays" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Set Holidays</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet"/>   
    <link href="myCss/HR_css.css" rel="stylesheet"/>

    <script src="myJs/jquery-ui.multidatespicker.js"></script>
    <script src="myJs/holidays_js.js"></script>
    <link href="myCss/holidays_css.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="out_div">
        <h3 style="display:inline; margin-right:3em;">假日設定</h3>
        <select id="year_sel"></select><br />
        <label id="total_days_lab" class="label-1" style="display:none;"></label>
        上班日: <label id="work_days_lab" class="label-1"></label> 天，
        放假日: <label id="holiday_days_lab" class="label-1"></label> 天
        <button id="reset_btn" style="margin:3px; margin-left:4em; font-size:14pt;">預設</button>        
        <button id="save_btn" style="margin:3px; font-size:14pt;">儲存</button>
        <div class="full-year"></div>
        <textarea id="pickDate_text" rows="5" cols="30" style="display:none;"></textarea>
    </div>  
</asp:Content>
