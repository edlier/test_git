<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="PE_ItemList.aspx.cs" Inherits="misSystem.PE.PE_ItemList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
 <div class="Lp400-Tm450-colorSet">
     <h1>項目清單頁面(Task List)</h1>
     <asp:GridView ID="grid_tasklist" runat="server" AutoGenerateColumns="False" BackColor="White" OnSelectedIndexChanged="grid_tasklist_SelectedIndexChanged" OnRowDataBound="grid_tasklist_RowDataBound" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
         <AlternatingRowStyle BackColor="#CCCCCC" />
          <Columns>
         <asp:TemplateField HeaderText="ID" ItemStyle-Height="35px" HeaderStyle-Height="35px"> 
                    <ItemTemplate>
                        <a href="PE_ProgressDetail.aspx?id=<%# Eval("ID") %>">
                            <asp:Label ID="id" Text='<%# Eval("ID") %>' runat="server" CssClass="fulltext" />
                        </a>
                    </ItemTemplate>

<HeaderStyle Height="35px"></HeaderStyle>

<ItemStyle Height="35px"></ItemStyle>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="工作項目(Title)" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <asp:Label ID="item" Text='<%# Eval("itemname") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                    <ItemStyle Width="180px"></ItemStyle>
                </asp:TemplateField>
<%--              <asp:TemplateField HeaderText="狀態(Status)" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <asp:Label ID="status" Text='<%# Eval("status") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                    <ItemStyle Width="180px"></ItemStyle>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="status" HeaderText="Status" HeaderStyle-Width="130px" >

<HeaderStyle Width="130px"></HeaderStyle>
              </asp:BoundField>

             <asp:TemplateField HeaderText="Owner" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <asp:Label ID="owner" Text='<%# Eval("missysID") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                    <ItemStyle Width="180px"></ItemStyle>
                </asp:TemplateField>
<%--             <asp:TemplateField HeaderText="到期日(Due)" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <asp:Label ID="due" Text='<%# Eval("duedate") %>' runat="server" CssClass="fulltext" DataFormatString="{0:yyyy-MM-dd}"  />
                    </ItemTemplate>
                    <ItemStyle Width="180px"></ItemStyle>
                </asp:TemplateField>--%>
                <asp:BoundField DataField="duedate" HeaderText="Due Date" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="100px" >

<HeaderStyle Width="100px"></HeaderStyle>
              </asp:BoundField>

              <asp:TemplateField HeaderText="Progress (%)" ItemStyle-Width="180px">
                    <ItemTemplate>
                        <asp:Label ID="due" Text='<%# Eval("currentprog") %>' runat="server" CssClass="fulltext" />
                    </ItemTemplate>
                    <ItemStyle Width="110px"></ItemStyle>
                </asp:TemplateField>
              <asp:TemplateField HeaderText="UPDATE" ItemStyle-Height="35px" HeaderStyle-Height="35px"> 
                    <ItemTemplate>
                        <a href="PE_UpdateProgress.aspx?id=<%# Eval("ID") %>">
                            <asp:Label ID="id" Text='UPDATE' runat="server" CssClass="fulltext" />
                        </a>
                    </ItemTemplate>

<HeaderStyle Height="35px"></HeaderStyle>

<ItemStyle Height="35px"></ItemStyle>
                </asp:TemplateField>
             </Columns>
         <FooterStyle BackColor="#CCCCCC" />
         <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
         <PagerStyle BackColor="#999999" ForeColor="Black" HorizontalAlign="Center" />
         <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
         <SortedAscendingCellStyle BackColor="#F1F1F1" />
         <SortedAscendingHeaderStyle BackColor="#808080" />
         <SortedDescendingCellStyle BackColor="#CAC9C9" />
         <SortedDescendingHeaderStyle BackColor="#383838" />
     </asp:GridView>
     <asp:Label ID="Label1" runat="server" Text="* 點選ID 可以顯示之前更新的訊息 Click ID to view history."></asp:Label>
     </br> 
     <asp:Label ID="Label2" runat="server" Text="* 點選UPDATE 以新增UPDATE訊息 Click UPDATE to update."></asp:Label>
     </div>
</asp:Content>
