<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="check_raarmg.aspx.cs" Inherits="misSystem.check.check_raarmg" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/check.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        <div id="title">Subject</div>
        <ol>
            <li><asp:Label ID="lbl_sub1" runat="server"></asp:Label></li>
            <li><asp:Label ID="lbl_sub2" runat="server"></asp:Label></li>
            <li><asp:Label ID="lbl_sub3" runat="server"></asp:Label></li>
            <li><asp:Label ID="lbl_sub4" runat="server"></asp:Label></li>
            <li><asp:Label ID="lbl_sub5" runat="server"></asp:Label></li>
        </ol>
        <a id="a1" runat="server">會辦單 Review</a><br/><br/>
        <asp:Button ID="btn_submit" Text="Submit" runat="server" OnClick="btn_submit_Click" />
    </div>
</asp:Content>
