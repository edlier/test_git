<%@ Page Title="" Language="C#" MasterPageFile="~/HR/mas_hr.master" AutoEventWireup="true" EnableEventValidation="false" CodeBehind="HR_checkReport_personal.aspx.cs" Inherits="misSystem.HR.HR_checkReport_personal" %>

<%@ Register Src="~/UserControl/uc_all_employee.ascx" TagPrefix="uc1" TagName="uc_all_employee" %>
<%@ Register Src="~/UserControl/uc_hr_memo_dl.ascx" TagPrefix="uc1" TagName="uc_hr_memo_dl" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Monthly Attendance Report</title>
<%--    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet" />
    <link href="myCss/HR_css.css" rel="stylesheet" />
    <script src="myJs/HR_js.js"></script>--%>
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
    <div class="HR_PublicTittle">月出勤記錄</div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="out_div" style="top: 180px;">
        <div id="search_div">
            <table id="search_tb">
                <tr>
                    <td style="text-align: left;">User:<asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
                        <br />
                        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                            <ContentTemplate>
                                Year:<asp:DropDownList runat="server" ID="year_dl" Style="margin-right: 1em;" Font-Size="14pt" OnSelectedIndexChanged="year_dl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                Month:<asp:DropDownList runat="server" ID="month_dl" Style="margin-right: 1em;" Font-Size="14pt" OnSelectedIndexChanged="month_dl_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <asp:Label ID="level_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
                        <asp:Label ID="dep_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label>
                    </td>
                    <td>
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <uc1:uc_all_employee runat="server" ID="uc_all_employee" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="text-align: right; padding-left: 1em;">
                        <uc1:uc_hr_memo_dl runat="server" ID="uc_hr_memo_dl" />
                        <br />
                        <asp:Button ID="search_btn" runat="server" Text="Search" OnClick="search_btn_Click" />
                    </td>
                    <td style="text-align: left;">
                        <asp:Panel ID="base_panel" runat="server" Style="margin-left: 1em;">
                            應工作天數:<asp:Label ID="days_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />
                            應工作時數:<asp:Label ID="hours_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />
                        </asp:Panel>
                    </td>
                </tr>
            </table>
        </div>
        <br />

        <asp:Button ID="excel_btn" runat="server" Text="Download" OnClick="excel_btn_Click" Visible="false" />

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Label ID="depID_lab" runat="server" Text="---" CssClass="label-1" Style="margin-right: 1em;" Visible="false"></asp:Label>
                WorkerNum:
                <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" Style="margin-right: 1em;"></asp:Label>
                Name:
                <asp:Label ID="search_name_lab" runat="server" Text="---" CssClass="label-1" Style="margin-right: 1em;"></asp:Label>
                Total Time:
                <asp:Label ID="time_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />


                <asp:GridView ID="checkinout_grid" runat="server" Style="width: 100%; min-width: 100%; background-color: white; margin: 0px; text-align: center; font-size: 16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AutoGenerateColumns="False" OnRowCommand="checkinout_grid_RowCommand">
                    <AlternatingRowStyle BackColor="White" />

                    <Columns>                       
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Date">
                            <ItemTemplate>
                                <asp:Label ID="date_lab1" runat="server" Text='<%# Eval("monthDate") %>' Visible="false"></asp:Label>
                                <asp:Label ID="date_lab" runat="server" Text='<%# Eval("day") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField> 
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="CheckIn">
                            <ItemTemplate>
                                <asp:Label ID="inTime_lab" runat="server" Text='<%# Eval("CheckIn") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="CheckOut" HeaderText="CheckOut" />
                        <asp:BoundField DataField="time" HeaderText="Time" />
                        <asp:BoundField DataField="memo" HeaderText="Memo" />
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Status" Visible="false">
                            <ItemTemplate>
                                <asp:Label ID="status_labs" runat="server" Text='<%# Eval("status") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="修正">
                            <ItemTemplate>
                                <a href="<%# "HR_IO_EditLog.aspx?id=2&wn="+Eval("encode_WorkerNum")+"&date="+Eval("monthDate") %>"><%# Eval("log") %></a>
                                <asp:Label ID="editLog_lab" runat="server" Text='修正' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="請假">
                            <ItemTemplate>
                                <a target="_blank" href="<%# "HR_QuicklyImportDT.aspx?&wn="+Eval("encode_WorkerNum")+"&date="+Eval("monthDate") %>">請假</a>
                                <!--  <a href="HR_IO_EditLog.aspx"><%# Eval("log") %></a> -->
                                <asp:Label ID="leave_lab" runat="server" Text='請假' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="LeaveT" HeaderText="請假時間1" />
                        <asp:BoundField DataField="LeaveT2" HeaderText="請假時間2" />
                        <%--<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="CheckIn">
                            <ItemTemplate>
                                <asp:Label ID="inTime_lab" runat="server" Text='<%# Eval("CheckIn") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="CheckOut">
                            <ItemTemplate>
                                <asp:Label ID="outTime_lab" runat="server" Text='<%# Eval("CheckOut") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>--%>
                       <%--<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Time">
                            <ItemTemplate>
                                <asp:Label ID="outTime_lab" runat="server" Text='<%# Eval("time") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Memo">
                            <ItemTemplate>
                                <asp:Label ID="outTime_lab" runat="server" Text='<%# Eval("memo") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>--%>
                        <%--<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="修正">
                            <ItemTemplate>
                                <a href="<%# "HR_IO_EditLog.aspx?id=2&wn="+Eval("encode_WorkerNum")+"&date="+Eval("monthDate") %>"><%# Eval("log") %></a>
                                <asp:Label ID="editLog_lab" runat="server" Text='修正' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="請假">
                            <ItemTemplate>
                                <a target="_blank" href="<%# "HR_QuicklyImportDT.aspx?&wn="+Eval("encode_WorkerNum")+"&date="+Eval("monthDate") %>">請假</a>
                                <!--  <a href="HR_IO_EditLog.aspx"><%# Eval("log") %></a> -->
                                <asp:Label ID="leave_lab" runat="server" Text='請假' Visible="false"></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>--%>
                       <%-- <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="請假時間1">
                            <ItemTemplate>
                                <asp:Label ID="leaveT_lab" runat="server" Text='<%# Eval("LeaveT") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="請假時間2">
                            <ItemTemplate>
                                <asp:Label ID="leaveT2_lab" runat="server" Text='<%# Eval("LeaveT2") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>--%>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="彈性補回">
                            <ItemTemplate>
                                <asp:Button ID="flex_btn" runat="server" Text='彈性補回' CommandName="flex" CommandArgument ='<%# Container.DataItemIndex %>' OnClientClick="return confirm('確定要彈性補回嗎？')" Visible='<%# (Eval("flexibleT") != "") %>' CssClass="Georgia18px"></asp:Button>     
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



            </ContentTemplate>
        </asp:UpdatePanel>





    </div>
</asp:Content>
