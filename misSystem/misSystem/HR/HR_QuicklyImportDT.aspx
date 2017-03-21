<%@ Page Title="Quicky Input Leave" Language="C#" MasterPageFile="~/HR/mas_hr.master" AutoEventWireup="true" CodeBehind="HR_QuicklyImportDT.aspx.cs" Inherits="misSystem.HR.HR_QuicklyImportDT" %>

<%@ Register Src="~/UserControl/uc_all_employee.ascx" TagPrefix="uc1" TagName="uc_all_employee" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="contentForHR" runat="server">
    <div style="position:absolute;left:50%;margin-left:-300px;margin-top:-100px;">
        <div style="color:red;font-size:50px;font-family:'Microsoft JhengHei UI'">快速輸入--------</div>
        <div class="Georgia18px">
        <br />
        <br />
        <%--工號:--%>
         <uc1:uc_all_employee runat="server" ID="uc_all_employee" />
        <%--<asp:TextBox ID="tb_workerNum" runat="server" CssClass="Georgia18px"></asp:TextBox>--%><br />
        <br />

        起始日期：
            <asp:DropDownList ID="drop_year" runat="server" CssClass="Georgia18px" AutoPostBack="true" >

            </asp:DropDownList>

            <asp:TextBox ID="tb_AMonth" runat="server" CssClass="Georgia18px" Text="" Width="20px" OnTextChanged="tb_AMonth_TextChanged" AutoPostBack="true"></asp:TextBox>月
            <asp:TextBox ID="tb_ADay" runat="server" Width="30px"  CssClass="Georgia18px" OnTextChanged="tb_ADay_TextChanged" AutoPostBack="true"></asp:TextBox>日
        &nbsp;&nbsp;&nbsp;&nbsp;
        中止日期：<asp:TextBox ID="tb_BMonth" runat="server" CssClass="Georgia18px" Text="" Width="20px"></asp:TextBox>月
            <asp:TextBox ID="tb_BDay" runat="server" Width="30px"  CssClass="Georgia18px"></asp:TextBox>日
            <br />
            <br />


            開始請假時間：
            
            <asp:TextBox ID="tb_startTime" runat="server" CssClass="Georgia18px" Width="100px"></asp:TextBox> (格式:xx:xx)(24進制)
<%--            <asp:DropDownList ID="drop_time" runat="server" CssClass="Georgia18px">
                <asp:ListItem Value="0"> 08:30:00</asp:ListItem>
                <asp:ListItem Value="1"> 13:00:00</asp:ListItem>
            </asp:DropDownList>--%>
        <br />
        <br />
        結束時間:<asp:TextBox ID="tb_endTime" runat="server" CssClass="Georgia18px" Width="100px"></asp:TextBox>(格式:xx:xx)(24進制)
            <%--<asp:Label ID="lbl_BTIME" runat="server" Text=" 17:30:00"></asp:Label>--%>
        <br />
            <br />

            請假假別:<asp:DropDownList ID="drop_leaveReason" runat="server" DataValueField="ID" DataTextField="Descript" CssClass="Georgia18px"></asp:DropDownList>
            <br />
            <br />        
            請假原因：<asp:TextBox ID="tb_reason" runat="server" CssClass="Georgia18px" ></asp:TextBox>
            <br />        
            <br />        


        </div>
        <asp:Button ID="btn_submit" runat="server" Text="Submit" OnClick="btn_submit_Click" CssClass="W100H28-georgia18px"/>
    </div>
</asp:Content>
