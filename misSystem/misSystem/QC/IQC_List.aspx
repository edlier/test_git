<%@ Page Title="IQC List" Language="C#" MasterPageFile="~/QC/QC.master" AutoEventWireup="true" CodeBehind="IQC_List.aspx.cs" Inherits="misSystem.QC.IQC_List" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentQC" runat="server">
    <div class="Lp550_Tm5_gradient40px">IQC List</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:GridView ID="grid_IQCList" runat="server" AutoGenerateColumns="False" OnRowDataBound="grid_IQCList_RowDataBound" BackColor="White" BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="3" ForeColor="Black" GridLines="Vertical">
            <AlternatingRowStyle BackColor="#CCCCCC" />
            <Columns>
                <asp:TemplateField HeaderText="NO" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" HeaderStyle-Width="80px">
                    <HeaderStyle Width="80px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Dscription" HeaderText="Description" HeaderStyle-Width="180px" ItemStyle-CssClass="gridStyle">
                    <HeaderStyle Width="180px"></HeaderStyle>

                    <ItemStyle CssClass="gridStyle"></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="CardCode" HeaderText="Vender Code" HeaderStyle-Width="80px">
                    <HeaderStyle Width="80px"></HeaderStyle>
                </asp:BoundField>
                <%--<asp:BoundField DataField="CardName" HeaderText="Vender Name" HeaderStyle-Width="180px" />--%>
                <asp:BoundField DataField="Qty" HeaderText="In Qty" HeaderStyle-Width="60">

                    <HeaderStyle Width="60px"></HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="TQty" HeaderText="Actual Qty" HeaderStyle-Width="80">
                    <HeaderStyle Width="80px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FQty" HeaderText="Failed Qty" HeaderStyle-Width="80">
                    <HeaderStyle Width="80px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="FRate" HeaderText="Failed %" HeaderStyle-Width="80" DataFormatString="{0:0.##}">
                    <HeaderStyle Width="80px"></HeaderStyle>
                </asp:BoundField>
                <asp:BoundField DataField="Issue_ID" HeaderText="IssueID" HeaderStyle-Width="40">

                    <HeaderStyle Width="40px"></HeaderStyle>
                </asp:BoundField>

                <asp:BoundField DataField="End_Time" HeaderText="Finish Date" DataFormatString="{0:yyyy-MM-dd}" HeaderStyle-Width="100px">


                    <HeaderStyle Width="100px"></HeaderStyle>
                </asp:BoundField>


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
