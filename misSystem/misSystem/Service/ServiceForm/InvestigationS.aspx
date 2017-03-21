<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="InvestigationS.aspx.cs" Inherits="misSystem.Service.InvestigationS" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Investigation(Service)</title>
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
    <div class="Lp550_Tm5_gradient40px">Investigation Report (Service)</div>

    <div class="Lp550-Tm325-georgia18px">

        <asp:Label ID="Label6" runat="server" Text="Service Log :" Font-Size="Larger" ForeColor="Red" Font-Bold="true"></asp:Label> 
        <asp:Label ID="ID" runat="server" Text="" Font-Size="Larger" ForeColor="Red" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Product SN：" Font-Bold="true"></asp:Label>
        <asp:Label ID="productsn" runat="server" Text=""></asp:Label>
        <br />
        <br />

        <asp:Label ID="Label7" runat="server" Text="Describe the Complaint：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="describec" runat="server" Height="150px" Width="400px" TextMode="MultiLine"  ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <hr  style="height:5px" noshade="noshade" />
        <br />
        <br />
        <asp:Label ID="lbl_Name" runat="server" Text="Failure Module or System："></asp:Label>
        <asp:DropDownList ID="FMOS" runat="server" DataTextField="partNo"  CssClass="Georgia18px" ></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Failure Part SN："></asp:Label>
        <asp:DropDownList ID="FPS" runat="server" DataTextField="SN"  CssClass="Georgia18px" ></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Preliminary Evaluation："></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="PE" runat="server" Height="150px" Width="400px" TextMode="MultiLine" ></asp:TextBox>
        <br />
        <br /><br />
        <asp:Button ID="btn_set" runat="server" Text="Submit" CssClass="W150H30-georgia20px" OnClick="btn_set_Click"/>
        <br />
        <br />
                <br />
        <br />
    </div>
</asp:Content>
