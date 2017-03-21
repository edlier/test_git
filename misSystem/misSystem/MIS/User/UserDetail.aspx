<%@ Page Title="" Language="C#" MasterPageFile="~/MIS/MIS.master" AutoEventWireup="true" CodeBehind="UserDetail.aspx.cs" Inherits="misSystem.MIS.User.UserDetail" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Edit User</title>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForMIS" runat="server">
    <div class="Lp550_Tm5_gradient40px">User Info Detail</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:Label ID="Label1" runat="server" Text="MIS ID : "></asp:Label>
        <asp:TextBox ID="tb_misID" runat="server" CssClass="Georgia18px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="MIS Password : "></asp:Label>
        <asp:Button ID="btn_changeMISsys_pwd" runat="server" Text="Change Password" CssClass="W180H30-georgia20px" OnClick="btn_changeMISsys_pwd_Click" />
        <br />
        <br />

        <asp:Label ID="Label3" runat="server" Text="Authorization of Module : "></asp:Label>
        <asp:Button ID="btn_click" runat="server" Text="Edit Detail AU" CssClass="W140H28-georgia18px"/>
        <asp:CheckBoxList ID="CBList_Depts" runat="server" DataValueField="id" DataTextField="depdes"></asp:CheckBoxList>
        <br />
        <br />
        <asp:CheckBoxList ID="CBList_accountStatus" runat="server" CssClass="Lp400-Tm450-colorSet">
            <asp:ListItem Value="1">停用AD</asp:ListItem>
            <asp:ListItem Value="2">停用Email</asp:ListItem>
            <asp:ListItem Value="3">停用Skype</asp:ListItem>
            <asp:ListItem Value="4">停用MIS Account</asp:ListItem>
            <asp:ListItem Value="5">員工離職</asp:ListItem>
        </asp:CheckBoxList>
        <div class="L0-T280">
            <asp:Button ID="btn_save" runat="server" Text="Save" CssClass="W100H28-georgia18px"/>&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_ComputerForm" runat="server" Text="Computer Form" CssClass="W160H28-georgia18px"/>
        </div>
    </div>

</asp:Content>
