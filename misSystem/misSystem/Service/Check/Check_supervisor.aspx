<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master"AutoEventWireup="true" CodeBehind="Check_supervisor.aspx.cs" Inherits="misSystem.Service.Check_supervisor" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>MIS - Supervisor Check</title>
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

    <div class="Lp550_Tm5_gradient40px">Supervisor Check</div>

    <div class="Lp550-Tm325-georgia18px">

        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Product SN : " Font-Size="Large" ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Label ID="productSN" runat="server" Text="" Font-Size="Large" ForeColor="Red"></asp:Label>
        <br />
        <br />

        <asp:Label ID="servicereport" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <br />
        <hr  style="height:5px" noshade="noshade" />
        <br />
        <asp:Label ID="Label12" runat="server" Text="Service Fill Form" Font-Size="Large" ForeColor="Red" Font-Bold="true"></asp:Label>        
        <br /><br />
        <asp:Label ID="Label3" runat="server" Text="Required Reworked ?  " Font-Bold="true"></asp:Label>
        <asp:RadioButton ID="rrk1" GroupName="rewo" runat="server" Text="Yes" Enabled="false" />
        <asp:RadioButton ID="rrk2" GroupName="rewo" runat="server" Text="No" Enabled="false" />
        <br />
        <br />
        <asp:Label ID="Label6" runat="server" Text="Reassigned Required ?  " Font-Bold="true"></asp:Label>
        <asp:RadioButton ID="rrq1" GroupName="rq" runat="server" Text="Yes"  Enabled="false" />
        <asp:RadioButton ID="rrq2" GroupName="rq" runat="server" Text="No"  Enabled="false"/>
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
        <asp:CheckBox id="acttrcheck" runat="server" AutoPostBack="True" Text="Already completed the Report" TextAlign="Right" Enabled="false"/>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label7" runat="server" Text="Conclusions：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:CheckBox id="fr1" runat="server" AutoPostBack="True" Text="Product or Part Defected" TextAlign="Right" Enabled="false"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr2" runat="server" AutoPostBack="True" Text="Improper / Unauthorized service" TextAlign="Right" Enabled="false"/>
         &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr3" runat="server" AutoPostBack="True" Text="Shipping / Handling damage" TextAlign="Right" Enabled="false"/>
        <br />
        <br />
        <asp:CheckBox id="fr4" runat="server" AutoPostBack="True" Text="Not met the specification" TextAlign="Right" Enabled="false"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr5" runat="server" AutoPostBack="True" Text="Improper usage" TextAlign="Right" Enabled="false"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr6" runat="server" AutoPostBack="True" Text="Other :" TextAlign="Right" Enabled="false"/>
        <asp:TextBox ID="fr6other" runat="server"  ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox id="fr7" runat="server" AutoPostBack="True" Text="Inadequate or improper manufacturing process" TextAlign="Right" Enabled="false"/>
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Document Change Requested :  " Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="fr81" GroupName="rw" runat="server" Text="No" Enabled="false" />
        <br />
        <br />
        <asp:RadioButton ID="fr82" GroupName="rw" runat="server" Text="Yes,DCR No: " Enabled="false" />
        <asp:Label ID="fr8dcr" runat="server"  ></asp:Label>
        <br />
        <br />
        <br />

        <br />
        <asp:Label ID="Label11" runat="server" Text="Is this required initial a vigilance protocol ?  " Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="vp1" GroupName="rww" runat="server" Text="No" Enabled="false" />
        <br />
        <br />
        <asp:RadioButton ID="vp2" GroupName="rww" runat="server" Text="Yes,VS No: " Enabled="false" />
        <asp:Label ID="vpmsg" runat="server"  ></asp:Label>
        <br />
        <br />
        <hr  style="height:5px" noshade="noshade" />
        <br />
        <asp:Label ID="Label13" runat="server" Text="Disposion Process" Font-Size="Large" ForeColor="Red" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbl_Name" runat="server" Text="Disposion Process：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="dp1" GroupName="DP" runat="server" Text="Returned reworked"  Enabled="false" />
        <br />
        <br />
        <asp:RadioButton ID="dp2" GroupName="DP" runat="server" Text="Restock" Enabled="false" />
        <br />
        <br />
        <asp:RadioButton ID="dp3" GroupName="DP" runat="server" Text="Disposed" Enabled="false" />
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Returned Information：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Shipped out date：" Font-Bold="true"></asp:Label>
        <asp:Label ID="outdate" runat="server"  ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label10" runat="server" Text="Chargeable：" Font-Bold="true"></asp:Label>
        <asp:RadioButton ID="cc1" GroupName="M" runat="server" Text="Yes" Enabled="false" />
        <asp:TextBox ID="chargeablemsg" runat="server" ReadOnly="true" ></asp:TextBox>
        <asp:RadioButton ID="cc2" GroupName="M" runat="server" Text="No" Enabled="false" />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="SPI No：" Font-Bold="true"></asp:Label>
        <asp:Label ID="spi" runat="server"  ></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Tracking No：" Font-Bold="true"></asp:Label>
        <asp:Label ID="tn" runat="server"  ></asp:Label>
        <br />
        <br />
        <br />
        <br />
        <asp:Button ID="btn_set" runat="server" Text="Confirm" CssClass="W150H30-georgia20px" OnClick="btn_set_Click" />
        <br />
        <br />
    </div>

</asp:Content>
