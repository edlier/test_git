<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="overview_raa.aspx.cs" Inherits="misSystem.check.overview_raa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/overview.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        主辦單位: <asp:Label ID="lbl_resdept" runat="server"></asp:Label><br/><br/>
        填表人員: <asp:Label ID="lbl_wrby" runat="server"></asp:Label><br/><br/>
        單位主管: <asp:Label ID="lbl_mg" runat="server"></asp:Label><br/><br/>
        編號: <asp:Label ID="lbl_no" runat="server"></asp:Label><br/><br/>
        <hr/>
        <h3>Subject</h3>
        <ul id="subject" runat="server"></ul>
        <hr/>
        <asp:Table ID="table1" BorderStyle="Solid" BorderWidth="1" CellSpacing="10" CellPadding="5" runat="server">
            <asp:TableHeaderRow>
                <asp:TableHeaderCell>Depts</asp:TableHeaderCell>
                <asp:TableHeaderCell>Comment</asp:TableHeaderCell>
                <asp:TableHeaderCell>Sign & Date</asp:TableHeaderCell>
            </asp:TableHeaderRow>
        </asp:Table>
    </div>
</asp:Content>
