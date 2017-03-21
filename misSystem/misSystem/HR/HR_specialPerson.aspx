<%@ Page Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_specialPerson.aspx.cs" Inherits="misSystem.HR.HR_specialPerson" %>

<%@ Register Src="~/UserControl/uc_all_employee.ascx" TagPrefix="uc1" TagName="uc_all_employee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Special Person</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet" />
    <script src="myJs/jquery.timepicker.js"></script>
    <link href="myCss/jquery.timepicker.css" rel="stylesheet" />
    <link href="myCss/HR_css.css" rel="stylesheet" />
    <script src="myJs/HR_js.js"></script>
    <%--<script src="../scripts/gridviewScroll.min.js"></script>
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=spePerson_grid.ClientID%>').gridviewScroll({
                width: 900,
                height: 300
            });
        }
    </script>--%>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="out_panel" CssClass="out_panel" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <asp:Panel ID="form_panel" runat="server">

                    <h3>特殊人員表單</h3>
                    <asp:GridView ID="spePerson_grid" runat="server" Style="width: 100%; min-width: 100%; background-color: white; margin: 0px; text-align: center; font-size: 16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AutoGenerateColumns="False" OnRowCommand="checkinoutlog_grid_RowCommand">
                        <AlternatingRowStyle BackColor="White" />

                        <Columns>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="流水號">
                                <ItemTemplate>
                                    <%#Container.DataItemIndex + 1%>
                                    <asp:HiddenField ID="id_hf" runat="server" Value='<%# Eval("id") %>' />
                                    <%--   <asp:Label ID="id_lab" runat="server" Text='<%# Eval("id") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="WorkNum">
                                <ItemTemplate>
                                    <asp:Label ID="workerNum_lab" runat="server" Text='<%# Eval("WorkerNum") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="ChiName">
                                <ItemTemplate>
                                    <asp:Label ID="name_lab" runat="server" Text='<%# Eval("ChiName") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Checkin">
                                <ItemTemplate>
                                    <asp:HiddenField ID="checkin_hf" runat="server" Value='<%# Eval("checkin") %>' />
                                    <asp:Label ID="checkin_lab" runat="server" Text='<%# Eval("inT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Checkout">
                                <ItemTemplate>
                                    <asp:HiddenField ID="checkout_hf" runat="server" Value='<%# Eval("checkout") %>' />
                                    <asp:Label ID="checkout_lab" runat="server" Text='<%# Eval("outT") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Type">
                                <ItemTemplate>
                                    <asp:HiddenField ID="type_hf" runat="server" Value='<%# Eval("type") %>' />
                                    <asp:Label ID="type_lab" runat="server" Text='<%# Eval("Descript") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="刪除">
                                <ItemTemplate>
                                    <asp:Button ID="del" runat="server" Text='Del' CommandName="del" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定要刪除嗎？')" CssClass="Georgia18px"></asp:Button>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="修改">
                                <ItemTemplate>
                                    <asp:Button ID="edit" runat="server" Text='Edit' CommandName="edi" CommandArgument='<%# Container.DataItemIndex %>' CssClass="Georgia18px"></asp:Button>
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
                    <asp:Button ID="insert_btn" runat="server" Text="Insert" CssClass="Georgia18px" OnClick="insert_btn_Click" />
                    <br />
                    <br />
                    <asp:Panel ID="Panel1" runat="server" Visible="false">
                        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                            <ContentTemplate>
                                <uc1:uc_all_employee runat="server" ID="uc_all_employee" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br />
                        類別:<asp:DropDownList ID="type_dl" runat="server" Font-Size="14" OnSelectedIndexChanged="type_dl_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList><asp:HiddenField ID="id2_hf" runat="server" />
                        <br />
                        <br />

                        <asp:Panel ID="dropPanel" runat="server" Visible="false">
                            設定<br />
                            Checkin:<asp:DropDownList ID="checkin_dl" runat="server" CssClass="Georgia18px"></asp:DropDownList><br />
                            Checkout:<asp:DropDownList ID="checkout_dl" runat="server" CssClass="Georgia18px"></asp:DropDownList>
                        </asp:Panel>
                        <asp:Panel ID="tbPanel" runat="server" Visible="false">
                            設定<br />
                            離職時間<asp:TextBox ID="date_text" runat="server" CssClass="datepicker" TextMode="DateTime"></asp:TextBox>
                        </asp:Panel>
                        <br />
                        <asp:Panel ID="submitPanel" runat="server" Visible="false">
                            <asp:Button ID="submit_btn" runat="server" Text="Submit" CssClass="Georgia18px" OnClick="submit_btn_Click" />
                        </asp:Panel>
                        <asp:Panel ID="updatePanel" runat="server" Visible="false">
                            <asp:Button ID="update_btn" runat="server" Text="Update" CssClass="Georgia18px" OnClick="update_btn_Click" />
                        </asp:Panel>
                    </asp:Panel>
                </asp:Panel>
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker("option", "minDate", null);
            $('.datepicker').datepicker("option", "maxDate", null);

            //配合update panel不會重新執行ready事件
            //為update panel註冊事件，在update完成後執行initi_up();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_pageLoaded(initi_up);
            function initi_up() {
                date_initi();
                $('.datepicker').datepicker("option", "minDate", null);
                $('.datepicker').datepicker("option", "maxDate", null);
            }
        });
    </script>
</asp:Content>

