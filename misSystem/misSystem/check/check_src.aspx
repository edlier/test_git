<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="check_src.aspx.cs" Inherits="misSystem.check.check_src" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/check.css" rel="stylesheet" />
    <link href="../CssFile/styles.css" rel="stylesheet" />
    <link href="../CssFile/default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        <div class="title">SRC 審查</div>
        <asp:Table ID="table_basic" BorderWidth="1" GridLines="Both" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Product Module</asp:TableHeaderCell>
                <asp:TableHeaderCell>DCR Assigned No</asp:TableHeaderCell>
                <asp:TableHeaderCell>R&D Project Name</asp:TableHeaderCell>
                <asp:TableHeaderCell>Change Proposed by</asp:TableHeaderCell>
                <asp:TableHeaderCell>Date</asp:TableHeaderCell>
                <asp:TableHeaderCell>Prority</asp:TableHeaderCell>
            </asp:TableHeaderRow>
            <asp:TableRow>
                <asp:TableCell><asp:Label ID="Label1" runat ="server" /></asp:TableCell>
                <asp:TableCell><asp:Label ID="Label2" runat ="server" /></asp:TableCell>
                <asp:TableCell><asp:Label ID="Label3" runat ="server" /></asp:TableCell>
                <asp:TableCell><asp:Label ID="Label4" runat ="server" /></asp:TableCell>
                <asp:TableCell><asp:Label ID="Label5" runat ="server" /></asp:TableCell>
                <asp:TableCell><asp:Label ID="Label6" runat ="server" /></asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <a id="rev" runat="server">Review Report</a>
        <asp:RadioButtonList runat="server">
            <asp:ListItem Text="Approved"></asp:ListItem>
            <asp:ListItem Text="Rejected"></asp:ListItem>
        </asp:RadioButtonList>
        <asp:Button ID="btn_submit" Text="Submit" runat="server" OnClick="btn_submit_Click" />
    </div>
</asp:Content>
