<%@ Page Title="" Language="C#" MasterPageFile="~/MIS/MIS.master"  AutoEventWireup="true" CodeBehind="AddNewManager.aspx.cs" Inherits="misSystem.MIS.Manager.AddNewManager" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Add Manager</title>
    <link href="../../CssFile/btn.css" rel="stylesheet" />
    <link href="../../CssFile/position.css" rel="stylesheet" />
    <link href="../../CssFile/tittle.css" rel="stylesheet" />
    <link href="../../CssFile/tb.css" rel="stylesheet" />
    <link href="../../CssFile/tb.css" rel="stylesheet" />
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentForMIS" runat="server">
    <div class="Lp550_Tm5_gradient40px">Add New Manager</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:Label ID="lbl_Name" runat="server" Text="User Account Name："></asp:Label>
        <asp:DropDownList ID="drop_UserList" runat="server" DataTextField="idMISid" DataValueField="id" CssClass="Georgia18px" OnInit="drop_UserInit"></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Dep："></asp:Label>
        <asp:DropDownList ID="drop_dept" runat="server" DataTextField="deps" DataValueField="id" CssClass="Georgia18px" OnInit="drop_deptInit"></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Level："></asp:Label>
        <asp:DropDownList ID="drop_level" runat="server" DataTextField="levelList" DataValueField="id" CssClass="Georgia18px" OnInit="drop_levelInit"></asp:DropDownList>
        <br />
        <br /><br />
        <asp:Button ID="btn_set" runat="server" Text="Set" OnClick="btn_set_Click" CssClass="W150H30-georgia20px"/>
    </div>
</asp:Content>
