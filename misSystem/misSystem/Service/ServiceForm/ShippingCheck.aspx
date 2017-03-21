<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="ShippingCheck.aspx.cs" Inherits="misSystem.Service.ShippingCheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Shipping Check</title>
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

    <div class="Lp550_Tm5_gradient40px">Shipping Check</div>

    <div class="Lp550-Tm325-georgia18px">

        <asp:Label ID="Label6" runat="server" Text="Service Log :" Font-Size="Larger" ForeColor="Red" Font-Bold="true"></asp:Label> 
        <asp:Label ID="ID" runat="server" Text="" Font-Size="Larger" ForeColor="Red" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbl_Name" runat="server" Text="Company Name：" Font-Bold="true"></asp:Label>
        <asp:Label ID="name" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Contact Person：" Font-Bold="true"></asp:Label>
        <asp:Label ID="cp" runat="server" Text=""></asp:Label>
        
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Contact Person Email：" Font-Bold="true"></asp:Label>
        <asp:Label ID="cpemail" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Product SN：" Font-Bold="true"></asp:Label>
        <asp:Label ID="productsn" runat="server" Text=""></asp:Label>
        <br />
        <br />

        <asp:Label ID="Label8" runat="server" Text="Describe the Complaint：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="describec" runat="server" Height="150px" Width="400px" TextMode="MultiLine"  ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <hr  style="height:5px" noshade="noshade" />
        <br />
        <br />
        <asp:Label ID="ViaName" runat="server" Text="Via：" ></asp:Label>
        <asp:TextBox ID="VIA" runat="server" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Design Recipient："></asp:Label>
        <asp:TextBox ID="DR" runat="server" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Tracking No. or AWB No.：" ></asp:Label>
        <asp:TextBox ID="TNAN" runat="server" ></asp:TextBox>
        <br />
        <br /><br />
        <asp:Button ID="btn_set" runat="server" Text="Submit" CssClass="W150H30-georgia20px" OnClick="btn_set_Click"/>
        <br />
        <br />
        <br />
        <br />
        <br />
        <br />
    </div>
</asp:Content>
