<%@ Page Title="Employee List" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="EmployeeList.aspx.cs" Inherits="misSystem.HR.personnel.EmployeeList" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Src="~/UserControl/utils/webUCDateBetween.ascx" TagName="webUCDateBetween" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/sys/webUCDDLSysCode.ascx" TagName="webUCDDLSysCode" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/sys/webUCDDLCompany.ascx" TagPrefix="uc3" TagName="webUCDDLCompany" %>
<%@ Register Src="~/UserControl/hr/webUCDDLDept.ascx" TagPrefix="uc4" TagName="webUCDDLDept" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../../css/calendar.css" rel="stylesheet" />
    <link href="../myCss/HR_css.css?v1.011.01" rel="stylesheet" />
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div id="tmpdiv" style="top: 150px;">

        <div>
            <asp:Button ID="btn_edu" runat="server" Text="學歷" Width="60px" Visible="false" /><%--&nbsp;&nbsp;&nbsp;--%>
            <asp:Button ID="btn_exp" runat="server" Text="經歷" Width="60px" Visible="false" /><%--&nbsp;&nbsp;&nbsp;--%>
            <asp:Button ID="btn_newEmp" runat="server" Text="新人作業" Width="80px" OnClick="btn_newEmp_Click" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_repu" runat="server" Text="獎懲紀錄" Width="80px" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_change" runat="server" Text="異動紀錄" Width="80px" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_trn" runat="server" Text="教育訓練" Width="80px" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_eva" runat="server" Text="考核紀錄" Width="80px" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_leave" runat="server" Text="請假紀錄" Width="80px" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btn_ot" runat="server" Text="加班紀錄" Width="80px" />&nbsp;&nbsp;&nbsp;
            <asp:Button ID="btnReturn" runat="server" Text="上一頁" OnClick="btnReturn_Click" />
            <%--<asp:ImageButton ID="imgbtnReturn" runat="server" ImageUrl="~/img/btn/images_return01.jpg" Width="30" Height="30" Visible="true" OnClick="btnReturn_Click"></asp:ImageButton>--%>
        </div>
       
        <br />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

                <div>
                    <asp:GridView ID="grid_sta" runat="server" AutoGenerateColumns="false" CellPadding="4">
                        <Columns>

                            <asp:TemplateField HeaderText="新人">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_new" runat="server" Text='<%# Eval("newEmp") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="轉調">
                                <ItemTemplate>
                                    <asp:Label ID="lbl_change" runat="server" Text='<%# Eval("change") %>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="離職">
                                <ItemTemplate>
                                    <a href="#" onclick="window.open('EmpPopupWindow.aspx?type=leave', null,'width=350,height=600,scrollbars=yes,left=350,top=200,right=5,bottom=5')"><%# Eval("leave") %></a>
                                    <%--<asp:Label ID="lbl_leave" runat="server" Text='<%# Eval("leave") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="本季壽星">
                                <ItemTemplate>
                                    <a href="#" onclick="<%# "window.open('EmpPopupWindow.aspx?type=birthday&quar="+quar+"', null,'width=350,height=600,scrollbars=yes,left=350,top=200,right=5,bottom=5')" %>"><%# Eval("birthday") %></a>
                                    <%--<asp:Label ID="lbl_birthday" runat="server" Text='<%# Eval("birthday") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="下季壽星">
                                <ItemTemplate>
                                    <a href="#" onclick="<%# "window.open('EmpPopupWindow.aspx?type=birthday&quar="+nextQuar+"', null,'width=350,height=600,scrollbars=yes,left=350,top=200,right=5,bottom=5')" %>"><%# Eval("nextbirthday") %></a>
                                    <%--<asp:Label ID="lbl_birthday" runat="server" Text='<%# Eval("nextbirthday") %>'></asp:Label>--%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" Wrap="True" />
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>

                </div>

                <br />

                <div id="div2" runat="server" style="background-color: white; border-style: groove; padding: 10px">
                    <table>
                        <tr>
                            <td style="width: 50px;" rowspan="2">
                                <asp:ImageButton ID="btnNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" OnClick="btnNew_Click" /></td>
                            <td>公司別：<uc3:webUCDDLCompany runat="server" ID="ucCompany" />
                                &nbsp;</td>
                            <td>部門別：<uc4:webUCDDLDept runat="server" ID="ucDept" />
                                &nbsp;</td>
                            <td>
                                <uc2:webUCDDLSysCode ID="ucChange" runat="server" Visible="false" />
                                <asp:Button ID="btn_adv" runat="server" Text="進階查詢" Width="80px" Visible="false" /></td>
                        </tr>
                        <tr>
                            <td colspan="2">到職日：<uc1:webUCDateBetween runat="server" ID="ucDTBetween" />
                                &nbsp;&nbsp;&nbsp;    
                            </td>
                            <td>
                                <%--<asp:ImageButton ID="imb_search" runat="server" ImageUrl="~/img/btn/images_search.png" Width="40" Height="40"></asp:ImageButton>--%>
                                <asp:Button ID="btn_search" runat="server" Text="搜尋" Width="60px" OnClick="btn_search_Click" />
                            </td>
                            <td>
                                <%--<asp:ImageButton ID="imb_search" runat="server" ImageUrl="~/img/btn/images_search.png" Width="40" Height="40"></asp:ImageButton>--%>
                                <asp:Button ID="btn_clear" runat="server" Text="清除條件" Width="80px" OnClick="btn_clear_Click" />
                            </td>
                        </tr>
                    </table>

                </div>


                <br />


                    <%--DataKeyNames="ID"
                        GridLines="None" BorderLine="None" 
                        AutoGenerateColumns="False" 
                        AllowPaging="True" 
                        OnPageIndexChanging="gvCodeType_PageIndexChanging"
                        OnPageIndexChanged="gvCodeType_PageIndexChanged"Width="
                        OnRowDataBound="gvCodeType_RowDataBound"
                        OnRowCreated="gvCodeType_RowCreated"
                        OnSelectedIndexChanged="gvCodeType_SelectedIndexChanged" 
                        OnRowCommand ="gvCodeType_RowCommand"
                        ShowFooter="False" 
                        ShowHeaderWhenEmpty="True"--%>
                    <asp:GridView ID="grid_emp" runat="server" Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" OnRowCreated="grid_emp_RowCreated" AllowSorting="true" AllowPaging="true" OnPageIndexChanging="grid_emp_PageIndexChanging" OnSorting="grid_emp_Sorting" OnRowCommand="grid_emp_RowCommand">
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:ImageButton ID="imbQuery" runat="server" CommandName="QRY" CommandArgument='<%# Container.DataItemIndex %>' ImageUrl="~/img/images_search01.png" Width="30" Height="30" />
                                </ItemTemplate>
                                <ItemStyle Width="32px" HorizontalAlign="Left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Item">
                                <ItemTemplate>
                                    <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
                                </ItemTemplate>
                                <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:TemplateField>
                            <asp:BoundField DataField="EmpPID" HeaderText="EmpPID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpNo" SortExpression="EmpNo" HeaderText="Emp No">
                                <ItemStyle Width="100px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpName" SortExpression="EmpName" HeaderText="Emp Name">
                                <ItemStyle Width="140px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="DeptID" SortExpression="DeptID" HeaderText="DeptID(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Dept" SortExpression="Dept" HeaderText="Department">
                                <ItemStyle Width="200px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="EmpCountry" SortExpression="EmpCountry" HeaderText="Emp Country(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="OnboardDT" SortExpression="OnboardDT" HeaderText="Onboard Date" DataFormatString="{0:yyyy-MM-dd}">
                                <ItemStyle Width="200px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="Seniority" SortExpression="Seniority" HeaderText="Seniority">
                                <ItemStyle Width="120px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="SenioritySort" HeaderText="SenioritySort(Hide)">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <%-- <asp:BoundField DataField="sen" HeaderText="sen">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="mon" HeaderText="mon">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>
                            <asp:BoundField DataField="days" HeaderText="days">
                                <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                            </asp:BoundField>--%>
                            <asp:TemplateField HeaderText="">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgbtn" ImageUrl="~/img/images_edit01.png" runat="server"
                                        Width="30" Height="30" />
                                </ItemTemplate>
                                <ItemStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
                        <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
                        <PagerStyle CssClass="gridview" />
                        <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
                        <RowStyle Height="35" />
                        <SelectedRowStyle BackColor="LightCyan" />
                    </asp:GridView>
                    <br />
            </ContentTemplate>
        </asp:UpdatePanel>


    </div>

</asp:Content>
