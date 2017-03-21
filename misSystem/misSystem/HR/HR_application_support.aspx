<%@ Page Title="" Language="C#" MasterPageFile="~/site.Master" AutoEventWireup="true" CodeBehind="HR_application_support.aspx.cs" Inherits="misSystem.HR.HR_application_support" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <title>Support application</title>
    <script src="myJs/jquery-2.1.4.min.js"></script>
    <script src="myJs/jquery-ui.js"></script>
    <link href="myCss/jquery-ui.css" rel="stylesheet"/>
    <script src="myJs/jquery.timepicker.js"></script>
    <link href="myCss/jquery.timepicker.css" rel="stylesheet" />    
    <link href="myCss/HR_css.css" rel="stylesheet"/>
    <script src="myJs/HR_js.js"></script>
<style>
    #sup_tb {
        vertical-align: central;
        border-collapse:collapse;
    }
    #sup_tb td {
        width: 50%;
        margin: 3px;
        border: 1pt solid #808080;
    }
</style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:Panel ID="out_panel" CssClass="out_panel" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>

        <asp:Panel ID="form_panel" runat="server">

        <h3>人力支援申請單</h3>
        申請人: <asp:Label ID="name_lab" runat="server" Text="---" CssClass="label-1"></asp:Label>
        <asp:Label ID="workNum_lab" runat="server" Text="---" CssClass="label-1" Visible="False"></asp:Label><br />
        部門: <asp:Label ID="dep_lab" runat="server" Text="---" CssClass="label-1"></asp:Label><br />        
        <asp:Label ID="now_dep_lab" runat="server" Text="---" CssClass="label-1" Visible="false"></asp:Label>
        支援人員(至多5位): 
        <table id="sup_tb">
            <tr>
                <td>
                    <asp:DropDownList ID="dep_dl" runat="server" Font-Size="14pt" AutoPostBack="True" OnSelectedIndexChanged="dep_dl_SelectedIndexChanged"></asp:DropDownList><br />
                    <asp:DropDownList ID="employee_dl" runat="server" Font-Size="14pt" AutoPostBack="True" OnSelectedIndexChanged="employee_dl_SelectedIndexChanged"></asp:DropDownList><br />
                    <asp:Button ID="add_btn" runat="server" Text="加入" OnClick="add_btn_Click" />
                    <asp:Button ID="reset_btn" runat="server" Text="重新選擇" style="margin-left:30px;" OnClick="reset_btn_Click" />
                </td>
                <td style="text-align:center;">
                    <asp:ListBox ID="sup_list" runat="server" style="overflow:hidden !important; font-size:16pt; width:100%;" Rows="3"></asp:ListBox>
                </td>
            </tr>
        </table>
        <p style="padding:0; margin:0;"><asp:Label ID="sup_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        期間: <asp:TextBox ID="start_date_text" runat="server" CssClass="datepicker" TextMode="DateTime" AutoPostBack="True" OnTextChanged="start_date_text_TextChanged"></asp:TextBox>
        <asp:TextBox ID="start_time_text" runat="server" CssClass="timepicker" AutoPostBack="True" OnTextChanged="start_date_text_TextChanged"></asp:TextBox> To 
        <asp:TextBox ID="end_date_text" runat="server" CssClass="datepicker" AutoPostBack="True" OnTextChanged="start_date_text_TextChanged"></asp:TextBox>
        <asp:TextBox ID="end_time_text" runat="server" CssClass="timepicker" AutoPostBack="True" OnTextChanged="start_date_text_TextChanged"></asp:TextBox>
        <asp:Label ID="hours_lab" Text="" runat="server" CssClass="label-2"></asp:Label><br />
        <p style="padding:0; margin:0;"><asp:Label ID="datetime_lab" Text="" runat="server" CssClass="label-error" Visible="false"></asp:Label></p>
        申請原因: <asp:DropDownList ID="reason_dl" runat="server" Font-Size="14pt"></asp:DropDownList><br />
            
        </asp:Panel>

        <asp:Button ID="send_btn" runat="server" Text="Send" Font-Size="16pt" style="margin-left:10em" OnClick="send_btn_Click" />
        <asp:Button ID="pass_btn" runat="server" Text="Pass" Font-Size="16pt" style="margin-left:10em" OnClick="pass_btn_Click" Visible="False" />
        <asp:Button ID="fail_btn" runat="server" Text="Fail" Font-Size="16pt" style="margin-left:1em" OnClick="fail_btn_Click" Visible="False" />
        <asp:Button ID="edit_btn" runat="server" Text="Edit" Font-Size="16pt" style="margin-left:10em" OnClick="edit_btn_Click" Visible="False" />
        <asp:Button ID="delete_btn" runat="server" Text="Delete" Font-Size="16pt" style="margin-left:1em" OnClick="delete_btn_Click" Visible="False" />
             
            </ContentTemplate>
        </asp:UpdatePanel>
    </asp:Panel>   
    <script>
        $(document).ready(function () {
            $('.datepicker').datepicker("option", "minDate", -5);
            $('.datepicker').datepicker("option", "maxDate", +20);

            //配合update panel不會重新執行ready事件
            //為update panel註冊事件，在update完成後執行initi_up();
            var prm = Sys.WebForms.PageRequestManager.getInstance();
            prm.add_pageLoaded(initi_up);
            function initi_up() {
                date_initi();
                $('.datepicker').datepicker("option", "minDate", -5);
                $('.datepicker').datepicker("option", "maxDate", +20);
            }
        });
    </script>
</asp:Content>
