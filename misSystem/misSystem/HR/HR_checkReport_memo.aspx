<%@ Page Title="" Language="C#" MasterPageFile="~/HR/mas_hr.master" AutoEventWireup="true" CodeBehind="HR_checkReport_memo.aspx.cs" Inherits="misSystem.HR.HR_checkReport_memo" %>

<%@ Register Src="~/UserControl/uc_all_employee.ascx" TagPrefix="uc1" TagName="uc_all_employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Monthly Leave Records</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet" />
    <link href="myCss/HR_css.css" rel="stylesheet" />
    <script src="myJs/HR_js.js"></script>
    <style>
        #search_div {
            font-size: 14pt;
            margin: 0;
            padding: 0;
            border-bottom: 2pt groove #0a89e5;
        }

        #search_tb {
            margin: 0 auto;
            padding: 0;
            text-align: left;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForHR" runat="server">
    <div class="HR_PublicTittle">月請假記錄</div>
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="out_div" style="top: 180px;">
        <asp:Panel ID="out_panel" runat="server">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                <ContentTemplate>
                    <div id="search_div">
                        <table id="search_tb">
                            <tr>
                                <td style="text-align: left;">User:
                                <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                                    <br />
                                    Year:<asp:DropDownList runat="server" ID="year_dl" Style="margin-right: 1em;" Font-Size="14pt" OnSelectedIndexChanged="year_dl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                    Month:<asp:DropDownList runat="server" ID="month_dl" Style="margin-right: 1em;" Font-Size="14pt" OnSelectedIndexChanged="month_dl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>

                                    <asp:Label ID="level_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
                                    <asp:Label ID="dep_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <uc1:uc_all_employee runat="server" ID="uc_all_employee" />
                                </td>
                                <td style="vertical-align: bottom;">
                                    <asp:Button ID="search_btn" runat="server" Text="Search" OnClick="search_btn_Click" /></td>
                            </tr>
                        </table>
                    </div>
                    <br />

                    <asp:Button ID="excel_btn" runat="server" Text="Download" Visible="false" />


                    <%--WorkerNum: <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" style="margin-right:1em;" Visible="false"></asp:Label>
        Name: <asp:Label ID="search_name_lab" runat="server" Text="---" CssClass="label-1" style="margin-right:1em;" Visible="false"></asp:Label>--%>

                    <asp:GridView ID="checkinout_grid" runat="server" Style="width: 100%; min-width: 100%; background-color: white; margin: 0px; text-align: center; font-size: 16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AutoGenerateColumns="False" OnRowCommand="checkinout_grid_RowCommand">
                        <AlternatingRowStyle BackColor="White" />

                        <Columns>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="ID" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="sn_lab" runat="server" Text='<%# Eval("SN") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="WorkerNum">
                                <ItemTemplate>
                                    <asp:Label ID="wn_lab" runat="server" Text='<%# Eval("workerNum") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Name">
                                <ItemTemplate>
                                    <a href="<%# "HR_checkReport_personal.aspx?wn="+Eval("encode_WorkerNum")+para %>"><%# Eval("Name") %></a>
                                    <%--<asp:Label ID="name_lab" runat="server" Text='<%# Eval("Name") %>'></asp:Label>   --%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Start">
                                <ItemTemplate>
                                    <asp:Label ID="start" runat="server" Text='<%# Eval("start_time", @"{0:yyyy-MM-dd HH:mm:ss}") %>' Visible="false"></asp:Label>                                                                   
                                    <asp:Label ID="start_lab" runat="server" Text='<%# Eval("start_time", @"{0:M/d HH:mm}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="End">
                                <ItemTemplate>
                                    <asp:Label ID="end" runat="server" Text='<%# Eval("end_time", @"{0:yyyy-MM-dd HH:mm:ss}") %>' Visible="false"></asp:Label>
                                    <asp:Label ID="end_lab" runat="server" Text='<%# Eval("end_time", @"{0:M/d HH:mm}") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Memo">
                                <ItemTemplate>
                                    <asp:Label ID="memo_lab" runat="server" Text='<%# Eval("memo") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Hour">
                                <ItemTemplate>
                                    <asp:Label ID="hour_lab" runat="server" Text='<%# Eval("time") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Status">
                                <ItemTemplate>
                                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("Status") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Type" Visible="false">
                                <ItemTemplate>
                                    <asp:Label ID="type_lab" runat="server" Text='<%# Eval("type") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:Button ID="del" runat="server" Text='del' CommandName="del" CommandArgument='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定要刪除嗎？')" CssClass="Georgia18px"></asp:Button>
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
                    <br />
                    <%--<asp:GridView ID="late_gv" runat="server" style="width:100%; min-width:100%; background-color:white; margin:0px; text-align:center; font-size:16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AutoGenerateColumns="False" >
            <AlternatingRowStyle BackColor="White" />
                        
            <Columns>             
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Date">
                <ItemTemplate>
                    <asp:Label ID="date_lab" runat="server" Text='<%# Eval("Date") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>   
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="CheckIn">
                <ItemTemplate>
                    <asp:Label ID="inTime_lab" runat="server" Text='<%# Eval("CheckIn") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="CheckOut">
                <ItemTemplate>
                    <asp:Label ID="outTime_lab" runat="server" Text='<%# Eval("CheckOut") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Memo">
                <ItemTemplate>
                    <asp:Label ID="outTime_lab" runat="server" Text='<%# Eval("memo") %>'></asp:Label>                    
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
                    --%>
                </ContentTemplate>
            </asp:UpdatePanel>
        </asp:Panel>

    </div>
</asp:Content>
