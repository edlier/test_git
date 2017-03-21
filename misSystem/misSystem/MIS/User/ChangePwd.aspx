<%@ Page Title="" Language="C#" MasterPageFile="~/MIS/MIS.master" AutoEventWireup="true" CodeBehind="ChangePwd.aspx.cs" Inherits="misSystem.MIS.User.ChangePwd" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForMIS" runat="server">
    <div class="Lp550_Tm5_gradient40px">Change MIS System Password</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:Label ID="lbl_oldpwd" runat="server" Text="Old Password："></asp:Label>
        <asp:TextBox ID="tb_oldpwd" runat="server" CssClass="Georgia18px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label1" runat="server" Text="New Password："></asp:Label>
        <asp:TextBox ID="tb_newpwd" runat="server" CssClass="Georgia18px"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Re-type New Password："></asp:Label>
        <asp:TextBox ID="tb_newpwdCheck" runat="server" CssClass="Georgia18px"></asp:TextBox>
        <br />
        <br />
        <asp:Button ID="btn_save" runat="server" Text="Save" OnClick="btn_save_Click" />

    </div>
</asp:Content>
