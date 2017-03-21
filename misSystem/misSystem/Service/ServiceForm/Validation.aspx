<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="Validation.aspx.cs" Inherits="misSystem.Service.Validation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Validation</title>
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

    
    <div class="Lp550_Tm5_gradient40px">Validation</div>

    <div class="Lp550-Tm325-georgia18px">

        <br />
        <br />
        <asp:Label ID="ID" runat="server" Text="" Font-Bold="true" Font-Size="Larger" ForeColor="Red"></asp:Label>
        <br />
        <br />
        <asp:Label ID="lbl_Name" runat="server" Text="Describe the condition：" Font-Bold="true"></asp:Label>
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
                    <asp:Label ID="rd1" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="cd1" runat="server" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ia2" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="rd2" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="cd2" runat="server" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ia3" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="rd3" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="cd3" runat="server" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="Action Plan" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Table ID="Table2" runat="server"
            GridLines="Both"
            BorderColor="black" 
            BorderWidth="1" 
            
            CellPadding="10" >
            <asp:TableRow BackColor="LightGoldenrodYellow" BorderColor="black" BorderWidth="1"  ForeColor="Black" >
                <asp:TableCell>
                    Corrective / Preventive Action
                </asp:TableCell>
                <asp:TableCell>
                    Responsible Dept.
                </asp:TableCell>
                <asp:TableCell>
                    Due Date
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ap1" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="aprd1" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="apdd1" runat="server" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ap2" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="aprd2" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="apdd2" runat="server"  ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ap3" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="aprd3" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="apdd3" runat="server" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:Label ID="Label4" runat="server" Text="Renew FMEA：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:RadioButton ID="rfyes" GroupName="M" runat="server" Text="Yes: "  Enabled="false"/>
        <asp:Label ID="yes" runat="server" ></asp:Label>
        
        <br />
        <br />
        <asp:RadioButton ID="rfno" GroupName="M" runat="server" Text="No,Reason : "  Enabled="false"/>
        <asp:Label ID="no" runat="server" Width="400px" ></asp:Label>
        <br />
        <br />
        <hr  style="height:5px" noshade="noshade" />
                <br />
        <br />
        <asp:Label ID="Label8" runat="server" Text="Validation Detail："></asp:Label>
        <br />
        <br />
        <asp:TextBox ID="validation" runat="server" Height="150px" Width="400px" TextMode="MultiLine"></asp:TextBox>
        <br />
        <br />
        <br />
        <asp:Button ID="btn_set" runat="server" Text="Submit" CssClass="W150H30-georgia20px" OnClick="btn_set_Click" />
        <br />
        <br />
    </div>

</asp:Content>
