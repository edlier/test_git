<%@ Page Title="IPQC-Fill" Language="C#" MasterPageFile="~/QC/QC.master" AutoEventWireup="true" CodeBehind="IPQC_Fill.aspx.cs" Inherits="misSystem.QC.IPQC_Fill" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .hideFuc {
            display: none;
            font-size: 18px;
            font-family: Georgia;
        }

        .showFuc {
            font-size: 18px;
            font-family: Georgia;
        }
    </style>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentQC" runat="server">
    <div class="Lp550_Tm5_gradient40px">Fill IPQC</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:Label ID="Label1" runat="server" Text="SN :"></asp:Label>
        <asp:TextBox ID="tb_SN" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="Product :"></asp:Label>
        <asp:DropDownList ID="drop_product" runat="server" CssClass="Georgia18px"></asp:DropDownList>
        <br />
        <br />
        Failed?
        <asp:RadioButtonList ID="Radio_FOrNot" runat="server" OnSelectedIndexChanged="Radio_FOrNot_SelectedIndexChanged" AutoPostBack="true" RepeatDirection="Horizontal" RepeatLayout="Flow">
            <asp:ListItem Selected="True" Value="0">No</asp:ListItem>
            <asp:ListItem Value="1">Yes</asp:ListItem>
        </asp:RadioButtonList>
        <br />
        <br />
        <asp:Panel ID="Panel1" runat="server" CssClass="hideFuc">
            <asp:Label ID="lbl_failedR" runat="server" Text="Failed Reason :" Enabled="false" Visible="false"></asp:Label>
            <asp:DropDownList ID="drop_T" runat="server" Enabled="false" Visible="false" CssClass="Georgia18px" OnSelectedIndexChanged="drop_T_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList ID="drop_R" runat="server" Enabled="false" Visible="false" CssClass="Georgia18px"></asp:DropDownList>
            <br />
            <br />
        </asp:Panel>
        <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" CssClass="Georgia18px" />
    </div>
</asp:Content>
