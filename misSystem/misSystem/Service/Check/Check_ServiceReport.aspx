<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="Check_ServiceReport.aspx.cs" Inherits="misSystem.Service.Check_ServiceReport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <title>MIS - Service Report 主管確認</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        $(function () {
            //會互相干擾
            $("#datepicker").datepicker({ dateFormat: 'yy-mm-dd' });
            //$("#datepicker").datepicker({
            //    changeMonth: true,
            //    changeYear: true,
            //});
        });
    </script>
    <style>
        .gradient30 {
            font-size: 30px;
            background: -webkit-linear-gradient(top,#fd0b58 0,#a32b68 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            font-family: Georgia;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForService" runat="server">

        <div class="Lp550_Tm5_gradient40px">Service Report 主管確認</div>

    <div class="Lp550-Tm325-georgia18px">

        <asp:Label ID="ID" runat="server" Text="" Visible="false"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Panel ID="IR" runat="server">
        <asp:Label ID="Label3" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <br />
        <br /></asp:Panel>
        <asp:Button ID="btn_set" runat="server" Text="Confirm" CssClass="W150H30-georgia20px" OnClick="btn_set_Click" />
        <br />
        <br />
    </div>

</asp:Content>
