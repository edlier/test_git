<%@ Page Title="" Language="C#" MasterPageFile="~/QC/QC.master" AutoEventWireup="true" CodeBehind="OQC.aspx.cs" Inherits="misSystem.QC.OQC" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentQC" runat="server">
    <div class="Lp550_Tm5_gradient40px">OPQC - Validation List</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:GridView ID="grid_OPQC" runat="server"></asp:GridView>
    </div>


</asp:Content>
