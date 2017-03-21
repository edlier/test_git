<%@ Page Title="" Language="C#" MasterPageFile="~/MIS/MIS.master" AutoEventWireup="true" CodeBehind="AddNewUser.aspx.cs" Inherits="misSystem.MIS.User.AddNewUser" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=Big5" />
    <title>MIS - Add User</title>
    <link href="../../CssFile/styles.css" rel="stylesheet" />
    <link href="../../CssFile/btn.css" rel="stylesheet" />
    <link href="../../CssFile/position.css" rel="stylesheet" />
    <link href="../../CssFile/tittle.css" rel="stylesheet" />
    <link href="../../CssFile/tb.css" rel="stylesheet" />
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
<asp:Content ID="Content2" ContentPlaceHolderID="contentForMIS" runat="server">
    <div class="Lp550_Tm5_gradient40px">Add New User</div>
<%--    <div class="Lp300-Tm350">
        <asp:Button ID="btn_UserList" runat="server" Text="User List" CssClass="W150H30-georgia20px" />&nbsp;&nbsp;&nbsp;
    </div>--%>
        <div class="Lp550-Tm325-georgia18px">
            <asp:Label ID="lbl_chineeseName" runat="server" Text="Chineese Name："></asp:Label>
            <asp:TextBox ID="tb_chineeseName" runat="server" CssClass="Georgia18px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label1" runat="server" Text="First Name："></asp:Label>
            <asp:TextBox ID="tb_FirstName" runat="server" CssClass="Georgia18px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label2" runat="server" Text="Last Name："></asp:Label>
            <asp:TextBox ID="tb_LastName" runat="server" CssClass="Georgia18px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label3" runat="server" Text="WorkerNumber："></asp:Label>
            <asp:TextBox ID="tb_workerNum" runat="server" CssClass="Georgia18px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label4" runat="server" Text="Dep："></asp:Label>
            <asp:DropDownList ID="drop_dept" runat="server" DataTextField="deps" DataValueField="id" CssClass="Georgia18px" OnInit="drop_deptInit"></asp:DropDownList>
            <br />
            <br />
            <asp:Label ID="Label5" runat="server" Text="Inauguration Date："></asp:Label>
            <asp:TextBox ID="datepicker" runat="server" CssClass="Georgia18px"></asp:TextBox>
            <br />
            <br />
            <asp:Label ID="Label16" runat="server" Text="Authorization of Dept："></asp:Label>

            <asp:CheckBoxList ID="CB_au" runat="server">
                <asp:ListItem Value="1">1, Accounting</asp:ListItem>
                <asp:ListItem Value="2">2, Sales</asp:ListItem>
                <asp:ListItem Value="3">3, PD&E</asp:ListItem>
                <asp:ListItem Value="4">4, Service</asp:ListItem>
                <asp:ListItem Value="5">5, Administration</asp:ListItem>
                <asp:ListItem Value="6">6, QC</asp:ListItem>
                <asp:ListItem Value="7">7, PCMC</asp:ListItem>
                <asp:ListItem Value="8">8, Productions</asp:ListItem>
                <asp:ListItem Value="9">9, Regulatory</asp:ListItem>
                <asp:ListItem Value="10">10, Check</asp:ListItem>
                <asp:ListItem Value="11">11, IT</asp:ListItem>


            </asp:CheckBoxList>
            <br />
            <br />
            <asp:Label ID="Label14" runat="server" Text="Level："></asp:Label>
            <asp:DropDownList ID="drop_level" runat="server" CssClass="Georgia18px">
                <asp:ListItem Value="1">1, Employee</asp:ListItem>
                <%--<asp:ListItem Value="2">2, Manager</asp:ListItem>--%>
                <asp:ListItem Value="2">2, Section manager</asp:ListItem>
                <asp:ListItem Value="3">3, Department manager</asp:ListItem>
                <asp:ListItem Value="4">4, Vice General Manager</asp:ListItem>
                <asp:ListItem Value="5">5, General Manager</asp:ListItem>
                <asp:ListItem Value="6">6, Exclusive assistant</asp:ListItem>
                <asp:ListItem Value="7">7, President</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="btn_generate" runat="server" Text="Generate Info" OnClick="btn_generate_Click" CssClass="W140H28-georgia18px" />


            <asp:Panel ID="Panel1" runat="server">
                <br />
                <br />
                <asp:Label ID="Label13" runat="server" Text="MIS System ID："></asp:Label>
                <asp:TextBox ID="tb_mis_account" runat="server" CssClass="Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label15" runat="server" Text="MIS System Pwd："></asp:Label>
                <asp:TextBox ID="tb_mis_pwd" runat="server" CssClass="Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label6" runat="server" Text="AD Account："></asp:Label>
                <asp:TextBox ID="tb_Ad_Account" runat="server" CssClass="Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label12" runat="server" Text="AD Pwd："></asp:Label>
                <asp:TextBox ID="tb_Ad_pwd" runat="server" CssClass="Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label7" runat="server" Text="Email："></asp:Label>
                <asp:TextBox ID="tb_email" runat="server" CssClass="W250_Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label8" runat="server" Text="Email Pwd："></asp:Label>
                <asp:TextBox ID="tb_email_pwd" runat="server" CssClass="Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label9" runat="server" Text="Skype ID："></asp:Label>
                <asp:TextBox ID="tb_skype" runat="server" CssClass="Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label10" runat="server" Text="Skype Pwd："></asp:Label>
                <asp:TextBox ID="tb_skype_pwd" runat="server" CssClass="Georgia18px"></asp:TextBox>
                <br />
                <br />
                <asp:Label ID="Label11" runat="server" Text="Registration Skype Email："></asp:Label>
                <asp:TextBox ID="tb_skypeRegistrationEmail" runat="server" CssClass="W250_Georgia18px"></asp:TextBox>
            </asp:Panel>
            <br />
            <br />
            <asp:Button ID="btn_nxt" runat="server" Text="Next ↓" OnClick="btn_nxt_Click" CssClass="W100H28-georgia18px" />
            <br />
            <br />
            <asp:Panel ID="Panel_ACC" runat="server" Visible="false">
                <div class="gradient30">1, Accouting was choosed</div>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_Sales" runat="server" Visible="false">
                <div class="gradient30">2, Sales was choosed</div>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_PDE" runat="server" Visible="false">
                <div class="gradient30">3, PD&E</div>
                <br />
                <asp:CheckBoxList ID="CheckList_PDE" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_service" runat="server" Visible="false">
                <div class="gradient30">4, Service</div>
                <br />
                <asp:CheckBoxList ID="CheckList_service" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_Administration" runat="server" Visible="false">
                <div class="gradient30">5, Administration</div>
                <br />
                <asp:CheckBoxList ID="CheckList_Admin" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_QC" runat="server" Visible="false">
                <div class="gradient30">6, QC</div>
                <br />
                <asp:CheckBoxList ID="CheckList_QC" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_PCMC" runat="server" Visible="false">
                <div class="gradient30">7, PCMC</div>
                <br />
                <asp:CheckBoxList ID="CheckList_PCMC" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_Production" runat="server" Visible="false">
                <div class="gradient30">8, Production</div>
                <br />
                <asp:CheckBoxList ID="CheckList_Production" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_Regulatory" runat="server" Visible="false">
                <div class="gradient30">9, Regulatory</div>
                <br />
                <asp:CheckBoxList ID="CheckList_Regulatory" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_Check" runat="server" Visible="false">
                <div class="gradient30">10, Check</div>
                <br />
                <asp:CheckBoxList ID="CheckList_Check" runat="server"></asp:CheckBoxList>
                <br />
                <br />
            </asp:Panel>
            <asp:Panel ID="Panel_IT" runat="server" Visible="false">
                <div class="gradient30">11, IT was choosed</div>
                <br />
                <br />
            </asp:Panel>
            <asp:Button ID="btn_Save" runat="server" Text="Save" CssClass="W100H28-georgia18px" Visible="false" OnClick="btn_Save_Click" />
            <br />
            <br />
        </div>
</asp:Content>
