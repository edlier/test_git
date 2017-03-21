<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="QC_index.aspx.cs" Inherits="misSystem.QC.QC_index" %>

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
        top:161px;
        width:1000px;
        left:300px;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="out_div">
    <div id="search_div">
        <table style="width:80%; height:70pt;">
            <tr style="height:50%;">
                <td style="width:75%;">
                    <asp:Label ID="process_lab" runat="server" CssClass="label" Text="Process Type:"></asp:Label>
                    <asp:DropDownList ID="process_dl" runat="server" CssClass="DropDownList" OnSelectedIndexChanged="process_dl_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                </td>
                <td rowspan="2" style="width:25%; text-align:left;">
                    <asp:DropDownList ID="show_d" runat="server">
                        <asp:ListItem Value="0">Not show completed.</asp:ListItem>
                        <asp:ListItem Value="1">Show Completed only.</asp:ListItem>
                        <asp:ListItem Value="2">Show Completed also.</asp:ListItem>
                    </asp:DropDownList><br /><br />
                    <asp:Button ID="search_btn" runat="server" Text="Search" OnClick="search_btn_Click" Font-Size="Medium" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="item_name_lab" CssClass="label" runat="server" Text="Item Name:" ></asp:Label>
                    <asp:DropDownList ID="itemName_dl" runat="server" CssClass="DropDownList" ></asp:DropDownList>
                    <asp:Label ID="item_detail_lab" CssClass="label" style="display:none;" runat="server" Text="Item Detail:" ></asp:Label>
                    <asp:DropDownList ID="itemDetail_dl" runat="server" CssClass="DropDownList" style="display:none;" ></asp:DropDownList>
                    <asp:Label ID="model_lab" CssClass="label" style="display:none;" runat="server" Text="Model Name:" ></asp:Label>
                    <asp:DropDownList ID="model_dl" runat="server" CssClass="DropDownList" style="display:none;" ></asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
    <br /> <br />
    <asp:GridView ID="qc_grid" style="width:100%; min-width:100%; background-color:white; margin:0px;" runat="server" CellPadding="3" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" OnRowDataBound="qc_grid_RowDataBound">
        <AlternatingRowStyle BackColor="White" />
        <Columns>
            <asp:TemplateField HeaderText="SN">
                <ItemTemplate>
                    <asp:Label ID="id_lab" runat="server" Text='<%# Eval("ID") %>' Visible="False"></asp:Label>
                    <asp:Label ID="sn_lab" runat="server" Text='<%# Eval("SN") %>' ></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Model Name">
                <ItemTemplate>
                    <asp:Label ID="model_no_lab" runat="server" Text='<%# Eval("Model_No") %>' style="display:none;"></asp:Label>
                    <asp:Label ID="model_name_lab" runat="server" Text='<%# Eval("Model_Name") %>'></asp:Label>
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
            <asp:TemplateField HeaderText="Lot/Batch Num">
                <ItemTemplate>
                    <asp:Label ID="batch_no_lab" runat="server" Text='<%# Eval("Batch_No") %>' style="display:none;"></asp:Label>
                    <asp:Label ID="batch_name_lab" runat="server" Text='<%# Eval("Batch_Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Process Type">
                <ItemTemplate>
                    <asp:Label ID="process_no_lab" runat="server" Text='<%# Eval("Process_No") %>' style="display:none;"></asp:Label>
                    <asp:Label ID="process_name_lab" runat="server" Text='<%# Eval("Process_Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Catalogue">
                <ItemTemplate>
                    <asp:Label ID="cata_no_lab" runat="server" Text='<%# Eval("Catalogue_No") %>' style="display:none;"></asp:Label>
                    <asp:Label ID="cata_name_lab" runat="server" Text='<%# Eval("Catalogue_Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Item Name">
                <ItemTemplate>
                    <asp:Label ID="item_no_lab" runat="server" Text='<%# Eval("Item_No") %>' style="display:none;"></asp:Label>
                    <asp:Label ID="item_name_lab" runat="server" Text='<%# Eval("Item_Name") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Complete">
                <ItemTemplate>
                    <asp:Label ID="endTime_lab" runat="server" Text='<%# Eval("End_Time", "{0:yyyy/MM/dd}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FQty">
                <ItemTemplate>
                    <asp:Label ID="FQty_lab" runat="server" Text='<%# Eval("FQty") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Qty">
                <ItemTemplate>
                    <asp:Label ID="Qty_lab" runat="server" Text='<%# Eval("Qty") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="FQty Ratio">
                <ItemTemplate>
                    <asp:Label ID="ratio_lab" runat="server" Text='' ForeColor="Red"></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Processer">
                <ItemTemplate>
                    <asp:Label ID="processerId_lab" runat="server" Text='<%# Eval("Processer") %>' Visible="False"></asp:Label>
                    <asp:Label ID="processer_lab" runat="server" Text='<%# Eval("missysID") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
            </asp:TemplateField>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" Text="edit" OnClick="edit_btn_Click" style="height: 21px" CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    <asp:Label ID="vendor_lab" runat="server" Text='<%# Eval("Vendor") %>' Visible="False"></asp:Label>                    
                    <asp:Label ID="manuBy_lab" runat="server" Text='<%# Eval("Manufacturer") %>' Visible="False"></asp:Label>
                    <asp:Label ID="item_lab" runat="server" Text='<%# Eval("Item_qc") %>' Visible="False"></asp:Label>
                    <asp:Label ID="sampling_lab" runat="server" Text='<%# Eval("Sampling") %>' Visible="False"></asp:Label>
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
