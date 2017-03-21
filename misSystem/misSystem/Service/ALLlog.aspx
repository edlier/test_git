<%@ Page Title="" Language="C#" MasterPageFile="~/Service/ServiceMaster.master" AutoEventWireup="true" CodeBehind="ALLlog.aspx.cs" Inherits="misSystem.Service.ALLlog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <title>MIS - ALL ServiceLog</title>
    <link rel="stylesheet" href="//code.jquery.com/ui/1.11.4/themes/smoothness/jquery-ui.css">
    <script src="//code.jquery.com/jquery-1.10.2.js"></script>
    <script src="//code.jquery.com/ui/1.11.4/jquery-ui.js"></script>
    <script>
        $(function () {
            //會互相干擾
            $("#datepicker").datepicker({ dateFormat: 'yy-mm-dd' });
            //$("#datepicker").datepicker({
            //    changeMonth: true,
            //    changeYear: true,
            //});
        });
    </script>
    <style>
        .gradient30 {
            font-size: 30px;
            background: -webkit-linear-gradient(top,#fd0b58 0,#a32b68 100%);
            -webkit-background-clip: text;
            -webkit-text-fill-color: transparent;
            font-family: Georgia;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForService" runat="server">


    <div class="Lp550_Tm5_gradient40px">ServiceLog清單</div>



    <div class="Lp550-Tm325-georgia18px">




    <div id="ServiceLog"  runat="server" >

        <br/>
        <br/>
        <asp:GridView ID="grid_UserList" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Both">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField HeaderText="序號" ItemStyle-Height="35px" HeaderStyle-Height="35px"> 
                    <ItemTemplate>
                        <%--<a href="editEmployeeDetail?id=<%# Eval("ID") %>"></a>--%>
                        <asp:Label ID="id" Text='<%#Container.DataItemIndex + 1%>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ServiceLog ID" ItemStyle-Width="180px">
                    <ItemTemplate>
                        
                        <div class="machine_padding">
                            <a href="/Service/Read/Read_ServiceLog.aspx?id=<%# Eval("ID") %>"><%# Eval("ID") %></a>
                        </div>
                    </ItemTemplate>
                    <ItemStyle Width="180px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="填寫人" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <asp:Label ID="ServicelogID" Text='<%# Eval("missysID") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                    <ItemStyle Width="180px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="flow" Text='<%#Eval("flow").ToString() +" , "+ Eval("status").ToString() %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <PagerStyle BackColor="PaleGoldenrod" ForeColor="DarkSlateBlue" HorizontalAlign="Center" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <SortedAscendingCellStyle BackColor="#FAFAE7" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>
        


    </div>
  

    
       
        <br/><br/>
    
    </div>


</asp:Content>
