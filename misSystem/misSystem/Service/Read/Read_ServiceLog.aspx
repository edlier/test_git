<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master"AutoEventWireup="true" CodeBehind="Read_ServiceLog.aspx.cs" Inherits="misSystem.Service.Read_ServiceLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>MIS - ServiceLog_information</title>
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
        Service Log : 
        <asp:Label ID="ID" runat="server" Text=""></asp:Label>
    </div>

    <div class="Lp550-Tm325-georgia18px">
        <asp:Label ID="lbl_Name" runat="server" Text="Company Name：" Font-Bold="true"></asp:Label>
        <asp:Label ID="name" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Contact Person：" Font-Bold="true"></asp:Label>
        <asp:Label ID="cp" runat="server" Text=""></asp:Label>
        
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Contact Person Email：" Font-Bold="true"></asp:Label>
        <asp:Label ID="cpemail" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Product SN：" Font-Bold="true"></asp:Label>
        <asp:Label ID="productsn" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Type of Complaint：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
        <asp:CheckBox id="c1" runat="server" AutoPostBack="True" Text="Packaging/Shipping" TextAlign="Right" Enabled="false"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="c2" runat="server" AutoPostBack="True" Text="Labeling" TextAlign="Right" Enabled="false"/>
         &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="c3" runat="server" AutoPostBack="True" Text="Injury or MDR" TextAlign="Right" Enabled="false"/>
         &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="c4" runat="server" AutoPostBack="True" Text="Design Issue" TextAlign="Right" Enabled="false"/>
        <br />
        <br />
        <asp:CheckBox id="c5" runat="server" AutoPostBack="True" Text="Product Failure" TextAlign="Right" Enabled="false"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="c6" runat="server" AutoPostBack="True" Text="Error message :" TextAlign="Right" Enabled="false"/>
        <asp:TextBox ID="c6msg" runat="server"  ReadOnly="true"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="c7" runat="server" AutoPostBack="True" Text="Other :" TextAlign="Right" Enabled="false"/>
        <asp:TextBox ID="c7other" runat="server" ReadOnly="true" ></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox id="c8" runat="server" AutoPostBack="True" Text="Module failure" TextAlign="Right" Enabled="false"/>
        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Module name："></asp:Label>
        <asp:TextBox ID="c8mname" runat="server" ReadOnly="true" ></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Label ID="Label6" runat="server" Text="Module S/N ："></asp:Label>
        <asp:TextBox ID="c8msn" runat="server" ReadOnly="true" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Complaint Received by ：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="Fax" GroupName="crb" runat="server" Text="Fax" Enabled="false"  />
        <asp:RadioButton ID="Email" GroupName="crb" runat="server" Text="Email" Enabled="false" />
        <asp:RadioButton ID="Phone" GroupName="crb" runat="server" Text="Phone" Enabled="false"/>
        <asp:RadioButton ID="Other" GroupName="crb" runat="server" Text="Other" Enabled="false"/>
        <asp:TextBox ID="othermsg" runat="server"  ReadOnly="true" ></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Describe the Complaint：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="describec" runat="server" Height="150px" Width="400px" TextMode="MultiLine"  ReadOnly="true"></asp:TextBox>
        <br />
        <br /><br />
        <asp:CheckBox ID="CR" runat="server"  Text="CR" TextAlign="Right" AutoPostBack="True" Enabled="false"/>

        <div id="IR"  runat="server"  visible="false">
        <br /><br />
        <asp:RadioButton ID="iny" GroupName="in" runat="server" Text="Need Investigation Report"   Visible="false" />
        <br /><br />
        <asp:RadioButton ID="inn" GroupName="in" runat="server" Text="Don't need Investigation Report" Visible="false" />
         </div>
        <br /><br />
        
        <asp:CheckBox ID="RMA" runat="server"  Text="RMA" TextAlign="Right" AutoPostBack="true" Enabled="false" />
             </ContentTemplate>
        </asp:UpdatePanel>
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
