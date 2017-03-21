<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="ServiceFillForm.aspx.cs" Inherits="misSystem.Service.ServiceFillForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Service Fill Form</title>
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
     <div class="Lp550_Tm5_gradient40px">Service Fill Form</div>

    <div class="Lp550-Tm325-georgia18px">

        <br />
        <br />
        <asp:Label ID="Label5" runat="server" Text="Product SN : " Font-Size="Large" ForeColor="Red" Font-Bold="true"></asp:Label>
        <asp:Label ID="productSN" runat="server" Text="" Font-Size="Large" ForeColor="Red"></asp:Label>
        <br />
        <br />

        <asp:Label ID="servicereport" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />        <br />
        <asp:Label ID="Label4" runat="server" Text="Required Reworked ?  " Font-Bold="true"></asp:Label>
        <asp:RadioButton ID="rrk1" GroupName="rewo" runat="server" Text="Yes"   Checked="true" />
        <asp:RadioButton ID="rrk2" GroupName="rewo" runat="server" Text="No"  />
        <br />
        <br />
        <asp:Label ID="Label9" runat="server" Text="Reassigned Required ?  " Font-Bold="true"></asp:Label>
        <asp:RadioButton ID="rrq1" GroupName="rq" runat="server" Text="Yes"  Checked="true" />
        <asp:RadioButton ID="rrq2" GroupName="rq" runat="server" Text="No" />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
        <asp:CheckBox id="acttrcheck" runat="server" AutoPostBack="True" Text="Already completed the Report" TextAlign="Right"/>
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Conclusions：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:CheckBox id="fr1" runat="server" AutoPostBack="True" Text="Product or Part Defected" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr2" runat="server" AutoPostBack="True" Text="Improper / Unauthorized service" TextAlign="Right"/>
         &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr3" runat="server" AutoPostBack="True" Text="Shipping / Handling damage" TextAlign="Right"/>
        <br />
        <br />
        <asp:CheckBox id="fr4" runat="server" AutoPostBack="True" Text="Not met the specification" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr5" runat="server" AutoPostBack="True" Text="Improper usage" TextAlign="Right"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox id="fr6" runat="server" AutoPostBack="True" Text="Other :" TextAlign="Right"/>
        <asp:TextBox ID="fr6other" runat="server" ></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox id="fr7" runat="server" AutoPostBack="True" Text="Inadequate or improper manufacturing process" TextAlign="Right"/>
        </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Document Change Requested :  " Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="fr81" GroupName="rw" runat="server" Text="No" Checked="true" />
        <br />
        <br />
        <asp:RadioButton ID="fr82" GroupName="rw" runat="server" Text="Yes,DCR No: " />
        <asp:TextBox ID="fr8dcr" runat="server" ></asp:TextBox>
        <br />
        <br />
        <br />

        <br />
        <asp:Label ID="Label2" runat="server" Text="Is this required initial a vigilance protocol ?  " Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="vp1" GroupName="rww" runat="server" Text="No" Checked="true" />
        <br />
        <br />
        <asp:RadioButton ID="vp2" GroupName="rww" runat="server" Text="Yes,VS No: " />
        <asp:TextBox ID="vpmsg" runat="server" ></asp:TextBox>
        <br />
        <br /><br />
        <asp:Button ID="btn_set" runat="server" Text="Submit" CssClass="W150H30-georgia20px" OnClick="btn_set_Click"/>
        <br />
        <br />
    </div>
</asp:Content>
