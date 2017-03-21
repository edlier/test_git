<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_IO_EditLog.aspx.cs" Inherits="misSystem.HR.HR_IO_EditLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Daily Report</title>
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
#insert_div {
    font-size:14pt;
    margin:0;
    padding:0;
}
#insert_tb {
    margin:0 auto;
    padding:0;
    text-align: center;
    border-collapse:collapse;
}
</style>
    <style type="text/css">
        .css1 {
            vertical-align:top;        
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        
    <div id="out_div" style="top:161px;">
        <div id="search_div">
            <table id="search_tb">
                <tr>
                <td style="text-align:left;">
                    User: <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                    <br />
                    工號: <asp:Label ID="id_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                    姓名: <asp:Label ID="name2_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                    <br />
                    日期: <asp:Label ID="date_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                    <asp:Label ID="level_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
                    <asp:Label ID="dep_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
                </td>
                </tr>
            </table>
        </div>
        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
        
        <asp:GridView ID="checkinoutlog_grid" runat="server" style="width:100%; min-width:100%; background-color:white; margin:0px; text-align:center; font-size:16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AutoGenerateColumns="False" onrowcommand="checkinoutlog_grid_RowCommand">
            <AlternatingRowStyle BackColor="White" />
                        
            <Columns>
                 <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="ID" Visible="false">
                <ItemTemplate>
                    <asp:Label ID="id_lab" runat="server" Text='<%# Eval("id") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Time">
                <ItemTemplate>
                    <asp:Label ID="time_lab" runat="server" Text='<%# Eval("time") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Satus">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("Status") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>               
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Delete">
                <ItemTemplate>
                    <asp:Button ID="del" runat="server" Text='del' CommandName="del" CommandArgument ='<%# Container.DataItemIndex %>'  OnClientClick="return confirm('確定要刪除嗎？')" CssClass="Georgia18px"></asp:Button>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Transfer">
                <ItemTemplate>
                    <asp:Button ID="transfer" runat="server" Text='<%# Eval("transfer") %>' CommandName="trans" CommandArgument ='<%# Container.DataItemIndex %>' CssClass="Georgia18px"></asp:Button>                    
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
                                

        <br/>
        <div id="insert_div">
            <table id="insert_tb">
                <tr  class="css1">
                <td style="text-align:right;">
                    新增打卡時間:<br />
                    <div style="color:blueviolet"><asp:CheckBox ID="cb_insert" runat="server" AutoPostBack="True"/>自訂打卡時間 </div>
                </td> 
                <td style="text-align:right;">
                    <asp:Panel ID="tbPanel" runat="server">
                        <asp:TextBox ID="time_tb" runat="server" Width="100px" CssClass="Georgia18px"></asp:TextBox>
                    </asp:Panel>
                    <asp:Panel ID="dropPanel" runat="server">
                        <asp:DropDownList ID="drop_time" runat="server" Width="100px" CssClass="Georgia18px">
                            <asp:ListItem Value="0"> 08:30:00</asp:ListItem>
                            <asp:ListItem Value="1"> 09:00:00</asp:ListItem>
                            <asp:ListItem Value="2"> 12:00:00</asp:ListItem>
                            <asp:ListItem Value="3"> 13:00:00</asp:ListItem>
                            <asp:ListItem Value="4"> 17:30:00</asp:ListItem>
                            <asp:ListItem Value="5"> 18:00:00</asp:ListItem>
                        </asp:DropDownList>
                    </asp:Panel>
                </td>  
                <td style="text-align:left; ">
                   <!-- <asp:RadioButton ID="checkin_rb" runat="server" text="Checkin "/> -->
                    <asp:RadioButtonList ID="checkinout_rbl" runat="server">
                        <asp:ListItem Value="Checkin">Checkin</asp:ListItem>
                        <asp:ListItem Value="Checkout">Checkout</asp:ListItem>
                    </asp:RadioButtonList>
                </td>  
                </tr>               
                <tr>
                <td />    
                <td style="text-align:right;"> 
                     <asp:Button ID="update_btn" runat="server" Text="Update" OnClick="update_btn_Click" CssClass="Georgia18px"/>
                </td>
                <td style="text-align:center;">                      
                    <asp:Button ID="prev_btn" runat="server" Text="Prev." OnClick="prev_btn_Click" CssClass="Georgia18px"/>
                </td>
                </tr>
            </table>
        </div>
            </ContentTemplate>
        </asp:UpdatePanel>

 </div>
</asp:Content>

