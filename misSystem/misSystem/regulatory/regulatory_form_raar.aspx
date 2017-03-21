<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="regulatory_form_raar.aspx.cs" Inherits="misSystem.regulatory.regulatory_form_raar" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/styles.css" rel="stylesheet" />
    <link href="../CssFile/default.css" rel="stylesheet" />
    <style>
        #raarFormTitle {
            font-weight: bold;
            font-size: 28px;
            color: blue;
        }

        #raar_middle {
            position: absolute;
            top: 170px;
            left: 300px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="raar_middle">
        <div id="raarFormTitle">各單位會辦單 (RAAR Form)</div>
        <br/>
        <br/>
        <div id="content">
            Written by (填表人員) : <asp:Label ID="lbl_writtenby" runat="server" Text="Label"></asp:Label>
            <br/>
            <br/>
            Written Date (填表日期) : <asp:Label ID="lbl_writtenDate" runat="server" Text="Label"></asp:Label>
            <br/>
            <br/>
            Responsible Department (主辦單位) : <asp:DropDownList ID="drop_resdept" DataValueField="id" DataTextField="des" runat="server"></asp:DropDownList>
            <br/>
            <br/>
            No.(編號)  : <asp:Label ID="lbl_fileno" runat="server"></asp:Label><asp:DropDownList ID="drop_no" DataValueField="id" DataTextField="no" runat="server"></asp:DropDownList> <%--<asp:TextBox ID="tb_fileno" runat="server"></asp:TextBox>--%>
            <br/>
            <br/>
            <asp:Table ID="tbl_subject" GridLines="Both" BorderColor="Black" runat="server">
                <asp:TableHeaderRow>
                   <asp:TableHeaderCell>Subject (會辦主題)</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_subject1" runat="server" required></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_subject2" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_subject3" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_subject4" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:TextBox ID="tb_subject5" runat="server"></asp:TextBox>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br/>
            <br/>
            <asp:Label ID="lbl_err" runat="server" ForeColor="Red" Visible="false">請選擇</asp:Label>
            <asp:Table ID="tbl_dept" BorderColor="Black" GridLines="Both" runat="server">
                <asp:TableHeaderRow>
                    <asp:TableHeaderCell>Depts (會辦單位)</asp:TableHeaderCell>
                </asp:TableHeaderRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="Depts1" runat="server" DataValueField="id" DataTextField="description" OnSelectedIndexChanged="Depts1_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems ="true"><asp:ListItem Value="0">請選擇</asp:ListItem></asp:DropDownList>
                        <asp:Label ID="lbl_manager1_1" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager1_2" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager1_3" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="Depts2" runat="server" DataValueField="id" DataTextField="description" OnSelectedIndexChanged="Depts2_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems ="true"><asp:ListItem Value="0">請選擇</asp:ListItem></asp:DropDownList>
                        <asp:Label ID="lbl_manager2_1" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager2_2" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager2_3" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="Depts3" runat="server" DataValueField="id" DataTextField="description" OnSelectedIndexChanged="Depts3_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems ="true"><asp:ListItem Value="0">請選擇</asp:ListItem></asp:DropDownList>
                        <asp:Label ID="lbl_manager3_1" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager3_2" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager3_3" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="Depts4" runat="server" DataValueField="id" DataTextField="description" OnSelectedIndexChanged="Depts4_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems ="true"><asp:ListItem Value="0">請選擇</asp:ListItem></asp:DropDownList>
                        <asp:Label ID="lbl_manager4_1" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager4_2" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager4_3" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="Depts5" runat="server" DataValueField="id" DataTextField="description" OnSelectedIndexChanged="Depts5_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems ="true"><asp:ListItem Value="0">請選擇</asp:ListItem></asp:DropDownList>
                        <asp:Label ID="lbl_manager5_1" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager5_2" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager5_3" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
                <asp:TableRow>
                    <asp:TableCell>
                        <asp:DropDownList ID="Depts6" runat="server" DataValueField="id" DataTextField="description" OnSelectedIndexChanged="Depts6_SelectedIndexChanged" AutoPostBack="true" AppendDataBoundItems ="true"><asp:ListItem Value="0">請選擇</asp:ListItem></asp:DropDownList>
                        <asp:Label ID="lbl_manager6_1" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager6_2" runat="server"></asp:Label>
                        <asp:Label ID="lbl_manager6_3" runat="server"></asp:Label>
                    </asp:TableCell>
                </asp:TableRow>
            </asp:Table>
            <br/>
            <br/>
            <%--<script src="http://ajax.googleapis.com/ajax/libs/jquery/1/jquery.min.js"></script>
            <script>
                $("#Depts1").change(function () {
                    if($("#Depts1 option:selected").val())
                    
                })
            </script>--%>

            <%--<br/><br/>
            <asp:Label ID="lbl_manager2_1" runat="server"></asp:Label><asp:Label ID="lbl_manager2_2" runat="server"></asp:Label><asp:Label ID="lbl_manager2_3" runat="server"></asp:Label>
            <br/><br/>
            <asp:Label ID="lbl_manager3_1" runat="server"></asp:Label><asp:Label ID="lbl_manager3_2" runat="server"></asp:Label><asp:Label ID="lbl_manager3_3" runat="server"></asp:Label>
            <br/><br/>
            <asp:Label ID="lbl_manager4_1" runat="server"></asp:Label><asp:Label ID="lbl_manager4_2" runat="server"></asp:Label><asp:Label ID="lbl_manager4_3" runat="server"></asp:Label>
            <br/><br/>
            <asp:Label ID="lbl_manager5_1" runat="server"></asp:Label><asp:Label ID="lbl_manager5_2" runat="server"></asp:Label><asp:Label ID="lbl_manager5_3" runat="server"></asp:Label>
            <br/><br/>
            <asp:Label ID="lbl_manager6_1" runat="server"></asp:Label><asp:Label ID="lbl_manager6_2" runat="server"></asp:Label><asp:Label ID="lbl_manager6_3" runat="server"></asp:Label>--%>
            <br/><br/>
            <asp:Button ID="btn_raar_submint" runat="server" Text="送出" OnClick="btn_raar_submint_Click"/>
        </div>
    </div>
</asp:Content>