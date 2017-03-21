<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmpPopupWindow.aspx.cs" Inherits="misSystem.HR.personnel.EmpPopupWindow" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Button ID="btn_download" runat="server" Text="Download" OnClick="btn_download_Click" />
        <br /><br />
         <asp:GridView ID="grid_leave" runat="server" AutoGenerateColumns="false" CellPadding="4" >
                <Columns>

                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                             <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工號">
                        <ItemTemplate>
                            <asp:Label ID="lbl_no" runat="server" Text='<%# Eval("EmpNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="員工">
                        <ItemTemplate>
                             <%--<a href="#"><%# Eval("leave") %></a>--%>
                            <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="離職日">
                        <ItemTemplate>
                            <asp:Label ID="lbl_leaveDT" runat="server" Text='<%# Eval("ResignationDT","{0:yyyy/MM/dd}") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>

         <asp:GridView ID="grid_brithday" runat="server" AutoGenerateColumns="false" CellPadding="4" >
                <Columns>

                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                             <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="工號">
                        <ItemTemplate>
                            <asp:Label ID="lbl_no" runat="server" Text='<%# Eval("EmpNo") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="員工">
                        <ItemTemplate>
                             <%--<a href="#"><%# Eval("leave") %></a>--%>
                            <asp:Label ID="lbl_name" runat="server" Text='<%# Eval("EmpName") %>'></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="生日">
                        <ItemTemplate>
                            <asp:Label ID="lbl_leaveDT" runat="server" Text='<%# Eval("birthday","{0:MM/dd}") %>' ></asp:Label>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Center" Wrap="True" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
    </div>
    </form>
</body>
</html>
