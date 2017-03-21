<%@ Page Title="IPQC List" Language="C#" MasterPageFile="~/QC/QC.master" AutoEventWireup="true" CodeBehind="IPQC_List.aspx.cs" Inherits="misSystem.QC.IPQC_List" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

</asp:Content>




<asp:Content ID="Content2" ContentPlaceHolderID="ContentQC" runat="server">
    <div class="Lp550_Tm5_gradient40px">FQC List</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:GridView ID="grid_IPQC" runat="server" AutoGenerateColumns="False" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:TemplateField HeaderText="NO" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:BoundField DataField="SN" HeaderText="SN" HeaderStyle-Width="130px"/>
                <asp:BoundField DataField="Product" HeaderText="Product" HeaderStyle-Width="100px"/>           
                <asp:BoundField DataField="Qulity" HeaderText="Qulity" />           
                <asp:BoundField DataField="FReason" HeaderText="Failed Reason" HeaderStyle-Width="150px" /> 
                        
                <asp:BoundField DataField="SaveTime" HeaderText="SaveTime" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="100px" />
                <asp:BoundField DataField="UserName" HeaderText="Operator" />           
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
    </div>
</asp:Content>
