<%@ Page Title="" Language="C#" MasterPageFile="~/QC/QC.Master" AutoEventWireup="true" CodeBehind="cnnSAPList.aspx.cs" Inherits="misSystem.QC.cnnSAPList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../CssFile/GridviewScroll.css" rel="stylesheet" />

    <%--<script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jquery/1.8.2/jquery.min.js"></script>
        <script type="text/javascript" src="http://ajax.googleapis.com/ajax/libs/jqueryui/1.9.1/jquery-ui.min.js"></script>--%>

    <script src="../scripts/1.8.2-jquery.min.js"></script>
    <script src="../scripts/1.9.1jquery-ui.min.js"></script>
    <%--↓縮減用JQUERY--%>
    <%--<script src="../scripts/gridviewScroll.min.js" type="text/javascript"></script>--%>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=GridView1.ClientID%>').gridviewScroll({
                    width: 1200,
                    height: 600
                });
            }
    </script>
    <style>
        .gridStyle {
            word-break: break-all;
            word-wrap: break-word;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentQC" runat="server">
    <div class="Lp550_Tm5_gradient40px">IQC - Validation List</div>
    <div class="Lp550-Tm325-georgia18px">
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False"
            BackColor="White" BorderColor="#DEDFDE" BorderWidth="1px"
            CellPadding="4" ForeColor="Black" GridLines="Vertical" BorderStyle="None">

            <AlternatingRowStyle BackColor="White" />
            <Columns>
                <asp:TemplateField HeaderText="NO" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <%#Container.DataItemIndex + 1%>
                    </ItemTemplate>

<ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="DocEntry" HeaderText="內部號碼" />--%>
                <asp:BoundField DataField="DocNum" HeaderText="文件號碼" />
                <asp:BoundField DataField="ItemCode" HeaderText="ItemCode" />
                <asp:TemplateField HeaderText="Qty">
                    <ItemTemplate>
                        <asp:HiddenField ID="LineNum" runat="server" Value='<%# Eval("LineNum") %>'/>
                        <asp:Label ID="Qty" runat="server" Text='<%# Eval("Qty") %>' DataFormatString="{0:0.##}" Width="50px" ></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <%--<asp:BoundField DataField="BaseLine" HeaderText="DNo"/>--%>
<%--                <asp:BoundField DataField="Qty" HeaderText="Qty" DataFormatString="{0:0.##}" ItemStyle-Width="50px">
                </asp:BoundField>--%>
                <asp:TemplateField HeaderText="">
                    <ItemTemplate>
                        <asp:Button ID="btn_start" runat="server" OnClick="btn_start_Click" CssClass="Georgia18px"  CommandArgument="<%# ((GridViewRow) Container).RowIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:Label ID="status" runat="server" Text='<%# Eval("status2") %>'></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="Dscription" HeaderText="Dscription" ItemStyle-Width="400px" ItemStyle-Wrap="true" ItemStyle-CssClass="gridStyle">
                    <ItemStyle Width="400px" Wrap="true"></ItemStyle>
                </asp:BoundField>
<%--                <asp:BoundField DataField="CardCode" HeaderText="CardCode" />
                <asp:BoundField DataField="CardName" HeaderText="CardName" />--%>
                <asp:BoundField DataField="ChiName" HeaderText="Operator" ItemStyle-Width="280px">
                    <ItemStyle Width=""></ItemStyle>
                </asp:BoundField>
                <asp:BoundField DataField="DocDate2" HeaderText="DocDate" DataFormatString="{0:yyyy-MM-dd}" />

            </Columns>





<%--            <HeaderStyle CssClass="GridviewScrollHeader" />
            <RowStyle CssClass="GridviewScrollItem" />
            <PagerStyle CssClass="GridviewScrollPager" />--%>
            <%--<FooterStyle BackColor="Tan" />--%>
            <%--<HeaderStyle BackColor="Tan" Font-Bold="True" CssClass="GridviewScrollHeader"/>--%>
            <FooterStyle BackColor="#CCCC99" />
            <HeaderStyle BackColor="#6B696B" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#F7F7DE" ForeColor="Black" HorizontalAlign="Right" />
            <RowStyle BackColor="#F7F7DE" />
            <SelectedRowStyle BackColor="#CE5D5A" ForeColor="White" Font-Bold="True" />
            <SortedAscendingCellStyle BackColor="#FBFBF2" />
            <SortedAscendingHeaderStyle BackColor="#848384" />
            <SortedDescendingCellStyle BackColor="#EAEAD3" />
            <SortedDescendingHeaderStyle BackColor="#575357" />
        </asp:GridView>
        <br />
        <br />
    </div>
</asp:Content>
