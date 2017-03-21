<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="Read_servicereport.aspx.cs" Inherits="misSystem.Service.Read_servicereport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>MIS - Read_ServiceReport</title>
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


    <div class="Lp550_Tm5_gradient40px">Service Report</div>

    <div class="Lp550-Tm325-georgia18px">
        <asp:Label ID="prilabel" runat="server" Text="Problem(s) Reported In (原故帳原因)：" Font-Bold="true"></asp:Label>
        <asp:Label ID="pri" runat="server" Width="300"></asp:Label>
        <br />
        <br />
        <asp:Label ID="cflabel" runat="server" Text="Condition(s) Founded (實際故障原因)：" Font-Bold="true"></asp:Label>
        <asp:Label ID="cf" runat="server" Width="300" ></asp:Label>
        <br />
        <br />
        <br />
        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
         <ContentTemplate>
        <asp:Label ID="Label3" runat="server" Text="Final Evaluation (最終結果)：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:CheckBox ID="fe1" runat="server" AutoPostBack="True" Text="Optical Issue" TextAlign="Right" Enabled="false"/>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="fe2" runat="server" AutoPostBack="True" Text="Misalignment Issue" TextAlign="Right" Enabled="false" />
        <br />
        <br />
        <asp:CheckBox ID="fe3" runat="server" AutoPostBack="True" Text="Hardware Issue :" TextAlign="Right" Enabled="false" />
        <asp:TextBox ID="fe3msg" runat="server" ReadOnly="true"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;&nbsp;
        <asp:CheckBox ID="fe4" runat="server" AutoPostBack="True" Text="Software Issue" TextAlign="Right" Enabled="false" />
        <br />
        <br />
        <asp:CheckBox ID="fe5" runat="server" AutoPostBack="True" Text="Other Issue :" TextAlign="Right" Enabled="false" />
        <asp:TextBox ID="fe5msg" runat="server" ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox ID="fe6" runat="server" AutoPostBack="True" Text="Operator Error or Issue :" TextAlign="Right" Enabled="false" />
        <asp:TextBox ID="fe6msg" runat="server" ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        </ContentTemplate>
        </asp:UpdatePanel>

        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
         <ContentTemplate>
        <asp:Label ID="Label7" runat="server" Text="Action(s) Taken (處理方式)：" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:CheckBox ID="at1" runat="server" AutoPostBack="True" Text="Optical Alignment :" TextAlign="Right" Enabled="false"/>
        <asp:TextBox ID="oamsg" runat="server" ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox ID="at2" runat="server" AutoPostBack="True" Text="Calibrated modules :" TextAlign="Right" Enabled="false" />
        <asp:TextBox ID="cmmsg" runat="server" ReadOnly="true"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox ID="at3" runat="server" AutoPostBack="True" Text="Adjusted or Cleaned Major Components :" TextAlign="Right" Enabled="false" />
        <asp:TextBox ID="acmcmsg" runat="server" ReadOnly="true"></asp:TextBox>
        <br />
        <br />

         </ContentTemplate>
        </asp:UpdatePanel>

        <asp:UpdatePanel ID="UpdatePanel4" runat="server">
         <ContentTemplate>
        <asp:CheckBox ID="at4" runat="server" AutoPostBack="True" Text="Replaced the following part(s) :" TextAlign="Right" Enabled="false"  />
        <br />
        <br />
        
        


        <asp:Table ID="Table1" runat="server"
            GridLines="Both"
            BorderColor="black" 
            BorderWidth="1" 
            
            CellPadding="10" >
            <asp:TableRow BackColor="LightGoldenrodYellow" BorderColor="black" BorderWidth="1"  ForeColor="Black" >
                <asp:TableCell>
                    Part no.
                </asp:TableCell>
                <asp:TableCell>
                    Part Description
                </asp:TableCell>
                <asp:TableCell>
                    SN.
                </asp:TableCell>
                <asp:TableCell>
                    Qty
                </asp:TableCell>
                <asp:TableCell>
                    U/P
                </asp:TableCell>
                <asp:TableCell>
                    T/P
                </asp:TableCell>
                <asp:TableCell>
                    Remark
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow ID="row1">
                <asp:TableCell>
                    <asp:Label ID="pn1" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label1" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label2" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="qty1" runat="server"  ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label5" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label6" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="Textbox1" runat="server" ReadOnly="true" ></asp:Textbox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="pn2" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label8" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label9" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="qty2" runat="server" ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label10" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label11" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="Textbox3" runat="server" ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="pn3" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label12" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label13" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="qty3" runat="server" ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label14" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label15" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="Textbox5" runat="server" ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="pn4" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label16" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label17" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="qty4" runat="server" ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label18" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label19" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="Textbox7" runat="server" ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
            </asp:TableRow>
            <asp:TableRow>
                <asp:TableCell>
                    <asp:Label ID="pn5" runat="server" Text=""></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label20" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label21" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="qty5" runat="server" ReadOnly="true"></asp:Textbox>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label22" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Label ID="Label23" runat="server" ></asp:Label>
                </asp:TableCell>
                <asp:TableCell>
                    <asp:Textbox Width="80" ID="Textbox9" runat="server" ReadOnly="true" ></asp:Textbox>
                </asp:TableCell>
            </asp:TableRow>
        </asp:Table>

            </ContentTemplate>
        </asp:UpdatePanel>
        <br />
        <br />
        <br />
        <br />
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
         <ContentTemplate>
        <asp:Label ID="labelLaborHrs" runat="server" Text="Labor Hrs:" Font-Bold="true" ></asp:Label>
        <asp:TextBox ID="dataLaborHr" runat="server" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
        <asp:Label ID="labelLaborCostHrs" runat="server" Text="x Labor Cost/Hrs:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="dataLaborCost" runat="server" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
        <asp:Label ID="labelLaborE" runat="server" Text="=" Font-Bold="true"></asp:Label>
        <asp:Label ID="labelLaborCost" runat="server" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="labelTravelHrs" runat="server" Text="Travel Hrs:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="dataTravelHr" runat="server" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
        <asp:Label ID="labelTravelCostHrs" runat="server" Text="x Travel Cost/Hrs:" Font-Bold="true"></asp:Label>
        <asp:TextBox ID="dataTravelCost" runat="server" AutoPostBack="True" ReadOnly="true"></asp:TextBox>
        <asp:Label ID="labelTravelE" runat="server" Text="=" Font-Bold="true"></asp:Label>
        <asp:Label ID="labelTravelCost" runat="server" Font-Bold="true"></asp:Label>
        <br />
        <br />
        <asp:Label ID="labelTotalE" runat="server" Text="Total Cost:" Font-Bold="true"></asp:Label>
        <asp:Label ID="labelTotalCost" runat="server" Font-Bold="true"></asp:Label>
        </ContentTemplate>
        </asp:UpdatePanel>

                
        <br />
        <br />
        <br />
        <br />
        <asp:Label ID="Label24" runat="server" Text="Work Status:  " Font-Bold="true"></asp:Label>
        <asp:RadioButton ID="cw" GroupName="rw" runat="server" Text="Completed/Waiting for Delivering" Enabled="false"/>
        &nbsp;&nbsp;
        <asp:RadioButton ID="ps" GroupName="rw" runat="server" Text="Part Shortage" Enabled="false" />
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
