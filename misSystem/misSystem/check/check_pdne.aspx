<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="check_pdne.aspx.cs" Inherits="misSystem.check.check_pdne" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/check.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        <div id="title">Evaluation  or Regulatory or Safety</div>
        <label>CE make </label>
        <asp:RadioButtonList ID="rdbtn_ce" runat="server">
            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
            <asp:ListItem Text="No" Value="0"></asp:ListItem>
        </asp:RadioButtonList><br/><br/>
        <label>510(K) </label>
        <asp:RadioButtonList ID="rdbtn_510" runat="server">
            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
            <asp:ListItem Text="No" Value="0"></asp:ListItem>
        </asp:RadioButtonList><br/><br/>
        <label>Safety risk/安全風險評估 </label>
        <asp:RadioButtonList ID="rdbtn_sf" runat="server">
            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
            <asp:ListItem Text="No" Value="0"></asp:ListItem>
        </asp:RadioButtonList><br/><br/>
        <label>Standard/涉及標準要求 </label>
        <asp:RadioButtonList ID="rdbtn_st" runat="server">
            <asp:ListItem Text="Yes" Value="1"></asp:ListItem>
            <asp:ListItem Text="No" Value="0"></asp:ListItem>
        </asp:RadioButtonList>
        <br/><br/>
        <asp:Button Text="Submit" runat="server" OnClick="Submit_Click" />
    </div>
</asp:Content>
