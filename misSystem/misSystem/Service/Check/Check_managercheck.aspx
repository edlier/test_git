<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="Check_managercheck.aspx.cs" Inherits="misSystem.Service.Check_managercheck" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <title>MIS - 主管審核</title>
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

    <div class="Lp550_Tm5_gradient40px">主管審核</div>

    <div class="Lp550-Tm325-georgia18px">

        <br />
        <br />
        <asp:Label ID="ID" runat="server" Text="" Font-Bold="true" Font-Size="Larger" ForeColor="Red" Visible="false"></asp:Label>
        <br />
        <br />

        <asp:Label ID="log" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Label ID="sr" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <br />
        <asp:Panel ID="IR" runat="server">
        <asp:Label ID="igr" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <br /></asp:Panel>
        <asp:Label ID="Label6" runat="server" Text="" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbl_Name" runat="server" Text="Describe the condition：" Font-Bold="true" ></asp:Label>
        <asp:RadioButton ID="dtc1" GroupName="Mm" runat="server" Text="Potential"  Enabled="false"  />
        <asp:RadioButton ID="dtc2" GroupName="Mm" runat="server" Text="Nonconformity" Enabled="false" />
        <br />
        <br />
        <asp:TextBox ID="describec" runat="server" Height="150px" Width="400px" TextMode="MultiLine"  ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="Analysis of Basic Root Cause：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="aobrc" runat="server" Height="150px" Width="400px" TextMode="MultiLine" ReadOnly="true" ></asp:TextBox>

        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Immediate Action" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Table ID="Table1" runat="server"
            GridLines="Both"
            BorderColor="black" 
            BorderWidth="1" 
            
            CellPadding="10" >
            <asp:TableRow BackColor="LightGoldenrodYellow" BorderColor="black" BorderWidth="1"  ForeColor="Black" >
                <asp:TableCell>
                    Immediate Action
                </asp:TableCell>
                <asp:TableCell>
                    Responsible Dept.
                </asp:TableCell>
                <asp:TableCell>
                    Commplete Date
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ia1" runat="server"  ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="rd1" runat="server" ReadOnly="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="cd1" runat="server" ReadOnly="true" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ia2" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="rd2" runat="server" ReadOnly="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="cd2" runat="server" ReadOnly="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ia3" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="rd3" runat="server" ReadOnly="true"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="cd3" runat="server" ReadOnly="true"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />

        <br />
        <br />
        <asp:Button ID="btn_set" runat="server" Text="Confirm" CssClass="W150H30-georgia20px" OnClick="btn_set_Click" />
        <br />
        <br />
        <br />
        <br />
    </div>

</asp:Content>
