<%@ Page Title="MIS - User List" Language="C#" MasterPageFile="~/MIS/MIS.master" AutoEventWireup="true" CodeBehind="UserList.aspx.cs" Inherits="misSystem.MIS.User.UserList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="contentForMIS" runat="server">
    <div class="Lp550_Tm5_gradient40px">User List</div>
    <asp:Panel ID="Panel1" runat="server">
        <asp:DropDownList ID="drop_select" runat="server" OnSelectedIndexChanged="drop_select_SelectedIndexChanged" CssClass="Lp900-Tm390-georgia18px" AutoPostBack="true">
            <asp:ListItem Value="0">All</asp:ListItem>
            <asp:ListItem Value="1">停用AD</asp:ListItem>
            <asp:ListItem Value="2">使用AD</asp:ListItem>
            <asp:ListItem Value="3">停用Email</asp:ListItem>
            <asp:ListItem Value="4">使用Email</asp:ListItem>
            <asp:ListItem Value="5">停用Skype</asp:ListItem>
            <asp:ListItem Value="6">使用Skype</asp:ListItem>
            <asp:ListItem Value="7">已離職</asp:ListItem>
            <asp:ListItem Value="8">未離職</asp:ListItem>
            <asp:ListItem Value="9">停用 MIS Account</asp:ListItem>
            <asp:ListItem Value="10">使用 MIS Account</asp:ListItem>
        </asp:DropDownList>
        <div class="Lp1080-Tm390">
            <asp:Button ID="btn_idSorting" runat="server" Text="DESC" CssClass="W100H28-georgia18px" OnClick="btn_idSorting_Click"/><asp:TextBox ID="tb_searchUser" runat="server"></asp:TextBox>
        </div>
        
    </asp:Panel>
    <div class="Lp550-Tm325-georgia18px">
        <asp:GridView ID="grid_UserList" runat="server" AutoGenerateColumns="False" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" CellPadding="2" ForeColor="Black" GridLines="Both">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:TemplateField HeaderText="ID" ItemStyle-Height="35px" HeaderStyle-Height="35px"> 
                    <ItemTemplate>
                        <a href="UserDetail.aspx?id=<%# Eval("ID") %>">
                            <asp:Label ID="id" Text='<%# Eval("ID") %>' runat="server" CssClass="fulltext" />
                        </a>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MIS SYS ID" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <asp:Label ID="missysID" Text='<%# Eval("missysID") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                    <ItemStyle Width="180px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A1">
                    <ItemTemplate>
                        <asp:Label ID="auDep1" Text='<%# Eval("auDep1") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A2">
                    <ItemTemplate>
                        <asp:Label ID="auDep2" Text='<%# Eval("auDep2") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A3">
                    <ItemTemplate>
                        <asp:Label ID="auDep3" Text='<%# Eval("auDep3") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A4">
                    <ItemTemplate>
                        <asp:Label ID="auDep4" Text='<%# Eval("auDep4") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A5">
                    <ItemTemplate>
                        <asp:Label ID="auDep5" Text='<%# Eval("auDep5") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A6">
                    <ItemTemplate>
                        <asp:Label ID="auDep6" Text='<%# Eval("auDep6") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A7">
                    <ItemTemplate>
                        <asp:Label ID="auDep7" Text='<%# Eval("auDep7") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A8">
                    <ItemTemplate>
                        <asp:Label ID="auDep8" Text='<%# Eval("auDep8") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A9">
                    <ItemTemplate>
                        <asp:Label ID="auDep9" Text='<%# Eval("auDep9") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A10">
                    <ItemTemplate>
                        <asp:Label ID="auDep10" Text='<%# Eval("auDep10") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="A11">
                    <ItemTemplate>
                        <asp:Label ID="auDep11" Text='<%# Eval("auDep11") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Num">
                    <ItemTemplate>
                        <asp:Label ID="workerNum" Text='<%# Eval("workerNum") %>' runat="server" CssClass="fulltext" />
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

</asp:Content>
