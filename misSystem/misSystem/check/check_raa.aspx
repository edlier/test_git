<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="check_raa.aspx.cs" Inherits="misSystem.check.check_raa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/check.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle" runat="server">
        <div id="title">Review and Approval</div>
        <h3>Subject</h3>
        <ul id="ul_subject" runat="server">
        </ul>
        <h3>Dep</h3>
        <asp:Label ID="lbl_dep" runat="server"/>
        <asp:Label ID="lbl_depindex" Visible="false" runat="server"/>
        <h3>Comment</h3>
        <asp:TextBox ID="tb_cmt" runat="server"></asp:TextBox><br/><br/>
        <a id="a1" runat="server">會辦單 Review</a><br/><br/>
        <asp:Button ID="btn_submit" Text="Submit" runat="server" OnClick="btn_submit_Click" />
    </div>
<%--    <div id="nothing" runat="server">
        <h1>You Have Nothing To Do</h1>
        <asp:Button ID="btn_back" Text="Back" runat="server" OnClick="btn_back_Click" />
    </div>--%>
</asp:Content>
