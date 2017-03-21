<%@ Page Title="Version" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="version.aspx.cs" Inherits="misSystem.version" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">




    <h1 style="color: red">Version = 1.011.01</h1>
    <h2 style="color:#000085">

            【HR版本更新】ATT v1.011.01<br />
            系統：BPM SYSTEM - HR<br />
            模組：Personnel (PER) & ATT<br />
            版本：v1.011.01<br />
            更新時間：2017 - 03 -16 11:01<br />
            更新頁面與內容：
            <blockquote style="color:#8A008A">
                1.	修正EmployeeDetail & EmployeeList 月曆顯示問題<br />
                2. 暫時新增Angel打卡資料特殊處理 -> 補回時間 1HR (因為暫時變更上班時間為 0900 ~ 1800)<br />
                3. 修改EmployeeDetail 新增人員時候自動跳過998 ，因為工號998已被使用(游姐)<br />
            </blockquote>
    </h2>
</asp:Content>
