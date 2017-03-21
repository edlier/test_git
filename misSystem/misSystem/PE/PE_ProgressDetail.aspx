<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="PE_ProgressDetail.aspx.cs" Inherits="misSystem.PE.PE_ProgressDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="Lp400-Tm450-colorSet">
     <h1>Progress Detail</h1>
    <asp:Label ID="Label1" runat="server" Text="工作項目(Title)："></asp:Label>
    <asp:Label ID="lbl_ShowTitle" runat="server" Text="Label"></asp:Label>
    <br/>
    <br/>
    <asp:Label ID="Label3" runat="server" Text="工作內容(Task Description)："></asp:Label><br/>
    <asp:ListBox ID="lb_descriptions" runat="server" BackColor="#9EECEC" AutoPostBack="True" OnSelectedIndexChanged="lb_descriptions_SelectedIndexChanged" Height="75%" Width="75%"></asp:ListBox>
     <br />
          <asp:Label ID="lbl_descriptions" runat="server" Text="點選上方以顯示內容 Click to View"></asp:Label>
         <br/>
    <br/>
    <asp:Label ID="Label4" runat="server" Text="工作產出(Project output)："></asp:Label><br />
    <asp:ListBox ID="lb_enditems" runat="server" BackColor="#9EECEC" AutoPostBack="True" OnSelectedIndexChanged="lb_enditems_SelectedIndexChanged" Height="75%" Width="75%"></asp:ListBox>
     <br />
           <asp:Label ID="lbl_enditems" runat="server" Text="點選上方以顯示內容 Click to View"></asp:Label>
         <br/>
    <br/>       
  <asp:GridView ID="grid_showprogress" runat="server" BackColor="LightGoldenrodYellow"  OnSelectedIndexChanged="grid_showprogress_SelectedIndexChanged">
       <AlternatingRowStyle BackColor="PaleGoldenrod" />
  </asp:GridView>
        <br />
        <br />
        <asp:Button ID="bt_back" runat="server" Text="Back" OnClick="bt_back_Click" />
    </div>
</asp:Content>
