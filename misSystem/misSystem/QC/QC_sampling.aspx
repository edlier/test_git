<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="QC_sampling.aspx.cs" Inherits="misSystem.QC.QC_sampling" %>

<asp:Content ID="head" ContentPlaceHolderID="head" runat="server">  
    <title>QC</title>  
<style>
    #search_div {
        width:80%; height:auto;
        margin:auto;
        padding-left:3em;
        border-bottom:2pt groove #0a89e5;        
    }
    .DropDownList {
        font-size:20px;
        margin-right:20px;
    }
    .label {
        font-size:20px;
        margin:0 10px;
        display:inline-block; 
    }
    #out_div {
        position:absolute;
        top:170px;
        width:700px;
        left:300px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="out_div">
    <div id="search_div" style="font-size:20px;">
        <asp:Label ID="Label1" runat="server" Text="SN: " ></asp:Label>        
        <asp:TextBox ID="sn_text" runat="server" style="margin-right:3em;"></asp:TextBox>
        <asp:Button ID="search_btn" runat="server" Text="Search" OnClick="search_btn_Click" />
        <br /><br />
    </div>
    <br /> <br />
    <asp:GridView ID="qc_grid" style="width:100%; min-width:100%; background-color:white; margin:0px;" runat="server" CellPadding="3" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="SN">
                <ItemTemplate>
                    <asp:Label ID="sn_lab" runat="server" Text='<%# Eval("SN") %>' ></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Part Num">
                <ItemTemplate>
                    <asp:Label ID="part_no_lab" runat="server" Text='<%# Eval("Part_No") %>' style="display:none;"></asp:Label>
                    <asp:Label ID="part_name_lab" runat="server" Text='<%# Eval("Part_Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                    <asp:Label ID="Qty_lab" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sampling">
                <ItemTemplate>
                    <asp:Label ID="sampling_lab" runat="server" Text='<%# Eval("Sampling","{0:0.##%}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" Text="Edit" OnClick="edit_btn_Click" style="height: 21px" CommandArgument=<%# Eval("SN") %> />
                    </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
        </Columns>
        <EditRowStyle BackColor="#2461BF" />
        <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="#EFF3FB" />
        <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
        <SortedAscendingCellStyle BackColor="#F5F7FB" />
        <SortedAscendingHeaderStyle BackColor="#6D95E1" />
        <SortedDescendingCellStyle BackColor="#E9EBEF" />
        <SortedDescendingHeaderStyle BackColor="#4870BE" />
    </asp:GridView>
    <p style="text-align:center;"><asp:Label ID="result_lab" runat="server" style="font-size:30pt;" Text=""></asp:Label></p>
    </div>
</asp:Content>
