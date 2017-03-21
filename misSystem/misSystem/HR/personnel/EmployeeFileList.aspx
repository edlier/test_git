<%@ Page Title="報到文件繳交清單" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="EmployeeFileList.aspx.cs" Inherits="misSystem.HR.personnel.EmployeeFileList" %>

<%@ Register Src="~/UserControl/utils/webUCDateBetween.ascx" TagName="webUCDateBetween" TagPrefix="uc1" %>
<%@ Register Src="~/UserControl/sys/webUCDDLSysCode.ascx" TagName="webUCDDLSysCode" TagPrefix="uc2" %>
<%@ Register Src="~/UserControl/sys/webUCDDLCompany.ascx" TagPrefix="uc3" TagName="webUCDDLCompany" %>
<%@ Register Src="~/UserControl/hr/webUCDDLDept.ascx" TagPrefix="uc4" TagName="webUCDDLDept" %>
<%@ Register Src="~/UserControl/uc_all_employee.ascx" TagPrefix="uc1" TagName="uc_all_employee" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="../myCss/HR_css.css?v1.011.01" rel="stylesheet" />

    <style type="text/css">
        h2 {
            color: blue;
        }
    </style>
    
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="out_div" style="top: 150px; width:1500px">
        <h2>報到文件繳交清單</h2>
        <asp:Button ID="btnReturn" runat="server" Text="上一頁" OnClick="btnReturn_Click" />
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>
                <div id="div2" runat="server" style="background-color: white; border-style: groove; padding: 10px; width:50%;">
                    <table>
                        <tr>
                            <td style="width: 50px;" rowspan="2">
                              <%--  <asp:ImageButton ID="btnNew" Text="New" ImageUrl="~/img/images_add03.png" runat="server" Width="30" Height="30" OnClick="btnNew_Click" />--%></td>
                            <td>
                                <uc1:uc_all_employee runat="server" ID="ucEmployee" />
                                <%--公司別：<uc3:webucddlcompany runat="server" ID="ucCompany" />&nbsp;--%></td>
                            <td><%--部門別：<uc4:webUCDDLDept runat="server" ID="ucDept" />&nbsp;--%></td>
                            <td>
                                <%--<uc2:webUCDDLSysCode ID="ucChange" runat="server" Visible="false" />
                                <asp:Button ID="btn_adv" runat="server" Text="進階查詢" Width="80px" />--%></td>
                        </tr>
                        <tr>
                           <td colspan="2">繳交狀態：
                               <asp:DropDownList ID="dl_status" runat="server" CssClass="Georgia18px">
                                   <asp:ListItem Value="0">--Select--</asp:ListItem>
                                   <asp:ListItem Value="1">未齊全</asp:ListItem>
                                   <asp:ListItem Value="2">繳交齊全</asp:ListItem>
                               </asp:DropDownList>
                                <%--到職日：<asp:TextBox ID="tb_startDate" runat="server"></asp:TextBox>
                                ~  
                                <asp:TextBox ID="tb_endDate" runat="server"></asp:TextBox>&nbsp;&nbsp;&nbsp;    --%>
                            </td>
                            <td>
                                <%--<asp:ImageButton ID="imb_search" runat="server" ImageUrl="~/img/btn/images_search.png" Width="40" Height="40"></asp:ImageButton>--%>
                                <asp:Button ID="btn_search" runat="server" Text="搜尋" Width="60px" CssClass="Georgia18px" OnClick="btn_search_Click" />
                            </td>
                        </tr>
                    </table>

                </div>



                <br /><br />

                <asp:GridView ID="file_grid" runat="server"  Style="overflow: auto" GridLines="None" BorderLine="None" AutoGenerateColumns="False" AllowSorting="False" AllowPaging="true" OnRowDataBound="file_grid_RowDataBound" OnRowCreated="file_grid_RowCreated" OnPageIndexChanging="file_grid_PageIndexChanging" Width="50%" EmptyDataText="None Data!" ShowHeaderWhenEmpty="true" >
                <AlternatingRowStyle BackColor="White" />

                <Columns>
                    <asp:TemplateField HeaderText="No.">
                        <ItemTemplate>
                            <asp:Label ID="lblRowNumber" Text='<%# Container.DataItemIndex + 1 %>' runat="server" Hight="30px" />
                        </ItemTemplate>
                        <ItemStyle Width="40px" HorizontalAlign="Center" BorderStyle="None" />
                    </asp:TemplateField>
                    <asp:BoundField DataField="EmpPID" HeaderText="PID">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EmpNo" HeaderText="工號">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                     <asp:BoundField DataField="EmpName" HeaderText="姓名">
                        <ItemStyle Width="200px" HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>                    
                    <asp:BoundField DataField="YN" HeaderText="繳交狀態">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="FileCount" HeaderText="缺少數量">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>                    
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtn_fileEdit" ImageUrl="~/img/images_edit.png" runat="server"
                                Width="30" Height="30" OnClick="imgBtn_fileEdit_Click" />
                        </ItemTemplate>
                        <ItemStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                    </asp:TemplateField>

                    <%--<asp:BoundField DataField="Notice" HeaderText="通知單">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="LeaveProve" HeaderText="離職證明">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="JobProve" HeaderText="工作證明">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Hospital" HeaderText="體檢表">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="perProve" HeaderText="身分證影本">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="EduProve" HeaderText="學歷證明">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Pic" HeaderText="照片">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="Passbook" HeaderText="存摺">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:BoundField DataField="seal" HeaderText="印章">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" />
                    </asp:BoundField>
                    <asp:BoundField DataField="account" HeaderText="資產">
                        <ItemStyle HorizontalAlign="Center" BorderStyle="None" BackColor="PaleGoldenrod" />
                    </asp:BoundField>
                    <asp:TemplateField HeaderText="">
                        <ItemTemplate>
                            <asp:ImageButton ID="imgBtn_fileEdit" ImageUrl="~/img/images_edit.png" runat="server"
                                Width="30" Height="30" OnClick="imgBtn_fileEdit_Click"/>
                        </ItemTemplate>
                        <ItemStyle Width="30px" HorizontalAlign="Center" BorderStyle="None" />
                    </asp:TemplateField>--%>
                </Columns>
                <AlternatingRowStyle BackColor="PaleGoldenrod" Height="35" />
                <HeaderStyle BackColor="YellowGreen" BorderStyle="None" Height="35" />
                <PagerStyle CssClass="gridview" />
                <FooterStyle BorderStyle="None" BackColor="White" BorderColor="White" />
                <RowStyle Height="35" />
                <SelectedRowStyle BackColor="LightCyan" forecolor="DarkBlue" font-bold="true" />
            </asp:GridView>

            </ContentTemplate>
        </asp:UpdatePanel>



        <asp:Button ID="btn_def" runat="server" Text="" CssClass="Georgia18px" OnClick="btn_def_Click" />
    </div>
</asp:Content>
