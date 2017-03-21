<%@ Page Title="Monthly Report" Language="C#" MasterPageFile="~/HR/mas_hr.master" AutoEventWireup="true" CodeBehind="HR_checkReport_month.aspx.cs" Inherits="misSystem.HR.HR_checkReport_month" %>

<%@ Register Src="~/UserControl/uc_all_employee.ascx" TagPrefix="uc1" TagName="uc_all_employee" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Monthly Report</title>
<%--    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet" />
    <link href="myCss/HR_css.css" rel="stylesheet" />
    <script src="myJs/HR_js.js"></script>--%>
    <script>
        // 顯示讀取遮罩
        function ShowProgressBar() {
            displayProgress();
            displayMaskFrame();
        }

        // 隱藏讀取遮罩
        function HideProgressBar() {
            var progress = $('#divProgress');
            var maskFrame = $("#divMaskFrame");
            progress.hide();
            maskFrame.hide();
        }
        // 顯示讀取畫面
        function displayProgress() {
            var w = $(document).width();
            var h = $(window).height();
            var progress = $('#divProgress');
            progress.css({ "z-index": 999999, "top": (h / 2) - (progress.height() / 2), "left": (w / 2) - (progress.width() / 2) });
            progress.show();
        }
        // 顯示遮罩畫面
        function displayMaskFrame() {
            var w = $(window).width();
            var h = $(document).height();
            var maskFrame = $("#divMaskFrame");
            maskFrame.css({ "z-index": 999998, "opacity": 0.7, "width": w, "height": h });
            maskFrame.show();
        }
    </script>
    <%--<script type="text/javascript" src="myJs//gridviewScroll.min.js"></script> 
    <script type="text/javascript">
        $(document).ready(function () {
            gridviewScroll();
        });

        function gridviewScroll() {
            $('#<%=checkinout_grid.ClientID%>').gridviewScroll({
                width: 900,
                height: 300
            });
        }
    </script>--%>
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
            text-align: center;
            border-collapse: collapse;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForHR" runat="server">
    <div class="HR_PublicTittle">月報表</div>

    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="out_div" style="top: 180px;">
        <div id="search_div">
            <table id="search_tb">
                <tr>
                    <td style="text-align: left;">
                        User:<asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
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
                    <td style="text-align: center;">
                        <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                            <ContentTemplate>
                                <uc1:uc_all_employee runat="server" ID="uc_all_employee" />
                            </ContentTemplate>
                        </asp:UpdatePanel>
                    </td>
                    <td style="vertical-align: bottom;">
                        <asp:Button ID="search_btn" runat="server" Text="Search" OnClientClick="ShowProgressBar()" OnClick="search_btn_Click" />
                    </td>
                </tr>
            </table>
        </div>
        <br />

        <asp:Label ID="title_lab" runat="server" Text="---" CssClass="label-1" Style="margin-right: 1em;" Visible="false"></asp:Label>
        <asp:Button ID="excel_btn" runat="server" Text="Download" Visible="false" OnClick="excel_btn_Click" />
        <asp:Button ID="btn_settleUp" runat="server" Text="結算" Visible="false" OnClick="btn_settleUp_Click"/>


        

        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="checkinout_grid" runat="server" Style="background-color: white; margin: 0px; text-align: center; font-size: 16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AutoGenerateColumns="False" OnRowDataBound="checkinout_grid_RowDataBound" Height="150px">                    
                    <AlternatingRowStyle BackColor="White" />

                    <Columns>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="工號">
                            <ItemTemplate>
                                <asp:Label ID="memo_lab" runat="server" Text='<%# Eval("workerNum") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="姓名" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <a href="<%# "HR_checkReport_personal.aspx?wn="+Eval("encode_WorkerNum")+para %>"><%# Eval("ChiName") %></a>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ChiName") %>' Width="150px" Visible="false"></asp:Label>
                                <%--<asp:Label ID="memo_lab" runat="server" Text='<%# Eval("ChiName") %>' Width="150px"></asp:Label>       --%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="xLeave" HeaderText="特休(時)" HeaderStyle-Width=""></asp:BoundField> 
                        <asp:BoundField DataField="perleave" HeaderText="事假(時)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="sickleave" HeaderText="病假(時)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="workout" HeaderText="外出(時)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="compensatory" HeaderText="補休(時)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="late" HeaderText="遲到(分)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="countLate" HeaderText="遲到(次)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="absenteeism" HeaderText="曠職(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="bereavement" HeaderText="喪假(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="publicleave" HeaderText="公假(時)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="xSickleave" HeaderText="公傷假(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="marriageleave" HeaderText="婚假(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="maternity" HeaderText="產假(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="paternity" HeaderText="陪產假(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="nopayleave" HeaderText="留職停薪(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="businesstrip" HeaderText="出差(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="Seekingleave" HeaderText="謀職假(天)" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="InaugurationDate" HeaderText="就職日" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="leavedDT" HeaderText="離職日" HeaderStyle-Width=""></asp:BoundField>

                          <%--        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="特休(天)">
                <ItemTemplate>
                    <asp:Label ID="memo_lab" runat="server" Text='<%# Eval("xLeave") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--<asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="事假(時)">
                <ItemTemplate>
                    <asp:Label ID="start_lab" runat="server" Text='<%# Eval("leave") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="病假(時)">
                <ItemTemplate>
                    <asp:Label ID="end_lab" runat="server" Text='<%# Eval("sick_leave") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="外出(時)">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("workout") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="補休(時)">
                <ItemTemplate>
                    <asp:Label ID="memo_lab" runat="server" Text='<%# Eval("compensatory") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="遲到(分)">
                <ItemTemplate>
                    <asp:Label ID="start_lab" runat="server" Text='<%# Eval("late") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="曠職(天)">
                <ItemTemplate>
                    <asp:Label ID="start_lab" runat="server" Text='<%# Eval("absenteeism") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="喪假(天)">
                <ItemTemplate>
                    <asp:Label ID="end_lab" runat="server" Text='<%# Eval("bereavement") %>'></asp:Label>                    
                </ItemTemplate>--%>
                        <%--                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>     --%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="公假(時)">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("public_leave") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="公傷假(時)">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("xSick_leave") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="婚假(天)">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("marriage_leave") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                        <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="產假(天)">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("maternity") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
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

        <div id="divProgress" style="text-align: center; display: none; position: fixed; top: 50%; left: 50%;">
            <asp:Image ID="imgLoading" runat="server" ImageUrl="../picture/ajax-loader.gif" />
            <br />
            <asp:Label ID="Label2" runat="server" Text="資料處理中" ForeColor="#1B3563" Font-Size="15px"></asp:Label>

        </div>
        <div id="divMaskFrame" style="background-color: #F2F4F7; display: none; left: 0px; position: absolute; top: 0px;">
        </div>

        <br /><br />

        <asp:Label ID="parttime_lab" runat="server" Text="---" CssClass="label-1" Style="margin-right: 1em;" Visible="false"></asp:Label>
        <asp:Button ID="download_btn" runat="server" Text="Download" Visible="false" OnClick="download_btn_Click" />

                <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:GridView ID="parttime_grid" runat="server" Style="background-color: white; margin: 0px; text-align: center; font-size: 16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" AutoGenerateColumns="False" Height="150px">
                    <AlternatingRowStyle BackColor="White" />

                    <Columns>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="工號">
                            <ItemTemplate>
                                <asp:Label ID="memo_lab" runat="server" Text='<%# Eval("workerNum") %>'></asp:Label>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="姓名" ItemStyle-Width="150px">
                            <ItemTemplate>
                                <a href="<%# "HR_checkReport_personal.aspx?wn="+Eval("encode_WorkerNum")+para %>"><%# Eval("ChiName") %></a>
                                <asp:Label ID="Label1" runat="server" Text='<%# Eval("ChiName") %>' Width="150px" Visible="false"></asp:Label>
                                <%--<asp:Label ID="memo_lab" runat="server" Text='<%# Eval("ChiName") %>' Width="150px"></asp:Label>       --%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center" Wrap="True" />
                        </asp:TemplateField>
                        <asp:BoundField DataField="day" HeaderText="總天數" HeaderStyle-Width=""></asp:BoundField>
                        <asp:BoundField DataField="time" HeaderText="時間" HeaderStyle-Width=""></asp:BoundField> 

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

                <%-- 以下button and gridview 為測試看資料用，上線請記得刪除 --%>
                <%--                <asp:Button ID="Button1" runat="server" Text="checkData" OnClick="Button1_Click"/>
                <asp:Button ID="Button2" runat="server" Text="leaveData" OnClick="Button2_Click"/>
            <asp:GridView ID="test" runat="server" style="width:100%; min-width:100%; background-color:white; margin:0px; text-align:center; font-size:16pt;" CellPadding="3" ForeColor="#333333" GridLines="None" EmptyDataText="Searching didn't match any records." AutoGenerateColumns="True" >
            <AlternatingRowStyle BackColor="White" />--%>

                <%--<Columns>--%>
                <%--                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Memo">
                <ItemTemplate>
                    <asp:Label ID="memo_lab" runat="server" Text='<%# Eval("memo") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Start">
                <ItemTemplate>
                    <asp:Label ID="start_lab" runat="server" Text='<%# Eval("start_time", @"{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="End">
                <ItemTemplate>
                    <asp:Label ID="end_lab" runat="server" Text='<%# Eval("end_time",@"{0:yyyy-MM-dd HH:mm:ss}") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>
                <asp:TemplateField ConvertEmptyStringToNull="True" HeaderText="Status">
                <ItemTemplate>
                    <asp:Label ID="status_lab" runat="server" Text='<%# Eval("Status") %>'></asp:Label>                    
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                </asp:TemplateField>--%>
                <%--</Columns>--%>
                <%--            <EditRowStyle BackColor="#2461BF" />
            <FooterStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#507CD1" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#2461BF" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#EFF3FB" />
            <SelectedRowStyle BackColor="#D1DDF1" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F5F7FB" />
            <SortedAscendingHeaderStyle BackColor="#6D95E1" />
            <SortedDescendingCellStyle BackColor="#E9EBEF" />
            <SortedDescendingHeaderStyle BackColor="#4870BE" />
        </asp:GridView>    --%>
            
    </div>
</asp:Content>
