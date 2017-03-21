<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_list.aspx.cs" Inherits="misSystem.HR.HR_list" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Searching application</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet"/>   
    <link href="myCss/HR_css.css" rel="stylesheet"/>
    <script src="myJs/HR_js.js"></script>
<style>
    #search_div {
        font-size:14pt;
        margin:0;
        padding:0;        
        border-bottom:2pt groove #0a89e5;
    }
    #search_tb {
        margin:0 auto;
        padding:0;
        text-align: center;
        border-collapse:collapse;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="out_div" style="top:161px;">
        <div id="search_div">
            <table id="search_tb">
                <tr><td style="text-align:left;">
                    User: <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                    <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label></td>
                <td>
                    <asp:DropDownList ID="check_dl" runat="server" style="margin-right:10pt;">
                        <asp:ListItem Value="uncheck">未審核</asp:ListItem>
                        <asp:ListItem Value="checking">審核中</asp:ListItem>
                        <asp:ListItem Value="finish">審核通過</asp:ListItem>
                        <asp:ListItem Value="fail">審核未通過</asp:ListItem>
                    </asp:DropDownList>
                    <asp:DropDownList ID="apply_dl" runat="server">
                        <asp:ListItem Value="leave">請假單</asp:ListItem>
                        <asp:ListItem Value="overtime">加班申請單</asp:ListItem>
                        <asp:ListItem Value="workingout">外出申請單</asp:ListItem>
                        <asp:ListItem Value="equipment">設備申請單</asp:ListItem>
                        <asp:ListItem Value="support">人力支援申請表</asp:ListItem>
                        <asp:ListItem Value="transfer">請調申請表</asp:ListItem>
                        <asp:ListItem Value="hire">人員撥補申請表</asp:ListItem>
                    </asp:DropDownList>                    
                </td><td></td></tr>
                <tr><td colspan="2">
                    Apply Time: <asp:TextBox ID="start_date_text" runat="server" CssClass="datepicker" TextMode="DateTime"></asp:TextBox>
                     ~ <asp:TextBox ID="end_date_text" runat="server" CssClass="datepicker" ></asp:TextBox><br />
                </td><td style="padding-left:1em;"><asp:Button ID="search_btn" runat="server" Text="Search" OnClick="search_btn_Click" /></td></tr>
            </table>
        </div>
        <asp:Label ID="title_lab" runat="server" Text="表單查詢" style="font-style:italic; font-weight:bold;"></asp:Label>
        <asp:GridView ID="hr_grid" runat="server" style="width:100%; min-width:100%; background-color:white; margin:0px; text-align:center; font-size:16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." OnRowDataBound="hr_grid_RowDataBound">
            <AlternatingRowStyle BackColor="White" />
                        
            <Columns>
                <asp:TemplateField ConvertEmptyStringToNull="True">
                <ItemTemplate>
                    <asp:Button ID="edit_btn" runat="server" Text="Edit" style="height: 21px" CommandArgument='<%# Eval("SN") %>' OnClick="edit_btn_Click" />
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
    </div>

    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker("option", "minDate", null);
            $('.datepicker').datepicker("option", "maxDate", null);
        });
    </script>
</asp:Content>
