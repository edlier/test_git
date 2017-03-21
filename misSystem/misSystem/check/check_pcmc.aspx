<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="check_pcmc.aspx.cs" Inherits="misSystem.check.check_pcmc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/check.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        <div class="title">Quantity on Hand and Order 庫存數量</div>
        Part 零件 : On Hand <asp:TextBox ID="tb_qpHand" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server"></asp:TextBox> ,On Order <asp:TextBox ID="tb_qpOrder" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server"></asp:TextBox><br/><br/>
        Sub Component 半成品 : On Hand <asp:TextBox ID="tb_qsHand" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server"></asp:TextBox> ,On Order <asp:TextBox ID="tb_qsOrder" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server"></asp:TextBox><br/><br/>
        Finished product 成品 : On Hand <asp:TextBox ID="tb_qfHand" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server"></asp:TextBox> ,On Order <asp:TextBox ID="tb_qfOrder" onkeyup="this.value=this.value.replace(/[^0-9]/g,'')" runat="server"></asp:TextBox><br/><br/>
        <div class="title">Stocks Deals 庫存處理</div>
        <asp:RadioButtonList ID="rdlist_stock" OnSelectedIndexChanged="rdlist_stock_SelectedIndexChanged" runat="server" AutoPostBack="True">
            <asp:ListItem Text="Revise stocks Date 修改庫存日期" Value="0" runat="server"></asp:ListItem>
            <asp:ListItem Text="Used Stocks 庫存品報廢" Value="1" runat="server"></asp:ListItem>
            <asp:ListItem Text="Deplete of remaining printout 至庫存用完,自然切換, New Issue Applied Date 預定使用本版日期" Value="2" runat="server"></asp:ListItem>
        </asp:RadioButtonList>
        <br/><br/>
         <asp:ImageButton ID="imgbtn_stockdate" Visible="false" runat="server" ImageUrl="~/picture/cald.png" OnClick="imgbtn_stockdate_Click"/>
        <asp:Calendar ID="cl_stockdate" Visible="false" OnSelectionChanged="cl_stockdate_SelectionChanged" runat="server"></asp:Calendar>
        <asp:Label ID="lbl_stockdate" runat="server"></asp:Label>

        <%--<asp:RadioButton ID="rd_rs" Text="Revise stocks Date 修改庫存日期" runat="server" />
        <asp:ImageButton ID="imgbtn_rsdate" runat="server" ImageUrl="~/picture/cald.png" OnClick="imgbtn_rsdate_Click"/>
        <asp:Calendar ID="date_rsselect" Visible="false" OnSelectionChanged="date_rsselect_SelectionChanged" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth" runat="server"></asp:Calendar>
        <asp:Label ID="lbl_rsdate" runat="server"></asp:Label>
        <br/><br/>

        <asp:RadioButton ID="rd_us" Text="Used Stocks 庫存品報廢" runat="server" />
        <asp:ImageButton ID="imgbtn_usdate" runat="server" ImageUrl="~/picture/cald.png" OnClick="imgbtn_usdate_Click"/>
        <asp:Calendar ID="date_usselect" Visible="false" OnSelectionChanged="date_usselect_SelectionChanged" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth" runat="server"></asp:Calendar>
        <asp:Label ID="lbl_usdate" runat="server"></asp:Label>
        <br/><br/>

        <asp:RadioButton ID="rd_drp" Text="Deplete of remaining printout 至庫存用完,自然切換, New Issue Applied Date 預定使用本版日期" runat="server" />
        <asp:ImageButton ID="imgbtn_drpdate" runat="server" ImageUrl="~/picture/cald.png" OnClick="imgbtn_drpdate_Click"/>
        <asp:Calendar ID="date_drpselect" Visible="false" OnSelectionChanged="date_drpselect_SelectionChanged" BackColor="White" BorderColor="Black" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="250px" Width="330px" BorderStyle="Solid" CellSpacing="1" NextPrevFormat="ShortMonth" runat="server"></asp:Calendar>
        <asp:Label ID="lbl_drpdate" runat="server"></asp:Label>
        <br/><br/>--%>
        <br/><br/>
        <asp:Button ID="btn_submit" Text="Submit" runat="server" OnClick="btn_submit_Click" />
    </div>
</asp:Content>
