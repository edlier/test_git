<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="overview.aspx.cs" Inherits="misSystem.check.overview" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/overview.css" rel="stylesheet" />
    <link href="../CssFile/styles.css" rel="stylesheet" />
    <link href="../CssFile/default.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div id="middle">
        <div class="title">OVERVIEW</div>
        <asp:GridView ID="Gridview1" runat="server" AutoGenerateColumns="False" AllowPaging="true" OnPageIndexChanging="GridView1_PageIndexChanging" CssClass="grid" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="No." ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label0" Text='<%# Eval("id") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DCR" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <a href="overview_dcr.aspx?id=<%# Eval("DocumentChangeRequestID") %>"><%# Eval("DocumentChangeRequestID") %></a>
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ECR" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label2" Text='<%# Eval("EngineeringChangeRequestID") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="RAAR" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label3" Text='<%# Eval("ReceiveAndApprovalRecordID") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="STATUS" ItemStyle-Width="80px" HeaderStyle-Height="30px" ItemStyle-Height="30px">
                    <ItemTemplate>
                        <div class="machine_padding">
                            <asp:Label ID="Label4" Text='<%# Eval("status") %>' runat ="server" />
                        </div>
                    </ItemTemplate>
                    <HeaderStyle Height="30px"></HeaderStyle>
                    <ItemStyle Height="30px" Width="80px"></ItemStyle>
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
</asp:Content>
