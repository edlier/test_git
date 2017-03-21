<%@ Page Title="" Language="C#" MasterPageFile="~/MIS/MIS.master" AutoEventWireup="true" CodeBehind="ManagerList.aspx.cs" Inherits="misSystem.MIS.Manager.ManagerList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>MIS - Manager List</title>
    <link href="../../CssFile/btn.css" rel="stylesheet" />
    <link href="../../CssFile/position.css" rel="stylesheet" />
    <link href="../../CssFile/tittle.css" rel="stylesheet" />
    <link href="../../CssFile/tb.css" rel="stylesheet" />
    <link href="../../CssFile/tb.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForMIS" runat="server" >
    <div class="Lp550_Tm5_gradient40px">Manager List</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:GridView ID="grid_managerList" runat="server" AutoGenerateColumns="false" OnRowCommand="grid_managerList_RowCommand">
            <Columns>
                <asp:TemplateField HeaderText="ID">
                    <ItemTemplate>
                        <%--<a href="editEmployeeDetail?id=<%# Eval("ID") %>"></a>--%>
                        <asp:Label ID="id" Text='<%# Eval("ID") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="UserID">
                    <ItemTemplate>
                        <asp:Label ID="userID" Text='<%# Eval("userID") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="MIS SYS ID">
                    <ItemTemplate>
                        <asp:Label ID="missysID" Text='<%# Eval("missysID") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Level">
                    <ItemTemplate>
                        <asp:Label ID="level" Text='<%# Eval("level") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DepID">
                    <ItemTemplate>
                        <asp:Label ID="depID" Text='<%# Eval("depID") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                </asp:TemplateField>
                <%--CommandArgument='<%# Eval("ID") %>'--%> 
                <asp:TemplateField HeaderText="Delete">
                    <ItemTemplate>
                        <asp:Button ID="btn_del" runat="server" Text="Delete"
                            CommandName="myy" 
                            CommandArgument='<%# Container.DataItemIndex %>'
                            onclientclick="return confirm('Are you sure you want to delete？')"
                            CssClass="W100H28-georgia18px"
                            />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
