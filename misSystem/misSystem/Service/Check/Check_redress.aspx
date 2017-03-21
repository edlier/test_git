<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="Check_redress.aspx.cs" Inherits="misSystem.Service.Check_redress" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

        <title>MIS - 矯正措施</title>
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

        <div class="Lp550_Tm5_gradient40px">矯正措施</div>

    <div class="Lp550-Tm325-georgia18px">


        <asp:Label ID="ID" runat="server" Text="" Font-Bold="true" Font-Size="Larger" ForeColor="Red" Visible="false"></asp:Label>


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
        <br /><br />
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
        <asp:Table ID="Table2" runat="server"
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
                    <asp:Label ID="rrd1" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="ccd1" runat="server"  ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ia2" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="rrd2" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="ccd2" runat="server" ></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label Width="300" ID="ia3" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="rrd3" runat="server"></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label  ID="ccd3" runat="server"></asp:Label>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <hr  style="height:5px" noshade="noshade" />
        <br />
        <br />


        <asp:Table ID="Table1" runat="server"
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
                    <asp:Textbox Width="300" ID="ap1" runat="server" ></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="rd1" runat="server" DataTextField="commentstr" DataValueField="customerPID" CssClass="Georgia18px"/>
                </asp:TableCell>
                <asp:TableCell>
                   <asp:UpdatePanel ID="UpdatePanel12" runat="server">
                    <ContentTemplate>
                       <asp:Label  ID="cd1" runat="server" Text="" ></asp:Label>
                        <asp:ImageButton ID="imgButtonRequestDate" runat="server" ImageUrl="~/picture/cald.png" OnClick="imgButtonRequestDate_Click" />
                        <asp:Calendar ID="calendarRequestDate" runat="server" OnSelectionChanged="calendarRequestDate_SelectionChanged" Visible="False" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth">
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                            <DayStyle BackColor="#CCCCCC" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                            <TitleStyle BackColor="#333399" Font-Bold="True" BorderStyle="Solid" Font-Size="12pt" ForeColor="White" Height="12pt" />
                            <TodayDayStyle BackColor="#999999" ForeColor="White" />


                        </asp:Calendar>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Textbox Width="300" ID="ap2" runat="server" ></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="rd2" runat="server" DataTextField="commentstr" DataValueField="customerPID" CssClass="Georgia18px"/>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                       <asp:Label  ID="cd2" runat="server" Text="" ></asp:Label>
                        <asp:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/picture/cald.png" OnClick="imgButtonRequestDate2_Click" />
                        <asp:Calendar ID="calendar1" runat="server" OnSelectionChanged="calendarRequestDate2_SelectionChanged" Visible="False" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth">
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                            <DayStyle BackColor="#CCCCCC" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                            <TitleStyle BackColor="#333399" Font-Bold="True" BorderStyle="Solid" Font-Size="12pt" ForeColor="White" Height="12pt" />
                            <TodayDayStyle BackColor="#999999" ForeColor="White" />


                        </asp:Calendar>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Textbox Width="300" ID="ap3" runat="server" ></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:DropDownList ID="rd3" runat="server" DataTextField="commentstr" DataValueField="customerPID" CssClass="Georgia18px"/>
                </asp:TableCell>
                <asp:TableCell>
                     <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                       <asp:Label  ID="cd3" runat="server" Text="" ></asp:Label>
                        <asp:ImageButton ID="ImageButton2" runat="server" ImageUrl="~/picture/cald.png" OnClick="imgButtonRequestDate3_Click" />
                        <asp:Calendar ID="calendar2" runat="server" OnSelectionChanged="calendarRequestDate3_SelectionChanged" Visible="False" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth">
                            <DayHeaderStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" Height="8pt" />
                            <DayStyle BackColor="#CCCCCC" />
                            <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="White" />
                            <OtherMonthDayStyle ForeColor="#999999" />
                            <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                            <TitleStyle BackColor="#333399" Font-Bold="True" BorderStyle="Solid" Font-Size="12pt" ForeColor="White" Height="12pt" />
                            <TodayDayStyle BackColor="#999999" ForeColor="White" />


                        </asp:Calendar>
                        <br />
                    </ContentTemplate>
                </asp:UpdatePanel>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>
        <br />
        <br />
        <asp:Button ID="btn_set" runat="server" Text="Confirm" CssClass="W150H30-georgia20px" OnClick="btn_set_Click" />
        <br />
        <br />
    </div>

</asp:Content>
