<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="QC.aspx.cs" Inherits="misSystem.Service.QC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - QC Examine</title>
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

        <div class="Lp550_Tm5_gradient40px">
            品保檢驗
            
        </div>

    <div class="Lp550-Tm325-georgia18px">

        <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="Product SN : " Font-Size="X-Large" ForeColor="Red" Font-Bold="true" ></asp:Label>
            <asp:Label ID="productSN" runat="server" Text="" Font-Size="X-Large" ForeColor="Red" Font-Bold="true"></asp:Label>
        <br />
            <br />
        <br />
            <br />

        <asp:Label ID="Label3" runat="server" Text="Returned List：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
        <asp:CheckBox id="QC1" runat="server" AutoPostBack="True" Text="Console" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC2" runat="server" AutoPostBack="True" Text="LCD" TextAlign="Right"/>
         &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC3" runat="server" AutoPostBack="True" Text="YAG or SLT" TextAlign="Right"/>
         &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC4" runat="server" AutoPostBack="True" Text="Delivery System" TextAlign="Right"/>
        <br />
        <br />
        <asp:CheckBox id="QC5" runat="server" AutoPostBack="True" Text="Accesories :" TextAlign="Right"/>
        <asp:TextBox ID="QC5accesories" runat="server" ></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC6" runat="server" AutoPostBack="True" Text="Manual" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC7" runat="server" AutoPostBack="True" Text="Table top & stands" TextAlign="Right"/>
        <br />
        <br />
        <asp:CheckBox id="QC8" runat="server" AutoPostBack="True" Text="TruScan Housing / TruScan" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC9" runat="server" AutoPostBack="True" Text="Brief Case" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC10" runat="server" AutoPostBack="True" Text="Laser Arm Module" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <br />
        <br />
        <asp:CheckBox id="QC11" runat="server" AutoPostBack="True" Text="Control Box" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="QC12" runat="server" AutoPostBack="True" Text="Others" TextAlign="Right"/>
        <asp:TextBox ID="QC12other" runat="server" ></asp:TextBox>
        <br />
        <br /><br />
        </ContentTemplate>
        </asp:UpdatePanel>
        <asp:Button ID="btn_set" runat="server" Text="Confirm" CssClass="W150H30-georgia20px" OnClick="btn_set_Click"/>
        <br />
        <br />
    </div>
</asp:Content>
