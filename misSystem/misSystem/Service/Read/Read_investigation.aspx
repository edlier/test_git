<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master"AutoEventWireup="true" CodeBehind="Read_investigation.aspx.cs" Inherits="misSystem.Service.Read_investigation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>MIS - investigation_information</title>
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

    <div class="Lp550_Tm5_gradient40px">Investigation Report </div>

    <div class="Lp550-Tm325-georgia18px">
        <asp:Label ID="Label17" runat="server" Text="ServiceLog ID：" Font-Bold="true"></asp:Label>
        <asp:Label ID="ID" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbl_Name" runat="server" Text="Failure Module or System：" Font-Bold="true"></asp:Label>
        <asp:Label ID="FMS" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Failure Part SN：" Font-Bold="true"></asp:Label>
        <asp:Label ID="FPS" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Preliminary Evaluation：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="PE" runat="server" Height="150px" Width="400px" TextMode="MultiLine" ReadOnly="true" ></asp:TextBox>
        <br />
        
        <br />
        <asp:Label ID="Label4" runat="server" Text="Investigation Detailed：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="1." Font-Bold="true"></asp:Label>
        <asp:Label ID="id1" runat="server" Width="400px"  ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="2." Font-Bold="true"></asp:Label>
        <asp:Label ID="id2" runat="server" Width="400px" ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="3." Font-Bold="true"></asp:Label>
        <asp:Label ID="id3" runat="server" Width="400px" ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label9" runat="server" Text="4." Font-Bold="true"></asp:Label>
        <asp:Label ID="id4" runat="server" Width="400px" ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label10" runat="server" Text="5." Font-Bold="true"></asp:Label>
        <asp:Label ID="id5" runat="server" Width="400px"  ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label11" runat="server" Text="6." Font-Bold="true"></asp:Label>
        <asp:Label ID="id6" runat="server" Width="400px" ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label12" runat="server" Text="7." Font-Bold="true"></asp:Label>
        <asp:Label ID="id7" runat="server" Width="400px" ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label13" runat="server" Text="Failure Root Cause or Possible Cause：" Font-Bold="true"></asp:Label>
        <asp:Label ID="FRCPC" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label14" runat="server" Text="Other :" Font-Bold="true"></asp:Label>
        <asp:Label ID="Other" runat="server" ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label15" runat="server" Text="Recommended Actions :" Font-Bold="true"></asp:Label>
        <asp:Label ID="recommended" runat="server" Width="400px" ></asp:Label>
        <br />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="回上一頁"  OnClick="Button1_Click"/>
        <br />
        <br />       
        <br />
        <br />
    </div>

</asp:Content>
