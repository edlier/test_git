<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="check_raafinal.aspx.cs" Inherits="misSystem.check.check_raafinal" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/check.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        <div id="title">Review and Approval Record</div>
        <asp:Table ID="tb1" GridLines="Both" BorderStyle="Solid" BorderWidth="1" Width="500px" runat="server">
            <asp:TableRow>
                <asp:TableCell>Subject</asp:TableCell>
                <asp:TableCell ColumnSpan="2">
                    <ul id="ul_subject" runat="server"></ul>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>Dept</asp:TableCell>
                <asp:TableCell>Comment</asp:TableCell>
                <asp:TableCell>Sign&Date</asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br/><br/>
        Comment: <asp:TextBox ID="tb_comment" runat="server"></asp:TextBox>
        <br/><br/>
        <a href="overview_raa.aspx">會辦單 Review</a><br/><br/>
        <asp:Button ID="btn_submit" Text="Submit" runat="server" OnClick="btn_submit_Click" />
    </div>
</asp:Content>
