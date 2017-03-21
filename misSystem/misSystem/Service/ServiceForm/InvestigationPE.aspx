<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="InvestigationPE.aspx.cs" Inherits="misSystem.Service.InvestigationPE" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Investigation(PE)</title>
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

    <div class="Lp550_Tm5_gradient40px">Investigation Report (PE)</div>

    <div class="Lp550-Tm325-georgia18px">
        <br />


        <asp:Label ID="log" runat="server" Text="" Font-Bold="true"></asp:Label>
        
        <br />
        <br />
            <asp:Label ID="Label11" runat="server" Text="Product SN : " Font-Bold="true"></asp:Label>
            <asp:Label ID="productSN" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label12" runat="server" Text="Failure Module or System：" Font-Bold="true"></asp:Label>
        <asp:Label ID="FMS" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label13" runat="server" Text="Failure Part SN：" Font-Bold="true"></asp:Label>
        <asp:Label ID="FPS" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label14" runat="server" Text="Preliminary Evaluation：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="PE" runat="server" Height="150px" Width="400px" TextMode="MultiLine" ReadOnly="true" ></asp:TextBox>
        <br />
        <br />
        <hr  style="height:5px" noshade="noshade" />
        <br />
        <asp:Label ID="lbl_Name" runat="server" Text="Investigation Detailed："></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="1."></asp:Label>
        <asp:TextBox ID="id1" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="2."></asp:Label>
        <asp:TextBox ID="id2" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="3."></asp:Label>
        <asp:TextBox ID="id3" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="4."></asp:Label>
        <asp:TextBox ID="id4" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="5."></asp:Label>
        <asp:TextBox ID="id5" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="6."></asp:Label>
        <asp:TextBox ID="id6" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="7."></asp:Label>
        <asp:TextBox ID="id7" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Failure Root Cause or Possible Cause："></asp:Label>
        <asp:DropDownList ID="frcpc" runat="server" DataTextField="idMISid" DataValueField="id" CssClass="Georgia18px" ></asp:DropDownList>
        <br />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Other"></asp:Label>
        <asp:TextBox ID="Other" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label10" runat="server" Text="Recommended Actions :"></asp:Label>
        <asp:TextBox ID="recommended" runat="server" Width="400px" ></asp:TextBox>
        <br />
        <br /><br />
        <asp:Button ID="btn_set" runat="server" Text="Submit" CssClass="W150H30-georgia20px" OnClick="btn_set_Click"/>
        <br />
        <br />
    </div>
</asp:Content>
